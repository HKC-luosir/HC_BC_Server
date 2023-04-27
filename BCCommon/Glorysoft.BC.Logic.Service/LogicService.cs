using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Logic.Contract;

using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;

using System.Threading;
using Glorysoft.BC.EQP.Contract;
using System.Collections.ObjectModel;
using Glorysoft.Auto.Contract.PLC;
using Glorysoft.BC.Entity.RVEntity;
using log4net;
using Glorysoft.BC.RV.Common;
using System.Collections.Concurrent;
using Glorysoft.BC.EIP;
using GlorySoft.BC.WebSocket;
using Glorysoft.BC.Entity.WebSocketEntity;
using System.Web.Script.Serialization;
using System.Windows;

namespace Glorysoft.BC.Logic.Service
{
    public class LogicService : AbstractEventHandler, ILogicService
    {
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        protected static readonly ITibcoRVService rvCmd = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected ILog BCLog = LogHelper.BCLog;

        #region Yuan
        public void MaterialValidationRequest(Unit oEQP, string MaterialStatus, string MaterialID, string MaterialType, string UnitNumber, string SlotNumber, string MaterialCount, string UnloadingCode, int requestNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, MaterialStatus, MaterialID, MaterialType, UnitNumber, SlotNumber, MaterialCount, UnloadingCode, requestNo, transactionID }));
                #endregion

                if (IsInLineMode(oEQP.EQPID))
                {
                    eqpService.SendMaterialValidationRequestReply(oEQP.UnitName, requestNo, "1");
                    return;
                }

                var subUnit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumber));
                RVMaterialValidation materialValidation = new RVMaterialValidation();
                materialValidation.EQUIPMENTID = oEQP.EQPID;
                materialValidation.OPERATOR = "";
                materialValidation.UNITLIST = new List<RVUNIT>();
                RVUNIT unit = new RVUNIT();
                unit.UNITID = oEQP.UnitID;
                unit.PORTID = subUnit != null ? subUnit.SUnitID : "";
                unit.MLOTLIST = new List<RVMaterialList>();
                RVMaterialList materialList = new RVMaterialList();
                materialList.MLOTID = MaterialID;
                materialList.MATERIALNAME = MaterialID.Length >= 14 ? MaterialID.Substring(0, 14) : "";//MaterialID 前14位
                materialList.POSITION = SlotNumber;
                materialList.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, MaterialType.ToString());
                materialList.MAINQTY = MaterialCount;
                unit.MLOTLIST.Add(materialList);
                materialValidation.UNITLIST.Add(unit);
                var replyHeader = mesService.SendToMESMaterialValidation(oEQP.EQPID, materialValidation, transactionID);
                eqpService.SendMaterialValidationRequestReply(oEQP.UnitName, requestNo, (replyHeader != null && replyHeader.RESULT == MESResult.SUCCESS.ToString()) ? "1" : "2");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MaterialValidationRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PalletInfoRequest(Unit oEQP, string palletID, string palletStatus, string boxQTY, string palletType, int portNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, palletID, palletStatus, boxQTY, palletType, transactionID }));
                #endregion

                //回复设备Reply
                eqpService.SendPalletInfoRequestReply(oEQP.UnitName, palletID, "0", "0", transactionID);
                //var portInfo = HostInfo.PortList.FirstOrDefault(o => o.UnitID == oEQP.UnitID);
                //if (portInfo != null)
                //{
                if (palletStatus == "2")//Unload Request
                {
                    var portInfo = HostInfo.PortList.FirstOrDefault(o => o.UnitID == oEQP.UnitID && o.PortNo == portNumber);
                    #region 需求6 6.空栈板请求退料后先触发LoadComplete yindeyu 20221019
                    if (!IsInLineMode(oEQP.EQPID))
                    {
                        RVLoadComplete loadComplete = new RVLoadComplete();
                        loadComplete.EQUIPMENTID = oEQP.EQPID;
                        loadComplete.PORTID = portInfo != null ? portInfo.PortID : "";
                        loadComplete.PORTTYPE = portInfo != null ? portInfo.PortType : "";
                        loadComplete.PORTNUM = portNumber.ToString();
                        loadComplete.DURABLEID = palletID;
                        mesService.SendToMESLoadComplete(oEQP.EQPID, loadComplete, transactionID);

                        Thread.Sleep(500);
                    }
                    #endregion
                    RVEmptyPalletReturnRequest emptyPalletReturnRequest = new RVEmptyPalletReturnRequest();
                    emptyPalletReturnRequest.EQUIPMENTID = oEQP.EQPID;
                    emptyPalletReturnRequest.PORTID = portInfo != null ? portInfo.PortID : "";
                    emptyPalletReturnRequest.PORTTYPE = portInfo != null ? portInfo.PortType : "";
                    emptyPalletReturnRequest.PALLETID = palletID;
                    emptyPalletReturnRequest.PARTNAME = "";
                    emptyPalletReturnRequest.QTY = boxQTY;
                    mesService.SendToMESEmptyPalletReturnRequest(oEQP.EQPID, emptyPalletReturnRequest, transactionID);

                    //更新DB
                    his_pallet hisdata = new his_pallet()
                    {
                        eqpid = oEQP.EQPID,
                        unitid = oEQP.UnitID,
                        palletid = palletID,
                        palletstatus = HostInfo.GetEQToBCValue(PLCEventItem.PalletStatus, palletStatus),
                        pallettype = HostInfo.GetEQToBCValue(PLCEventItem.PalletType, palletType),
                        boxqty = boxQTY
                    };
                    dbService.Inserthis_pallet(hisdata);
                }
                //}
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ PalletInfoRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void LableInfoRequestReport(Unit oEQP, string jobID, string boxID, string labelType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, boxID, labelType, transactionID }));
                #endregion

                RVLabelInfoRequest labelInfoRequest = new RVLabelInfoRequest();
                labelInfoRequest.EQUIPMENTID = oEQP.EQPID;
                labelInfoRequest.UNITID = oEQP.UnitID;
                labelInfoRequest.LOTID = jobID == "" ? boxID : jobID;
                labelInfoRequest.LABELTYPE = labelType;
                mesService.SendToMESLabelInfoRequest(oEQP.EQPID, labelInfoRequest, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ LableInfoRequestReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void DefectCodeReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string defectCodeName, string jobJudgeCode, string jobGradeCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, lotSequenceNumber, slotSequenceNumber, defectCodeName, jobJudgeCode, jobGradeCode, transactionID }));
                #endregion

                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "DefectCodeReport", jobID, lotSequenceNumber, slotSequenceNumber);
                    if (glass != null)
                    {
                        glass.FunctionName = "DefectCodeReport";
                        glass.GlassJudgeCode = jobJudgeCode;
                        glass.GlassGradeCode = jobGradeCode;
                        glass.DefectCodes = defectCodeName;
                        //TBD DefectCode 保存数据库
                        dbService.UpdateGlassInfo(glass);
                    }

                    RVPanelJudgeReport panelJudgeReport = new RVPanelJudgeReport();
                    panelJudgeReport.EQUIPMENTID = oEQP.EQPID;
                    panelJudgeReport.UNITID = oEQP.UnitID;
                    panelJudgeReport.LOTID = jobID;
                    panelJudgeReport.GRADE = jobGradeCode;
                    panelJudgeReport.ABNORMALCODE = "";
                    panelJudgeReport.ISOUTSOURCING = "";//TBD BL是否外购{ Y | N} 只有BL外购填Y
                    panelJudgeReport.DEFECTLIST = new List<RVDEFECTCODE>();//
                    RVDEFECTCODE defectCode = new RVDEFECTCODE();
                    defectCode.DEFECTCODE = defectCodeName;
                    defectCode.DEFECTMAIN = "Y";
                    panelJudgeReport.DEFECTLIST.Add(defectCode);
                    mesService.SendToMESPanelJudgeReport(oEQP.EQPID, panelJudgeReport, transactionID);
                //}
                //else
                //    BCLog.Debug($"DefectCodeReport [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ DefectCodeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void BoxWeightCheckRequest(Unit oEQP, string cassetteIDOrBoxID, string boxWeight, string unitNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, cassetteIDOrBoxID, boxWeight, unitNumber, transactionID }));
                #endregion

                eqpService.SendBoxWeightCheckRequestReply(oEQP.UnitName, "1", transactionID);

                RVWeightReport weightReport = new RVWeightReport();
                weightReport.EQUIPMENTID = oEQP.EQPID;
                weightReport.UNITID = oEQP.UnitID;
                weightReport.BOXID = cassetteIDOrBoxID;
                weightReport.WEIGHT = boxWeight;
                mesService.SendToMESWeightReport(oEQP.EQPID, weightReport);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ BoxWeightCheckRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void JobAssemblyReport(Unit oEQP, string bluID, string bluIDLotSequenceNumber, string bluIDSlotSequenceNumber, string jobID, string jobIDLotSequenceNumber, string jobIDSlotSequenceNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, bluID, bluIDLotSequenceNumber, bluIDSlotSequenceNumber, jobID, jobIDLotSequenceNumber, jobIDSlotSequenceNumber, transactionID }));
                #endregion
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ JobAssemblyReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void SamplingFlagReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, lotSequenceNumber, slotSequenceNumber, transactionID }));
                #endregion

                //机种和工位 取值
                string partname = "";
                string stepname = "";
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "SamplingFlagReport", jobID, lotSequenceNumber, slotSequenceNumber);
                    if (glass != null)
                    {
                        partname = glass.ProductID;
                        stepname = glass.OperationID;
                        jobID = glass.GlassID;
                    }
                //}
                //else
                //    BCLog.Debug($"SamplingFlagReport [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");

                RVSamplingFlagReport samplingFlagReport = new RVSamplingFlagReport();
                samplingFlagReport.EQUIPMENTID = oEQP.EQPID;
                samplingFlagReport.UNITID = oEQP.UnitID;
                samplingFlagReport.PARTNAME = partname;
                samplingFlagReport.PANELID = jobID;
                samplingFlagReport.STEPNAME = stepname;
                mesService.SendToMESSamplingFlagReport(oEQP.EQPID, samplingFlagReport, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ SamplingFlagReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void SamplingRequest(Unit oEQP, string samplingCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, samplingCode, transactionID }));
                #endregion

                if (IsInLineMode(oEQP.EQPID))
                {
                    eqpService.SendSamplingRequestReply(oEQP.UnitName, "", transactionID);
                    return;
                }

                //机种 取值
                string partname = "";
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CurrentUnit == oEQP.UnitID)))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CurrentUnit == oEQP.UnitID));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CurrentUnit == oEQP.UnitID);
                //    partname = glass.ProductID;
                //}

                RVSamplingRequest samplingRequest = new RVSamplingRequest();
                samplingRequest.EQUIPMENTID = oEQP.EQPID;
                samplingRequest.UNITID = oEQP.UnitID;
                samplingRequest.PARTNAME = partname;
                var samplingRequestReply = mesService.SendToMESSamplingRequest(oEQP.EQPID, samplingRequest, transactionID);
                var batchQTY = "";
                if (samplingRequestReply != null)
                    batchQTY = samplingRequestReply.BATCHQTY;
                eqpService.SendSamplingRequestReply(oEQP.UnitName, batchQTY, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ SamplingRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void MachineModeChangeReport(Unit oEQP, string machineMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, machineMode, transactionID }));
                #endregion

                //1:Normal
                //2:packing
                //3:Unpacking
                RVChangeProcessMode changeProcessMode = new RVChangeProcessMode();
                changeProcessMode.EQUIPMENTID = oEQP.EQPID;
                changeProcessMode.PROCESSMODE = HostInfo.Current.GetBCToMESValue("MachineMode", machineMode);
                mesService.SendToMESChangeProcessMode(oEQP.EQPID, changeProcessMode, transactionID);
                oEQP.UnitMode = Convert.ToInt32(machineMode);
                dbService.UpdateUnitInfo(oEQP);

                if (oEQP.UnitID.Contains("-DOM"))
                {
                    //切整线mode
                    var EQPInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                    EQPInfo.LineMode = (LineMode)Enum.ToObject(typeof(LineMode), oEQP.UnitMode);
                    EQPInfo.FunctionName = this.GetType().Name;
                    dbService.UpdateEQPInfo(EQPInfo);
                    BCLog.Debug($"DOM MachineMode Changed {machineMode}");
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MachineModeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        #region MES Command
        public void MESSamplingDownload(RVSamplingDownload samplingDownload, object requestMessage, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { samplingDownload, "", transactionID }));
                #endregion

                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == samplingDownload.EQUIPMENTID).Units.FirstOrDefault(d => d.UnitID == samplingDownload.UNITID);
                if (oEQP != null)
                {
                    eqpService.SendSamplingDownloadCommand(oEQP.UnitName, samplingDownload.BATCHQTY, transactionID);
                    CancellationObject cancellationObject = new CancellationObject();
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    cancellationObject.CancellationTokenSource = cancellationTokenSource;
                    cancellationObject.MessageData = requestMessage;
                    HostInfo.CancellationObjectDic.TryAdd(transactionID, cancellationObject);
                    bool issuccess = true;
                    if (cancellationTokenSource.Token.WaitHandle.WaitOne(5000))
                    {
                        //设备正常回复,无需处理
                    }
                    else
                    {
                        LogHelper.EIPLog.ErrorFormat("+++ MESSamplingDownload:{0} EQP Reply Timeout +++", samplingDownload.UNITID);
                        issuccess = false;
                    }
                    RVSamplingDownloadReply samplingDownloadReply = new RVSamplingDownloadReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = samplingDownloadReply.MessageName;
                    replyHeader.TRANSACTIONID = transactionID;
                    replyHeader.RESULT = issuccess ? MESResult.SUCCESS.ToString() : MESResult.FAIL.ToString();
                    if (!issuccess)
                        replyHeader.RESULTMESSAGE = "equipmentID:" + samplingDownload.EQUIPMENTID + " Reply Timeout";
                    mesService.SendToMESSamplingDownloadReply(oEQP.EQPID, samplingDownloadReply, replyHeader, requestMessage);
                    HostInfo.CancellationObjectDic.TryRemove(transactionID, out var val);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MESSamplingDownload:{0} ,Error:{1} +++", samplingDownload.EQUIPMENTID, ex.ToString());
            }
        }
        public void MESSPCRateDownload(RVSPCRateDownload sPCRateDownload, object requestMessage, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("SPC=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { sPCRateDownload, "", transactionID }));
                #endregion

                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == sPCRateDownload.EQUIPMENTID).Units.FirstOrDefault(d => d.UnitID == sPCRateDownload.UNITID);
                if (oEQP != null)
                {
                    eqpService.SendDVSamplingFlagCommand(oEQP.UnitName, sPCRateDownload.RATE, transactionID);
                    CancellationObject cancellationObject = new CancellationObject();
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    cancellationObject.CancellationTokenSource = cancellationTokenSource;
                    cancellationObject.MessageData = requestMessage;
                    HostInfo.CancellationObjectDic.TryAdd(transactionID, cancellationObject);
                    bool issuccess = true;
                    if (cancellationTokenSource.Token.WaitHandle.WaitOne(5000))
                    {
                        //设备正常回复,无需处理
                        return;
                    }
                    else
                    {
                        LogHelper.EIPLog.ErrorFormat("+++ MESSPCRateDownload:{0} EQP Reply Timeout +++", sPCRateDownload.UNITID);
                        issuccess = false;
                    }
                    RVSPCRateDownloadReply sPCRateDownloadReply = new RVSPCRateDownloadReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = sPCRateDownloadReply.MessageName;
                    replyHeader.TRANSACTIONID = transactionID;
                    replyHeader.RESULT = issuccess ? MESResult.SUCCESS.ToString() : MESResult.FAIL.ToString();
                    if (!issuccess)
                        replyHeader.RESULTMESSAGE = "UnitID:" + sPCRateDownload.UNITID + " Reply Timeout";
                    mesService.SendToMESSPCRateDownloadReply(oEQP.EQPID, sPCRateDownloadReply, replyHeader, requestMessage);
                    HostInfo.CancellationObjectDic.TryRemove(transactionID, out var val);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MESSPCRateDownload:{0} ,Error:{1} +++", sPCRateDownload.UNITID, ex.ToString());
            }
        }
        public void MESRecipeParamRequest(RVRecipeParameterRequest recipeParameterRequest, object requestMessage, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { recipeParameterRequest, "", transactionID }));
                #endregion

                var resultEQPDic = new Dictionary<string, Unit>();
                var unitList = recipeParameterRequest.UNITLIST;
                foreach (var unit in unitList)
                {
                    var unitID = unit.UNITID;
                    var recipeName = unit.RECIPENAME;
                    var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == recipeParameterRequest.EQUIPMENTID).Units.FirstOrDefault(d => d.UnitID == unitID);
                    if (oEQP != null)
                    {
                        oEQP.RecipeParamCheckOK = false;
                        oEQP.Recipe = new Recipe();
                        oEQP.Recipe.RecipeNo = recipeName;
                        oEQP.Recipe.RecipeVersion = unit.VERSION;
                        resultEQPDic.Add(unitID, oEQP);
                        //Send EQP Recipe Para Check
                        BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:{0} EQP wait Reply:{1} Error +++", oEQP.UnitName, oEQP.Recipe.RecipeNo);
                        eqpService.SendRecipeParameterRequestCommand(oEQP.UnitName, recipeName, transactionID);
                    }
                }
                Task.Factory.StartNew(() =>
                {
                    int retryCount = 0;
                    while (true)
                    {
                        retryCount++;
                        Thread.Sleep(1000);
                        var falseEQP = resultEQPDic.Values.FirstOrDefault(o => o.RecipeParamCheckOK == false);
                        if (falseEQP != null)
                        {
                            BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:{0} EQP Not Reply:{1} Retry Count:{2} +++", falseEQP.UnitName, falseEQP.Recipe.RecipeNo, retryCount);
                            if (retryCount > 10)
                            {
                                BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:Wait TimeOut at last one EQP not Reply:{0} {1} Retry End +++", falseEQP.Recipe.RecipeNo, falseEQP.RecipeParamCheckOK);
                                break;
                            }
                            continue;
                        }
                        else
                        {
                            BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:ALL EQP already Reply +++");
                            break;
                        }
                    }
                    var falseEQPList = resultEQPDic.Values.Where(o => o.RecipeParamCheckOK == false);
                    if (falseEQPList != null)
                    {
                        foreach (var eqp in falseEQPList)
                        {
                            LogHelper.EIPLog.InfoFormat("+++ SendRecipeParameterRequestCommand:{0} EQP Still Not Reply:{1} Error +++", eqp.UnitName, eqp.Recipe.RecipeNo);
                        }
                    }
                    //获取eqp缓存中的paraList，回复给MES
                    RVRecipeParameterRequestReply recipeParameterRequestReply = new RVRecipeParameterRequestReply();
                    recipeParameterRequestReply.EQUIPMENTID = recipeParameterRequest.EQUIPMENTID;
                    recipeParameterRequestReply.UNITLIST = new List<UNITRECIPE>();
                    foreach (var unit in resultEQPDic.Values)
                    {
                        UNITRECIPE unitRecipe = new UNITRECIPE();
                        unitRecipe.UNITID = unit.UnitID;
                        unitRecipe.RECIPENAME = unit.Recipe.RecipeNo;
                        unitRecipe.VERSION = unit.Recipe.RecipeVersion;
                        unitRecipe.PARAMALIST = new List<PARAM>();
                        unit.ParameterList.ForEach(o =>
                        {
                            unitRecipe.PARAMALIST.Add(new PARAM() { NAME = o.ParameterName, VALUE = o.ParameterValue });
                        });
                        recipeParameterRequestReply.UNITLIST.Add(unitRecipe);
                    }
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = recipeParameterRequestReply.MessageName;
                    replyHeader.TRANSACTIONID = transactionID;
                    replyHeader.RESULT = MESResult.SUCCESS.ToString();
                    mesService.SendToMESRecipeParameterRequestReply(recipeParameterRequest.EQUIPMENTID, recipeParameterRequestReply, replyHeader, requestMessage);
                });
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ RecipeChangeReport:{0} ,Error:{1} +++", recipeParameterRequest.EQUIPMENTID, ex.ToString());
            }
        }
        #endregion
        public void RecipeChangeReport(Unit oEQP, int recipeNumber, string changeType, string recipeVersion, string unitNumber, string operatorID, List<Parameter> parameterList, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, recipeNumber, changeType, recipeVersion, unitNumber, operatorID, parameterList, transactionID }));
                #endregion

                //RVRecipeChangeReport recipeChangeReport = new RVRecipeChangeReport();
                //recipeChangeReport.EQUIPMENTID = oEQP.EQPID;
                //recipeChangeReport.UNITID = oEQP.UnitID;
                //recipeChangeReport.RECIPENAME = recipeNumber.ToString();
                //foreach (var parameter in parameterList)
                //{
                //    PARAM mesPara = new PARAM();
                //    mesPara.NAME = parameter.ParameterName;
                //    mesPara.VALUE = parameter.ParameterValue;
                //    recipeChangeReport.PARAMLIST.Add(mesPara);
                //}
                //var replyHeader = mesService.SendToMESRecipeChangeReport(recipeChangeReport, transactionID);

                //不用给MES发了 默认回设备OK
                //CuttingRequestReply
                eqpService.SendRecipeChangeReportReply(oEQP.UnitName, "1", transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ RecipeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void JobJudgeResultReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string unitNumber, string slotNumber, string jobJudgeCode, string jobGradeCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, lotSequenceNumber, slotSequenceNumber, unitNumber, slotNumber, jobJudgeCode, jobGradeCode, transactionID }));
                #endregion
                var glass = logicService.GetGlassInfoByCode(oEQP, "JobJudgeResultReport", jobID, lotSequenceNumber, slotSequenceNumber);
                if (glass != null)
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                {
                    //var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                    //var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    //var glassmap = new Hashtable();
                    //glassmap.Add("GlassID", jobID);
                    ////glassmap.Add("CassetteSequenceNo", lotSequenceNumber);
                    ////glassmap.Add("SlotSequenceNo", slotSequenceNumber);
                    //var glass = dbService.GetGlassInfoList(glassmap).FirstOrDefault();
                    //if (glass != null)
                    //{
                    //    glass.FunctionName = "JobJudgeResultReport";
                    //    glass.GlassJudgeCode = jobJudgeCode;
                    //    glass.GlassGradeCode = jobGradeCode;
                    //    //TBD DefectCode 保存数据库
                    //    dbService.UpdateGlassInfo(glass);
                    //}
                    lock (glass)
                    {
                        glass.FunctionName = "JobJudgeResultReport";
                        glass.GlassJudgeCode = jobJudgeCode;
                        glass.GlassGradeCode = jobGradeCode;
                        //TBD DefectCode 保存数据库
                        dbService.UpdateGlassInfo(glass);
                    }

                    RVPanelJudgeReport panelJudgeReport = new RVPanelJudgeReport();
                    panelJudgeReport.EQUIPMENTID = oEQP.EQPID;
                    panelJudgeReport.UNITID = oEQP.UnitID;
                    panelJudgeReport.LOTID = jobID;
                    panelJudgeReport.GRADE = jobGradeCode;
                    panelJudgeReport.ABNORMALCODE = "";
                    panelJudgeReport.ISOUTSOURCING = "";//TBD BL是否外购{ Y | N} 只有BL外购填Y
                    panelJudgeReport.DEFECTLIST = new List<RVDEFECTCODE>();//
                    RVDEFECTCODE defectCode = new RVDEFECTCODE();
                    defectCode.DEFECTCODE = glass.DefectCodes;
                    defectCode.DEFECTMAIN = "Y";
                    panelJudgeReport.DEFECTLIST.Add(defectCode);
                    LogHelper.BCLog.Info("Before SendToMESPanelJudgeReport:" + DateTime.Now.ToString());
                    mesService.SendToMESPanelJudgeReport(oEQP.EQPID, panelJudgeReport, transactionID);
                }
                //else
                // BCLog.Debug($"JobJudgeResultReport [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ JobJudgeResultReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PanelDataUpdateReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string judgeArray, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, lotSequenceNumber, slotSequenceNumber, judgeArray, transactionID }));
                #endregion

                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "PanelDataUpdateReport", jobID, lotSequenceNumber, slotSequenceNumber);
                    if (glass != null)
                    {
                        glass.PanelJudge = judgeArray;
                        dbService.UpdateGlassInfo(glass);

                        //是否上报MES PanelJudgeData，如何报
                    }
                //}
                //else
                //    BCLog.Debug($"PanelDataUpdateReport [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ PanelDataUpdateReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PanelJudgeDataDownloadRequest(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobID, lotSequenceNumber, slotSequenceNumber, transactionID }));
                #endregion

                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "PanelJudgeDataDownloadRequest", jobID, lotSequenceNumber, slotSequenceNumber);
                if (glass != null)
                {
                    eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 1, glass, transactionID);
                }
                else
                {
                    eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 2, new GlassInfo(), transactionID);
                }
                //}
                //else
                //{
                //    BCLog.Debug($"PanelJudgeDataDownloadRequest [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");
                //    eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 2, new GlassInfo(), transactionID);
                //    //if (IsInLineMode(oEQP.EQPID))
                //    //{
                //    //    eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 1, new GlassInfo(), transactionID);
                //    //    return;
                //    //}
                //    //RVPanelInfoDownload panelInfoDownload = new RVPanelInfoDownload();
                //    //panelInfoDownload.EQUIPMENTID = oEQP.EQPID;
                //    //panelInfoDownload.UNITID = oEQP.UnitID;
                //    //panelInfoDownload.PANELID = jobID;
                //    //var panelInfoDownloadReply = mesService.SendToMESPanelInfoDownload(oEQP.EQPID, panelInfoDownload, transactionID);
                //    //if (panelInfoDownloadReply != null)
                //    //{
                //    //    if (panelInfoDownloadReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                //    //    {
                //    //        //创建缓存
                //    //        GlassInfo pi = new GlassInfo();
                //    //        pi.ProductID = panelInfoDownloadReply.PARTNAME;
                //    //        pi.OperationID = panelInfoDownloadReply.STEPNAME;
                //    //        pi.GlassID = panelInfoDownloadReply.PANELID;
                //    //        pi.GlassGradeCode = panelInfoDownloadReply.GRADE;
                //    //        pi.SlotSatus = EnumGlassSlotStatus.Processing;
                //    //        pi.PanelGrade = panelInfoDownloadReply.SUBGRADE;
                //    //        pi.WorkOrder = panelInfoDownloadReply.WOID;
                //    //        pi.CassetteSequenceNo = int.Parse(lotSequenceNumber);
                //    //        pi.SlotSequenceNo = int.Parse(slotSequenceNumber);
                //    //        var portlist = HostInfo.Current.PortList.FirstOrDefault();
                //    //        portlist.GlassInfos.Add(pi);
                //    //        //更新数据库
                //    //        var ret = dbService.InsertGlassInfo(pi);
                //    //        eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 1, pi, transactionID);
                //    //    }
                //    //    else//没有Jobdata数据
                //    //    {
                //    //        var glassInfo = new GlassInfo();
                //    //        glassInfo.GlassID = jobID;
                //    //        glassInfo.CassetteSequenceNo = int.Parse(lotSequenceNumber);
                //    //        glassInfo.SlotSequenceNo = int.Parse(slotSequenceNumber);
                //    //        eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 2, glassInfo, transactionID);
                //    //    }
                //    //}
                //    //else
                //    //{
                //    //    var glassInfo = new GlassInfo();
                //    //    glassInfo.GlassID = jobID;
                //    //    glassInfo.CassetteSequenceNo = int.Parse(lotSequenceNumber);
                //    //    glassInfo.SlotSequenceNo = int.Parse(slotSequenceNumber);
                //    //    eqpService.SendPanelJudgeDataDownloadRequestReply(oEQP.UnitName, 2, glassInfo, transactionID);
                //    //}
                //}
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ PanelJudgeDataDownloadRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CurrentRecipeNumberChangeReport(Unit oEQP, int recipeNumber, string recipeVersion, string unitNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, recipeNumber, recipeVersion, unitNumber, transactionID }));
                #endregion

                oEQP.CurrentRecipeID = recipeNumber;
                oEQP.CurrentRecipeVersion = recipeVersion;
                dbService.UpdateUnitInfo(oEQP);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ CurrentRecipeNumberChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void AutoRecipeChangeModeReport(Unit oEQP, string autoRecipeChangeMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, autoRecipeChangeMode, transactionID }));
                #endregion

                oEQP.AutoRecipeChangeMode = HostInfo.GetEQToBCValue(PLCEventItem.AutoRecipeChangeMode, autoRecipeChangeMode);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ AutoRecipeChangeModeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void AlarmReport(Unit oEQP, string alarmID, string alarmState, string alarmUnit, string alarmType, string alarmCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, alarmID, alarmState, alarmUnit, alarmType, alarmCode, transactionID }));
                #endregion

                // read alarm info from db
                Hashtable map = new Hashtable
                {
                    {"EQPID",oEQP.EQPID },
                    {"UNITID",oEQP.UnitID},
                    {"AlarmID",alarmID }
                };
                AlarmInfo alarmInfospec = dbService.FindOneAlarm(map);

                AlarmInfo alarmInfo = new AlarmInfo();
                alarmInfo.EQPID = oEQP.EQPID;
                alarmInfo.UNITID = oEQP.UnitID;
                alarmInfo.AlarmID = alarmID;
                alarmInfo.AlarmCode = alarmCode;
                alarmInfo.AlarmStatus = HostInfo.GetEQToBCValue(MESEventItem.AlarmStatus, alarmState);
                alarmInfo.AlarmText = alarmInfospec != null ? alarmInfospec.AlarmText : "";
                alarmInfo.AlarmType = HostInfo.GetEQToBCValue(MESEventItem.AlarmType, alarmType);
                alarmInfo.AlarmUnitNumber = alarmUnit;

                RVAlarmReport alarmReport = new RVAlarmReport();
                alarmReport.EQUIPMENTID = alarmInfo.EQPID;
                alarmReport.UNITID = alarmInfo.UNITID;
                alarmReport.ALARMID = alarmInfo.AlarmID;
                alarmReport.ALARMCODE = alarmInfo.AlarmCode;
                alarmReport.ALARMTEXT = alarmInfo.AlarmText;
                alarmReport.ALARMLEVEL = alarmInfo.AlarmType;
                alarmReport.ALARMSTATE = HostInfo.GetBCToMESValue(MESEventItem.AlarmStatus, alarmState);
                mesService.SendToMESAlarmReport(oEQP.EQPID, alarmReport, transactionID);

                if (alarmInfo.AlarmStatus.ToUpper().Equals("SET"))
                {
                    //set消息 写入wip表
                    dbService.InsertWipAlarmInfo(alarmInfo);
                }
                else
                {
                    //clear消息 删除wip表
                    Hashtable delHT = new Hashtable();
                    delHT.Add("EQPID", oEQP.EQPID);
                    delHT.Add("UNITID", oEQP.UnitID);
                    delHT.Add("AlarmID", alarmInfo.AlarmID);
                    dbService.DeleteWipAlarmInfo(delHT);
                }

                dbService.InsertAlarmHistory(alarmInfo);

                #region AlarmReport OPI推送
                //推送OPI,Error Alarm;alarmtype=1
                Hashtable hashtable = new Hashtable() {
                            {"eqpid",oEQP.EQPID }
                        };
                webSocketService.SendToWebSocketAlarmReport(hashtable);
                #endregion
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ AlarmReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void MaterialCountChangeReport(Unit oEQP, int requestNo, string materialID, string unitNumber, string slotNumber, string materialType, string materialCount, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, requestNo, materialID, unitNumber, slotNumber, materialType, materialCount, transactionID }));
                #endregion

                if (String.IsNullOrEmpty(materialID))
                {
                    return;
                }

                //更新数据库
                MaterialInfo mtl = new MaterialInfo();
                mtl.EQPID = oEQP.EQPID;
                mtl.UnitID = oEQP.UnitID;
                mtl.MaterialID = materialID;
                var opr = HostInfo.GetEQToBCValue(PLCEventItem.MaterialTypeOperationProportion, materialType, true);
                if (string.IsNullOrEmpty(opr))
                {
                    opr = "1";
                }
                mtl.MaterialUseCount = (Convert.ToInt32(materialCount) * Convert.ToDecimal(opr)).ToString();
                dbService.UpdateMaterialInfo(mtl);

                RVMaterialCountReport materialCountReport = new RVMaterialCountReport();
                materialCountReport.EQUIPMENTID = oEQP.EQPID;
                materialCountReport.UNITID = oEQP.UnitID;
                materialCountReport.ACTIONTYPE = "Daily";
                materialCountReport.MATERIALNAME = materialID.Length >= 14 ? materialID.Substring(0, 14) : "";//MaterialID 前14位;
                materialCountReport.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType);
                materialCountReport.MLOTID = materialID;
                materialCountReport.USEQTY = materialCount;
                materialCountReport.MATERIALSN = "";
                mesService.SendToMESMaterialCountReport(oEQP.EQPID, materialCountReport, transactionID);

                RVEQPMaterialQTY EQPMaterialQTYReport = new RVEQPMaterialQTY();
                EQPMaterialQTYReport.EQUIPMENTID = oEQP.EQPID;
                EQPMaterialQTYReport.UNITID = oEQP.UnitID;
                var sunit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(unitNumber));
                EQPMaterialQTYReport.PORTID = sunit != null ? sunit.SUnitID : "";
                EQPMaterialQTYReport.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType);
                EQPMaterialQTYReport.MATERIALQTY = materialCount;
                mesService.SendToMESEQPMaterialQTYReport(oEQP.EQPID, EQPMaterialQTYReport, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MaterialCountChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CellMaterialAssemblyReport(Unit oEQP, string lotSequenceNumber, string slotSequenceNumber, List<MaterialInfo> materialList, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, lotSequenceNumber, slotSequenceNumber, materialList, transactionID }));
                #endregion

                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(lotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(slotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "CellMaterialAssemblyReport", "", lotSequenceNumber, slotSequenceNumber);
                    if (glass != null)
                    {
                        RVMaterialUseReport materialUseReport = new RVMaterialUseReport();
                        materialUseReport.EQUIPMENTID = oEQP.EQPID;
                        materialUseReport.UNITID = oEQP.UnitID;
                        materialUseReport.PANELID = glass.GlassID;
                        materialUseReport.MLOTLIST = new List<RVMaterialList>();
                    foreach (var material in materialList)
                    {
                        RVMaterialList ml = new RVMaterialList();
                        var ACTIONTYPE = HostInfo.GetBCToMESValue(PLCEventItem.MaterialType, material.MaterialType);
                        ml.MLOTID = material.MaterialType == "1" ? "" : material.MaterialID;
                        ml.SUBUNITID = "";
                        ml.MATERIALNAME = material.MaterialID.Length >= 14 ? material.MaterialID.Substring(0, 14) : "";//MaterialID 前14位
                        ml.POSITION = material.MaterialPosition;
                        ml.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, material.MaterialType);
                        #region 需求7 3.判定物料若无SNID则不上报MES yindeyu 20221102
                        ml.MATERIALSN = (ACTIONTYPE == "PCB" || ACTIONTYPE == "BL") ? material.MaterialID : "";
                        #endregion
                        var opr = HostInfo.GetEQToBCValue(PLCEventItem.MaterialTypeOperationProportion, material.MaterialType, true);
                        if (string.IsNullOrEmpty(opr))
                        {
                            opr = "1";
                        }
                        ml.USEQTY = (Convert.ToInt32(material.MaterialUseCount) * Convert.ToDecimal(opr)).ToString();
                        materialUseReport.MLOTLIST.Add(ml);

                        if (ACTIONTYPE == "PCB" || ACTIONTYPE == "BLU")
                        {
                            RVLotBindingAlias mesbind = new RVLotBindingAlias();
                            mesbind.EQUIPMENTID = oEQP.EQPID;
                            mesbind.UNITID = oEQP.UnitID;
                            mesbind.ACTIONTYPE = ACTIONTYPE == "BLU" ? "BINDING" : "BINDINGPCB";
                            mesbind.MATERIALSN = material.MaterialID;
                            mesbind.LOTID = glass.GlassID;
                            var resMes = mesService.SendToMESLotBindingAlias(oEQP.EQPID, mesbind, transactionID);
                            if (resMes != null)
                            {
                                if (resMes.RESULT == MESResult.SUCCESS.ToString())
                                {
                                    //将物料ID绑定到Glass
                                    glass.BLID = material.MaterialID;
                                    dbService.UpdateGlassInfo(glass);
                                    BCLog.Debug($"CellMaterialAssemblyReport update glassid:{glass.GlassID} MaterialType:{ACTIONTYPE} MaterialID:{material.MaterialID}");

                                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "999", "0", "MES LOTBINDING OK", transactionID);
                                }
                                else//失败
                                {
                                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "1", "999", "0", "MES LOTBINDING NG", transactionID);
                                }
                            }
                        }
                    }
                        mesService.SendToMESMaterialUseReport(oEQP.EQPID, materialUseReport, transactionID);
                    }
                    else
                    {
                        BCLog.Info("CellMaterialAssemblyReport:panel not exist:lotSequenceNumber:" + lotSequenceNumber + ";slotSequenceNumber:" + slotSequenceNumber);
                    }
                //}
                //else
                //    BCLog.Debug($"CellMaterialAssemblyReport [{oEQP.UnitName}] Not found Glass [{lotSequenceNumber}][{slotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ CellMaterialAssemblyReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void MaterialLotInfoRequest(Unit oEQP, int requestNo, string materialID, string unitNumber, string slotNumber, string materialType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, requestNo, materialID, unitNumber, slotNumber, materialType, transactionID }));
                #endregion

                var subUnit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(unitNumber));

                if (IsInLineMode(oEQP.EQPID))
                {
                    // TBD MES回复内容需确认
                    #region 需求6.1 回复Offline设备请求物料信息 yindeyu 20221012
                    var MaterialList = new List<MaterialInfo>();
                    if (!String.IsNullOrEmpty(materialID))
                    {
                        var materiallist = dbService.ViewMaterialInfo(new MaterialInfo() { MaterialLotID = materialID });
                        if (materiallist != null && materiallist.Count > 0)
                        {
                            MaterialList = materiallist.ToList();
                            foreach (var mat in MaterialList)
                            {
                                mat.MaterialQty = Convert.ToInt32(mat.MaterialUseCount);
                            }
                        }
                    }
                    eqpService.SendMaterialLotInfoRequestReply(oEQP.UnitName, requestNo, (materialType == "1" ? materialID : ""), MaterialList, 1, transactionID);

                    ////插入数据库
                    //MaterialInfo mtl = new MaterialInfo();
                    //mtl.EQPID = oEQP.EQPID;
                    //mtl.UnitID = oEQP.UnitID;
                    //mtl.MaterialID = materialID;
                    //mtl.MaterialLotID = materialID;
                    //mtl.MaterialType = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType);
                    //mtl.MaterialState = "";
                    //mtl.MaterialUseCount = "";
                    //mtl.MaterialPosition = "";
                    //dbService.InsertMaterialInfo(mtl);
                    return;
                    #endregion
                }

                RVMaterialLotInfoRequest materialLotInfoRequest = new RVMaterialLotInfoRequest();
                materialLotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                materialLotInfoRequest.UNITID = oEQP.UnitID;
                materialLotInfoRequest.PORTID = subUnit != null ? subUnit.SUnitID : "";
                materialLotInfoRequest.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType);
                materialLotInfoRequest.DURABLEID = materialID;
                materialLotInfoRequest.MLOTID = "";
                materialLotInfoRequest.OPERATOR = "";
                var materialLotInfoRequestReply = mesService.SendToMESMaterialLotInfoRequest(oEQP.EQPID, materialLotInfoRequest, transactionID);
                if (materialLotInfoRequestReply != null)
                {
                    //清残帐
                    MaterialInfo mtl = new MaterialInfo();
                    mtl.EQPID = oEQP.EQPID;
                    mtl.UnitID = oEQP.UnitID;
                    mtl.MaterialLotID = materialID;
                    dbService.DeleteMaterialInfo(mtl);

                    var materialList = new List<MaterialInfo>();
                    foreach (var mlot in materialLotInfoRequestReply.MLOTLIST)
                    {
                        MaterialInfo materialInfo = new MaterialInfo();
                        materialInfo.EQPID = oEQP.EQPID;
                        materialInfo.UnitID = oEQP.UnitID;
                        materialInfo.MaterialID = mlot.MLOTID;
                        materialInfo.MaterialLotID = materialID;
                        materialInfo.MaterialName = mlot.MATERIALNAME;
                        materialInfo.MaterialType = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType);
                        double count = 0;
                        double.TryParse(mlot.MAINQTY, out count);
                        count = Math.Round(count, 0);
                        materialInfo.MaterialQty = Convert.ToInt32(count);
                        int Qtime = 0;
                        int.TryParse(mlot.MAXQTIME, out Qtime);
                        materialInfo.MaterialQTime = Qtime;
                        materialInfo.MaterialUseCount = Convert.ToInt32(count).ToString();
                        materialInfo.MaterialPosition = mlot.POSITION;
                        materialInfo.MaterialState = "";
                        materialList.Add(materialInfo);

                        //插入数据库
                        dbService.InsertMaterialInfo(materialInfo);
                    }
                    eqpService.SendMaterialLotInfoRequestReply(oEQP.UnitName, requestNo, (materialType == "1" ? materialID : ""), materialList, materialLotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString() ? 1 : 2, transactionID);


                }
                else
                {
                    eqpService.SendMaterialLotInfoRequestReply(oEQP.UnitName, requestNo, (materialType == "1" ? materialID : ""), new List<MaterialInfo>(), 2, transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MaterialLotInfoRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void TransferBoxReport(Unit oEQP, int materialType, int materialQty, int materialCurrentQty, int reportOption, int portNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, materialType, materialQty, materialCurrentQty, reportOption, portNumber, transactionID }));
                #endregion

                var subUnit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == portNumber);
                if (reportOption == 1)//LoadRequest
                {
                    RVMaterialLoadRequest materialLoadRequest = new RVMaterialLoadRequest();
                    materialLoadRequest.EQUIPMENTID = oEQP.EQPID;
                    materialLoadRequest.UNITID = oEQP.UnitID;
                    materialLoadRequest.PORTID = subUnit != null ? subUnit.SUnitID : "";
                    materialLoadRequest.MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, materialType.ToString());
                    materialLoadRequest.MATERIALQTY = materialQty.ToString();
                    materialLoadRequest.OPERATOR = "";
                    mesService.SendToMESMaterialLoadRequest(oEQP.EQPID, materialLoadRequest, transactionID);
                }
                else if (reportOption == 2)//UnloadRequest
                {
                    RVMaterialUnloadRequest materialUnloadRequest = new RVMaterialUnloadRequest();
                    materialUnloadRequest.EQUIPMENTID = oEQP.EQPID;
                    materialUnloadRequest.UNITID = oEQP.UnitID;
                    materialUnloadRequest.PORTID = subUnit != null ? subUnit.SUnitID : "";
                    materialUnloadRequest.DURABLEID = "";//TBD
                    mesService.SendToMESMaterialUnloadRequest(oEQP.EQPID, materialUnloadRequest, transactionID);
                }
                else
                {
                    //Error
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ TransferBoxReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        #endregion


        #region matti
        public void SendOutJobEventReport(Unit oEQP, int i, List<JobDataInfo> jobdatas, string transactionID = "")
        {
            try
            {
                #region 需求20 1.优化log日志量 liuyusen 20230327
                var logjob = "";
                foreach (var jobdata in jobdatas)
                {
                    logjob += $"[jobid:{jobdata.JobID} cstseqno:{jobdata.LotSequenceNumber} slotseqno:{jobdata.SlotSequenceNumber}]";
                }
                #endregion
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, i, logjob, transactionID }));
                #endregion

                foreach (var jobdata in jobdatas)
                {
                    //job缓存数据
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber)));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber));
                        var glass = logicService.GetGlassInfoByCode(oEQP, "SendOutJob", jobdata.JobID, jobdata.LotSequenceNumber, jobdata.SlotSequenceNumber);
                        if (glass != null)
                        {
                            glass.FunctionName = "SendOutJob";
                            UpdateJobData(ref glass, jobdata);
                            var ret = dbService.UpdateGlassInfo(glass);

                            //MES
                            RVUnitOut unitout = new RVUnitOut();
                            unitout.ACTIONTYPE = "UNITOUT";
                            unitout.EQUIPMENTID = oEQP.EQPID;
                            unitout.UNITID = oEQP.UnitID;
                            unitout.SUBUNITID = "";
                            unitout.PANELID = glass.GlassID;
                            unitout.PARTNAME = glass.ProductID;
                            unitout.STEPNAME = glass.OperationID;
                            unitout.RECIPENAME = glass.ProductRecipe;
                            unitout.INTIME = (glass.CurrentUnit == oEQP.UnitID && glass.RecvJobTime != null) ? glass.RecvJobTime.Value.ToString("yyyyMMddHHmmss") : null;
                            unitout.OUTTIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                            mesService.SendToMESUnitOutReport(oEQP.EQPID, unitout, transactionID);

                            //多线体 第一个线体最后一个设备需要上报MES PanelTrackInOutReport
                            //多线体 如果上一个线体的最后一个设备没有标记，则下一个线体第一个设备需要上报MES PanelTrackInOutReport
                            var mesEQPID = "";
                            var nextEQPID = "";
                            bool sendMes = false;
                            if (oEQP.IsEqpEnd)
                            {
                                //需要取上一条线体的最后一个设备
                                var preLineeqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.IsEqpStart == true)).Units.FirstOrDefault(d => d.IsEqpStart == true);
                                mesEQPID = oEQP.EQPID;
                                nextEQPID = preLineeqp.EQPID;
                                sendMes = true;
                            }
                            if (String.IsNullOrEmpty(glass.RGlassID))//大板不报trackinout
                                sendMes = false;
                            if (sendMes)
                            {
                                RVPanelTrackInOut paneltrackinout = new RVPanelTrackInOut();
                                paneltrackinout.EQUIPMENTID = mesEQPID;
                                paneltrackinout.NEXTEQUIPMENTID = nextEQPID;
                                paneltrackinout.PANELID = glass.GlassID;
                                paneltrackinout.LOTTYPE = "";
                                paneltrackinout.BONDINGID = "";
                                paneltrackinout.GRADE = glass.GlassGradeCode;
                                paneltrackinout.POSITION = glass.SlotSequenceNo.ToString();
                                paneltrackinout.ABNORMALCODE = glass.AbnormalCodes;
                                if (!string.IsNullOrEmpty(glass.DefectCodes))
                                {
                                    if (!String.IsNullOrEmpty(glass.DefectCodes.TrimEnd(';')))
                                        paneltrackinout.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = glass.DefectCodes.TrimEnd(';'), DEFECTMAIN = "Y" });
                                }
                                var resMes = mesService.SendToMESPanelTrackInOutReport(oEQP.EQPID, paneltrackinout, transactionID);

                                if (resMes != null)
                                {
                                    if (resMes.RESULT == MESResult.SUCCESS.ToString())
                                    {
                                        //打上已发送MES的标记
                                        glass.IsMesTrackIn = true;
                                        ret = dbService.UpdateGlassInfo(glass);
                                    }
                                    else//失败则记录到数据库中
                                    {
                                        wip_processend_glass wipglass = new wip_processend_glass();
                                        wipglass.equipmentid = paneltrackinout.EQUIPMENTID;
                                        wipglass.panelid = paneltrackinout.PANELID;
                                        wipglass.lottype = paneltrackinout.LOTTYPE;
                                        wipglass.blid = paneltrackinout.BONDINGID;
                                        wipglass.grade = paneltrackinout.GRADE;
                                        wipglass.position = paneltrackinout.POSITION;
                                        wipglass.actioncomment = "";
                                        wipglass.abnormalcode = paneltrackinout.ABNORMALCODE;
                                        wipglass.defectcode = glass.DefectCodes;
                                        wipglass.parentid = 0;
                                        wipglass.returncode = resMes.RESULT;
                                        wipglass.returnmsg = resMes.RESULTMESSAGE;
                                        dbService.Insertwip_processend_glass(wipglass);
                                    }
                                }
                            }
                        }
                    //}
                    //else
                    //    BCLog.Debug($"SendOutJobEventReport [{oEQP.UnitName}] Not found Glass [{jobdata.LotSequenceNumber}][{jobdata.SlotSequenceNumber}]");
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ SendOutJobEventReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void ReceiveJobEventReport(Unit oEQP, int i, List<JobDataInfo> jobdatas, string transactionID = "")
        {
            try
            {
                #region 需求20 1.优化log日志量 liuyusen 20230327
                var logjob = "";
                foreach (var jobdata in jobdatas)
                {
                    logjob += $"[jobid:{jobdata.JobID} cstseqno:{jobdata.LotSequenceNumber} slotseqno:{jobdata.SlotSequenceNumber}]";
                }
                #endregion
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, i, logjob, transactionID }));
                #endregion

                ////根据unitid获取modelposition  robot put时用于寻找target position
                //var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault();

                foreach (var jobdata in jobdatas)
                {
                    ////job缓存数据
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber)));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber));
                        var glass = logicService.GetGlassInfoByCode(oEQP, "ReceiveJob", jobdata.JobID, jobdata.LotSequenceNumber, jobdata.SlotSequenceNumber);
                        if (glass != null)
                        {
                            //记录时间 在sendout时需要报给MES
                            glass.FunctionName = "ReceiveJob";
                            glass.CurrentUnit = oEQP.UnitID;
                            glass.CurrentSUnit = oEQP.UnitID;
                            glass.RecvJobTime = DateTime.Now;
                            if (oEQP.IsProcessEnd)
                                glass.ProcessingCount += oEQP.LocalNo.ToString() + ";";
                            //if (robotmodel != null)
                            //    glass.ModelPosition = robotmodel.ModelPosition;

                            UpdateJobData(ref glass, jobdata);
                            var ret = dbService.UpdateGlassInfo(glass);

                            //ReSendTrackInOut(oEQP, glass, transactionID);
                        }
                    //}
                    //else
                    //    BCLog.Debug($"ReceiveJobEventReport [{oEQP.UnitName}] Not found Glass [{jobdata.LotSequenceNumber}][{jobdata.SlotSequenceNumber}]");
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ ReceiveJobEventReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void StoreInJobEventReport(Unit oEQP, string panelID, string carrierSeq, string slotID, string unitOrPort, string unitID, string portID, string slotNum, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, panelID, carrierSeq, slotID, unitOrPort, unitID, portID, slotNum, transactionID }));
                #endregion

                //找到Job信息，更新CarrierID和PortID

                //job缓存数据
                //var glassmap = new Hashtable();
                //glassmap.Add("GlassID", panelID);
                //glassmap.Add("CassetteSequenceNo", jobdata.LotSequenceNumber);
                //glassmap.Add("SlotSequenceNo", jobdata.SlotSequenceNumber);
                //var glass = dbService.GetGlassInfoList(glassmap).FirstOrDefault();
                //GlassInfo glass = null;
                //PortInfo Inportinfo = null;
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotID))))
                //{
                //    Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotID)));
                //    glass = Inportinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotID));
                //}
                //else
                //    BCLog.Debug($"StoreInJobEventReport [{oEQP.UnitName}] Not found Glass [{carrierSeq}][{slotID}]");

                GlassInfo glass = logicService.GetGlassInfoByCode(oEQP, "StoreInJob", panelID, carrierSeq, slotID);

                if (unitOrPort == "2")//1-Unit 2-Port
                {
                    PortInfo Inportinfo = null;
                    if (glass != null)
                    {
                        Inportinfo = logicService.GetPortInfoByCode(oEQP, panelID, carrierSeq, slotID, 1);
                        //Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotID)));
                    }
                    //Hashtable csths = new Hashtable();
                    //csths.Add("CassetteSequenceNo", carrierSeq);
                    //var TCarrierInfo = dbService.GetCassetteList(csths).FirstOrDefault();

                    var Outportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == Convert.ToInt32(portID));
                    //更新panel信息
                    if (glass != null)
                    {
                        if (Outportinfo != null)
                        {
                            if (Outportinfo.CassetteSequenceNo == 0)
                            {
                                BCLog.InfoFormat("+++ StoreInJobEventReport: Cannot Find Target CarrierInfo {0} +++", Outportinfo.CassetteSequenceNo);
                                return;
                            }
                            //更新wip
                            glass.SlotSatus = EnumGlassSlotStatus.ProcessEnd;
                            dbService.UpdateGlassInfo(glass);
                            if (!glass.IsStoreIn)
                            {
                                glass.FunctionName = "StoreInJobPort";
                                glass.CurrentUnit = oEQP.UnitID;
                                glass.CurrentSUnit = oEQP.UnitID + "-" + Outportinfo.PortID;
                                glass.CassetteID = Outportinfo.CassetteID;
                                glass.CassetteSequenceNo = Convert.ToInt32(carrierSeq);
                                glass.SlotSequenceNo = Convert.ToInt32(slotID);
                                int islotNum = Convert.ToInt32(slotNum);
                                glass.SlotPosition = islotNum / 1000;
                                glass.Position = islotNum % 1000;
                                glass.PortID = Outportinfo.PortID;
                                glass.OutCSTID = glass.CassetteID;
                                glass.OutPortID = Outportinfo.PortID;
                                glass.IsStoreIn = true;
                                dbService.UpdateGlassInfo(glass);

                                //缓存数据移动到对应的Port
                                Outportinfo.GlassInfos.Add(glass);
                                Inportinfo.GlassInfos.Remove(glass);
                            }
                            RVPanelInByIndexReport data = new RVPanelInByIndexReport();
                            data.EQUIPMENTID = oEQP.EQPID;
                            data.PANELID = glass.GlassID;
                            data.LOTTYPE = "";
                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")) || (oEQP.UnitID.Contains("M2CUT") && oEQP.UnitID.Contains("TST1")))
                                data.POSITION = glass.Position.ToString();
                            else
                                data.POSITION = ((glass.SlotPosition * 1000) + glass.Position).ToString();
                            data.DURABLEID = glass.CassetteID;
                            data.BONDINGID = "";
                            data.GRADE = glass.GlassGradeCode;
                            data.ABNORMALCODE = glass.AbnormalCodes;
                            if (!string.IsNullOrEmpty(glass.DefectCodes))
                            {
                                if (!String.IsNullOrEmpty(glass.DefectCodes.TrimEnd(';')))
                                    data.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = glass.DefectCodes.TrimEnd(';'), MAINFLAG = "Y" });
                            }
                            mesService.SendToMESPanelInByIndexReport(oEQP.EQPID, data, transactionID);
                            //MES
                            RVUnitOut unitout = new RVUnitOut();
                            unitout.ACTIONTYPE = "UNITOUT";
                            unitout.EQUIPMENTID = oEQP.EQPID;
                            unitout.UNITID = oEQP.UnitID;
                            unitout.SUBUNITID = "";
                            unitout.PANELID = glass.GlassID;
                            unitout.PARTNAME = glass.ProductID;
                            unitout.STEPNAME = glass.OperationID;
                            unitout.RECIPENAME = glass.ProductRecipe;
                            unitout.INTIME = (glass.CurrentUnit == oEQP.UnitID && glass.RecvJobTime != null) ? glass.RecvJobTime.Value.ToString("yyyyMMddHHmmss") : null;
                            unitout.OUTTIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                            mesService.SendToMESUnitOutReport(oEQP.EQPID, unitout, transactionID);
                        }
                    }
                    //判断Port是否放满
                    if (slotNum.Length == 4)//卡夹按放片顺序判断层数
                    {
                        LogHelper.BCLog.Debug($"StoreIn RobotUnit UnitID:{oEQP.UnitID} GlassID:{glass.GlassID}");
                        var robotmodel = oEQP.RobotModelList.FirstOrDefault(c => c.PortID == Outportinfo.PortID);
                        if (robotmodel != null)
                        {
                            //if (robotmodel.PortGetType == PortGetType.DESC)
                            //{
                            var GlassInfos = Outportinfo.GlassInfos.OrderBy(o => o.Position).ToList();
                            var minglass = GlassInfos.FirstOrDefault();
                            if (minglass.Position == 1)
                            {
                                //eqpService.SendPortControlCommand(oEQP.UnitName, Outportinfo.PortNo, "2", transactionID);
                                Outportinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessEnd;
                                eqpService.SendCassetteControlCommand(oEQP.UnitName, Outportinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessEnd, Outportinfo.CassetteInfo.JobExistenceSlot, Outportinfo.CassetteInfo.JobCount.ToString(), transactionID);
                            }
                            //}
                            //else
                            //{
                            //    var GlassInfos = Outportinfo.GlassInfos.OrderByDescending(o => o.Position).ToList();
                            //    var maxglass = GlassInfos.FirstOrDefault();
                            //    if (maxglass.Position >= Outportinfo.Capacity / 2)
                            //    {
                            //        eqpService.SendPortControlCommand(oEQP.UnitName, Outportinfo.PortNo, "2", transactionID);
                            //        eqpService.SendCassetteControlCommand(oEQP.UnitName, Outportinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessEnd, Outportinfo.CassetteInfo.JobExistenceSlot, Outportinfo.CassetteInfo.JobCount.ToString(), transactionID);
                            //    }
                            //}
                        }
                    }
                    else//BOX按判断glass数量是否满
                    {
                        LogHelper.BCLog.Debug($"StoreIn Unit UnitID:{oEQP.UnitID} GlassID:{glass.GlassID} PortID:{Outportinfo.PortID} GlassCount:{Outportinfo.GlassInfos.Count(c => c.SlotFlag != EnumGlassSlotStatus.Removed)}");
                        if (Outportinfo.GlassInfos.Count(c => c.SlotFlag != EnumGlassSlotStatus.Removed) >= Outportinfo.Capacity)
                        {
                            //eqpService.SendPortControlCommand(oEQP.UnitName, Outportinfo.PortNo, "2", transactionID);
                            Outportinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessEnd;
                            eqpService.SendCassetteControlCommand(oEQP.UnitName, Outportinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessEnd, Outportinfo.CassetteInfo.JobExistenceSlot, Outportinfo.CassetteInfo.JobCount.ToString(), transactionID);
                        }
                    }
                }
                else
                {
                    //更新panel信息
                    if (glass != null)
                    {
                        var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                        if (eqpinfo != null)
                        {
                            var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitID == oEQP.UnitID);
                            if (unitinfo != null)
                            {
                                var sunitinfo = unitinfo.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(unitID));
                                if (sunitinfo != null)
                                {
                                    //更新wip
                                    glass.FunctionName = "StoreInJobUnit";
                                    glass.CurrentUnit = oEQP.UnitID;
                                    glass.CurrentSUnit = sunitinfo.SUnitID;
                                    //glass.CassetteID = "";
                                    //glass.CassetteSequenceNo = 0;
                                    glass.RecvUnitJobTime = DateTime.Now;
                                    dbService.UpdateGlassInfo(glass);

                                    ReSendTrackInOut(oEQP, glass, transactionID, unitID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ StoreInJobEventReport:EQPName:{0},Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        public void FetchOutJobEventReport(Unit oEQP, string panelID, string carrierSeq, string slotSeq, string unitOrPort, string unitID, string portID, string slotNum, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, panelID, carrierSeq, slotSeq, unitOrPort, unitID, portID, slotNum, transactionID }));
                #endregion

                GlassInfo glass = logicService.GetGlassInfoByCode(oEQP, "FetchOutJob", panelID, carrierSeq, slotSeq);

                //GlassInfo glass = null;
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotSeq))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotSeq)));
                //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(carrierSeq) && d.SlotSequenceNo == Convert.ToInt32(slotSeq));
                //}
                //else
                //    BCLog.Debug($"FetchOutJobEventReport [{oEQP.UnitName}] Not found Glass [{carrierSeq}][{slotSeq}]");
                if (unitOrPort == "2")
                {
                    var vport = HostInfo.Current.PortList.FirstOrDefault(c => c.PortID == "VP");
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == int.Parse(portID));
                    if (portinfo != null)
                    {
                        if (glass != null)
                        {
                            //更新DB 
                            glass.FunctionName = "FetchOutJobPort";
                            glass.SlotSatus = EnumGlassSlotStatus.Processing;
                            glass.CassetteID = "";
                            glass.PortID = "";
                            //glass.CurrentUnit = oEQP.UnitID;
                            //glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                            glass.RecvJobTime = DateTime.Now;
                            if (oEQP.IsProcessEnd)
                                glass.ProcessingCount += oEQP.LocalNo.ToString() + ";";
                            glass.IsFetchOutPort = true;
                            var ret = dbService.UpdateGlassInfo(glass);

                            //缓存数据移动到对应的Port
                            vport.GlassInfos.Add(glass);
                            portinfo.GlassInfos.Remove(glass);

                            //MES
                            RVPanelOutByIndexReport data = new RVPanelOutByIndexReport();
                            data.EQUIPMENTID = oEQP.EQPID;
                            data.LOTID = glass.LotID;
                            data.DURABLEID = glass.CassetteID;
                            data.GLASSID = glass.GlassID;
                            data.PANELID = glass.GlassID;
                            mesService.SendToMESPanelOutByIndexReport(oEQP.EQPID, data, transactionID);
                        }

                        if (portinfo.PortType == "PL")
                        {
                            //判断是否第一片和最后一片
                            lock (portinfo.GlassInfos)
                            {
                                var waitCount = portinfo.GlassInfos.Where(o => o.CassetteSequenceNo == glass.CassetteSequenceNo && o.IsFetchOutPort == false).Count();
                                var allCount = portinfo.GlassInfos.Count;
                                //if (waitCount == allCount - 1)//第一片
                                //{
                                //    portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.InProcessing;
                                //}
                                if (waitCount == 0)//最后一片
                                {
                                    BCLog.Debug($"Unit:{oEQP.UnitID} Port:{portinfo.PortID} SlotSeqNo:{slotSeq} RemainGlassCount{waitCount} Auto CassetteProcessEnd");
                                    //process End Command
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessEnd;
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessEnd, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                }
                                else
                                {
                                    BCLog.Debug($"Unit:{oEQP.UnitID} Port:{portinfo.PortID} SlotSeqNo:{slotSeq} RemainGlassCount{waitCount}");
                                }
                            }
                        }
                    }
                }
                else//Unit
                {
                    //更新panel信息
                    if (glass != null)
                    {
                        var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                        if (eqpinfo != null)
                        {
                            var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitID == oEQP.UnitID);
                            if (unitinfo != null)
                            {
                                var sunitinfo = unitinfo.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(unitID));
                                if (sunitinfo != null)
                                {
                                    //更新wip
                                    glass.FunctionName = "FetchOutJobUnit";
                                    //glass.CurrentUnit = oEQP.UnitID;
                                    //glass.CurrentSUnit = sunitinfo.SUnitID;
                                    var ret = dbService.UpdateGlassInfo(glass);

                                    //MES
                                    RVUnitOut unitout = new RVUnitOut();
                                    unitout.ACTIONTYPE = "SUBUNITOUT";
                                    unitout.EQUIPMENTID = oEQP.EQPID;
                                    unitout.UNITID = oEQP.UnitID;
                                    unitout.SUBUNITID = sunitinfo.SUnitID;
                                    unitout.PANELID = glass.GlassID;
                                    unitout.PARTNAME = glass.ProductID;
                                    unitout.STEPNAME = glass.OperationID;
                                    unitout.RECIPENAME = glass.ProductRecipe;
                                    unitout.INTIME = (glass.CurrentUnit == oEQP.UnitID && glass.CurrentSUnit == sunitinfo.SUnitID && glass.RecvUnitJobTime != null) ? glass.RecvUnitJobTime.Value.ToString("yyyyMMddHHmmss") : null;
                                    unitout.OUTTIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    mesService.SendToMESUnitOutReport(oEQP.EQPID, unitout, transactionID);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ FetchOutJobEventReport:EQPName:{0},Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        private void ReSendTrackInOut(Unit oEQP, GlassInfo glass, string transactionID, string subunitno)
        {
            //多线体 第一个线体最后一个设备需要上报MES PanelTrackInOutReport
            //多线体 如果上一个线体的最后一个设备没有标记，则下一个线体第一个设备需要上报MES PanelTrackInOutReport
            var mesEQPID = "";
            var nextEQPID = "";
            bool sendMes = false;
            //BCLog.Debug($"glassid:{glass.GlassID} unit:{oEQP.UnitID} IsEqpStart:{oEQP.IsEqpStart} glassIsMesTrackIn:{glass.IsMesTrackIn}");
            if (oEQP.IsEqpStart && !glass.IsMesTrackIn && subunitno == "4")//CT1的清洗单元
            {
                //需要取上一条线体的最后一个设备
                var lastlineeqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.IsEqpEnd == true)).Units.FirstOrDefault(d => d.IsEqpEnd == true);
                mesEQPID = lastlineeqp.EQPID;
                nextEQPID = oEQP.EQPID;
                sendMes = true;
            }
            if (String.IsNullOrEmpty(glass.RGlassID))//大板不报trackinout
                sendMes = false;
            if (sendMes)
            {
                RVPanelTrackInOut paneltrackinout = new RVPanelTrackInOut();
                paneltrackinout.EQUIPMENTID = mesEQPID;
                paneltrackinout.NEXTEQUIPMENTID = nextEQPID;
                paneltrackinout.PANELID = glass.GlassID;
                paneltrackinout.LOTTYPE = "";
                paneltrackinout.BONDINGID = "";
                paneltrackinout.GRADE = glass.GlassGradeCode;
                paneltrackinout.POSITION = glass.SlotSequenceNo.ToString();
                paneltrackinout.ABNORMALCODE = glass.AbnormalCodes;
                if (!string.IsNullOrEmpty(glass.DefectCodes))
                {
                    if (!String.IsNullOrEmpty(glass.DefectCodes.TrimEnd(';')))
                        paneltrackinout.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = glass.DefectCodes.TrimEnd(';'), DEFECTMAIN = "Y" });
                }
                var resMes = mesService.SendToMESPanelTrackInOutReport(oEQP.EQPID, paneltrackinout, transactionID);

                if (resMes != null)
                {
                    if (resMes.RESULT == MESResult.SUCCESS.ToString())
                    {
                        //打上已发送MES的标记
                        glass.IsMesTrackIn = true;
                        var ret = dbService.UpdateGlassInfo(glass);
                    }
                    else//失败则记录到数据库中
                    {
                        wip_processend_glass wipglass = new wip_processend_glass();
                        wipglass.equipmentid = paneltrackinout.EQUIPMENTID;
                        wipglass.panelid = paneltrackinout.PANELID;
                        wipglass.lottype = paneltrackinout.LOTTYPE;
                        wipglass.blid = paneltrackinout.BONDINGID;
                        wipglass.grade = paneltrackinout.GRADE;
                        wipglass.position = paneltrackinout.POSITION;
                        wipglass.actioncomment = "";
                        wipglass.abnormalcode = paneltrackinout.ABNORMALCODE;
                        wipglass.defectcode = glass.DefectCodes;
                        wipglass.parentid = 0;
                        wipglass.returncode = resMes.RESULT;
                        wipglass.returnmsg = resMes.RESULTMESSAGE;
                        dbService.Insertwip_processend_glass(wipglass);
                    }
                }
            }
        }
        public void JobDataChangeReport(Unit oEQP, JobDataInfo jobdata, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, jobdata, transactionID }));
                #endregion

                //job缓存数据
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(jobdata.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(jobdata.SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "JobDataChange", jobdata.JobID, jobdata.LotSequenceNumber, jobdata.SlotSequenceNumber);
                    if (glass != null)
                    {
                        glass.FunctionName = "JobDataChange";
                        UpdateJobData(ref glass, jobdata);
                        var ret = dbService.UpdateGlassInfo(glass);
                    }
                //}
                //else
                //    BCLog.Debug($"JobDataChangeReport [{oEQP.UnitName}] Not found Glass [{jobdata.LotSequenceNumber}][{jobdata.SlotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ JobDataChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        private void UpdateJobData(ref GlassInfo glass, JobDataInfo job)
        {
            try
            {
                glass.ProductType = job.JobType;
                glass.PropertyCode = job.PropertyCode;
                glass.GlassJudgeCode = job.JobJudgeCode;
                glass.GlassGradeCode = job.JobGradeCode;
                string sProcessingFlag = "";
                for (int i = 0; i < job.ProcessingFlag1.Length; i++)
                {
                    if (job.ProcessingFlag1[i] == '1')
                    {
                        sProcessingFlag += (i + 1).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.ProcessingFlag2.Length; i++)
                {
                    if (job.ProcessingFlag2[i] == '1')
                    {
                        sProcessingFlag += (i + 1 + 16).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.ProcessingFlag3.Length; i++)
                {
                    if (job.ProcessingFlag3[i] == '1')
                    {
                        sProcessingFlag += (i + 1 + 32).ToString() + ";";
                    }
                }
                glass.ProcessingFlag = sProcessingFlag;

                string sSkipFlag = "";
                for (int i = 0; i < job.SkipFlag1.Length; i++)
                {
                    if (job.SkipFlag1[i] == '1')
                    {
                        sSkipFlag += (i + 1).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.SkipFlag2.Length; i++)
                {
                    if (job.SkipFlag2[i] == '1')
                    {
                        sSkipFlag += (i + 1 + 16).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.SkipFlag3.Length; i++)
                {
                    if (job.SkipFlag3[i] == '1')
                    {
                        sSkipFlag += (i + 1 + 32).ToString() + ";";
                    }
                }
                glass.SkipFlag = sSkipFlag;

                string sAbnormalCode = "";
                //for (int i = 0; i < job.AbnormalFlag1.Length; i++)
                //{
                //    if (job.AbnormalFlag1[i] == '1')
                //    {
                //        var adddata = (i + 1).ToString() + ";";
                //        sAbnormalCode += (i + 1).ToString() + ";";
                //    }
                //}
                //for (int i = 0; i < job.AbnormalFlag2.Length; i++)
                //{
                //    if (job.AbnormalFlag2[i] == '1')
                //    {
                //        sAbnormalCode += (i + 1 + 16).ToString() + ";";
                //    }
                //}
                //for (int i = 0; i < job.AbnormalFlag3.Length; i++)
                //{
                //    if (job.AbnormalFlag3[i] == '1')
                //    {
                //        sAbnormalCode += (i + 1 + 32).ToString() + ";";
                //    }
                //}
                //前3个FLAG是MES值，不更新
                if (!String.IsNullOrEmpty(glass.AbnormalCodes))
                {
                    var abnors = glass.AbnormalCodes.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < abnors.Length; i++)
                    {
                        if (abnors[i].Contains("|"))
                        {
                            sAbnormalCode += abnors[i] + ";";
                        }
                        else if (Convert.ToInt32(abnors[i]) <= 48)
                        {
                            sAbnormalCode += abnors[i] + ";";
                        }
                    }
                }
                for (int i = 0; i < job.AbnormalFlag4.Length; i++)
                {
                    if (job.AbnormalFlag4[i] == '1')
                    {
                        sAbnormalCode += (i + 1 + 48).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.AbnormalFlag5.Length; i++)
                {
                    if (job.AbnormalFlag5[i] == '1')
                    {
                        sAbnormalCode += (i + 1 + 64).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.AbnormalFlag6.Length; i++)
                {
                    if (job.AbnormalFlag6[i] == '1')
                    {
                        sAbnormalCode += (i + 1 + 80).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.AbnormalFlag7.Length; i++)
                {
                    if (job.AbnormalFlag7[i] == '1')
                    {
                        sAbnormalCode += (i + 1 + 96).ToString() + ";";
                    }
                }
                for (int i = 0; i < job.AbnormalFlag8.Length; i++)
                {
                    if (job.AbnormalFlag8[i] == '1')
                    {
                        sAbnormalCode += (i + 1 + 112).ToString() + ";";
                    }
                }
                glass.AbnormalCodes = sAbnormalCode;

                glass.GlassThicknessCode = job.GlassThickness;
                glass.JobAngle = job.JobAngle;
                glass.JobFlip = job.JobFlip;
                glass.MMGCode = job.MMGCode;
                glass.PanelInchSizeX = job.PanelInchSizeX;
                glass.PanelInchSizeY = job.PanelInchSizeY;
                glass.WorkOrder = job.WorkOrderID;
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ UpdateJobData,Error:{0} +++", ex.ToString());
            }
        }
        public void JobManualMoveReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string JobPosition, string ReportOption, string OperatorID, string UnitorPort, string UnitNumberorPortNo, string SlotNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, JobPosition, ReportOption, OperatorID, UnitorPort, UnitNumberorPortNo, SlotNumber, transactionID }));
                #endregion

                //获取SubUnit
                string sunitid = "";
                try
                {
                    if (!String.IsNullOrEmpty(UnitNumberorPortNo))
                    {
                        if (UnitorPort == "1")
                        {
                            var sunitinfo = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumberorPortNo));
                            if (sunitinfo != null)
                            {
                                sunitid = sunitinfo.SUnitID;
                            }
                        }
                        else if (UnitorPort == "2")
                        {
                            var unitportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == Convert.ToInt32(UnitNumberorPortNo));
                            if (unitportinfo != null)
                            {
                                sunitid = oEQP.UnitID + "-" + unitportinfo.PortID;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    BCLog.ErrorFormat("+++ JobManualMoveReport:{0} ,Get SubUnit Error:{1} +++", oEQP.UnitID, ex.ToString());
                }

                //var vport = HostInfo.Current.PortList.FirstOrDefault(c => c.PortID == "VP");
                //1: Line In
                //2: Line Out
                //3: Scrap
                //4: Delete(Just Delete Residual Data In Machine)
                if (ReportOption == "1")//Line In
                {
                    ////job缓存数据
                    ////var glassmap = new Hashtable();
                    ////glassmap.Add("GlassID", JobID);
                    ////glassmap.Add("CassetteSequenceNo", LotSequenceNumber);
                    ////glassmap.Add("SlotSequenceNo", SlotSequenceNumber);
                    ////var glass = dbService.GetGlassInfoList(glassmap).FirstOrDefault();
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                    //    if (glass != null)
                    //    {
                    //        //记录拨片标记
                    //        glass.SlotFlag = EnumGlassSlotStatus.Recovery;
                    //        glass.CurrentUnit = oEQP.UnitID;
                    //        glass.CurrentSUnit = oEQP.UnitID;
                    //        string sCurrentSUnit = "";
                    //        if (UnitorPort == "1")//1-Unit 2-Port
                    //        {
                    //            var sunit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumberorPortNo));
                    //            sCurrentSUnit = sunit.SUnitID;
                    //        }
                    //        else
                    //        {
                    //            var port = HostInfo.Current.PortList.FirstOrDefault(c => c.UnitID == oEQP.UnitID && c.PortNo == Convert.ToInt32(UnitNumberorPortNo));
                    //            sCurrentSUnit = oEQP.UnitID + "-" + port.PortID;
                    //        }
                    //        glass.CurrentSUnit = sCurrentSUnit;
                    //        var ret = dbService.UpdateGlassInfo(glass);

                    //        //MES
                    //        RVPanelInOut data = new RVPanelInOut();
                    //        data.EQUIPMENTID = oEQP.EQPID;
                    //        data.UNITID = oEQP.UnitID;
                    //        data.PANELID = glass.GlassID;
                    //        data.PARTNAME = glass.ProductID;
                    //        data.STEPNAME = glass.OperationID;
                    //        data.GRADE = glass.GlassGradeCode;
                    //        data.ACTIONTYPE = "LINEIN";
                    //        mesService.SendToMESPanelInOutLine(data, transactionID);
                    //    }
                    //}
                }
                else if (ReportOption == "2")//Line Out
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "GlassLineOut", JobID, LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        if (glass.SlotSatus == EnumGlassSlotStatus.ProcessEnd)
                        {
                            //只记录History
                            GlassInfo HisInfo = (GlassInfo)glass.Clone();
                            HisInfo.FunctionName = "GlassLineOut(End)";
                            HisInfo.CurrentUnit = oEQP.UnitID;
                            HisInfo.CurrentSUnit = sunitid;
                            dbService.InsertHisGlassInfo(HisInfo);
                        }
                        else
                        {
                            var portinfo = logicService.GetPortInfoByCode(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, 1);
                            //var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                            lock (glass)
                            {
                                //记录拨片标记
                                glass.FunctionName = "GlassLineOut";
                                glass.SlotFlag = EnumGlassSlotStatus.Removed;
                                glass.SlotSatus = EnumGlassSlotStatus.Removed;
                                glass.PortID = "";
                                glass.CassetteID = "";
                                glass.CurrentUnit = oEQP.UnitID;
                                glass.CurrentSUnit = sunitid;
                                var ret = dbService.UpdateGlassInfo(glass);
                            }
                            //缓存数据移动到对应的Port
                            //vport.GlassInfos.Add(glass);
                            portinfo.GlassInfos.Remove(glass);
                            //删除数据库
                            Hashtable glasstb = new Hashtable();
                            glasstb.Add("ID", glass.ID);
                            dbService.DeleteGlassInfoList(glasstb);
                            glass.FunctionName = "DeleteWIP";
                            dbService.InsertHisGlassInfo(glass);
                            BCLog.Debug($"JobManualMoveReport {oEQP.UnitID} GlassID {glass.GlassID} Line Out");

                            //MES
                            RVPanelInOut data = new RVPanelInOut();
                            data.EQUIPMENTID = oEQP.EQPID;
                            data.UNITID = oEQP.UnitID;
                            data.PANELID = glass.GlassID;
                            data.PARTNAME = glass.ProductID;
                            data.STEPNAME = glass.OperationID;
                            data.GRADE = glass.GlassGradeCode;
                            data.ACTIONTYPE = "LINEOUT";
                            mesService.SendToMESPanelInOutLine(oEQP.EQPID, data, transactionID);
                        }
                    }
                    //}
                    //else
                    //    BCLog.Debug($"JobManualMoveReport [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");
                }
                else if (ReportOption == "3")//Scrap
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "GlassScrap", JobID, LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        var portinfo = logicService.GetPortInfoByCode(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, 1);
                        //var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                        lock (glass)
                        {
                            //记录拨片标记
                            glass.FunctionName = "GlassScrap";
                            glass.SlotFlag = EnumGlassSlotStatus.Removed;
                            glass.SlotSatus = EnumGlassSlotStatus.Scrap;
                            glass.PortID = "";
                            glass.CassetteID = "";
                            glass.CurrentUnit = oEQP.UnitID;
                            glass.CurrentSUnit = sunitid;
                            var ret = dbService.UpdateGlassInfo(glass);
                        }
                        //缓存数据移动到对应的Port
                        //vport.GlassInfos.Add(glass);
                        portinfo.GlassInfos.Remove(glass);
                        //删除数据库
                        Hashtable glasstb = new Hashtable();
                        glasstb.Add("ID", glass.ID);
                        dbService.DeleteGlassInfoList(glasstb);
                        glass.FunctionName = "DeleteWIP";
                        dbService.InsertHisGlassInfo(glass);
                        BCLog.Debug($"JobManualMoveReport {oEQP.UnitID} GlassID {glass.GlassID} Scrap");

                        var subUnitID = "";
                        if (UnitorPort == "") //1 - Unit 2 - Port
                        {
                            var subUnit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumberorPortNo));
                            subUnitID = subUnit == null ? "" : subUnit.SUnitID;
                        }
                        else
                        {
                            var sPort = HostInfo.Current.PortList.FirstOrDefault(o => o.UnitID == oEQP.UnitID && o.PortNo == Convert.ToInt32(UnitNumberorPortNo));
                            subUnitID = sPort != null ? sPort.PortID : "";
                        }
                        //MES
                        RVPanelScrap data = new RVPanelScrap();
                        data.EQUIPMENTID = oEQP.EQPID;
                        data.UNITID = oEQP.UnitID;
                        data.SUBUNITID = subUnitID;
                        data.PANELID = glass.GlassID;
                        data.PARTNAME = glass.ProductID;
                        data.STEPNAME = glass.OperationID;
                        data.GRADE = glass.GlassGradeCode;
                        mesService.SendToMESPanelScrap(oEQP.EQPID, data, transactionID);
                    }
                    //}
                    //else
                    //    BCLog.Debug($"JobManualMoveReport [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");
                }
                else if (ReportOption == "4")
                {
                    //记History
                    GlassInfo glass = new GlassInfo();
                    glass.FunctionName = "EQP Delete";
                    glass.GlassID = JobID;
                    glass.CassetteSequenceNo = Convert.ToInt32(LotSequenceNumber);
                    glass.SlotSequenceNo = Convert.ToInt32(SlotSequenceNumber);
                    glass.CurrentUnit = oEQP.UnitID;
                    glass.CurrentSUnit = sunitid;
                    dbService.InsertHisGlassInfo(glass);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ JobManualMoveReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oEQP"></param>
        /// <param name="JobID"></param>
        /// <param name="LotSequenceNumber"></param>
        /// <param name="SlotSequenceNumber"></param>
        /// <param name="RequestOption">1: Job ID 2: Sequence Number 3: Job ID for VCR Miss match 4.BLU ID</param>
        /// <param name="transactionID"></param>
        public void JobDataRequest(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string RequestOption, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, RequestOption, transactionID }));
                #endregion

                //job缓存数据
                GlassInfo glass = null;
                if (RequestOption == "1")//Job ID
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                    //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                    //}
                    //else
                    //    LogHelper.BCLog.Info($"DB not found GlassID {JobID}");
                    glass = logicService.GetGlassInfoByCode(oEQP, "", JobID, "", "", 2, "");
                }
                else if (RequestOption == "2")
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                    //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    //    JobID = glass.GlassID;
                    //}
                    //else
                    //    LogHelper.BCLog.Info($"DB not found GlassCode {LotSequenceNumber} {SlotSequenceNumber}");
                    glass = logicService.GetGlassInfoByCode(oEQP, "", "", LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        JobID = glass.GlassID;
                    }
                }
                else if (RequestOption == "3")//Job ID
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                    //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                    //}
                    //else
                    //    LogHelper.BCLog.Info($"DB not found GlassID {JobID}");
                    glass = logicService.GetGlassInfoByCode(oEQP, "", JobID, "", "", 2, "");
                }
                else if (RequestOption == "4" || RequestOption == "5")//BLU ID PCBID
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.BLID == JobID)))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.BLID == JobID));
                    //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.BLID == JobID);
                    //    //JobID = glass.GlassID;
                    //}
                    //else
                    //    LogHelper.BCLog.Info($"DB not found BLID {JobID}");
                    glass = logicService.GetGlassInfoByCode(oEQP, "", "", "", "", 3, JobID);
                }
                else if (RequestOption == "6")//Job ID
                {
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                    //    glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                    //}
                    //else//直接回设备NG
                    //{
                    //    LogHelper.BCLog.Info($"DB not found GlassID {JobID}");
                    //    //发送CIM MESSAGE
                    //    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "1", "0", "BC not found GlassID", transactionID);
                    //    //JobDataRequestReply
                    //    eqpService.SendJobDataRequestReply(oEQP.UnitName, new JobDataInfo(), "2", transactionID);
                    //}
                    glass = logicService.GetGlassInfoByCode(oEQP, "", JobID, "", "", 2, "");
                }

                RVPanelInfoDownload data = new RVPanelInfoDownload();
                data.EQUIPMENTID = oEQP.EQPID;
                data.UNITID = oEQP.UnitID;
                if (RequestOption == "4")
                {
                    data.BONDINGID = JobID;
                    data.IDTYPE = "BL";
                }
                else if (RequestOption == "5")
                {
                    data.BONDINGID = JobID;
                    data.IDTYPE = "PCB";
                }
                else
                {
                    data.PANELID = JobID;
                    data.IDTYPE = "";
                }


                if (IsInLineMode(oEQP.EQPID) || RequestOption == "3")
                {
                    if (glass != null)
                    {
                        //更新拨片标记
                        glass.FunctionName = "JobDataRequest";
                        glass.SlotFlag = EnumGlassSlotStatus.Recovery;
                        glass.SlotSatus = EnumGlassSlotStatus.Processing;
                        glass.CurrentUnit = oEQP.UnitID;
                        glass.CurrentSUnit = oEQP.UnitID;

                        //根据unitid获取modelposition  robot put时用于寻找target position
                        var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault();
                        if (robotmodel != null)
                            glass.ModelPosition = robotmodel.ModelPosition;

                        if (glass.ID > 0)
                            dbService.UpdateGlassInfo(glass);
                        else
                            dbService.InsertGlassInfo(glass);

                        JobDataInfo jobdata = SetJobDataInfo(glass);
                        //JobDataRequestReply
                        eqpService.SendJobDataRequestReply(oEQP.UnitName, jobdata, "1", transactionID);
                    }
                    else
                    {
                        //发送CIM MESSAGE
                        eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "1", "0", "BC not found GlassID", transactionID);
                        //JobDataRequestReply
                        eqpService.SendJobDataRequestReply(oEQP.UnitName, new JobDataInfo(), "2", transactionID);
                    }
                    return;
                }

                if (RequestOption == "2" && glass == null)//option为2的情况下 如果本地没帐 无需再请mes
                {
                    //发送CIM MESSAGE
                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "1", "0", "BC not found GlassID", transactionID);
                    eqpService.SendJobDataRequestReply(oEQP.UnitName, new JobDataInfo(), "2", transactionID);
                    return;
                }

                RVPanelInfoDownloadResponse mesres = mesService.SendToMESPanelInfoDownload(oEQP.EQPID, data, transactionID);
                if (mesres != null)
                {
                    if (mesres.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                    {
                        if (glass != null)
                        {
                            glass.GlassGradeCode = mesres.GRADE;
                            glass.PanelGrade = mesres.SUBGRADE;
                            glass.OperationID = mesres.STEPNAME;
                        }
                        else
                        {
                            //创建缓存
                            glass = new GlassInfo();
                            glass.SlotFlag = EnumGlassSlotStatus.Processing;
                            glass.GlassID = mesres.PANELID;
                            glass.WorkOrder = mesres.WOID;
                            glass.BLID = mesres.BONDINGID;
                            glass.AbnormalCodes = mesres.ABNORMALCODE;
                            glass.ProductID = mesres.PARTNAME;
                            glass.OperationID = mesres.STEPNAME;
                            glass.GlassGradeCode = mesres.GRADE;
                            glass.PanelGrade = mesres.SUBGRADE;
                            SetJobPPID(ref glass, mesres.UNITRECIPELIST);
                            var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.PortID == "VP");
                            var portcount = (HostInfo.Current.PortList.Count(c => c.EQPID == oEQP.EQPID) + 1) * 1000 + oEQP.LocalNo;
                            var slotcount = 0;
                            //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == portcount)))
                            //{
                            //    var portinfos = HostInfo.Current.PortList.Where(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == portcount));
                            //    foreach (var p in portinfos)
                            //    {
                            //        var maxslotcount = p.GlassInfos.Where(d => d.CassetteSequenceNo == portcount).Max(c => c.SlotSequenceNo);
                            //        if (maxslotcount > slotcount)
                            //        {
                            //            slotcount = maxslotcount;
                            //        }
                            //    }
                            //}
                            slotcount = GetPortMaxSlotCount(portcount);
                            slotcount += 1;
                            glass.CassetteSequenceNo = portcount;
                            glass.SlotSequenceNo = slotcount;
                            portinfo.GlassInfos.Add(glass);
                            BCLog.Debug($"AddPortGlass:{portinfo.EQPID + "_" + portinfo.PortID} GlassID:{glass.GlassID}");
                        }

                        if (mesres.STATE == "WAIT" && RequestOption != "3")
                        {
                            glass.IsMesTrackIn = true;
                            RVPanelTrackIn TrackIndata = new RVPanelTrackIn();
                            TrackIndata.EQUIPMENTID = oEQP.EQPID;
                            TrackIndata.PANELID = glass.GlassID;
                            TrackIndata.LOTTYPE = mesres.LOTTYPE;
                            TrackIndata.POSITION = "";
                            TrackIndata.GRADE = mesres.GRADE;
                            mesService.SendToMESPanelTrackInReport(oEQP.EQPID, TrackIndata, transactionID);
                        }

                        //更新拨片标记
                        if (RequestOption != "3")
                            glass.SlotFlag = EnumGlassSlotStatus.Recovery;

                        glass.FunctionName = "JobDataRequest";
                        glass.CurrentUnit = oEQP.UnitID;
                        glass.CurrentSUnit = oEQP.UnitID;
                        glass.SlotSatus = EnumGlassSlotStatus.Processing;

                        //根据unitid获取modelposition  robot put时用于寻找target position
                        var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault();
                        if (robotmodel != null)
                            glass.ModelPosition = robotmodel.ModelPosition;
                        glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";

                        if (glass.ID > 0)
                            dbService.UpdateGlassInfo(glass);
                        else
                            dbService.InsertGlassInfo(glass);

                        JobDataInfo jobdata = SetJobDataInfo(glass);
                        //JobDataRequestReply
                        eqpService.SendJobDataRequestReply(oEQP.UnitName, jobdata, "1", transactionID);
                    }
                    else
                    {
                        //发送CIM MESSAGE
                        eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "1", "0", "MES Return NG", transactionID);
                        //回复设备NG
                        eqpService.SendJobDataRequestReply(oEQP.UnitName, new JobDataInfo(), "2", transactionID);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ JobDataRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        private int GetPortMaxSlotCount(int portcount)
        {
            bool noerr = true;
            var data = GetPortMaxSlotCountInfo(portcount, out noerr);
            if (!noerr)
            {
                int trycount = 3;
                while (trycount > 0)
                {
                    trycount--;
                    if (!noerr)
                    {
                        Thread.Sleep(5);
                        LogHelper.BCLog.Debug($"ReTry GetPortMaxSlotCount {portcount}");
                        data = GetPortMaxSlotCountInfo(portcount, out noerr);
                        if (noerr)
                            trycount = 0;
                    }
                }
            }
            return data;
        }
        private int GetPortMaxSlotCountInfo(int portcount, out bool noerr)
        {
            int slotcount = 0;
            noerr = true;
            try
            {
                if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == portcount)))
                {
                    var portinfos = HostInfo.Current.PortList.Where(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == portcount));
                    foreach (var p in portinfos)
                    {
                        var maxslotcount = p.GlassInfos.Where(d => d.CassetteSequenceNo == portcount).Max(c => c.SlotSequenceNo);
                        if (maxslotcount > slotcount)
                        {
                            slotcount = maxslotcount;
                        }
                    }
                    if (slotcount >= 65535)
                    {
                        //重新累计
                        int newslot = 1;
                        while (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == portcount && d.SlotSequenceNo == newslot)))
                        {
                            newslot = newslot + 1;
                        }
                        slotcount = newslot;
                    }
                }
            }
            catch (Exception ex)
            {
                noerr = false;
            }
            return slotcount;
        }
        public void AbnormalCodeReport(Unit oEQP, string LotSequenceNumber, string SlotSequenceNumber, string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, LotSequenceNumber, SlotSequenceNumber, AbnormalFlag1, AbnormalFlag2, AbnormalFlag3, AbnormalFlag4, AbnormalFlag5, AbnormalFlag6, AbnormalFlag7, AbnormalFlag8, transactionID }));
                #endregion

                //根据LotSequenceNumber和SlotSequenceNumber查出BC缓存的panelid
                //job缓存数据
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "AbnormalCodeReport", "", LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        glass.FunctionName = "AbnormalCodeReport";
                        glass.AbnormalCodes = GetMESAbnormalCodes(AbnormalFlag1, AbnormalFlag2, AbnormalFlag3, AbnormalFlag4, AbnormalFlag5, AbnormalFlag6, AbnormalFlag7, AbnormalFlag8);
                        dbService.UpdateGlassInfo(glass);
                    }
                //}
                //else
                //    BCLog.Debug($"AbnormalCodeReport [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ AbnormalCodeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void DVDataReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string UnitNumber, string SlotNumber, string RecipeNumber, string DVSamplingFlag, List<Item> ITEMLIST, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, UnitNumber, SlotNumber, RecipeNumber, "", transactionID }));
                #endregion

                #region 记dvdata log
                string dvdataloginfo = "";
                for (int i = 0; i < ITEMLIST.Count; i++)
                {
                    dvdataloginfo += $"Type:[{ITEMLIST[i].ITEMNAME}]" + "\r\n";
                    for (int j = 0; j < ITEMLIST[i].SITELIST.Count; j++)
                    {
                        var meseqdata = "";
                        if (HostInfo.Current.DVDataList.ContainsKey(oEQP.UnitID))
                        {
                            var mesdata = HostInfo.Current.DVDataList[oEQP.UnitID];
                            {
                                if (mesdata.Any(c => c.ItemName == ITEMLIST[i].ITEMNAME && c.Index == Convert.ToInt32(ITEMLIST[i].SITELIST[j].SITENAME)))
                                {
                                    meseqdata = mesdata.FirstOrDefault(c => c.ItemName == ITEMLIST[i].ITEMNAME && c.Index == Convert.ToInt32(ITEMLIST[i].SITELIST[j].SITENAME)).DVName;
                                }
                            }
                        }
                        dvdataloginfo += $"Number:[{ITEMLIST[i].SITELIST[j].ID.ToString()}] ID:[{ITEMLIST[i].SITELIST[j].SITENAME}] MESName:[{meseqdata}] Value:[{ITEMLIST[i].SITELIST[j].SITEVALUE}]" + "\r\n";
                    }
                }
                LogHelper.CreatDVFile(oEQP.UnitID, JobID, dvdataloginfo);
                #endregion

                List<Item> MESITEMLIST = new List<Item>();
                if (HostInfo.Current.DVDataList.ContainsKey(oEQP.UnitID))
                {
                    var mesdata = HostInfo.Current.DVDataList[oEQP.UnitID];
                    string[] type = new string[] { "INT", "SI", "FLOAT", "ASCII" };
                    for (int i = 0; i < type.Length; i++)
                    {
                        if (mesdata.Any(c => c.ItemName == type[i]))
                        {
                            var mestypedata = mesdata.Where(c => c.ItemName == type[i]).OrderBy(c => c.Index).ToList();

                            var eqdatas = new List<Site>();
                            if (ITEMLIST.Any(c => c.ITEMNAME == type[i]))
                            {
                                eqdatas = ITEMLIST.FirstOrDefault(c => c.ITEMNAME == type[i]).SITELIST;
                            }

                            for (int j = 0; j < mestypedata.Count; j++)
                            {
                                Item item = new Item() { ITEMNAME = mestypedata[j].DVName };
                                Site sitedata = new Site() { SITENAME = "1" };
                                var eqdata = eqdatas.FirstOrDefault(c => c.SITENAME == mestypedata[j].Index.ToString());
                                if (type[i] == "ASCII")
                                {
                                    sitedata.SITEVALUE = (eqdata != null && !String.IsNullOrEmpty(eqdata.SITEVALUE)) ? eqdata.SITEVALUE : "";
                                }
                                else
                                {
                                    double eqvalue = (eqdata != null && !String.IsNullOrEmpty(eqdata.SITEVALUE)) ? Convert.ToDouble(eqdata.SITEVALUE) : 0;
                                    if (mestypedata[j].OperationProportion != 0 && mestypedata[j].OperationProportion != 1)
                                    {
                                        eqvalue = eqvalue * mestypedata[j].OperationProportion;
                                    }
                                    sitedata.SITEVALUE = eqvalue.ToString();
                                    mestypedata[j].DVValue = eqvalue.ToString();
                                }
                                item.SITELIST.Add(sitedata);
                                MESITEMLIST.Add(item);
                            }
                        }
                    }
                    //if (ITEMLIST.Count > 0)
                    //{
                    //    foreach (var item in ITEMLIST)
                    //    {
                    //        if (item.SITELIST.Count > 0)
                    //        {
                    //            foreach (var site in item.SITELIST)
                    //            {
                    //                if (dvdata.Any(c => c.ItemName == item.ITEMNAME && c.Index == Convert.ToInt32(site.SITENAME)))
                    //                {
                    //                    var cfgdv = dvdata.FirstOrDefault(c => c.ItemName == item.ITEMNAME && c.Index == Convert.ToInt32(site.SITENAME));
                    //                    if (cfgdv.OperationProportion != 0 && cfgdv.OperationProportion != 1)
                    //                    {
                    //                        site.SITEVALUE = (Convert.ToInt64(site.SITEVALUE) * cfgdv.OperationProportion).ToString();
                    //                    }
                    //                    cfgdv.DVValue = site.SITEVALUE;
                    //                    //修改sitename
                    //                    site.SITENAME = cfgdv.DVName;
                    //                }
                    //                site.SITEID = null;
                    //            }
                    //        }
                    //    }
                    //}
                }

                string ProductID = "";
                string OperationID = "";
                string CassetteID = "";
                string ProductRecipe = "";
                string LotName = "";
                string Position = "";
                string SubUnitName = "";
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                var glass = logicService.GetGlassInfoByCode(oEQP, "DVDataReport", JobID, LotSequenceNumber, SlotSequenceNumber);
                if (glass != null)//本地有帐
                {
                    ProductID = glass.ProductID;
                    OperationID = glass.OperationID;
                    CassetteID = glass.CassetteID;
                    ProductRecipe = glass.ProductRecipe;
                    LotName = glass.LotID;
                    Position = glass.Position.ToString();
                }

                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                if (eqpinfo != null)
                {
                    var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitID == oEQP.UnitID);
                    if (unitinfo != null)
                    {
                        var sunitinfo = unitinfo.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumber));
                        if (sunitinfo != null)
                        {
                            SubUnitName = sunitinfo.SUnitID;
                        }
                    }
                }
                //}
                //else
                //    BCLog.Debug($"DVDataReport [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");


                //发送MES
                RVProcessData data = new RVProcessData();
                data.MACHINENAME = oEQP.EQPID;
                data.UNITNAME = oEQP.UnitID;
                data.SUBUNITNAME = SubUnitName;
                data.LOTNAME = LotName;
                data.CARRIERNAME = CassetteID;
                data.PRODUCTNAME = JobID;
                data.MACHINERECIPENAME = ProductRecipe;
                data.PROCESSOPERATIONNAME = OperationID;
                data.PRODUCTSPECNAME = ProductID;
                data.POSITION = Position;
                data.PROCESSTIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                data.ITEMLIST = MESITEMLIST;
                mesService.SendToMESProcessData(oEQP.EQPID, data, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ DVDataReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        public void CVDataReport(Unit oEQP, List<Item> ITEMLIST, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, "", transactionID }));
                #endregion

                List<DailyCheckDataItem> MESITEMLIST = new List<DailyCheckDataItem>();
                if (HostInfo.Current.SVDataList.ContainsKey(oEQP.UnitID))
                {
                    var mesdata = HostInfo.Current.SVDataList[oEQP.UnitID];
                    string[] type = new string[] { "INT", "SI", "FLOAT", "ASCII" };
                    for (int i = 0; i < type.Length; i++)
                    {
                        if (mesdata.Any(c => c.ItemName == type[i]))
                        {
                            //Item item = new Item() { ITEMNAME = type[i] };
                            var mestypedata = mesdata.Where(c => c.ItemName == type[i]).OrderBy(c => c.Index).ToList();

                            var eqdatas = new List<Site>();
                            if (ITEMLIST.Any(c => c.ITEMNAME == type[i]))
                            {
                                eqdatas = ITEMLIST.FirstOrDefault(c => c.ITEMNAME == type[i]).SITELIST;
                            }

                            for (int j = 0; j < mestypedata.Count; j++)
                            {
                                //Site sitedata = new Site() { SITENAME = mestypedata[j].SVName };
                                DailyCheckDataItem sitedata = new DailyCheckDataItem() { ITEMNAME = mestypedata[j].SVName };
                                var eqdata = eqdatas.FirstOrDefault(c => c.SITENAME == mestypedata[j].Index.ToString());
                                if (type[i] == "ASCII")
                                {
                                    sitedata.ITEMVALUE = (eqdata != null && !String.IsNullOrEmpty(eqdata.SITEVALUE)) ? eqdata.SITEVALUE : "";
                                }
                                else
                                {
                                    double eqvalue = (eqdata != null && !String.IsNullOrEmpty(eqdata.SITEVALUE)) ? Convert.ToDouble(eqdata.SITEVALUE) : 0;
                                    if (mestypedata[j].OperationProportion != 0 && mestypedata[j].OperationProportion != 1)
                                    {
                                        eqvalue = eqvalue * mestypedata[j].OperationProportion;
                                    }
                                    sitedata.ITEMVALUE = eqvalue.ToString();
                                    mestypedata[j].SVValue = eqvalue.ToString();
                                }
                                MESITEMLIST.Add(sitedata);
                                //item.SITELIST.Add(sitedata);
                            }
                            //MESITEMLIST.Add(item);
                        }
                    }
                    //var svdata = HostInfo.Current.SVDataList[oEQP.UnitID];
                    //if (ITEMLIST.Count > 0)
                    //{
                    //    List<DailyCheckDataItem> MESITEMLIST = new List<DailyCheckDataItem>();
                    //    foreach (var item in ITEMLIST)
                    //    {
                    //        if (item.SITELIST.Count > 0)
                    //        {
                    //            foreach (var site in item.SITELIST)
                    //            {
                    //                if (svdata.Any(c => c.ItemName == item.ITEMNAME && c.Index == Convert.ToInt32(site.SITENAME)))
                    //                {
                    //                    var cfgsv = svdata.FirstOrDefault(c => c.ItemName == item.ITEMNAME && c.Index == Convert.ToInt32(site.SITENAME));
                    //                    if (cfgsv.OperationProportion != 0 && cfgsv.OperationProportion != 1)
                    //                    {
                    //                        site.SITEVALUE = (Convert.ToInt64(site.SITEVALUE) * cfgsv.OperationProportion).ToString();
                    //                    }
                    //                    cfgsv.SVValue = site.SITEVALUE;
                    //                    //修改sitename
                    //                    site.SITENAME = cfgsv.SVName;
                    //                    MESITEMLIST.Add(new DailyCheckDataItem() { ITEMNAME = cfgsv.SVName, ITEMVALUE = cfgsv.SVValue });
                    //                }
                    //                site.SITEID = null;
                    //            }
                    //        }
                    //    }
                    //}
                }
                //机种和Recipe 取值
                string partname = "";
                string recipename = "";
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CurrentUnit == oEQP.UnitID)))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CurrentUnit == oEQP.UnitID));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CurrentUnit == oEQP.UnitID);
                //    partname = glass.ProductID;
                //    recipename = glass.ProductRecipe;
                //}
                //else
                //    BCLog.Debug($"CVDataReport [{oEQP.UnitName}] Not found Glass CurrentUnit:[{oEQP.UnitID}]");
                //发送MES
                RVDailyCheckDataReport data = new RVDailyCheckDataReport();
                data.EQUIPMENTID = oEQP.EQPID;
                data.UNITID = oEQP.UnitID;
                data.PARTNAME = partname;
                data.RECIPENAME = recipename;
                data.ITEMLIST = MESITEMLIST;
                mesService.SendToMESDailyCheckDataReport(oEQP.EQPID, data, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ CVDataReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        public void VCRStatusReport(Unit oEQP, string VCRNumber, string VCRStatus, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, VCRNumber, VCRStatus, transactionID }));
                #endregion

                if (!String.IsNullOrEmpty(oEQP.VCRStatus))
                {
                    var VCRStatuss = oEQP.VCRStatus.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, string> DictVCR = new Dictionary<string, string>();
                    if (VCRStatuss.Length > 0)
                    {
                        foreach (var VCR in VCRStatuss)
                        {
                            var VCRs = VCR.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            DictVCR.Add(VCRs[0], VCRs[1]);
                        }
                    }

                    if (DictVCR.ContainsKey(VCRNumber))
                    {
                        DictVCR[VCRNumber] = HostInfo.GetEQToBCValue(PLCEventItem.VCRStatus, VCRStatus);
                    }
                    else
                    {
                        DictVCR.Add(VCRNumber, HostInfo.GetEQToBCValue(PLCEventItem.VCRStatus, VCRStatus));
                    }

                    oEQP.VCRStatus = "";
                    foreach (var dict in DictVCR)
                    {
                        oEQP.VCRStatus += String.Format("{0}:{1};", dict.Key, dict.Value);
                    }
                }
                else
                {
                    oEQP.VCRStatus = String.Format("{0}:{1};", VCRNumber, HostInfo.GetEQToBCValue(PLCEventItem.VCRStatus, VCRStatus));
                }

                dbService.UpdateUnitInfo(oEQP);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ VCRStatusReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        public void DateTimeRequest(Unit oEQP, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, transactionID }));
                #endregion

                //DateTimeRequestReply
                eqpService.SendDateTimeRequestReply(oEQP.UnitName, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ DateTimeRequest:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        /// <summary>
        /// MachineStatusChangeReport
        /// </summary>
        /// <param name="oEQP"></param>
        /// <param name="MachineStatus">2-EX实验 3-JC换线 5-UD宕机 6-CM换料 7-ID等待 8-RUN跑货</param>
        public void MachineStatusChangeReport(Unit oEQP, string MachineStatus, string MachinestatusReasonCode, ConcurrentDictionary<int, string> UnitList, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, MachineStatus, UnitList, transactionID }));
                #endregion

                bool needSendMes = false;//设备或者Unit是否需要上报MES
                string sMachineStatus = HostInfo.Current.GetEQToBCValue(MESEventItem.EQPStatus, MachineStatus);//Consts.dicEQPStatus[Convert.ToInt32(MachineStatus)];
                if (oEQP.UnitStatus != sMachineStatus)
                {
                    needSendMes = true;
                    oEQP.UnitStatus = sMachineStatus;
                    oEQP.ReasonCode = MachinestatusReasonCode;
                    dbService.UpdateUnitInfo(oEQP);
                }

                //根据规则计算整线状态
                var newLineStatus = GetLineStatus(oEQP.EQPID);

                //如果整线状态变化 上报MES Equipment State Report
                if (HostInfo.Current.AllEQPInfo.Any(c => c.EQPID == oEQP.EQPID))
                {
                    var lineinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                    if (lineinfo.EqpStatus != newLineStatus)
                    {
                        lineinfo.EqpStatus = newLineStatus;
                        dbService.UpdateEQPInfo(lineinfo);//更新数据库

                        //发送MES Equipment State Report
                        RVEquipmentState mesline = new RVEquipmentState();
                        mesline.EQUIPMENTID = oEQP.EQPID;
                        mesline.COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, newLineStatus);
                        mesline.STATE = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, newLineStatus);
                        mesService.SendToMESEquipmentStateReport(oEQP.EQPID, mesline, transactionID);
                    }
                }

                //上报设备各Unit
                RVUnitState meseqp = new RVUnitState();
                meseqp.EQUIPMENTID = oEQP.EQPID;
                var levelthree = !String.IsNullOrEmpty(oEQP.ReasonCode) ? HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatusLevelThree, oEQP.UnitStatus + "_" + oEQP.ReasonCode) : HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, oEQP.UnitStatus);
                var unitstat = new RVUnitList()
                {
                    UNITID = oEQP.UnitID,
                    COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, oEQP.UnitStatus),
                    STATE = levelthree
                };
                meseqp.UNITLIST.Add(unitstat);
                mesService.SendToMESUnitStateReport(oEQP.EQPID, meseqp, transactionID);

                //上报Unit各Sunit
                RVUnitState meseqpsunit = new RVUnitState();
                meseqpsunit.EQUIPMENTID = oEQP.UnitID;

                foreach (var unit in oEQP.SUnitList)
                {
                    var unitno = unit.SubUnitNo;
                    if (UnitList.ContainsKey(unitno))
                    {
                        string sSUnitStatus = HostInfo.Current.GetEQToBCValue(MESEventItem.EQPStatus, UnitList[unitno]);
                        if (unit.SUnitStatus != sSUnitStatus)
                        {
                            //更新缓存及数据库
                            unit.SUnitStatus = sSUnitStatus;
                            dbService.UpdateSUnitInfo(unit);
                            //UpdateUnitStatus(unit.Value);
                            if (unit.ReportMesState)//是否需要上报MES
                            {
                                needSendMes = true;
                            }
                        }
                    }
                    meseqpsunit.UNITLIST.Add(new RVUnitList() { UNITID = unit.SUnitID, COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, unit.SUnitStatus), STATE = levelthree });
                }
                if (needSendMes)
                {
                    mesService.SendToMESUnitStateReport(oEQP.EQPID, meseqpsunit, transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MachineStatusChangeReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        private string GetLineStatus(string EQPID)
        {
            String LineStatus = "";
            try
            {
                //根据优先级按顺序判断
                Hashtable groupHT = new Hashtable();
                groupHT.Add("eqpid", EQPID);
                var statusgroup = dbService.Viewcfg_eqpstatusgroup(groupHT).ToList();
                var statusrule = dbService.Viewcfg_eqpstatusrule(groupHT).ToList();
                if (statusgroup != null && statusgroup.Count > 0)
                {
                    if (HostInfo.Current.AllEQPInfo.Any(c => c.EQPID == EQPID))
                    {
                        var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == EQPID);
                        foreach (var statusgr in statusgroup)
                        {
                            //按优先级状态找到多个条件
                            if (statusrule.Any(c => c.eqpstatus == statusgr.eqpstatus))
                            {
                                var srulelist = statusrule.Where(c => c.eqpstatus == statusgr.eqpstatus).ToList();
                                if (srulelist != null && srulelist.Count > 0)
                                {
                                    foreach (var srule in srulelist)
                                    {
                                        //判断每个条件是否有达到要求
                                        var unitids = srule.unitidlist.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                        var status = srule.eqpstatus;
                                        bool isallok = true;
                                        if (unitids.Length > 0)
                                        {
                                            foreach (var unitid in unitids)
                                            {
                                                if (!eqpinfo.Units.Any(c => c.UnitID == unitid && c.UnitStatus == status))
                                                {
                                                    isallok = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            isallok = false;
                                        }
                                        if (isallok)
                                        {
                                            LineStatus = statusgr.eqpstatus;
                                            goto HasRes;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ GetLineStatus,Error:{0} +++", ex.ToString());
            }
        HasRes:
            return LineStatus;
        }
        public void MaterialStatusChangeReport(Unit oEQP, int i, string MaterialStatus, string MaterialID, string MaterialType, string UnitNumber, string SlotNumber, string MaterialCount, string UnloadingCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, MaterialStatus, MaterialID, MaterialType, UnitNumber, SlotNumber, MaterialCount, UnloadingCode, transactionID }));
                #endregion

                var subUnit = oEQP.SUnitList.FirstOrDefault(c => c.SubUnitNo == Convert.ToInt32(UnitNumber));

                //发送MES
                RVMaterialState data = new RVMaterialState();
                data.EQUIPMENTID = oEQP.EQPID;
                data.UNITID = oEQP.UnitID;
                data.PORTID = subUnit != null ? subUnit.SUnitID : "";
                data.DURABLEID = MaterialID;
                data.OPERATOR = "";
                data.STATE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialStatus, MaterialStatus);

                if (!String.IsNullOrEmpty(MaterialID))
                {
                    var materiallist = dbService.ViewMaterialInfo(new MaterialInfo() { MaterialLotID = MaterialID });
                    if (materiallist != null && materiallist.Count > 0)
                    {
                        foreach (var mat in materiallist)
                        {
                            data.MLOTLIST.Add(new RVMaterialList() { MLOTID = mat.MaterialID, POSITION = data.PORTID, MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, MaterialType), MATERIALNAME = mat.MaterialName, MAINQTY = mat.MaterialUseCount, ACTIONCODE = HostInfo.GetEQToBCValue(PLCEventItem.UnloadingCode, UnloadingCode) });
                        }
                    }
                    else
                    {
                        BCLog.Info($"UnitName:{oEQP.UnitID} MaterialID:{MaterialID} DB WIP Info not exist");
                        data.MLOTLIST.Add(new RVMaterialList() { MLOTID = MaterialID, POSITION = data.PORTID, MATERIALTYPE = HostInfo.GetEQToBCValue(PLCEventItem.MaterialType, MaterialType), MATERIALNAME = "", MAINQTY = "", ACTIONCODE = HostInfo.GetEQToBCValue(PLCEventItem.UnloadingCode, UnloadingCode) });
                    }
                }
                var mesres = mesService.SendToMESMaterialStateReport(oEQP.EQPID, data, transactionID);
                var mesok = false;
                if (mesres != null)
                {
                    if (mesres.RESULT == MESResult.SUCCESS.ToString())
                    {
                        mesok = true;
                    }
                }

                if (mesok)
                {
                    //更新数据库
                    MaterialInfo mtl = new MaterialInfo();
                    mtl.EQPID = oEQP.EQPID;
                    mtl.UnitID = oEQP.UnitID;
                    mtl.MaterialLotID = MaterialID;
                    //if (MaterialType != "1")
                    //    mtl.MaterialUseCount = MaterialCount;
                    mtl.MaterialState = HostInfo.GetEQToBCValue(PLCEventItem.MaterialStatus, MaterialStatus);
                    dbService.UpdateMaterialInfo(mtl);

                    if (MaterialStatus == "2")
                    {
                        RVMaterialUnloadComplete materialUnloadComplete = new RVMaterialUnloadComplete();
                        materialUnloadComplete.EQUIPMENTID = oEQP.EQPID;
                        materialUnloadComplete.UNITID = oEQP.UnitID;
                        materialUnloadComplete.PORTID = subUnit != null ? subUnit.SUnitID : "";
                        materialUnloadComplete.DURABLEID = MaterialID;
                        mesService.SendToMESMaterialUnloadComplete(oEQP.EQPID, materialUnloadComplete, transactionID);

                        //下料删除wip数据
                        dbService.DeleteMaterialInfo(mtl);
                    }

                    //回复设备
                    eqpService.SendMaterialStatusChangeReportReply(oEQP.UnitName, i, "1", transactionID);
                }
                else
                {
                    //回复设备
                    eqpService.SendMaterialStatusChangeReportReply(oEQP.UnitName, i, "2", transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ MaterialStatusChangeReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oEQP"></param>
        /// <param name="JobID"></param>
        /// <param name="LotSequenceNumber"></param>
        /// <param name="SlotSequenceNumber"></param>
        /// <param name="UnitNumber"></param>
        /// <param name="VCRNumber"></param>
        /// <param name="VCRResult">VCR Result：（
        /// 1、VCR Reading OK & Match With Job Data Glass ID 
        /// 2、VCR Reading OK & Miss Match With Job Data Glass ID
        /// 3、VCR Reading Fail & Key In & Match With Job Data Glass ID
        /// 4、VCR Reading Fail & Key In & Miss Match With Job Data Glass ID）</param>
        /// <param name="transactionID"></param>
        public void VCRReadCompleteReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string UnitNumber, string VCRNumber, string VCRResult, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, UnitNumber, VCRNumber, VCRResult, transactionID }));
                #endregion

                string vcrpanelid = JobID;//VCR读出的panelid
                string panelid = "";//原panelid
                if (VCRResult == "1" || VCRResult == "3")
                {
                    panelid = vcrpanelid;
                }
                if (VCRResult == "2" || VCRResult == "4")//VCR Read ID与Received Job ID不匹配
                {
                    //根据LotSequenceNumber和SlotSequenceNumber查出BC缓存的panelid
                    //job缓存数据
                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                    //{
                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "VCRReadComplete", JobID, LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        panelid = glass.GlassID;

                        if (glass.GlassID != vcrpanelid)
                        {
                            //查出后 需要将vcrpanelid 与 panelid对应的本地帐数据进行置换
                            //查出vcrpanelid对应的帐
                            //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == vcrpanelid)))
                            //{
                            //    var portinfo_old = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == vcrpanelid));
                            //    var glass_old = portinfo_old.GlassInfos.FirstOrDefault(d => d.GlassID == vcrpanelid);
                            var glass_old = logicService.GetGlassInfoByCode(oEQP, "", vcrpanelid, "", "", 2, "");
                            if (glass_old != null)
                            {
                                //进行置换
                                glass_old.GlassID = panelid;
                                glass_old.FunctionName = "VCRReadComplete";
                                glass.GlassID = vcrpanelid;
                                glass.FunctionName = "VCRReadComplete";
                                //更新数据库
                                dbService.UpdateGlassInfo(glass_old);
                                dbService.UpdateGlassInfo(glass);
                            }
                            //}
                        }
                    }
                    //}
                    //else
                    //    BCLog.Debug($"VCRReadCompleteReport [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");
                }
                if (!String.IsNullOrEmpty(vcrpanelid) && !String.IsNullOrEmpty(panelid))
                {
                    //发送MES
                    RVVCRRead data = new RVVCRRead();
                    data.EQUIPMENTID = oEQP.EQPID;
                    data.UNITID = oEQP.UnitID;
                    data.PANELID = panelid;
                    data.VCRPANELID = vcrpanelid;
                    data.VCRSTATE = "MANUAL";
                    data.RESULT = VCRResult;
                    mesService.SendToMESVCRReadReport(oEQP.EQPID, data, transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ VCRReadCompleteReport:{0} ,Error:{1} +++", oEQP.UnitID, ex.ToString());
            }
        }
        public void MachineModeChangeCommandReply(Unit oEQP, string MachineModeChangeReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, MachineModeChangeReturnCode, transactionID }));
                #endregion

                string result = MachineModeChangeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = MachineModeChangeReturnCode == "1" ? "" : "error";
                string msg = string.Format("MachineModeChangeCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ MachineModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void RecipeParameterRequestCommandReply(Unit oEQP, string Result, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, Result, transactionID }));
                #endregion

                if (HostInfo.Current.OPICommandTrans.ContainsKey(oEQP.UnitName + "_RecipeParameterRequestCommand"))
                {
                    if (HostInfo.Current.OPICommandTrans[oEQP.UnitName + "_RecipeParameterRequestCommand"] == transactionID)
                    {
                        string result = Result == "1" ? "OK" : "NG";
                        string resultlevel = Result == "1" ? "" : "error";
                        string msg = string.Format("RecipeParameterRequestCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                        SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ MachineModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CIMModeChangeCommandReply(Unit oEQP, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, ReturnCode, transactionID }));
                #endregion

                string result = ReturnCode == "1" ? "OK" : "NG";
                string resultlevel = ReturnCode == "1" ? "" : "error";
                string msg = string.Format("CIMModeChangeCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CIMModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CIMModeChange(Unit oEQP, string CIMMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, CIMMode, transactionID }));
                #endregion

                bool cimmode = CIMMode == "1" ? true : false;
                oEQP.CIMMode = cimmode;
                dbService.UpdateUnitInfo(oEQP);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CIMModeChange:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CIMMessageSetCommandReply(Unit oEQP, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, ReturnCode, transactionID }));
                #endregion

                string result = ReturnCode == "1" ? "OK" : "NG";
                string resultlevel = ReturnCode == "1" ? "" : "error";
                string msg = string.Format("CIMMessageSetCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CIMMessageSetCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CIMMessageClearCommandReply(Unit oEQP, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, ReturnCode, transactionID }));
                #endregion

                string result = ReturnCode == "1" ? "OK" : "NG";
                string resultlevel = ReturnCode == "1" ? "" : "error";
                string msg = string.Format("CIMMessageClearCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CIMMessageClearCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void DVSamplingFlagCommandReply(Unit oEQP, string ReasonCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, ReasonCode, transactionID }));
                #endregion

                string result = ReasonCode == "1" ? "OK" : "NG";
                string resultlevel = ReasonCode == "1" ? "" : "error";
                string msg = string.Format("DVSamplingFlagCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
                if (HostInfo.CancellationObjectDic.ContainsKey(transactionID))
                {
                    var cancellationObject = HostInfo.CancellationObjectDic[transactionID];
                    var cancel = cancellationObject.CancellationTokenSource;
                    if (cancel != null)
                    {
                        cancel.Cancel();

                        RVSPCRateDownloadReply sPCRateDownloadReply = new RVSPCRateDownloadReply();
                        RVHeader replyHeader = new RVHeader();
                        replyHeader.MESSAGENAME = sPCRateDownloadReply.MessageName;
                        replyHeader.TRANSACTIONID = transactionID;
                        replyHeader.RESULT = ReasonCode == "1" ? "SUCCESS" : "FAIL";
                        replyHeader.RESULTMESSAGE = ReasonCode == "1" ? "" : ("UNITID:" + oEQP.UnitID + " Reject");
                        mesService.SendToMESSPCRateDownloadReply(oEQP.EQPID, sPCRateDownloadReply, replyHeader, cancellationObject.MessageData);
                        HostInfo.CancellationObjectDic.TryRemove(transactionID, out var val);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ DVSamplingFlagCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void SamplingDownloadCommandReply(Unit oEQP, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, ReturnCode, transactionID }));
                #endregion

                string result = ReturnCode == "1" ? "OK" : "NG";
                string resultlevel = ReturnCode == "1" ? "" : "error";
                string msg = string.Format("SamplingDownloadCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
                if (HostInfo.CancellationObjectDic.ContainsKey(transactionID))
                {
                    var cancellationObject = HostInfo.CancellationObjectDic[transactionID];
                    var cancel = cancellationObject.CancellationTokenSource;
                    if (cancel != null)
                    {
                        cancel.Cancel();

                        RVSamplingDownloadReply samplingDownloadReply = new RVSamplingDownloadReply();
                        RVHeader replyHeader = new RVHeader();
                        replyHeader.MESSAGENAME = samplingDownloadReply.MessageName;
                        replyHeader.TRANSACTIONID = transactionID;
                        replyHeader.RESULT = ReturnCode == "1" ? "SUCCESS" : "FAIL";
                        replyHeader.RESULTMESSAGE = ReturnCode == "1" ? "" : ("UNITID:" + oEQP.UnitID + " Reject");
                        mesService.SendToMESSamplingDownloadReply(oEQP.EQPID, samplingDownloadReply, replyHeader, cancellationObject.MessageData);
                        HostInfo.CancellationObjectDic.TryRemove(transactionID, out var val);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ SamplingDownloadCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PositionStatusChange(Unit oEQP, List<GlassExistencePosition> data, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, data, transactionID }));
                #endregion

                foreach (var da in data)
                {
                    dbService.UpdateGlassExistencePosition(da);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ SamplingDownloadCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void SpecialCodeRequest(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, transactionID }));
                #endregion

                string abnormalflag1 = "0000000000000000";
                string abnormalflag2 = "0000000000000000";
                string abnormalflag3 = "0000000000000000";
                string abnormalflag4 = "0000000000000000";
                string abnormalflag5 = "0000000000000000";
                string abnormalflag6 = "0000000000000000";
                string abnormalflag7 = "0000000000000000";
                string abnormalflag8 = "0000000000000000";
                string workorder = "";
                //job缓存数据
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(SlotSequenceNumber));
                    var glass = logicService.GetGlassInfoByCode(oEQP, "SpecialCodeRequest", JobID, LotSequenceNumber, SlotSequenceNumber);
                    if (glass != null)
                    {
                        glass.FunctionName = "SpecialCodeRequest";
                        dbService.InsertHisGlassInfo(glass);

                        string code = GetAbnormalFlag(glass.AbnormalCodes);
                        var codes = code.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        abnormalflag1 = codes[0];
                        abnormalflag2 = codes[1];
                        abnormalflag3 = codes[2];
                        abnormalflag4 = codes[3];
                        abnormalflag5 = codes[4];
                        abnormalflag6 = codes[5];
                        abnormalflag7 = codes[6];
                        abnormalflag8 = codes[7];
                        workorder = glass.WorkOrder;
                    }
                //}
                //else
                //    BCLog.Debug($"SpecialCodeRequest [{oEQP.UnitName}] Not found Glass [{LotSequenceNumber}][{SlotSequenceNumber}]");

                //SpecialCodeRequestReply
                eqpService.SendSpecialCodeRequestReply(oEQP.UnitName, JobID, LotSequenceNumber, SlotSequenceNumber, abnormalflag1, abnormalflag2, abnormalflag3, abnormalflag4, abnormalflag5, abnormalflag6, abnormalflag7, abnormalflag8, workorder, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ SpecialCodeRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CuttingRequest(Unit oEQP, CutPanelList data, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, data, transactionID }));
                #endregion

                Boolean MesRes = false;
                //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(data.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(data.SlotSequenceNumber))))
                //{
                //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(data.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(data.SlotSequenceNumber)));
                //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(data.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(data.SlotSequenceNumber));
                var glass = logicService.GetGlassInfoByCode(oEQP, "CuttingRequest", data.JobID, data.LotSequenceNumber, data.SlotSequenceNumber);
                if (glass != null)//大板
                {
                    var portinfo = logicService.GetPortInfoByCode(oEQP, data.JobID, data.LotSequenceNumber, data.SlotSequenceNumber, 1);
                    //var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(data.LotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(data.SlotSequenceNumber)));

                    RVCuttingComplete mesdata = new RVCuttingComplete();
                    mesdata.EQUIPMENTID = oEQP.EQPID;
                    mesdata.UNITID = oEQP.UnitID;
                    mesdata.PARTNAME = glass.ProductID;
                    mesdata.STEPNAME = glass.OperationID;
                    mesdata.RECIPENAME = glass.ProductRecipe;
                    mesdata.GLASSID = glass.GlassID;
                    if (data.PanelInfos.Count > 0)
                    {
                        var glassGrade = glass.PanelGrade;
                        var glassGradeList = glassGrade.ToArray();
                        int index = 0;
                        bool checkok = true;
                        foreach (var panelinfo in data.PanelInfos)
                        {
                            RVCuttingCompletePanel mespanel = new RVCuttingCompletePanel();
                            string panelid = glass.GlassID;
                            #region 根据规则生成panelid
                            if (!String.IsNullOrEmpty(panelinfo.QPanelCode))
                            {
                                panelid = panelid.Remove(10, 1);
                                panelid = panelid.Insert(10, panelinfo.QPanelCode);
                            }
                            if (!String.IsNullOrEmpty(panelinfo.ScriberModuleType))
                            {
                                panelid = panelid.Remove(1, 2);
                                panelid = panelid.Insert(1, panelinfo.ScriberModuleType);
                            }
                            if (!String.IsNullOrEmpty(panelinfo.CutPanelID))
                            {
                                panelid = panelid.Remove(11, 2);
                                panelid = panelid.Insert(11, panelinfo.CutPanelID);
                            }
                            #endregion
                            panelinfo.CutJobID = panelid;//这里把生成后的也赋值 下面逻辑会用到
                            mespanel.PANELID = panelid;
                            mespanel.GRADE = (glassGradeList.Count() >= (index + 1)) ? glassGradeList[index].ToString() : "";
                            mesdata.PANELLIST.Add(mespanel);
                            index++;

                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(panelinfo.CutPanelLotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(panelinfo.CutPanelSlotSequenceNumber))))
                            {
                                var glassportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(panelinfo.CutPanelLotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(panelinfo.CutPanelSlotSequenceNumber)));
                                var glasseach = glassportinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(panelinfo.CutPanelLotSequenceNumber) && d.SlotSequenceNo == Convert.ToInt32(panelinfo.CutPanelSlotSequenceNumber));
                                if (glasseach != null)
                                {
                                    checkok = false;
                                    BCLog.Debug($"CuttingRequest [{oEQP.UnitName}] Q-Panel [{data.JobID}][{data.LotSequenceNumber}][{data.SlotSequenceNumber}] glass exist IN BC [{panelinfo.CutPanelLotSequenceNumber}][{panelinfo.CutPanelSlotSequenceNumber}]");
                                }
                            }
                        }

                        if (!checkok)
                        {
                            eqpService.SendCuttingRequestReply(oEQP.UnitName, "2", transactionID);
                            return;
                        }
                    }
                    //发送MES
                    var res = mesService.SendToMESCuttingCompleteReport(oEQP.EQPID, mesdata, transactionID);
                    if (res != null && res.RESULT == MESResult.SUCCESS.ToString())
                    {
                        MesRes = true;
                    }

                    #region 需求3 2.修改MES回复NG后BC不生成小板信息
                    if (MesRes || IsInLineMode(oEQP.EQPID))
                    #endregion
                    {

                        //写入数据库及缓存
                        foreach (var panelinfo in data.PanelInfos)
                        {
                            GlassInfo panelGlassInfo = (GlassInfo)glass.Clone();//字段值先全继承 后续确认后再改
                            panelGlassInfo.FunctionName = "CuttingRequest";
                            panelGlassInfo.GlassID = panelinfo.CutJobID;
                            panelGlassInfo.CassetteSequenceNo = Convert.ToInt32(panelinfo.CutPanelLotSequenceNumber);
                            panelGlassInfo.SlotSequenceNo = Convert.ToInt32(panelinfo.CutPanelSlotSequenceNumber);
                            panelGlassInfo.RGlassID = glass.GlassID;
                            //panelGlassInfo.RecvJobTime = DateTime.Now;
                            panelGlassInfo.CurrentUnit = oEQP.UnitID;
                            //根据模位 继承AbnormalCode
                            panelGlassInfo.AbnormalCodes = "";
                            List<CODE> codes = GetAbnormalCodes(glass.AbnormalCodes);
                            if (codes.Count > 0)
                            {
                                //按模位取自己的abnormalcode
                                var matchcode = codes.Where(c => c.PANELID == panelinfo.CutPanelID);
                                if (matchcode != null && matchcode.Count() > 0)
                                {
                                    panelGlassInfo.AbnormalCodes = String.Join(";", matchcode.Select(c => c.CODEID));
                                }
                                //继承glass的abnormalcode
                                var glasscode = codes.Where(c => c.PANELID == "");
                                if (glasscode != null && glasscode.Count() > 0)
                                {
                                    panelGlassInfo.AbnormalCodes += String.Join(";", glasscode.Select(c => c.CODEID));
                                }
                            }

                            //更新缓存
                            portinfo.GlassInfos.Add(panelGlassInfo);
                            //更新数据库
                            var ret = dbService.InsertGlassInfo(panelGlassInfo);
                        }

                        //删除大板的wip
                        glass.FunctionName = "CuttingRequest";
                        dbService.InsertHisGlassInfo(glass);
                        //删除缓存
                        portinfo.GlassInfos.Remove(glass);
                        //删除数据库
                        Hashtable glasstb = new Hashtable();
                        glasstb.Add("ID", glass.ID);
                        dbService.DeleteGlassInfoList(glasstb);
                        glass.FunctionName = "DeleteWIP";
                        dbService.InsertHisGlassInfo(glass);
                    }
                }
                //}
                //else
                //    BCLog.Debug($"CuttingRequest [{oEQP.UnitName}] Not found Q-Panel [{data.LotSequenceNumber}][{data.SlotSequenceNumber}]");

                //CuttingRequestReply
                eqpService.SendCuttingRequestReply(oEQP.UnitName, ((MesRes || IsInLineMode(oEQP.EQPID)) ? "1" : "2"), transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ CuttingRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void BufferJobMonitoring(Unit oEQP, List<string> numbers, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, numbers, transactionID }));
                #endregion

                //发送MES
                RVNGBufferInfoReport data = new RVNGBufferInfoReport();
                data.EQUIPMENTID = oEQP.EQPID;
                data.UNITID = oEQP.UnitID;
                data.SUBUNITID = "";
                data.QTY = numbers.Count.ToString();
                mesService.SendToMESNGBufferInfoReport(oEQP.EQPID, data, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ BufferJobMonitoring:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CVReportTimeChangeCommandReply(Unit oEQP, string CVCommandReturnCode, string CycleType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, CVCommandReturnCode, CycleType, transactionID }));
                #endregion

                string result = CVCommandReturnCode == "1" ? "OK" : "NG";
                string resultlevel = CVCommandReturnCode == "1" ? "" : "error";
                string msg = string.Format("CVReportTimeChangeCommand EQP:{0} Result:{1}", oEQP.UnitID, result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CVReportTimeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }

        public GlassInfo GetGlassInfoByCode(Unit oEQP, string functionname, string JobID, string CSTSeqNo, string SlotSeqNo, int type = 1, string BLID = "")
        {
            bool noerr = true;
            var data = GetGlassInfo(oEQP, functionname, JobID, CSTSeqNo, SlotSeqNo, out noerr, type, BLID);
            if (!noerr)
            {
                int trycount = 3;
                while (trycount > 0)
                {
                    trycount--;
                    if (!noerr)
                    {
                        Thread.Sleep(5);
                        LogHelper.BCLog.Debug($"ReTry GetGlassInfoByCode {oEQP.UnitID} {functionname} {JobID} {CSTSeqNo} {SlotSeqNo} {type} {BLID}");
                        data = GetGlassInfo(oEQP, functionname, JobID, CSTSeqNo, SlotSeqNo, out noerr, type, BLID);
                        if (noerr)
                            trycount = 0;
                    }
                }
            }
            return data;
        }
        private GlassInfo GetGlassInfo(Unit oEQP, string functionname, string JobID, string CSTSeqNo, string SlotSeqNo, out bool noerr, int type = 1, string BLID = "")
        {
            noerr = true;
            try
            {
                switch (type)
                {
                    case 1://glasscode
                        {
                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(CSTSeqNo) && d.SlotSequenceNo == Convert.ToInt32(SlotSeqNo))))
                            {
                                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(CSTSeqNo) && d.SlotSequenceNo == Convert.ToInt32(SlotSeqNo)));
                                var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == Convert.ToInt32(CSTSeqNo) && d.SlotSequenceNo == Convert.ToInt32(SlotSeqNo));
                                if (glass != null)
                                {
                                    return glass;
                                }
                            }
                            else
                            {
                                BCLog.Debug($"{functionname} [{oEQP.UnitID}] Not found Glass [{CSTSeqNo}][{SlotSeqNo}]");
                                if (!String.IsNullOrEmpty(JobID))
                                {
                                    //只记录History
                                    GlassInfo HisInfo = new GlassInfo();
                                    HisInfo.FunctionName = functionname + "(NoWIP)";
                                    HisInfo.CurrentUnit = oEQP.UnitID;
                                    HisInfo.GlassID = JobID;
                                    HisInfo.CassetteSequenceNo = Convert.ToInt32(CSTSeqNo);
                                    HisInfo.SlotSequenceNo = Convert.ToInt32(SlotSeqNo);
                                    dbService.InsertHisGlassInfo(HisInfo);
                                    ////glasscode查不到就用glassid去查，然后记履历
                                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                                    //{
                                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                                    //    if (glass != null)
                                    //    {
                                    //        //只记录History
                                    //        GlassInfo HisInfo = (GlassInfo)glass.Clone();
                                    //        HisInfo.FunctionName = functionname;
                                    //        HisInfo.CurrentUnit = oEQP.UnitID;
                                    //        HisInfo.CassetteSequenceNo = Convert.ToInt32(CSTSeqNo);
                                    //        HisInfo.SlotSequenceNo = Convert.ToInt32(SlotSeqNo);
                                    //        dbService.InsertHisGlassInfo(HisInfo);
                                    //    }
                                    //}
                                    //else
                                    //    BCLog.Debug($"{functionname} [{oEQP.UnitID}] Not found Glass [{JobID}]");
                                }
                            }
                        }
                        break;
                    case 2://glassid
                        {
                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == JobID)))
                            {
                                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == JobID));
                                var glass = portinfo.GlassInfos.FirstOrDefault(d => d.GlassID == JobID);
                                if (glass != null)
                                {
                                    return glass;
                                }
                            }
                        }
                        break;
                    case 3://BLID
                        {
                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.BLID == BLID)))
                            {
                                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.BLID == BLID));
                                var glass = portinfo.GlassInfos.FirstOrDefault(d => d.BLID == BLID);
                                if (glass != null)
                                {
                                    return glass;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                noerr = false;
                if (!ex.ToString().Contains("System.InvalidOperationException"))//集合已修改；可能无法执行枚举操作
                    LogHelper.BCLog.Error(ex);
            }
            return null;
        }
        public PortInfo GetPortInfoByCode(Unit oEQP, string JobID, string CSTSeqNo, string SlotSeqNo, int type = 1)
        {
            bool noerr = true;
            var data = GetPortInfo(oEQP, JobID, CSTSeqNo, SlotSeqNo, out noerr, type);
            if (!noerr)
            {
                int trycount = 3;
                while (trycount > 0)
                {
                    trycount--;
                    if (!noerr)
                    {
                        Thread.Sleep(5);
                        LogHelper.BCLog.Debug($"ReTry GetPortInfoByCode {oEQP.UnitID} {JobID} {CSTSeqNo} {SlotSeqNo} {type}");
                        data = GetPortInfo(oEQP, JobID, CSTSeqNo, SlotSeqNo, out noerr, type);
                        if (noerr)
                            trycount = 0;
                    }
                }
            }
            return data;
        }
        private PortInfo GetPortInfo(Unit oEQP, string JobID, string CSTSeqNo, string SlotSeqNo, out bool noerr, int type = 1)
        {
            noerr = true;
            try
            {
                switch (type)
                {
                    case 1://glasscode
                        {
                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(CSTSeqNo) && d.SlotSequenceNo == Convert.ToInt32(SlotSeqNo))))
                            {
                                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == Convert.ToInt32(CSTSeqNo) && d.SlotSequenceNo == Convert.ToInt32(SlotSeqNo)));
                                return portinfo;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                noerr = false;
                if (!ex.ToString().Contains("System.InvalidOperationException"))//集合已修改；可能无法执行枚举操作
                    LogHelper.BCLog.Error(ex);
            }
            return null;
        }

        #region forOPI
        public BCInforamtionReport GetOPILineInfo(EQPInfo lineInfo)
        {
            BCInforamtionReport bCInforamtionReport = new BCInforamtionReport();
            try
            {

                #region 相关赋值
                string mesControlMode = "", lineOperationMode = "", eqpOperationMode = "";
                switch (lineInfo.ControlState)
                {
                    case EnumControlState.OffLine:
                        mesControlMode = "OFFLINE";
                        break;
                    case EnumControlState.OnLineLocal:
                        mesControlMode = "LOCAL";
                        break;
                    case EnumControlState.OnLineRemote:
                        mesControlMode = "REMOTE";
                        break;
                    default:
                        break;
                }
                var operationmode = HostInfo.Current.operationmodelist.FirstOrDefault(c => c.eqpid == lineInfo.EQPID && c.equipmentvalue == (int)lineInfo.LineMode);
                if (operationmode != null)
                {
                    lineOperationMode = operationmode.operationmodename;
                }

                bool EIPisConnected = true;
                List<Equipments> equipmentsList = new List<Equipments>();
                //组合线这里将所有线的设备都加进去
                foreach (var eqpinfo in HostInfo.Current.AllEQPInfo)
                {
                    for (int i = 0; i < eqpinfo.Units.Count; i++)
                    {
                        var type = eqpinfo.Units[i].GetType().Name;
                        if (type != "Unit" && type != "Robot")
                        {
                            continue;
                        }

                        if (eqpinfo.Units[i].IsConnect != "Alive")
                            EIPisConnected = false;//有一个设备false 则整线false

                        Equipments equipments = new Equipments();
                        equipments.lineId = eqpinfo.EQPID;
                        equipments.equipmentId = eqpinfo.Units[i].UnitID;
                        equipments.equipmentName = eqpinfo.Units[i].UnitName;
                        equipments.currentStatus = eqpinfo.Units[i].UnitStatus;//设备状态
                        equipments.alive = eqpinfo.Units[i].IsConnect;
                        equipments.cimMode = eqpinfo.Units[i].CIMMode ? "CIMON" : "CIMOFF";
                        eqpOperationMode = "";
                        var eqoprmode = HostInfo.Current.operationmodelist.FirstOrDefault(c => c.eqpid == eqpinfo.EQPID && c.equipmentvalue == eqpinfo.Units[i].UnitMode);
                        if (eqoprmode != null)
                        {
                            eqpOperationMode = eqoprmode.operationmodename;
                        }
                        equipments.eqpOperationMode = eqpOperationMode;
                        equipments.cclinkStatus = eqpinfo.Units[i].IsConnect == "Alive" ? "ON" : "OFF";
                        equipments.equipmentNo = eqpinfo.Units[i].UnitNo;
                        int eqpjobcount = 0;
                        if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].UnitID))))
                        {
                            foreach (var pr in HostInfo.Current.PortList.Where(c => c.GlassInfos.Any(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].UnitID))))
                            {
                                eqpjobcount += pr.GlassInfos.Count(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].UnitID));
                            }
                        }
                        equipments.jobCount = eqpjobcount;//eqpinfo.Units[i].CurrentWIPCount;//JobCount
                        equipments.commandType = HostInfo.Current.GetEQToBCValue(MESEventItem.UnitCommandType, eqpinfo.Units[i].CommandType.ToString());// Consts.dicUnitCommandType.ContainsKey(eqpinfo.Units[i].CommandType) ? Consts.dicUnitCommandType[eqpinfo.Units[i].CommandType] : "";
                        equipments.localAlarmStatus = eqpinfo.Units[i].LoadingStop ? "ON" : "OFF";
                        equipments.currentRecipeIdCheck = eqpinfo.Units[i].CurrentRecipeIdCheck ? "True" : "False";
                        equipments.isProcessEnd = eqpinfo.Units[i].IsProcessEnd ? "True" : "False";
                        equipments.isJobDataRequest = eqpinfo.Units[i].IsJobDataRequest ? "True" : "False";
                        equipments.currentRecipeId = eqpinfo.Units[i].CurrentRecipeID.ToString();
                        equipments.vCRStatus = eqpinfo.Units[i].VCRStatus;

                        equipments.ports = new List<Ports>();
                        var portlist = HostInfo.Current.PortList.Where(t => t.EQPID.Contains(eqpinfo.Units[i].EQPID == null ? "null" : eqpinfo.Units[i].EQPID)).ToList();
                        for (int j = 0; j < portlist.Count(); j++)
                        {
                            Ports portss = new Ports();
                            portss.equipmentNo = portlist[j].UnitID;
                            portss.unitId = portlist[j].UnitID;
                            portss.portId = portlist[j].PortID;
                            portss.portStatus = HostInfo.Current.GetEQToBCValue(MESEventItem.PortStatus, portlist[j].PortStatus.ToString()); //Consts.dicPortStatus.ContainsKey(portlist[j].PortStatus) ? Consts.dicPortStatus[portlist[j].PortStatus] : "";

                            string sPortMode = portlist[j].PortMode.ToString().PadLeft(5, '0');
                            //int iSubstrate_Type = Convert.ToInt32(sPortMode.Substring(0, 1));
                            //int iJob_Type = Convert.ToInt32(sPortMode.Substring(1, 2));
                            //int iJudge_Port_Use_Type = Convert.ToInt32(sPortMode.Substring(3, 2));
                            string sPortMode_Substrate_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Substrate_Type, sPortMode.Substring(0, 1));//Consts.dicPortMode_Substrate_Type.ContainsKey(iSubstrate_Type) ? Consts.dicPortMode_Substrate_Type[iSubstrate_Type] : "";
                            string sPortMode_Job_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Job_Type, sPortMode.Substring(1, 2));//Consts.dicPortMode_Job_Type.ContainsKey(iJob_Type) ? Consts.dicPortMode_Job_Type[iJob_Type] : "";
                            string sPortMode_Judge_Port_Use_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Judge_Port_Use_Type, sPortMode.Substring(3, 2));//Consts.dicPortMode_Judge_Port_Use_Type.ContainsKey(iJudge_Port_Use_Type) ? Consts.dicPortMode_Judge_Port_Use_Type[iJudge_Port_Use_Type] : "";
                            portss.portMode = sPortMode_Substrate_Type + "/" + sPortMode_Job_Type + "/" + sPortMode_Judge_Port_Use_Type;

                            portss.portCSTType = HostInfo.Current.GetEQToBCValue(MESEventItem.PortCassetteType, portlist[j].PortCSTType.ToString());//Consts.dicPortCassetteType.ContainsKey(portlist[j].PortCSTType) ? Consts.dicPortCassetteType[portlist[j].PortCSTType] : "";
                                                                                                                                                    //portss.portOperationMode = portlist[j].PortOperationMode.ToString();
                            portss.portTypeAutoChg = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTypeAutoChangeMode, portlist[j].PortTypeAutoChangeMode.ToString());//Consts.dicPortTypeAutoChangeMode.ContainsKey(portlist[j].PortTypeAutoChangeMode) ? Consts.dicPortTypeAutoChangeMode[portlist[j].PortTypeAutoChangeMode] : "";
                            portss.portQTime = portlist[j].PortQTime;
                            portss.portGrade = portlist[j].PortGrade;
                            portss.portEnableMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortEnableMode, portlist[j].PortEnableMode.ToString());//Consts.dicPortEnableMode.ContainsKey(portlist[j].PortEnableMode) ? Consts.dicPortEnableMode[portlist[j].PortEnableMode] : "";
                            portss.porttype = portlist[j].PortType;
                            portss.transferMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTransferMode, portlist[j].TransferMode);//Consts.dicPortTransferMode.ContainsKey(Convert.ToInt16(portlist[j].TransferMode)) ? Consts.dicPortTransferMode[Convert.ToInt16(portlist[j].TransferMode)] : "";
                            portss.portPauseMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortPauseMode, portlist[j].PortPauseMode.ToString());//Consts.dicPortPauseMode.ContainsKey(portlist[j].PortPauseMode) ? Consts.dicPortPauseMode[portlist[j].PortPauseMode] : "";
                            portss.cassetteSeq = portlist[j].CassetteSequenceNo;
                            portss.cstid = portlist[j].CassetteID;
                            portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                            portss.glassExistence = portlist[j].CassetteInfo.JobExistenceSlot;
                            portss.jobCountIncassette = portlist[j].CassetteInfo.ProductQuantity;//portlist[j].GlassInfos.Count();
                            portss.capacity = portlist[j].Capacity;
                            //portss.partialFullFlag = null;
                            //portss.completedCassetteData = portlist[j].CassetteInfo.CompeletedCassetteData;
                            equipments.ports.Add(portss);
                        }
                        equipments.units = new List<Units>();
                        for (int k = 0; k < eqpinfo.Units[i].SUnitList.Count(); k++)
                        {
                            Units units = new Units();
                            units.unitId = eqpinfo.Units[i].SUnitList[k].UnitID;
                            units.subUnitId = eqpinfo.Units[i].SUnitList[k].SUnitID;
                            units.subUnitName = eqpinfo.Units[i].SUnitList[k].SUnitName;
                            units.subUnitNo = eqpinfo.Units[i].SUnitList[k].SubUnitNo;
                            units.sUnitStatus = eqpinfo.Units[i].SUnitList[k].SUnitStatus;
                            int unitjobcount = 0;
                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].SUnitList[k].SUnitID))))
                            {
                                foreach (var pr in HostInfo.Current.PortList.Where(c => c.GlassInfos.Any(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].SUnitList[k].SUnitID))))
                                {
                                    unitjobcount += pr.GlassInfos.Count(d => d.CurrentSUnit != null && d.CurrentSUnit.Contains(eqpinfo.Units[i].SUnitList[k].SUnitID));
                                }
                            }
                            units.jobCount = unitjobcount;
                            equipments.units.Add(units);
                        }
                        equipmentsList.Add(equipments);
                    }
                }

                #endregion

                #region LinkSignal
                List<linkSignal> linkSignals = new List<linkSignal>();
                foreach (var item in HostInfo.Current.OPILinkList)
                {
                    linkSignal linkSignal = new linkSignal();
                    linkSignal.signalName = item.LinkSignalName;
                    StringBuilder sb = new StringBuilder();
                    foreach (var itemdata in item.UpstreamLinkData)
                    {
                        sb.Append(itemdata.ItemValue == true ? "1" : "0");
                    }
                    linkSignal.upSignalString = sb.ToString();
                    sb = new StringBuilder();
                    foreach (var itemdata in item.DownstreamLinkData)
                    {
                        sb.Append(itemdata.ItemValue == true ? "1" : "0");
                    }
                    linkSignal.downSignalString = sb.ToString();

                    linkSignals.Add(linkSignal);
                }
                #endregion

                bCInforamtionReport = new BCInforamtionReport()
                {
                    serverName = lineInfo.EQPID,
                    clientLine = new ClientLine()
                    {
                        lineType = lineInfo.LineType.ToString(),
                        equipments = equipmentsList,
                        mesControlMode = mesControlMode,
                        lineStatus = lineInfo.EqpStatus,
                        lineId = lineInfo.EQPID,
                        lineOperationMode = lineOperationMode,
                        dispatchMode = lineInfo.RobotDispatchMode.ToString(),
                        linkSignal = linkSignals,
                        EIPisConnected = EIPisConnected
                    }
                };
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ GetOPILineInfo:{0} ,Error:{1} +++", lineInfo.EQPID, ex.ToString());
            }
            return bCInforamtionReport;
        }


        #endregion

        public void PanelProcessEndRequest(Unit oEQP, string JobID, string returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                            System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                            new object[] { oEQP.UnitName, JobID, returnCode, transactionID }));
                #endregion

                if (String.IsNullOrEmpty(JobID))
                {
                    //SendPanelProcessEndRequestReply
                    eqpService.SendPanelProcessEndRequestReply(oEQP.UnitName, JobID, "2", transactionID);
                    return;
                }

                var glass = logicService.GetGlassInfoByCode(oEQP, "", JobID, "", "", 2, "");
                if (glass != null)
                {
                    RVPanelTrackOut paneltrackinout = new RVPanelTrackOut();
                    paneltrackinout.EQUIPMENTID = oEQP.UnitID;
                    paneltrackinout.PANELID = glass.GlassID;
                    paneltrackinout.LOTTYPE = "";
                    paneltrackinout.BONDINGID = "";
                    paneltrackinout.GRADE = glass.GlassGradeCode;
                    paneltrackinout.POSITION = glass.SlotSequenceNo.ToString();
                    paneltrackinout.ABNORMALCODE = glass.AbnormalCodes;
                    var resMes = mesService.SendToMESPanelTrackOutReport(oEQP.EQPID, paneltrackinout, transactionID);

                    var replyeqp = "2";
                    if (resMes != null)
                    {
                        if (resMes.RESULT == MESResult.SUCCESS.ToString())
                        {
                            replyeqp = "1";

                            var portinfo = logicService.GetPortInfoByCode(oEQP, glass.GlassID, glass.CassetteSequenceNo.ToString(), glass.SlotSequenceNo.ToString(), 1);
                            lock (glass)
                            {
                                //记录拨片标记
                                glass.FunctionName = "PanelProcessEnd";
                                glass.SlotFlag = EnumGlassSlotStatus.ProcessEnd;
                                glass.SlotSatus = EnumGlassSlotStatus.ProcessEnd;
                                glass.CurrentUnit = oEQP.UnitID;
                                var ret = dbService.InsertHisGlassInfo(glass);
                            }
                            //缓存数据删除
                            portinfo.GlassInfos.Remove(glass);
                            //删除数据库
                            Hashtable glasstb = new Hashtable();
                            glasstb.Add("ID", glass.ID);
                            dbService.DeleteGlassInfoList(glasstb);
                            glass.FunctionName = "DeleteWIP";
                            dbService.InsertHisGlassInfo(glass);
                            BCLog.Debug($"PanelProcessEndRequest {oEQP.UnitID} GlassID {glass.GlassID} End");
                        }
                    }

                    //SendPanelProcessEndRequestReply
                    eqpService.SendPanelProcessEndRequestReply(oEQP.UnitName, JobID, replyeqp, transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ PanelProcessEndRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }

        public void OperatorLoginReport(Unit oEQP, string OperatorID, string TouchPanelNumber, string ReportOption, string Time, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                            System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                            new object[] { oEQP.UnitName, OperatorID, TouchPanelNumber, Time, transactionID }));
                #endregion

                RVOperatorLogin operatorlogin = new RVOperatorLogin();
                operatorlogin.MACHINEID = oEQP.EQPID;
                operatorlogin.UNITID = oEQP.UnitID;
                operatorlogin.OPERATORID = OperatorID;
                operatorlogin.TOUCHPANELNUMBER = TouchPanelNumber;
                operatorlogin.REPORTOPTION = ReportOption;
                operatorlogin.TIME = Time;
                mesService.SendToMESOperatorLoginReport(oEQP.EQPID, operatorlogin, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ OperatorLoginReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }

        #endregion

        #region
        //BLU预绑定事件
        public void CheckLotBindingRequest(Unit oEQP, string JobID, string BLUID, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                            System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                            new object[] { oEQP.UnitName, JobID, BLUID, transactionID }));
                #endregion

                if (String.IsNullOrEmpty(JobID) || String.IsNullOrEmpty(BLUID))
                {
                    BCLog.ErrorFormat("+++ [EQ=>BC]CheckLotBindingRequest:{0} ,Error:{1} +++", oEQP.UnitName, "JobId Or BLUID is Empty");
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "2", transactionID);
                    return;
                }
                RVCheckLotBindingRequest rVCheckLotBinding = new RVCheckLotBindingRequest();
                rVCheckLotBinding.LOTID = JobID;
                rVCheckLotBinding.BLUID = BLUID;
                var replyHeader = mesService.SendToMESCheckLotBindingRequest(oEQP.EQPID, rVCheckLotBinding, transactionID);
                if (replyHeader == null)
                {
                    BCLog.ErrorFormat("+++ [BC=>MES] CheckLotBindingRequest: ,Error:{0} +++", "MES reply Result info is null");
                    return;
                }
                if (replyHeader.RESULT == MESResult.SUCCESS.ToString())
                {
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "1", " ", transactionID);
                }
                else if (replyHeader.RESULTCODE.Contains("lot_already_binding_blu"))
                {
                    String NGType = "1";//OC_NG
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "2", NGType, transactionID);
                    //NG则发送CIMMessage
                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "9999", "0", replyHeader.RESULTCODE, transactionID);
                }
                else if (replyHeader.RESULTCODE.Contains("blu_already_binding_lot"))
                {
                    String NGType = "2";//BLU_NG
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "2", NGType, transactionID);
                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "9999", "0", replyHeader.RESULTCODE, transactionID);
                }
                else if(replyHeader.RESULTCODE.Contains("blu_and_lot__already_binding"))
                {
                    String NGType = "3";//OC_AND_BLU_NG
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "2", NGType, transactionID);
                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "3", "9999", "0", replyHeader.RESULTCODE, transactionID);
                }
                else
                {
                    String NGType = "4";//OC or BLU not exits
                    eqpService.SendCheckLotBindingRequestReply(oEQP.UnitName, "3", NGType, transactionID);
                    eqpService.SendCIMMessageSetCommand(oEQP.UnitName, "4", "9999", "0", replyHeader.RESULTCODE, transactionID);
                }

            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++ CheckLotBindingRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }

        }
        #endregion
    }
}
