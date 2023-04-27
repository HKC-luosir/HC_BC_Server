using Glorysoft.Auto.Contract;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Logic.Contract;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Glorysoft.BC.Logic.Service
{
    public abstract class AbstractEventHandler
    {
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly IEQPService eqpService = CommonContexts.ResolveInstance<IEQPService>();
        protected static readonly ITibcoRVService mesService = CommonContexts.ResolveInstance<ITibcoRVService>();

        protected static readonly IDbPalletService dbPallet = CommonContexts.ResolveInstance<IDbPalletService>();
        protected static readonly IDbProcessEndService dbProcessEnd = CommonContexts.ResolveInstance<IDbProcessEndService>();
        protected static readonly IDbEqpProfileService dbEqpProfile = CommonContexts.ResolveInstance<IDbEqpProfileService>();
        protected static readonly IDbPanelService dbPanel = CommonContexts.ResolveInstance<IDbPanelService>();
        protected static readonly IDbRecipeService dbRecipe = CommonContexts.ResolveInstance<IDbRecipeService>();
        protected static readonly IDbEquipmentService dbEqp = CommonContexts.ResolveInstance<IDbEquipmentService>();
        protected static readonly IDbAlarmService dbAlarm = CommonContexts.ResolveInstance<IDbAlarmService>();
        protected static readonly IDbPortService dbPort = CommonContexts.ResolveInstance<IDbPortService>();
        protected static readonly IDbMaterialService dbMaterial = CommonContexts.ResolveInstance<IDbMaterialService>();
        protected static readonly IDbRobotService dbRobot = CommonContexts.ResolveInstance<IDbRobotService>();
        protected static readonly IDBDVDataService dbDVData = CommonContexts.ResolveInstance<IDBDVDataService>();
        protected static readonly IDBConfigService dbConfig = CommonContexts.ResolveInstance<IDBConfigService>();
        protected static readonly IDbUserService dbUser = CommonContexts.ResolveInstance<IDbUserService>();
        protected static readonly ITest dbtest = CommonContexts.ResolveInstance<ITest>();
        protected static readonly IWebSocketService webSocketService = CommonContexts.ResolveInstance<IWebSocketService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        protected AbstractEventHandler()
        {
        }

        #region Matti
        public bool IsInLineMode(string eqpid)
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return true;
            else
                return false;
        }
        public string WriteLog(string direct, string functionname, string[] paramnames, object[] paramvalues)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            StringBuilder Logstr = new StringBuilder();
            Logstr.Append(direct + ": ");
            Logstr.Append("[" + functionname + "] ");
            for (int i = 0; i < paramnames.Length; i++)
            {
                var condi = "";
                try
                {
                    condi = js.Serialize(paramvalues[i]);
                }
                catch { }
                Logstr.Append(paramnames[i] + ":" + condi + " ");
            }
            return Logstr.ToString();
        }
        public List<CODE> GetAbnormalCodes(string AbnormalCode)
        {
            List<CODE> code = new List<CODE>();
            try
            {
                if (!String.IsNullOrEmpty(AbnormalCode))
                {
                    var AbnormalCodes = AbnormalCode.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (AbnormalCodes.Length > 0)
                    {
                        foreach (var abs in AbnormalCodes)
                        {
                            var ab = abs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                            if (ab.Length == 1)//glass
                            {
                                code.Add(new CODE() { CODEID = ab[0] });
                            }
                            else if (ab.Length == 2)//panel
                            {
                                code.Add(new CODE() { PANELID = ab[0], CODEID = ab[1] });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ GetAbnormalCodes:{0} ,Error:{1} +++", AbnormalCode, ex.ToString());
            }

            return code;
        }
        public string GetAbnormalFlag(string AbnormalCodes)
        {
            string code = "";
            string abnormalflag1 = "";
            string abnormalflag2 = "";
            string abnormalflag3 = "";
            string abnormalflag4 = "";
            string abnormalflag5 = "";
            string abnormalflag6 = "";
            string abnormalflag7 = "";
            string abnormalflag8 = "";
            List<string> abcode = new List<string>();
            if (!String.IsNullOrEmpty(AbnormalCodes))
            {
                string[] codeids = AbnormalCodes.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (codeids.Length > 0)
                {
                    foreach (var abs in codeids)
                    {
                        var ab = abs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        if (ab.Length == 1)//glass
                        {
                            abcode.Add(ab[0]);
                        }
                        else if (ab.Length == 2)//panel
                        {
                            abcode.Add(ab[1]);
                        }
                    }
                }
            }
            #region flag赋值
            for (int i = 16; i >= 1; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag1 += "1";
                else
                    abnormalflag1 += "0";
            }
            for (int i = 32; i >= 17; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag2 += "1";
                else
                    abnormalflag2 += "0";
            }
            for (int i = 48; i >= 33; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag3 += "1";
                else
                    abnormalflag3 += "0";
            }
            for (int i = 64; i >= 49; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag4 += "1";
                else
                    abnormalflag4 += "0";
            }
            for (int i = 80; i >= 65; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag5 += "1";
                else
                    abnormalflag5 += "0";
            }
            for (int i = 96; i >= 81; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag6 += "1";
                else
                    abnormalflag6 += "0";
            }
            for (int i = 112; i >= 97; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag7 += "1";
                else
                    abnormalflag7 += "0";
            }
            for (int i = 128; i >= 113; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag8 += "1";
                else
                    abnormalflag8 += "0";
            }
            #endregion
            code = abnormalflag1 + ";" + abnormalflag2 + ";" + abnormalflag3 + ";" + abnormalflag4 + ";" + abnormalflag5 + ";" + abnormalflag6 + ";" + abnormalflag7 + ";" + abnormalflag8;
            return code;
        }
        public JobDataInfo SetJobDataInfo(GlassInfo glass)
        {
            JobDataInfo jobdata = new JobDataInfo();
            jobdata.PRODID = glass.ProductID ?? "";
            jobdata.OperID = glass.OperationID ?? "";
            jobdata.JobID = glass.GlassID ?? "";
            jobdata.LotID = glass.LotID ?? "";
            jobdata.PPID1 = glass.PPID1 ?? "0";
            jobdata.PPID2 = glass.PPID2 ?? "0";
            jobdata.PPID3 = glass.PPID3 ?? "0";
            jobdata.PPID4 = glass.PPID4 ?? "0";
            jobdata.PPID5 = glass.PPID5 ?? "0";
            jobdata.PPID6 = glass.PPID6 ?? "0";
            jobdata.PPID7 = glass.PPID7 ?? "0";
            jobdata.PPID8 = glass.PPID8 ?? "0";
            jobdata.PPID9 = glass.PPID9 ?? "0";
            jobdata.PPID10 = glass.PPID10 ?? "0";
            jobdata.PPID11 = glass.PPID11 ?? "0";
            jobdata.PPID12 = glass.PPID12 ?? "0";
            jobdata.PPID13 = glass.PPID13 ?? "0";
            jobdata.PPID14 = glass.PPID14 ?? "0";
            jobdata.PPID15 = glass.PPID15 ?? "0";
            jobdata.PPID16 = glass.PPID16 ?? "0";
            jobdata.PPID17 = glass.PPID17 ?? "0";
            jobdata.PPID18 = glass.PPID18 ?? "0";
            jobdata.PPID19 = glass.PPID19 ?? "0";
            jobdata.PPID20 = glass.PPID20 ?? "0";
            jobdata.PPID21 = glass.PPID21 ?? "0";
            jobdata.PPID22 = glass.PPID22 ?? "0";
            jobdata.PPID23 = glass.PPID23 ?? "0";
            jobdata.PPID24 = glass.PPID24 ?? "0";
            jobdata.PPID25 = glass.PPID25 ?? "0";
            jobdata.PPID26 = glass.PPID26 ?? "0";
            jobdata.PPID27 = glass.PPID27 ?? "0";
            jobdata.PPID28 = glass.PPID28 ?? "0";
            jobdata.PPID29 = glass.PPID29 ?? "0";
            jobdata.PPID30 = glass.PPID30 ?? "0";
            jobdata.JobType = glass.ProductType ?? "0";
            jobdata.LotSequenceNumber = glass.CassetteSequenceNo.ToString();
            jobdata.SlotSequenceNumber = glass.SlotSequenceNo.ToString();
            jobdata.PropertyCode = glass.PropertyCode ?? "0";
            jobdata.JobJudgeCode = glass.GlassJudgeCode ?? "";
            jobdata.JobGradeCode = glass.GlassGradeCode ?? "";
            jobdata.SubstrateType = glass.GlassType;
            //glass.ProcessingFlag TBD
            jobdata.ProcessingFlag1 = "0000000000000000";
            jobdata.ProcessingFlag2 = "0000000000000000";
            jobdata.ProcessingFlag3 = "0000000000000000";
            //glass.SkipFlag TBD
            jobdata.SkipFlag1 = "0000000000000000";
            jobdata.SkipFlag2 = "0000000000000000";
            jobdata.SkipFlag3 = "0000000000000000";
            jobdata.GlassThickness = glass.GlassThicknessCode ?? "0";
            jobdata.JobAngle = glass.JobAngle ?? "0";
            jobdata.JobFlip = glass.JobFlip ?? "0";
            jobdata.MMGCode = glass.MMGCode?? "0000000000000000";
            jobdata.PanelInchSizeX = glass.PanelInchSizeX ?? "0";
            jobdata.PanelInchSizeY = glass.PanelInchSizeY ?? "0";
            string code = GetAbnormalFlag(glass.AbnormalCodes);
            var codes = code.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            jobdata.AbnormalFlag1 = codes[0];
            jobdata.AbnormalFlag2 = codes[1];
            jobdata.AbnormalFlag3 = codes[2];
            jobdata.AbnormalFlag4 = codes[3];
            jobdata.AbnormalFlag5 = codes[4];
            jobdata.AbnormalFlag6 = codes[5];
            jobdata.AbnormalFlag7 = codes[6];
            jobdata.AbnormalFlag8 = codes[7];
            jobdata.WorkOrderID = glass.WorkOrder ?? "";

            return jobdata;
        }
        public string GetMESAbnormalCodes(string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8)
        {
            string MESAbnormalCodes = "";
            if (!String.IsNullOrEmpty(AbnormalFlag1))
            {
                for (int i = 1; i <= AbnormalFlag1.Length; i++)
                {
                    if (AbnormalFlag1[i - 1] == '1')
                        MESAbnormalCodes += i.ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag2))
            {
                for (int i = 1; i <= AbnormalFlag2.Length; i++)
                {
                    if (AbnormalFlag2[i - 1] == '1')
                        MESAbnormalCodes += (i + 16).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag3))
            {
                for (int i = 1; i <= AbnormalFlag3.Length; i++)
                {
                    if (AbnormalFlag3[i - 1] == '1')
                        MESAbnormalCodes += (i + 32).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag4))
            {
                for (int i = 1; i <= AbnormalFlag4.Length; i++)
                {
                    if (AbnormalFlag4[i - 1] == '1')
                        MESAbnormalCodes += (i + 48).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag5))
            {
                for (int i = 1; i <= AbnormalFlag5.Length; i++)
                {
                    if (AbnormalFlag5[i - 1] == '1')
                        MESAbnormalCodes += (i + 64).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag6))
            {
                for (int i = 1; i <= AbnormalFlag6.Length; i++)
                {
                    if (AbnormalFlag6[i - 1] == '1')
                        MESAbnormalCodes += (i + 80).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag7))
            {
                for (int i = 1; i <= AbnormalFlag7.Length; i++)
                {
                    if (AbnormalFlag7[i - 1] == '1')
                        MESAbnormalCodes += (i + 96).ToString() + ";";
                }
            }
            if (!String.IsNullOrEmpty(AbnormalFlag8))
            {
                for (int i = 1; i <= AbnormalFlag8.Length; i++)
                {
                    if (AbnormalFlag8[i - 1] == '1')
                        MESAbnormalCodes += (i + 112).ToString() + ";";
                }
            }
            MESAbnormalCodes = MESAbnormalCodes.TrimEnd(';');
            return MESAbnormalCodes;
        }

        public void SetJobPPID(ref GlassInfo glass, List<UNITRECIPE> PPIDs)
        {
            if (PPIDs != null && PPIDs.Count > 0)
            {
                for (int i = 0; i < PPIDs.Count; i++)
                {
                    var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitID == PPIDs[i].UNITID)).Units.FirstOrDefault(d => d.UnitID == PPIDs[i].UNITID);
                    var propName = "PPID" + (oEQP.LocalNo - 1).ToString();
                    if (glass.GetType().GetProperty(propName) != null)
                    {
                        glass.GetType().GetProperty(propName).SetValue(glass, PPIDs[i].UNITRECIPENAME);
                    }
                }
            }
        }
                
        public void StartT9TimeOut(Unit oEQPdata, PortInfo portdata, string transactionID = "")
        {
            try
            {
                StopT9TimeOut(oEQPdata.UnitID, portdata.PortID, "StartT9TimeOut");
                if (HostInfo.Current.SystemSetting.Any(c => c.bckey == "T9TimeOut"))
                {
                    var T9TimeOut = HostInfo.Current.SystemSetting.FirstOrDefault(c => c.bckey == "T9TimeOut").bcvalue;
                    int iT9TimeOut = 0;
                    int.TryParse(T9TimeOut, out iT9TimeOut);
                    if (iT9TimeOut > 0)
                    {
                        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                        Task taskT9 = Task.Factory.StartNew(delegate
                        {
                            int iTimeOut = iT9TimeOut;
                            Unit oEQP = oEQPdata;
                            PortInfo portinfo = portdata;
                            while (iTimeOut > 0 && !cancelTokenSource.IsCancellationRequested)
                            {
                                if (iTimeOut > 0)
                                {
                                    Thread.Sleep(1000);
                                    iTimeOut--;
                                }
                                if (iTimeOut == 0)
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = "BC T9 Time Out";
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.GlassInfos.Count.ToString(), transactionID);
                                }
                            }
                        }, cancelTokenSource);
                        if (HostInfo.Current.AllT9Task.ContainsKey(oEQPdata.UnitID + "-" + portdata.PortID))
                        {
                            HostInfo.Current.AllT9Task[oEQPdata.UnitID + "-" + portdata.PortID] = taskT9;
                        }
                        else
                        {
                            HostInfo.Current.AllT9Task.TryAdd(oEQPdata.UnitID + "-" + portdata.PortID, taskT9);
                        }
                        if (HostInfo.Current.AllT9CancelToken.ContainsKey(oEQPdata.UnitID + "-" + portdata.PortID))
                        {
                            HostInfo.Current.AllT9CancelToken[oEQPdata.UnitID + "-" + portdata.PortID] = cancelTokenSource;
                        }
                        else
                        {
                            HostInfo.Current.AllT9CancelToken.TryAdd(oEQPdata.UnitID + "-" + portdata.PortID, cancelTokenSource);
                        }
                        //taskT9.Start();
                        LogHelper.BCLog.Debug($"Unit:{oEQPdata.UnitID} PortID:{portdata.PortID} Start T9 Timer");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ StartT9TimeOut:{0} ,Error:{1} +++", oEQPdata.UnitID, ex.ToString());
            }
        }
        public void StopT9TimeOut(string UnitID, string PortID, string Action)
        {
            try
            {
                if (HostInfo.Current.AllT9Task.ContainsKey(UnitID + "-" + PortID))
                {
                    if (HostInfo.Current.AllT9Task[UnitID + "-" + PortID] != null)
                    {
                        var task = HostInfo.Current.AllT9Task[UnitID + "-" + PortID];
                        var taskcancel = HostInfo.Current.AllT9CancelToken[UnitID + "-" + PortID];
                        if (!task.IsCanceled && !task.IsCompleted && !task.IsFaulted)
                        {
                            taskcancel.Cancel();
                            Thread.Sleep(1500);
                            task.Dispose();
                            LogHelper.BCLog.Debug($"Unit:{UnitID} PortID:{PortID} {Action} Stop T9 Timer");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ StopT9TimeOut:{0} ,Error:{1} +++", UnitID, ex.ToString());
            }
        }

        /// <summary>
        /// 按卡夹清除数据
        /// </summary>
        /// <param name="portinfo"></param>
        /// <param name="Reason"></param>
        public void ClearPortGlassData(PortInfo portinfo, string Reason, int SlotSatus = 0)
        {
            var oldCassetteID = portinfo.CassetteID;//待删除的cst

            if (!String.IsNullOrEmpty(oldCassetteID))
            {
                var delglass = portinfo.GlassInfos.Where(c => c.CassetteID == oldCassetteID);
                if (delglass != null && delglass.Count() > 0 && SlotSatus>0)//过滤SlotSatus
                {
                    var enumSlotSatus = (EnumGlassSlotStatus)Enum.Parse(typeof(EnumGlassSlotStatus), SlotSatus.ToString(), true);
                    delglass = delglass.Where(c => c.SlotSatus == enumSlotSatus);
                }
                if (delglass != null && delglass.Count() > 0)
                {
                    var dellist = delglass.ToList();
                    for (int i = dellist.Count - 1; i >= 0; i--)
                    {
                        if (!String.IsNullOrEmpty(Reason))
                        {
                            dellist[i].FunctionName = Reason;
                            dbService.InsertHisGlassInfo(dellist[i]);
                            dellist[i].FunctionName = "DeleteWIP";
                            dbService.InsertHisGlassInfo(dellist[i]);
                        }
                        portinfo.GlassInfos.Remove(dellist[i]);
                    }
                }

                //删除wip_cassette
                Hashtable csttb = new Hashtable();
                csttb.Add("PortID", portinfo.PortID);
                csttb.Add("CassetteID", oldCassetteID);
                dbService.DeleteCassetteList(csttb);
                //删除wip_glass
                Hashtable glasstb = new Hashtable();
                glasstb.Add("CassetteID", oldCassetteID);
                if(SlotSatus>0)
                    glasstb.Add("SlotSatus", SlotSatus);
                dbService.DeleteGlassInfoList(glasstb);
            }
            portinfo.CassetteID = "";
            portinfo.CassetteInfo = new Cassette();
            dbService.UpdatePortInfo(portinfo);
        }
        #endregion

        protected string GetItemValue(Dictionary<string, object> dict, string itemName)
        {
            try
            {
                if (dict.ContainsKey(itemName))
                {
                    var a = dict[itemName].ToString().Trim();
                    return a;
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

            return "";
        }
        //protected static Dictionary<int, string> dicPortType = new Dictionary<int, string>
        //{
        //    {1,"PL" },
        //    {2,"PU" },
        //    {3,"PB" }
        //};

        protected Dictionary<string, string> dicPanelProcessStart = new Dictionary<string, string>
        {
            {"POLCleaner","POLCleaner" },
            {"CleanerCV","CleanerCV" },
            {"OLBCleaner","OLBCleaner" },
            {"ASSY","ASSY" },
            {"OEMBOX","OEMBOX" },
            {"OEMCST","OEMCST" }
        };

        protected Dictionary<string, string> dicPanelProcessEnd = new Dictionary<string, string>
        {
            {"AOI","TCPBonding" },
            {"Dispenser","AOI" },
        };
    }
}
