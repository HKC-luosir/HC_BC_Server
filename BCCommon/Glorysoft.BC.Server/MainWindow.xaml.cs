using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Framework;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using System.Configuration;
using Glorysoft.Auto.Contract.SECS;
using System.Threading;
using Glorysoft.BC.Logic.Contract;
using System.Threading.Tasks;
using System.Collections;
using Glorysoft.BC.Server.ViewModel;
using System.Windows.Input;
using Glorysoft.BC.RV.Common;
using GlorySoft.BC.WebSocket;
using Glorysoft.BC.Entity.Configuration;
using Glorysoft.BC.Server.Infrastructure;
using Glorysoft.BC.Entity.WebSocketEntity;
using System.Windows.Threading;
using System.Text;
using System.Reflection;
using Glorysoft.BC.EIP;
using System.Xml;
using System.IO;
using Glorysoft.BC.SECS.Contract;

namespace Glorysoft.BC.Server
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly object syncConv = new object();
        private ISECSContext context;
        protected static ILogicService logicService;
        protected static IDBService dbService;
        protected static IEQPService eqpService;
        protected static ITibcoRVService rvService;
        protected static IWebSocketService webSocketService;
        protected static IRobotService robotService;
        //protected static IFDCService fdcService;
        //protected static IRobotService robotService;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private List<KeyValuePair<string, IConnectionContext>> plcs;
        protected string _text = "";
        DispatcherTimer timer;//OPI定时推送
        public Dictionary<string, Glorysoft.EIPDriver.Block> DictEIPBCAlives = new Dictionary<string, Glorysoft.EIPDriver.Block>();
        //public static Thread th;//右边栏刷新
        // private IDBService dBService = CommonContexts.ResolveInstance<IDBService>();
        public MainWindow()
        {
            try
            {
                LogHelper.BCLog.Debug("MainWindow");
                ConnectionContexts = new List<IConnectionContext>();
                Startup applicationStartup = new Startup();
                applicationStartup.Initialization();
                var wConfirm = new ConfirmWin { lblTitle = { Content = "LC Exit Confirm!" } };

                appContext = CommonContexts.ApplicationContext;
                logicService = CommonContexts.ResolveInstance<ILogicService>();
                webSocketService = CommonContexts.ResolveInstance<IWebSocketService>();
                dbService = CommonContexts.ResolveInstance<IDBService>();
                eqpService = CommonContexts.ResolveInstance<IEQPService>();
                rvService = CommonContexts.ResolveInstance<ITibcoRVService>();
                robotService = CommonContexts.ResolveInstance<IRobotService>();


                HostInfo.Current.EQPID = ConfigurationManager.AppSettings["EQPID"];

                //设备初始化数据
                HostInfo.Current.PortPanelInforSaveFunction = PortPanelInforSave;
                HostInfo.Current.PortPanelInforUpdateFunction = PortPanelInforUpdate;
                HostInfo.Current.PortPanelInforAddFunction = PortPanelInforAdd;



                // HostInfo.Current.PortPanelInforRemoveFunction = PortPanelInforRemove;

                HostInfo.Current.SaveUnitTraceListInfoFunction = SaveUnitTraceListInfo;
                //HostInfo.Current.RemoveCurrentPanelInfoListFunction = RemoveCurrentPanelInfoList;
                //// HostInfo.Current.RemoveOldCurrentPanelInfoListFunction = RemoveOldCurrentPanelInfoList;
                //HostInfo.Current.UpdateCurrentPanelInforFunction = UpdateCurrentPanelInfor;

                //HostInfo.Current.ADDCurrentTraceListInfoFunction = ADDCurrentTraceListInfo;
                //HostInfo.Current.RemoveCurrentTraceListInfoFunction = RemoveCurrentTraceListInfo;
                //HostInfo.Current.RemoveOldCurrentTraceListInfoFunction = RemoveOldCurrentTraceListInfo;
                HostInfo.Current.RVMappingConfig = XmlSerializeManager.Deserialize<RVMappingConfig>(Consts.RVMappingConfig);
                HostInfo.Current.SystemConfig = XmlSerializeManager.Deserialize<SystemConfig>(Consts.SystemConfig); //dbService.ViewSystemConfig(HostInfo.Current.EQPID);
                
                HostInfo.Current.MESRule = XmlSerializeManager.Deserialize<MESRule>(Consts.MESRule); //dbService.ViewSystemConfig(HostInfo.Current.EQPID);  
                HostInfo.Current.EQRule = XmlSerializeManager.Deserialize<EQRule>(Consts.EQRule);
                HostInfo.Current.SECSRule = XmlSerializeManager.Deserialize<SECSRule>(Consts.SECSRule);
                HostInfo.Current.LinkSignal = XmlSerializeManager.Deserialize<LinkSignal>(Consts.LinkSignal);

                HostInfo.Current.S9F13AddFunction = S9F13Add;
                HostInfo.Current.S9F13RemoveFunction = S9F13Remove;

                //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.CF)
                //{
                //    dbService.DeleteGlassInforByDateTime();
                //    dbService.DeleteCassetteByDateTime();
                //}

                HostInfo.Current.SystemSetting = dbService.Viewbc_sys_setting(new Hashtable() { }).ToList();
                var eqpids = HostInfo.Current.EQPID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                #region DVData
                foreach (var eqpid in eqpids)
                {
                    Hashtable dvdataHT = new Hashtable();
                    dvdataHT.Add("EQPID", eqpid);
                    var dvdata = dbService.ViewDVDataList(dvdataHT).ToList();
                    if (dvdata.Count > 0)
                    {
                        var dvgroup = dvdata.Select(c => c.UNITID).Distinct();
                        foreach (var dg in dvgroup)
                        {
                            HostInfo.Current.DVDataList.TryAdd(dg, dvdata.Where(c => c.UNITID == dg).ToList());
                        }
                    }
                }
                #endregion
                #region SVData
                foreach (var eqpid in eqpids)
                {
                    Hashtable svdataHT = new Hashtable();
                    svdataHT.Add("EQPID", eqpid);
                    var svdata = dbService.ViewSVDataList(svdataHT).ToList();
                    if (svdata.Count > 0)
                    {
                        var svgroup = svdata.Select(c => c.UNITID).Distinct();
                        foreach (var sg in svgroup)
                        {
                            HostInfo.Current.SVDataList.TryAdd(sg, svdata.Where(c => c.UNITID == sg).ToList());
                        }
                    }
                }
                #endregion
                #region RecipeParameter
                foreach (var eqpid in eqpids)
                {
                    RecipeParameter rcpSel = new RecipeParameter();
                    rcpSel.EQPID = eqpid;
                    var rcpdata = dbService.GetRecipeParameterList(rcpSel).ToList();
                    if (rcpdata.Count > 0)
                    {
                        var rcpgroup = rcpdata.Select(c => c.UnitID).Distinct();
                        foreach (var rg in rcpgroup)
                        {
                            HostInfo.Current.RecipeParameterList.TryAdd(rg, rcpdata.Where(c => c.UnitID == rg).ToList());
                        }
                    }
                }
                #endregion
                #region PortGradeGroup
                foreach (var eqpid in eqpids)
                {
                    Hashtable PortGradeGroupHT = new Hashtable();
                    PortGradeGroupHT.Add("eqpid", eqpid);
                    PortGradeGroupHT.Add("enabled", 0);
                    var PortGradeGroup = dbService.Viewcfg_portgradegroup(PortGradeGroupHT).ToList();
                    if (PortGradeGroup.Count > 0)
                    {
                        HostInfo.Current.PortGradeGroupList.TryAdd(eqpid, PortGradeGroup);
                    }
                }
                #endregion
                #region RVMessageList
                string filename = AppDomain.CurrentDomain.BaseDirectory + "Configuration\\RVMessageList.xml";
                if (File.Exists(filename))
                {
                    XmlDocument xmldocu = new XmlDocument();
                    xmldocu.Load(filename);
                    XmlNode pXmlNode = xmldocu.DocumentElement;
                    foreach (XmlNode p in pXmlNode)
                    {
                        HostInfo.Current.RVMessageList.Add(p.Name, p.InnerText);
                    }
                }
                #endregion


                ObjectInitialize();
                InitLingSignal();
                InitRobotDispath();//派工加载
                InitConnections();

                InitializeComponent();
                InitNotifyIcon();

                CreateBCAlive();
                InitScheduler();//初始化全局调度器

                //BC重启读取Robot执行结果以及状态
                //var BC100 = appContext.GetContext(ConfigurationManager.AppSettings["EQPID"]);
                //InitPLCData(BC100 as IPLCContext);

                //PlcMonitor();
                //eqpService.HostAlive();
                //robotService.Start();
                //Thread covThread = new Thread(ThreadHandleConversation);
                //covThread.Start();

                //Thread ReadWipCountThread = new Thread(ReadWipCount);

                this.DataContext = new MainWindowViewModel();
                //HostInfo.Current.IsHostOpen = true;
                HostInfo.Current.OPCallListAddFun = OPCallListAdd;
                //Thread clearInformationThread = new Thread(ClearInformation);
                //clearInformationThread.Start();
                var name = Assembly.GetEntryAssembly().GetName();
                var ver = name.Version;
                _text = string.Format("进入系统成功，设备ID：{0};Version:{1}", ConfigurationManager.AppSettings["EQPID"], ver);
                this.txtLineName.AppendText(_text);

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(Convert.ToInt32(ConfigurationManager.AppSettings["OPIRefreshRate"]));
                timer.Tick += timer1_Tick;
                timer.Start();

                HostInfo.Current.RemainedGlassFlagModifyFunction = RemainedGlassFlagModify;

                var portList = HostInfo.Current.GetProcessPortList();
                if (portList.Count() > 0)
                {
                    LogHelper.BCLog.Debug(string.Format("[MainWindow Modify;]   RemainedGlassFlag = true;"));
                    HostInfo.Current.EQPInfo.RemainedGlassFlag = true;
                }
                else
                {
                    LogHelper.BCLog.Debug(string.Format("[MainWindow Modify;]   RemainedGlassFlag = false;"));
                    HostInfo.Current.EQPInfo.RemainedGlassFlag = false;
                }
                HostInfo.Current.RemainedGlassFlagModifyFunction = RemainedGlassFlagModify;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
                Environment.Exit(0);
            }

        }

        /// <summary>
        /// 初始化全局调度器
        /// </summary>
        public void InitScheduler()
        {
            try
            {                                
                //Scheduler.Add("LoaderTrayGroupIDTimeOut", 60, ScheduleOptions.Loop, new Action<object>(obj => LoaderTrayGroupIDTimeOut()), null);

                Scheduler.Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine($"InitScheduler Error:{e.Message}");
            }
        }
        //private void UpdateGlassSlotSatus(int CassetteSequenceNo, int[] SlotSequenceNos, int SlotSatus, Hashtable hashtable)
        //{
        //    hashtable.Add("SlotSatus", SlotSatus);
        //    hashtable.Add("CassetteSequenceNo", CassetteSequenceNo);
        //    for (int i = 0; i < 26; i++)
        //    {                
        //       // hashtable.Add(string.Format("CSNo{0}", (i + 1)), CassetteSequenceNos[i]);
        //        hashtable.Add(string.Format("SSNo{0}", (i + 1)), SlotSequenceNos[i]);
        //    }
        //    dbService.UpdateGlassSlotSatus(hashtable);
        //}

        //private void FormatWipDataInfo(List<PortWIPDataInfo> WIPDataList, int[] SlotSequenceNos)
        //{
        //    for (int i = 0; i < WIPDataList.Count; i++)
        //    {
        //        //CassetteSequenceNos[i] = WIPDataList[i].CassetteSequenceNo;
        //        SlotSequenceNos[i] = WIPDataList[i].SlotSequenceNo;
        //    }
        //}
        private void timer1_Tick(object sender, EventArgs e)
        {
            //定时执行的内容
            LineInformationPush();
        }

        public static void LineInformationPush()
        {
            try
            {
                if (!HostInfo.Current.EQPInfo.OPIConnect)
                    return;

                foreach (var currentinfo in HostInfo.Current.AllEQPInfo)
                {
                    var bCInforamtionReport = logicService.GetOPILineInfo(currentinfo);
                    SendOPIMessage.SendBCInforamtionReport(bCInforamtionReport);//WebSocket发送
                }
            }
            catch (Exception ex)
            {
                LogHelper.WebSocketLog.Error(ex);
            }
        }
        //private void ThFun()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            if (HostInfo.Current.isSendOPIInforamtion)
        //            {
        //                SendOPIInforamtion();
        //            }
        //            Thread.Sleep(Convert.ToInt32(ConfigurationManager.AppSettings["OPIRefreshRate"]));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WebSocketLog.Error(ex);
        //    }
        //}
        //public void SendOPIInforamtion()
        //{
        //    try
        //    {
        //        if (HostInfo.Current.LineMode != null)
        //        {
        //            #region Line状态、MES状态推送
        //            string mesControlMode = "", lineOperationMode = "";
        //            int lineOperationModeCode = 0;
        //            switch (HostInfo.Current.EQPInfo.ControlState)
        //            {
        //                case EnumControlState.OffLine:
        //                    mesControlMode = "OFFLINE";
        //                    break;
        //                case EnumControlState.OnLineLocal:
        //                    mesControlMode = "LOCAL";
        //                    break;
        //                case EnumControlState.OnLineRemote:
        //                    mesControlMode = "REMOTE";
        //                    break;
        //                default:
        //                    break;
        //            }
        //            Hashtable hashtable = new Hashtable();
        //            hashtable.Add("equipmentvalue", (int)HostInfo.Current.EQPInfo.LineMode);
        //            var operationmode = dbService.Viewcfg_operationmode(hashtable).FirstOrDefault();
        //            if (operationmode != null)
        //            {
        //                lineOperationMode = operationmode.operationmodename;
        //                lineOperationModeCode = operationmode.equipmentvalue;
        //            }
        //            Equipments equipments = new Equipments();
        //            for (int i = 0; i < HostInfo.Current.EQPInfo.Units.Count; i++)
        //            {
        //                var type = HostInfo.Current.EQPInfo.Units[i].GetType().Name;
        //                if (type != "Unit" && type != "Robot")
        //                {
        //                    continue;
        //                }
        //                if (HostInfo.Current.EQPInfo.Units[i].UnitNo != HostInfo.Current.LineMode.equipmentNo)
        //                {
        //                    continue;
        //                }
        //                string cimMode = "", cclinkStatus = "";
        //                #region 相关转换处理处理
        //                if (HostInfo.Current.EQPInfo.Units[i].CIMMode)
        //                {
        //                    cimMode = "CIMON";
        //                }
        //                else
        //                {
        //                    cimMode = "CIMOFF";
        //                }

        //                if (HostInfo.Current.EQPInfo.Units[i].IsConnect == "Alive")
        //                {
        //                    cclinkStatus = "ON";
        //                }
        //                else
        //                {
        //                    cclinkStatus = "OFF";
        //                }
        //                if (HostInfo.Current.EQPInfo.Units[i].LoadingStop)
        //                {
        //                    equipments.localAlarmStatus = "ON";
        //                }
        //                else
        //                {
        //                    equipments.localAlarmStatus = "OFF";
        //                }
        //                #endregion
        //                string UnitStatus = HostInfo.Current.EQPInfo.Units[i].UnitStatus;
        //                equipments.currentStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, UnitStatus.ToString());//设备状态
        //                equipments.reportMode = "PLC";
        //                equipments.jobCount = HostInfo.Current.EQPInfo.Units[i].CurrentWIPCount;//JobCount
        //                equipments.alive = HostInfo.Current.EQPInfo.Units[i].IsConnect;
        //                equipments.cimMode = cimMode;
        //                equipments.cclinkStatus = cclinkStatus;
        //                equipments.equipmentId = HostInfo.Current.EQPInfo.Units[i].UnitID;
        //                equipments.equipmentNo = HostInfo.Current.EQPInfo.Units[i].UnitNo;
        //                equipments.eqpOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, HostInfo.Current.EQPInfo.Units[i].UnitMode.ToString());
        //                equipments.equipmentName = HostInfo.Current.EQPInfo.Units[i].UnitName;
        //                equipments.stcode = HostInfo.Current.EQPInfo.Units[i].UnitSTCode;
        //                equipments.recipeIdCheck = HostInfo.Current.EQPInfo.Units[i].CurrentRecipeID.ToString();

        //                equipments.indexerOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, HostInfo.Current.EQPInfo.LineMode.ToString());
        //                equipments.autoRecipeChangeMode = "";
        //                string UpstreamInlineMode = "";
        //                if (HostInfo.Current.EQPInfo.Units[i].UpstreamInlineMode)
        //                {
        //                    UpstreamInlineMode = "ON";
        //                }
        //                else
        //                {
        //                    UpstreamInlineMode = "OFF";
        //                }
        //                equipments.upstreamInlineMode = UpstreamInlineMode;
        //                string DownstreamInlineMode = "";
        //                if (HostInfo.Current.EQPInfo.Units[i].DownstreamInlineMode)
        //                {
        //                    DownstreamInlineMode = "ON";
        //                }
        //                else
        //                {
        //                    DownstreamInlineMode = "OFF";
        //                }
        //                equipments.downstreamInlineMode = DownstreamInlineMode;
        //                string VCREnableMode = "";
        //                Hashtable map = new Hashtable
        //            {
        //                {"EQPID",HostInfo.Current.EQPInfo.EQPID },
        //                {"UnitID",HostInfo.Current.EQPInfo.Units[i].UnitID },
        //            };
        //                var vcr = dbService.ViewVCRList(map).FirstOrDefault();
        //                if (vcr != null)
        //                {
        //                    switch (vcr.VCREnableMode)
        //                    {
        //                        case 1:
        //                            VCREnableMode = "Enable";
        //                            break;
        //                        case 2:
        //                            VCREnableMode = "Disable";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                equipments.vcrEnableMode = VCREnableMode;
        //                string CommandType = "";
        //                switch (HostInfo.Current.EQPInfo.Units[i].CommandType)
        //                {
        //                    case 1:
        //                        CommandType = "CCLink";
        //                        break;
        //                    case 2:
        //                        CommandType = "HSMS";
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                equipments.commandType = CommandType;
        //                equipments.loaderQTime = HostInfo.Current.EQPInfo.Units[i].PortQTime;
        //                equipments.ports = new List<Ports>();
        //                var portlist = HostInfo.Current.PortList.Where(t => t.EQPID.Contains(HostInfo.Current.EQPInfo.Units[i].EQPID == null ? "null" : HostInfo.Current.EQPInfo.Units[i].EQPID)).ToList();
        //                for (int j = 0; j < portlist.Count(); j++)
        //                {
        //                    Ports portss = new Ports();
        //                    portss.equipmentNo = portlist[j].UnitID;
        //                    portss.unitId = portlist[j].UnitID;
        //                    portss.portId = portlist[j].PortID;
        //                    portss.cassetteSeq = portlist[j].CassetteSequenceNo;
        //                    portss.cstid = portlist[j].CassetteID;
        //                    string PortStatus = "";
        //                    switch (portlist[j].PortStatus)
        //                    {
        //                        case 1:
        //                            PortStatus = "LOADREADY";
        //                            break;
        //                        case 2:
        //                            PortStatus = "INUSE";
        //                            break;
        //                        case 3:
        //                            PortStatus = "UNLOADREADY";
        //                            break;
        //                        case 4:
        //                            PortStatus = "EMPTY";
        //                            break;
        //                        case 5:
        //                            PortStatus = "BLOCKED";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    portss.portStatus = PortStatus;
        //                    portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
        //                    string PortType = portlist[j].PortType;
        //                    portss.porttype = PortType;
        //                    portss.portMode = portlist[j].PortMode.ToString();
        //                    string portTypeAutoChg = "";
        //                    switch (portlist[j].PortTypeAutoChangeMode)
        //                    {
        //                        case 1:
        //                            portTypeAutoChg = "Enable";
        //                            break;
        //                        case 2:
        //                            portTypeAutoChg = "Disabled";
        //                            break;
        //                        default:
        //                            portTypeAutoChg = portlist[j].PortTypeAutoChangeMode.ToString();
        //                            break;
        //                    }
        //                    portss.portTypeAutoChg = portTypeAutoChg;
        //                    portss.capacity = portlist[j].Capacity;
        //                    string TransferMode = portlist[j].TransferMode;
        //                    portss.transferMode = TransferMode;
        //                    string PortOperationMode = portlist[j].PortOperationMode.ToString();
        //                    portss.portOperationMode = PortOperationMode;
        //                    string PortCSTType = "";
        //                    switch (portlist[j].PortCSTType)
        //                    {
        //                        case 1:
        //                            PortCSTType = "1AC";
        //                            break;
        //                        case 2:
        //                            PortCSTType = "1EC";
        //                            break;
        //                        case 3:
        //                            PortCSTType = "1EF";
        //                            break;
        //                        case 4:
        //                            PortCSTType = "1EM";
        //                            break;
        //                        case 5:
        //                            PortCSTType = "2AC";
        //                            break;
        //                        case 6:
        //                            PortCSTType = "2EC";
        //                            break;
        //                        case 7:
        //                            PortCSTType = "2EF";
        //                            break;
        //                        case 8:
        //                            PortCSTType = "2EM";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                    portss.portCSTType = PortCSTType;
        //                    portss.partialFullFlag = null;
        //                    portss.glassExistence = portlist[j].CassetteInfo.JobExistenceSlot;
        //                    portss.jobCountIncassette = portlist[j].CassetteInfo.ProductQuantity;//portlist[j].GlassInfos.Count();
        //                    portss.completedCassetteData = portlist[j].CassetteInfo.CompeletedCassetteData;
        //                    equipments.ports.Add(portss);
        //                }
        //                equipments.units = new List<Units>();
        //                for (int k = 0; k < HostInfo.Current.EQPInfo.Units[i].SUnitList.Count(); k++)
        //                {
        //                    Units units = new Units();
        //                    units.unitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].UnitID;
        //                    units.subUnitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitID;
        //                    units.subUnitName = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitName;
        //                    units.subUnitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
        //                    units.sUnitStatus = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;

        //                    units.sUnitSTCode = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitSTCode;

        //                    units.unitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
        //                    units.currentStauts = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;
        //                    equipments.units.Add(units);
        //                }
        //            }

        //            List<Equipments> EquipmentsList = new List<Equipments>();
        //            EquipmentsList.Add(equipments);
        //            AllData allData = new AllData()
        //            {
        //                serverName = HostInfo.Current.EQPInfo.EQPID,
        //                line = new Line()
        //                {
        //                    lineType = HostInfo.Current.EQPInfo.LineType.ToString(),
        //                    equipments = EquipmentsList,
        //                    mesControlMode = mesControlMode,
        //                    lineStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, HostInfo.Current.EQPInfo.EqpStatus),
        //                    lineId = HostInfo.Current.EQPInfo.EQPID,
        //                    lineOperationMode = lineOperationMode,
        //                    lineOperationModeCode = lineOperationModeCode,
        //                    dispatchMode = HostInfo.Current.EQPInfo.RobotDispatchMode.ToString(),
        //                    coldRunTotalQuantity = HostInfo.Current.EQPInfo.ColdRunTotalQuantity,
        //                    coldRunCurrentQuantity = HostInfo.Current.EQPInfo.ColdRunCurrentQuantity
        //                }
        //            };
        //            #endregion
        //            SendOPIMessage.SendEquipmentInforamtionReport(allData);//WebSocket发送
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WebSocketLog.Error(ex);
        //    }
        //}
        #region SV TraceData
        /// <summary>
        /// 查看UnitTraceListInfoDic 对应的unit信息里是否存在相应的trid数据,
        /// 如果不存在,添加新的trid,并且启动相对应的新的线程
        /// 如果存在,修改trid数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="trid"></param>
        /// <param name="CurrentTraceListInfo"></param>
        private void SaveUnitTraceListInfo(string unitid, string trid, UnitTraceListInfo UnitTraceListInfo)
        {
            var TraceListInfo = HostInfo.Current.UnitTraceListInfoDic[unitid];
            if (TraceListInfo.ContainsKey(trid))
            {
                TraceListInfo[trid].UnitID = UnitTraceListInfo.UnitID;
                TraceListInfo[trid].TRID = UnitTraceListInfo.TRID;
                TraceListInfo[trid].CommandType = UnitTraceListInfo.CommandType;
                TraceListInfo[trid].SVIDList = UnitTraceListInfo.SVIDList;
                //TraceListInfo[trid].DateTime = CurrentTraceListInfo.DateTime;
                //TraceListInfo[trid].ParameterList = CurrentTraceListInfo.ParameterList;
                TraceListInfo[trid].DSPER = UnitTraceListInfo.DSPER;
                TraceListInfo[trid].TOTSMP = UnitTraceListInfo.TOTSMP;
            }
            else
            {
                TraceListInfo.Add(trid, UnitTraceListInfo);
                if (UnitTraceListInfo.CommandType == 1)
                {
                    ParameterizedThreadStart parStart = new ParameterizedThreadStart(CurrentSVDataFunction);
                    //Thread CurrentSVDataThread = new Thread(parStart);
                    //object obj = TraceListInfo[trid];
                    //CurrentSVDataThread.Start(obj);
                }
            }
        }
        private void CurrentSVDataFunction(object obj)
        {
            UnitTraceListInfo currentSVData = (UnitTraceListInfo)obj;
            while (currentSVData.TOTSMP != 0)
            {
                try
                {
                    if (currentSVData.TOTSMP == -1)
                    {
                        //获取parameterList
                        var SVData = HostInfo.Current.CCLinkSVDataList.FirstOrDefault(o => o.UnitID == currentSVData.UnitID);
                        if (SVData != null)
                        {
                            List<Parameter> uploadParameter = new List<Parameter>();
                            foreach (var parameter in SVData.ParameterList)
                            {
                                if (currentSVData.SVIDList.Contains(parameter.ParameterName))
                                {
                                    uploadParameter.Add(parameter);
                                }
                            }
                            // logicService.TraceDataFDC(currentSVData.UnitID, "", "", SVData.DateTime, uploadParameter);
                        }
                        //logicService.TraceListReplyFDC(currentSVData.UnitID, "", "");
                        int time = currentSVData.DSPER * 1000;
                        if (time == 0)
                        {
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Thread.Sleep(time);
                        }
                    }
                    else
                    {
                        if (currentSVData.TOTSMP > 0)
                        {
                            //logicService.TraceDataFDC(currentSVData.UnitID, "", "", currentSVData.DateTime, currentSVData.ParameterList);
                            var SVData = HostInfo.Current.CCLinkSVDataList.FirstOrDefault(o => o.UnitID == currentSVData.UnitID);
                            if (SVData != null)
                            {
                                List<Parameter> uploadParameter = new List<Parameter>();
                                foreach (var parameter in SVData.ParameterList)
                                {
                                    if (currentSVData.SVIDList.Contains(parameter.ParameterName))
                                    {
                                        uploadParameter.Add(parameter);
                                    }
                                }
                                // logicService.TraceDataFDC(currentSVData.UnitID, "", "", SVData.DateTime, uploadParameter);
                            }
                            //logicService.TraceListReplyFDC(currentSVData.UnitID, "", "");
                            currentSVData.TOTSMP = currentSVData.TOTSMP - 1;
                        }
                        int time = currentSVData.DSPER * 1000;
                        if (time == 0)
                        {
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Thread.Sleep(time);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.Error(ex);
                }
            }
        }
        #endregion
        //private void CurrentSVDataFunction(object obj)
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            CurrentSVData currentSVData = (CurrentSVData)obj;
        //            if (currentSVData.TOTSMP == -1)
        //            {
        //                logicService.TraceDataFDC(currentSVData.UnitID, "", "", currentSVData.DateTime, currentSVData.ParameterList);
        //                //logicService.TraceListReplyFDC(currentSVData.UnitID, "", "");

        //                int time = currentSVData.DSPER * 1000;
        //                if (time == 0)
        //                {
        //                    Thread.Sleep(2000);
        //                }
        //                else
        //                {
        //                    Thread.Sleep(time);
        //                }

        //            }
        //            else
        //            {
        //                if (currentSVData.TOTSMP > 0)
        //                {
        //                    logicService.TraceDataFDC(currentSVData.UnitID, "", "", currentSVData.DateTime, currentSVData.ParameterList);
        //                    //logicService.TraceListReplyFDC(currentSVData.UnitID, "", "");
        //                    currentSVData.TOTSMP = currentSVData.TOTSMP - 1;
        //                }
        //                int time = currentSVData.DSPER * 1000;
        //                if (time == 0)
        //                {
        //                    Thread.Sleep(2000);
        //                }
        //                else
        //                {
        //                    Thread.Sleep(time);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.BCLog.Debug(ex);
        //        }
        //    }
        //}
        //private void ClearInformation()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            DateTime datetime = DateTime.Now;
        //            if (datetime.Hour == 14)
        //            {
        //                //var sum = DBService.PanelDB.DeletePanelInformationByDateTime();
        //                IDBService dbservice = CommonContexts.ResolveInstance<IDBService>();
        //                // DateTime datetime = DateTime.Now;
        //                datetime = datetime.AddDays(-21);
        //                var stringTime = datetime.ToString("yyyy-MM-dd");
        //                var panelsum = dbservice.DeleteGlassInfoByDateTime(stringTime);
        //                //var spanelsum = dbservice.DeleteSPanelInforByDateTime(stringTime);
        //                LogHelper.BCLog.Debug(string.Format("[ClearInformation] Panelsum:{0}", panelsum));
        //            }
        //            Thread.Sleep(3500000);
        //        }
        //        catch (Exception ex)
        //        {
        //            //LogHelper.PcimwellLog.Error(ex.Message, ex);
        //            LogHelper.BCLog.Debug(ex);
        //        }
        //    }
        //}
        //private void PlcMonitor()
        //{
        //    try
        //    {
        //        IAliveService AliveService = CommonContexts.ResolveInstance<IAliveService>();
        //        //Glorysoft.BC.Logic.Service.RemouldPLCMonitoring RemouldPLC = new Glorysoft.BC.Logic.Service.RemouldPLCMonitoring();
        //        AliveService.MonitorPLCAlive();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Error(ex);
        //    }


        //}
        #region S9F13
        public void S9F13Add(string portid)
        {
            App.Current.Dispatcher.Invoke(new S9F13AddDelegate(S9F13AddFun), portid);
        }
        private delegate void S9F13AddDelegate(string portid);
        private void S9F13AddFun(string portid)
        {
            try
            {
                lock (syncConv)
                {
                    if (!HostInfo.Current.S9F13Message.Keys.Contains(portid))
                    {
                        HostInfo.Current.S9F13Message.Add(portid, DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }

        public void S9F13Remove(string portid)
        {
            App.Current.Dispatcher.Invoke(new S9F13RemoveDelegate(S9F13RemoveFun), portid);
        }
        private delegate void S9F13RemoveDelegate(string portid);
        private void S9F13RemoveFun(string portid)
        {
            try
            {
                lock (syncConv)
                {
                    if (HostInfo.Current.S9F13Message.Keys.Contains(portid))
                    {
                        HostInfo.Current.S9F13Message.Remove(portid);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
        #endregion
        #region Port Panel 信息处理
        public void PortPanelInforSave(string UnitID, string PortID, List<GlassInfo> panelInfos)
        {
            App.Current.Dispatcher.Invoke(new PortPanelInforSaveDelegate(PortPanelInforSaveFun), UnitID, PortID, panelInfos);
        }
        private delegate void PortPanelInforSaveDelegate(string UnitID, string PortID, List<GlassInfo> panelInfos);
        private static readonly object portPanelSaveObj = new object();
        private void PortPanelInforSaveFun(string UnitID, string PortID, List<GlassInfo> GlassInfos)
        {
            lock (portPanelSaveObj)
            {
                // var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.Append(string.Format("[PortPanelInforSaveFun]   begin"));
                    str.AppendLine();
                    List<GlassInfo> glassInfos = new List<GlassInfo>();
                    str.Append(string.Format("[PortPanelInforSaveFun]  [PortID:{0}]", PortID));
                    str.AppendLine();
                    if (GlassInfos.Count > 0)
                    {
                        str.Append(string.Format("[PortPanelInforSaveFun]  [GlassInfos.Count>0]"));
                        str.AppendLine();
                        foreach (var item in GlassInfos)
                        {
                            glassInfos.Add(item);
                            str.Append(string.Format("[PortPanelInforSaveFun]  glass:[{0},{1}];glassid:{2};", item.CassetteSequenceNo, item.SlotSequenceNo, item.GlassID));
                            str.AppendLine();
                        }
                    }
                    else
                    {
                        str.Append(string.Format("[PortPanelInforSaveFun]  [GlassInfos.Count==0]"));
                        str.AppendLine();
                    }
                    var port = HostInfo.Current.PortList.FirstOrDefault(o => o.UnitID == UnitID && o.PortID == PortID);
                    port.GlassInfos = glassInfos;

                    str.Append(string.Format("[PortPanelInforSaveFun] [port.GlassInfos.Count:{0} {1}]", port.GlassInfos.Count(), port.UnitID));
                    str.AppendLine();
                    str.Append(string.Format("[PortPanelInforSaveFun]   end"));
                    str.AppendLine();
                    LogHelper.BCLog.Debug(str.ToString());
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.Error(string.Format("[PortPanelInforSaveFun]  [ex:{0}]", ex));
                }
            }
        }
        public void PortPanelInforUpdate(PortInfo port, GlassInfo panelInfo)
        {
            App.Current.Dispatcher.Invoke(new PortPanelInforUpdateDelegate(PortPanelInforUpdateFun), port, panelInfo);
        }
        private delegate void PortPanelInforUpdateDelegate(PortInfo port, GlassInfo panelInfo);
        private void PortPanelInforUpdateFun(PortInfo port, GlassInfo GlassInfo)
        {
            try
            {
                //port.GlassInfos = GlassInfos;
                if (GlassInfo != null)
                {
                    var setPanelInfo = port.GlassInfos.FirstOrDefault(o => o.SlotSequenceNo == GlassInfo.SlotSequenceNo);
                    SetPanelInfo(setPanelInfo, GlassInfo);
                }

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }
        }
        //public void PortPanelInforReload(PortInfo port, ObservableCollection<PanelInfo> panelInfos)
        //{
        //    App.Current.Dispatcher.Invoke(new PortPanelInforReloadDelegate(PortPanelInforReloadFun), port, panelInfos);
        //}
        //private delegate void PortPanelInforReloadDelegate(PortInfo port, ObservableCollection<PanelInfo> panelInfos);
        //private void PortPanelInforReloadFun(PortInfo port, ObservableCollection<PanelInfo> panelInfos)
        //{
        //    if (port.PortType != EnumPortType.PU.ToString())
        //    {
        //        ObservableCollection<PanelInfo> panelList = new ObservableCollection<PanelInfo>();
        //        var count = port.TransferMode == "1" ? 60 : 120;
        //        for (int i = 1; i <= count; i++)
        //        {
        //            PanelInfo panel = new PanelInfo();
        //            panel.FSlotNO = i;
        //            panel.FJobSequenceNumber = i.ToString();
        //            panel.FCstID = port.CassetteID;
        //            panel.FCassetteSequence = port.CassetteSequence;
        //            panel.FPtID = port.PortID;
        //            panelList.Add(panel);
        //        }
        //        port.PanelInfos = panelList;
        //    }

        //    // port.PanelInfos = panelInfos;
        //}

        public void PortPanelInforAdd(PortInfo port, GlassInfo panelInfo)
        {
            App.Current.Dispatcher.Invoke(new PortPanelInforAddDelegate(PortPanelInforAddFun), port, panelInfo);
        }
        private delegate void PortPanelInforAddDelegate(PortInfo port, GlassInfo panelInfo);
        private void PortPanelInforAddFun(PortInfo port, GlassInfo panelInfo)
        {
            // port.PanelInfos = panelInfo;
            try
            {
                port.GlassInfos.Add(panelInfo);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }
        }

        //public void PortPanelInforRemove(PortInfo port, GlassInfo panelInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new PortPanelInforRemoveDelegate(PortPanelInforRemoveFun), port, panelInfo);
        //}
        //private delegate void PortPanelInforRemoveDelegate(PortInfo port, GlassInfo panelInfo);
        //private void PortPanelInforRemoveFun(PortInfo port, GlassInfo panelInfo)
        //{
        //    // port.PanelInfos = panelInfo;
        //    try
        //    {
        //        port.GlassInfos.Remove(panelInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //}

        public void SetPanelInfo(GlassInfo setPanelInfo, GlassInfo panelInfo)
        {
            try
            {
                if (setPanelInfo != null)
                {
                    //       public string FunctionName { get; set; }
                    //public string ProductID { get; set; }
                    //public string OperationID { get; set; }
                    //public string LotID { get; set; }
                    //public string PPID1 { get; set; }
                    setPanelInfo.ProductID = panelInfo.ProductID;
                    setPanelInfo.OperationID = panelInfo.OperationID;
                    setPanelInfo.LotID = panelInfo.LotID;
                    setPanelInfo.PPID1 = panelInfo.PPID1;
                    //public string PPID2 { get; set; }
                    //public string PPID3 { get; set; }
                    //public string PPID4 { get; set; }
                    //public string PPID5 { get; set; }
                    //public string PPID6 { get; set; }
                    setPanelInfo.PPID2 = panelInfo.PPID2;
                    setPanelInfo.PPID3 = panelInfo.PPID3;
                    setPanelInfo.PPID4 = panelInfo.PPID4;
                    setPanelInfo.PPID5 = panelInfo.PPID5;
                    setPanelInfo.PPID6 = panelInfo.PPID6;
                    //public string PPID7 { get; set; }
                    //public string PPID8 { get; set; }
                    //public string PPID9 { get; set; }
                    //public string PPID10 { get; set; }
                    setPanelInfo.PPID7 = panelInfo.PPID7;
                    setPanelInfo.PPID8 = panelInfo.PPID8;
                    setPanelInfo.PPID9 = panelInfo.PPID9;
                    setPanelInfo.PPID10 = panelInfo.PPID10;
                    //public string GlassID { get; set; }
                    //public string RGlassID { get; set; }
                    //public int CassetteSequenceNo { get; set; }
                    setPanelInfo.GlassID = panelInfo.GlassID;
                    setPanelInfo.RGlassID = panelInfo.RGlassID;
                    setPanelInfo.CassetteSequenceNo = panelInfo.CassetteSequenceNo;
                    //public int SlotSequenceNo { get; set; }
                    //public int SlotPosition { get; set; }
                    //public int CuttingSequenceNo { get; set; }
                    setPanelInfo.SlotSequenceNo = panelInfo.SlotSequenceNo;
                    setPanelInfo.SlotPosition = panelInfo.SlotPosition;
                    setPanelInfo.CuttingSequenceNo = panelInfo.CuttingSequenceNo;
                    //public string GlassJudgeCode { get; set; }
                    //public string GlassGradeCode { get; set; }
                    //public string ProcessingFlag { get; set; }
                    setPanelInfo.GlassJudgeCode = panelInfo.GlassJudgeCode;
                    setPanelInfo.GlassGradeCode = panelInfo.GlassGradeCode;
                    setPanelInfo.ProcessingFlag = panelInfo.ProcessingFlag;
                    //public string GlassSizeCode { get; set; }
                    //public string GlassThicknessCode { get; set; }
                    //public string GlassType { get; set; }
                    setPanelInfo.GlassSizeCode = panelInfo.GlassSizeCode;
                    setPanelInfo.GlassThicknessCode = panelInfo.GlassThicknessCode;
                    setPanelInfo.GlassType = panelInfo.GlassType;
                    //public string LotCode { get; set; }
                    //public string ProcessingCount { get; set; }
                    //public string InspectionFlag { get; set; }
                    //public string SkipFlag { get; set; }
                    //public string InLineEQData { get; set; }
                    setPanelInfo.LotCode = panelInfo.LotCode;
                    setPanelInfo.ProcessingCount = panelInfo.ProcessingCount;
                    setPanelInfo.InspectionFlag = panelInfo.InspectionFlag;
                    setPanelInfo.SkipFlag = panelInfo.SkipFlag;
                    setPanelInfo.InLineEQData = panelInfo.InLineEQData;

                    //public string WorkOrder { get; set; }
                    //public string ProcessFlowName { get; set; }
                    //public string ProcessFlowVersion { get; set; }
                    //public string ProcessOperationVersion { get; set; }
                    //public string ProductSpecVersion { get; set; }
                    setPanelInfo.WorkOrder = panelInfo.WorkOrder;
                    setPanelInfo.ProcessFlowName = panelInfo.ProcessFlowName;
                    setPanelInfo.ProcessFlowVersion = panelInfo.ProcessFlowVersion;
                    setPanelInfo.ProcessOperationVersion = panelInfo.ProcessOperationVersion;
                    setPanelInfo.ProductSpecVersion = panelInfo.ProductSpecVersion;
                    //public string ProductionType { get; set; }
                    //public string HalfProductJudge { get; set; }
                    //public string GlassMaker { get; set; }
                    //public string ProductRecipe { get; set; }
                    //public string ReworkType { get; set; }
                    setPanelInfo.ProductionType = panelInfo.ProductionType;
                    setPanelInfo.HalfProductJudge = panelInfo.HalfProductJudge;
                    setPanelInfo.GlassMaker = panelInfo.GlassMaker;
                    setPanelInfo.ProductRecipe = panelInfo.ProductRecipe;
                    setPanelInfo.ReworkType = panelInfo.ReworkType;
                    //public string ReworkCount { get; set; }
                    //public string SamplingFlag { get; set; }
                    //public string ExposureRecipeName { get; set; }
                    //public string Turndegree { get; set; }
                    //public string FlowModeValue { get; set; }
                    setPanelInfo.ReworkCount = panelInfo.ReworkCount;
                    setPanelInfo.SamplingFlag = panelInfo.SamplingFlag;
                    setPanelInfo.ExposureRecipeName = panelInfo.ExposureRecipeName;
                    setPanelInfo.Turndegree = panelInfo.Turndegree;
                    setPanelInfo.FlowModeValue = panelInfo.FlowModeValue;
                    //public string EVASkipFlag { get; set; }
                    //public string BeforeProcessMachine { get; set; }
                    //public string ProductType { get; set; }
                    //public string MaskSpecName { get; set; }
                    //public string ProcessFlowRunState { get; set; }
                    setPanelInfo.EVASkipFlag = panelInfo.EVASkipFlag;
                    setPanelInfo.BeforeProcessMachine = panelInfo.BeforeProcessMachine;
                    setPanelInfo.ProductType = panelInfo.ProductType;
                    setPanelInfo.MaskSpecName = panelInfo.MaskSpecName;
                    setPanelInfo.ProcessFlowRunState = panelInfo.ProcessFlowRunState;


                    //public int Position { get; set; }
                    //public string ProcessingInfo { get; set; }
                    //public string VCRProductName { get; set; }
                    //public string NGInputComment { get; set; }
                    //public string PanelGrade { get; set; }
                    setPanelInfo.Position = panelInfo.Position;
                    setPanelInfo.ProcessingInfo = panelInfo.ProcessingInfo;
                    setPanelInfo.VCRProductName = panelInfo.VCRProductName;
                    setPanelInfo.NGInputComment = panelInfo.NGInputComment;
                    setPanelInfo.PanelGrade = panelInfo.PanelGrade;

                    //public string PanelJudge { get; set; }
                    //public DateTime UpdateDate { get; set; }
                    //public string ModePath { get; set; }
                    setPanelInfo.PanelJudge = panelInfo.PanelJudge;
                    setPanelInfo.UpdateDate = panelInfo.UpdateDate;
                    setPanelInfo.ModePath = panelInfo.ModePath;
                    //public int ModelPosition { get; set; }
                    //public string CurrentUnit { get; set; }
                    //public string CurrentSUnit { get; set; }
                    setPanelInfo.ModelPosition = panelInfo.ModelPosition;
                    setPanelInfo.CurrentUnit = panelInfo.CurrentUnit;
                    setPanelInfo.CurrentSUnit = panelInfo.CurrentSUnit;
                    //public string CurrentSSUnit { get; set; }
                    //public bool ProductAlarm { get; set; }
                    //public EnumGlassSlotStatus SlotSatus { get; set; }
                    setPanelInfo.CurrentSSUnit = panelInfo.CurrentSSUnit;
                    setPanelInfo.ProductAlarm = panelInfo.ProductAlarm;
                    setPanelInfo.SlotSatus = panelInfo.SlotSatus;
                    //public DateTime FetchDatetime { get; set; }
                    //public string PortID { get; set; }
                    //public string CassetteID { get; set; }
                    setPanelInfo.FetchDatetime = panelInfo.FetchDatetime;
                    setPanelInfo.PortID = panelInfo.PortID;
                    setPanelInfo.CassetteID = panelInfo.CassetteID;
                    //public DateTime BeginDate { get; set; }
                    //public DateTime EndDate { get; set; }
                    //public DateTime CreateDate { get; set; }
                    setPanelInfo.BeginDate = panelInfo.BeginDate;
                    setPanelInfo.EndDate = panelInfo.EndDate;
                    setPanelInfo.CreateDate = panelInfo.CreateDate;
                    //public int CurrentSlotNo { get; set; }
                    //public EnumGlassSlotStatus SlotFlag { get; set; }
                    setPanelInfo.CurrentSlotNo = panelInfo.CurrentSlotNo;
                    setPanelInfo.SlotFlag = panelInfo.SlotFlag;
                }

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
        #endregion

        #region CurrentPanelInfoList
        //public void UpdateCurrentPanelInfor(GlassInfo panelInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new UpdateCurrentPanelInforDelegate(UpdateCurrentPanelInforFun), panelInfo);
        //}
        //private delegate void UpdateCurrentPanelInforDelegate(GlassInfo panelInfo);
        //private void UpdateCurrentPanelInforFun(GlassInfo panelInfo)
        //{
        //    try
        //    {
        //        var panel = HostInfo.Current.CurrentPanelInfoList.FirstOrDefault(o => o.GlassID == panelInfo.GlassID);
        //        if (panel == null)
        //        {
        //            panelInfo.UpdateDate = DateTime.Now;
        //            HostInfo.Current.CurrentPanelInfoList.Add(panelInfo);
        //        }
        //        else
        //        {
        //            //panel.PanelGrade = panelInfo.PanelGrade;
        //            SetPanelInfo(panel, panelInfo);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("UpdateCurrentPanelInforFun  panelid:{0}; ", panel.GlassID));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}

        //public void ADDCurrentPanelInfoList(GlassInfo panelInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new ADDCurrentPanelInfoListDelegate(ADDCurrentPanelInfoListFun), panelInfo);
        //}
        //private delegate void ADDCurrentPanelInfoListDelegate(GlassInfo panelInfo);
        //private void ADDCurrentPanelInfoListFun(GlassInfo panelInfo)
        //{
        //    try
        //    {
        //        var panel = HostInfo.Current.CurrentPanelInfoList.FirstOrDefault(o => o.GlassID == panelInfo.GlassID);
        //        if (panel == null)
        //        {
        //            HostInfo.Current.CurrentPanelInfoList.Add(panelInfo);
        //        }
        //        else
        //        {
        //            SetPanelInfo(panel, panelInfo);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("ADDCurrentPanelInfoListFun HostInfo.Current.CurrentPanelInfoList.count:{0}", HostInfo.Current.CurrentPanelInfoList.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}

        //public void RemoveCurrentPanelInfoList(GlassInfo panelInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new RemoveCurrentPanelInfoListDelegate(RemoveCurrentPanelInfoListFun), panelInfo);
        //}
        //private delegate void RemoveCurrentPanelInfoListDelegate(GlassInfo panelInfo);
        //private void RemoveCurrentPanelInfoListFun(GlassInfo panelInfo)
        //{
        //    try
        //    {
        //        var panel = HostInfo.Current.CurrentPanelInfoList.FirstOrDefault(o => o.GlassID == panelInfo.GlassID);
        //        if (panel != null)
        //        {
        //            HostInfo.Current.CurrentPanelInfoList.Remove(panel);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("RemoveCurrentPanelInfoListFun HostInfo.Current.CurrentPanelInfoList.count:{0}", HostInfo.Current.CurrentPanelInfoList.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}

        //public void RemoveOldCurrentPanelInfoList()
        //{
        //    App.Current.Dispatcher.Invoke(new RemoveOldCurrentPanelInfoListDelegate(RemoveOldCurrentPanelInfoListFun));
        //}
        //private delegate void RemoveOldCurrentPanelInfoListDelegate();
        //private void RemoveOldCurrentPanelInfoListFun()
        //{
        //    try
        //    {
        //        var datetime = DateTime.Now.AddHours(-1);
        //        var panels = HostInfo.Current.CurrentPanelInfoList.Where(o => o.CurrentDateTime < datetime).ToList();
        //        foreach (var item in panels)
        //        {
        //            HostInfo.Current.CurrentPanelInfoList.Remove(item);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("RemoveOldCurrentPanelInfoListFun HostInfo.Current.CurrentPanelInfoList.count:{0}", HostInfo.Current.CurrentPanelInfoList.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}
        #endregion
        #region CurrentTraceListInfo

        //public void ADDCurrentTraceListInfo(CurrentTraceListInfo CurrentTraceListInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new ADDCurrentTraceListInfoDelegate(ADDCurrentTraceListInfoFun), CurrentTraceListInfo);
        //}
        //private delegate void ADDCurrentTraceListInfoDelegate(CurrentTraceListInfo CurrentTraceListInfo);
        //private void ADDCurrentTraceListInfoFun(CurrentTraceListInfo CurrentTraceListInfo)
        //{
        //    try
        //    {
        //        if(HostInfo.Current.CurrentTraceListInfoDic.ContainsKey(CurrentTraceListInfo.UnitID))
        //        {
        //            HostInfo.Current.CurrentTraceListInfoDic[CurrentTraceListInfo.UnitID] = CurrentTraceListInfo;
        //        }else
        //        {
        //            HostInfo.Current.CurrentTraceListInfoDic.Add(CurrentTraceListInfo.UnitID, CurrentTraceListInfo);
        //        }

        //        LogHelper.BCLog.Debug(string.Format("ADDCurrentTraceListInfoFun HostInfo.Current.CurrentTraceListInfo.count:{0}", HostInfo.Current.CurrentTraceListInfoDic.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}

        //public void RemoveCurrentTraceListInfo(CurrentTraceListInfo CurrentTraceListInfo)
        //{
        //    App.Current.Dispatcher.Invoke(new RemoveCurrentTraceListInfoDelegate(RemoveCurrentTraceListInfoFun), CurrentTraceListInfo);
        //}
        //private delegate void RemoveCurrentTraceListInfoDelegate(CurrentTraceListInfo CurrentTraceListInfo);
        //private void RemoveCurrentTraceListInfoFun(CurrentTraceListInfo CurrentTraceListInfo)
        //{
        //    try
        //    {
        //        if (HostInfo.Current.CurrentTraceListInfoDic.ContainsKey(CurrentTraceListInfo.UnitID))
        //        {
        //            HostInfo.Current.CurrentTraceListInfoDic.Remove(CurrentTraceListInfo.UnitID);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("RemoveCurrentTraceListInfoFun HostInfo.Current.CurrentTraceListInfo.count:{0}", HostInfo.Current.CurrentTraceListInfoDic.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}

        //public void RemoveOldCurrentTraceListInfo()
        //{
        //    App.Current.Dispatcher.Invoke(new RemoveOldCurrentTraceListInfoDelegate(RemoveOldCurrentTraceListInfoFun));
        //}
        //private delegate void RemoveOldCurrentTraceListInfoDelegate();
        //private void RemoveOldCurrentTraceListInfoFun()
        //{
        //    try
        //    {
        //        var datetime = DateTime.Now.AddMinutes(-20);
        //        var CurrentTraceListInfos = HostInfo.Current.CurrentTraceListInfoDic.Where(o => o.Value.CreateTime < datetime).ToList();
        //        foreach (var item in CurrentTraceListInfos)
        //        {
        //            HostInfo.Current.CurrentTraceListInfoDic.Remove(item.Key);
        //        }
        //        LogHelper.BCLog.Debug(string.Format("RemoveOldCurrentTraceListInfoFun HostInfo.Current.CurrentTraceListInfo.count:{0}", HostInfo.Current.CurrentTraceListInfoDic.Count));
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
        //}
        #endregion
        #region OPCAll
        public void OPCallListAdd(string opcallInfor)
        {

            if (HostInfo.Current.OPCallList.Count > 20)
            {
                App.Current.Dispatcher.Invoke(new RemoveOPCallListDelegate(RemoveOPCallList));
            }
            App.Current.Dispatcher.Invoke(new InsertOPCallListDelegate(InsertOPCallList), opcallInfor);
        }
        private delegate void RemoveOPCallListDelegate();
        private void RemoveOPCallList()
        {
            try
            {
                HostInfo.Current.OPCallList.RemoveAt(20);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }

        private delegate void InsertOPCallListDelegate(string opcallInfor);
        private void InsertOPCallList(string opcallInfor)
        {
            try
            {
                HostInfo.Current.OPCallList.Insert(0, opcallInfor);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
        #endregion


        public void RemainedGlassFlagModify()
        {
            //if (HostInfo.Current.EQPID.Contains("A1IMP100"))
            //{
            //    eqpService.WriteRemainedGlassFlag("IMP", HostInfo.Current.EQPInfo.RemainedGlassFlag);
            //}
        }

        private void ObjectInitialize()
        {

            try
            {
                InitialEQPList();

                InitialPortList();
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
                Environment.Exit(0);
            }

        }
        //初始化设备列表

        private void InitialEQPList()
        {
            try
            {
                //LogHelper.BCLog.Debug("EQPID:" + HostInfo.Current.EQPID);
                var eqpids = HostInfo.Current.EQPID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var eqpid in eqpids)
                {
                    var eqps = dbService.ViewEQP(eqpid);
                    HostInfo.Current.AllEQPInfo.AddRange(eqps);
                    foreach (var eqp in eqps)
                    {
                        LogHelper.BCLog.Debug(eqp.EQPID + " UnitsCount:" + eqp.Units.Count());
                        if (eqp.Units.Any(c => c.UnitType == EnumUnitType.Robot))
                        {
                            HostInfo.Current.EQPInfo = eqp;
                        }
                        foreach (var item in eqp.Units)
                        {
                            if (item.GetType().Name == "Unit")
                            {
                                //HostInfo.Current.CurrentAlarmList.Add(item.UnitID, new List<string>());
                                //HostInfo.Current.UnitTraceListInfoDic.Add(item.UnitID, new Dictionary<string, UnitTraceListInfo>());
                            }
                            if (item.UnitType == EnumUnitType.Robot)
                            {
                                var robot = (Robot)item;
                                robot.thredDispatchTimeOut.Start();
                                //eqpService.ReadPortWIPDataBlock(item.UnitName);
                            }
                            //if (item.CommandType == Consts.CommandType.HSMS.GetHashCode())
                            //{
                            //    item.LotProcessCanceledFunction = LotProcessCanceled;
                            //    item.thredHSMSLotCancel.Start();
                            //}
                            //if (item.CommandType == Consts.CommandType.PLC.GetHashCode())
                            //{
                            //    item.RecipeChangeFunction = RecipeChange;
                            //    item.thredCCLINKRecipeChange.Start();
                            //}

                            //item.thredOPIRequest.Start();
                        }
                    }

                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("eqpid", eqpid);
                    var operationmode = dbService.Viewcfg_operationmode(hashtable);
                    if (operationmode != null)
                        HostInfo.Current.operationmodelist.AddRange(operationmode);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
                Environment.Exit(0);
            }
        }
        //初始化Port信息，以后取用Port信息直接从HostInfo中取
        private void InitialPortList()
        {
            try
            {
                var eqpids = HostInfo.Current.EQPID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                var glassmap = new Hashtable();
                var allpanelInfos = dbService.GetGlassInfoList(glassmap).ToList();

                bool isAddTempGlass = false;
                //创建一个虚拟Port 放正在生产的片
                var vpport = new PortInfo() { EQPID = "VEQP", UnitID = "VUNIT", PortID = "VP" };
                HostInfo.Current.PortList.Add(vpport);

                if (!isAddTempGlass)//第一个Port把临时插片的数据存进去 //i == pList.Count - 1 && 
                {
                    isAddTempGlass = true;
                    var TemppanelInfos = allpanelInfos.Where(c => c.CassetteID == null || c.CassetteID == "");
                    if (TemppanelInfos != null)
                        HostInfo.Current.PortPanelInforSave(vpport.UnitID, vpport.PortID, TemppanelInfos.ToList());
                    //ObservablePanelInfos.AddRange(TemppanelInfos);
                }

                foreach (var eqpid in eqpids)
                {
                    var map = new Hashtable
                    {
                        {"PortID","" },
                        {"PortNo","" },
                        {"EQPID",eqpid }
                    };
                    var pList = dbService.ViewPortList(map).ToList();
                    //var cList = logicService.ViewCarrierList(HostInfo.Current.LineInfo.LineID);
                    //pList = pList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
                    //pList = pList.OrderBy(o => o.WaitingforProcessingTime).ToList();
                    HostInfo.Current.PortList.AddRange(pList);

                    if (pList != null && pList.Count > 0)
                    {
                        for (int i = 0; i < pList.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(pList[i].CassetteID))
                            {
                                map = new Hashtable
                                {
                                    {"PortID",pList[i].PortID },
                                    {"CassetteID",pList[i].CassetteID }
                                };
                                var Cassette = dbService.GetCassetteList(map).FirstOrDefault();
                                if (Cassette != null)
                                {
                                    pList[i].CassetteInfo = Cassette;
                                }
                            }
                            //HostInfo.Current.CurrentPortCommand.Add(pList[i].PortID, 0);

                            #region Glass

                            //var glassmap = new Hashtable();
                            //// glassmap.Add("GlassID", GlassID);
                            //glassmap.Add("CassetteSequenceNo", pList[i].CassetteSequenceNo);
                            //glassmap.Add("Position", Position);
                            List<GlassInfo> ObservablePanelInfos = new List<GlassInfo>();
                            List<GlassInfo> panelInfos = null;
                            if (!String.IsNullOrEmpty(pList[i].CassetteID))
                                panelInfos = allpanelInfos.Where(c => c.CassetteID == pList[i].CassetteID).ToList();
                            if (panelInfos != null)
                                ObservablePanelInfos.AddRange(panelInfos);
                           
                            //List<GlassInfo> ObservablePanelInfos = new List<GlassInfo>();
                            //foreach (var item in panelInfos)
                            //{
                            //    ObservablePanelInfos.Add(item);
                            //}
                            HostInfo.Current.PortPanelInforSave(pList[i].UnitID, pList[i].PortID, ObservablePanelInfos);
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

            //HostInfo.Current.CarrierList = cList;
        }
        private void InitLingSignal()
        {
            try
            {
                if (HostInfo.Current.EQPInfo != null)
                {
                    var map = new Hashtable
                    {
                        {"EQPID",HostInfo.Current.EQPInfo.EQPID }
                    };
                    var robotModelList = robotService.ViewRobotModelList(map);
                    // var robotLinksignalList = robotService.ViewRobotLinksignalList(map);
                    foreach (var unit in HostInfo.Current.EQPInfo.Units)
                    {
                        if (unit.GetType().Name != "PortInfo")
                        {
                            var robotModelListByUnit = robotModelList.Where(o => o.UnitID == unit.UnitID).ToList();
                            unit.RobotModelList.AddRange(robotModelListByUnit);
                            foreach (var RobotModelItem in unit.RobotModelList)
                            {
                                // var robotLinksignalListByUnit = robotLinksignalList.Where(o => o.UnitID == unit.UnitID && (o.LinkNo == RobotModelItem.UPLinkNO || o.LinkNo == RobotModelItem.DownLinkNO)).ToList();
                                //foreach (var robotLinksignalItem in robotLinksignalListByUnit)
                                //{
                                if (!string.IsNullOrEmpty(RobotModelItem.UPLinkName))
                                {
                                    Linksignal linksignal = new Linksignal();
                                    linksignal.LinkName = RobotModelItem.UPLinkName;
                                    linksignal.UnitName = unit.UnitName;
                                    linksignal.LinkType = Consts.LinkType.UpstreamLinkSignal.GetHashCode();
                                    linksignal.LinkSignalItem = new UpstreamLinkSignal();
                                    RobotModelItem.LinksignalList.Add(linksignal);
                                }
                                if (!string.IsNullOrEmpty(RobotModelItem.DownLinkName))
                                {
                                    Linksignal linksignal = new Linksignal();
                                    linksignal.LinkName = RobotModelItem.DownLinkName;
                                    linksignal.UnitName = unit.UnitName;
                                    linksignal.LinkType = Consts.LinkType.DownstreamLinkSignal.GetHashCode();
                                    linksignal.LinkSignalItem = new DownstreamLinkSignal();
                                    RobotModelItem.LinksignalList.Add(linksignal);
                                }
                            }
                        }
                        //if (unit.UnitType == (int)EnumUnitType.Robot)
                        //{

                        //    //unit.Linksignals.Add(new Linksignal() { LinkPaths = "1", UnitName = unit.UnitName,LinkSignalItem=new DownstreamLinkSignal() });
                        //    //unit.Linksignals.Add(new Linksignal() { LinkPaths = "2", UnitName = unit.UnitName, LinkSignalItem = new UpstreamLinkSignal() });
                        //    //unit.Value.Linksignals.Add(new Linksignal() { LinkPaths = "2", UnitName = unit.Value.UnitName });
                        //}
                        //else if (unit.HasSUnit)
                        //{
                        //    //unit.Value.SUnitList[0].Linksignals.Add(new Linksignal() { LinkPaths = "1", UnitName = unit.Value.UnitName, OSUnitName = unit.Value.SUnitList[0].UnitName, SUnitName = unit.Value.SUnitList[0].SUnitName });
                        //    //unit.Value.SUnitList[1].Linksignals.Add(new Linksignal() { LinkPaths = "2", UnitName = unit.Value.UnitName, OSUnitName = unit.Value.SUnitList[1].UnitName, SUnitName = unit.Value.SUnitList[1].SUnitName });
                        //    unit.Linksignals.Add(new Linksignal() { LinkPaths = "1", UnitName = unit.UnitName});
                        //    unit.Linksignals.Add(new Linksignal() { LinkPaths = "2", UnitName = unit.UnitName });
                        //    //unit.Value.Linksignals.Add(new Linksignal() { LinkPaths = "2", UnitName = unit.Value.UnitName});
                        //}

                    }

                }
                //map = new Hashtable
                //    {
                //        {"EQPID",HostInfo.Current.EQPID }
                //    };
                //HostInfo.Current.OPILinkList = dbService.ViewOPILink(map).ToList();

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
        public static List<IConnectionContext> ConnectionContexts { get; set; }

        private static IApplicationContext appContext;
        /// <summary>
        /// 自行控制打开连接的先后顺序；如果连接无关先后，可以直接用OpenAllConnectionContext接口一次打开所有链接
        /// </summary>
        public static void InitConnections()
        {

            try
            {
                var allconns = appContext.OpenAllConnectionContext();

                //EIP初始化
                PLCContexts.Current.InitializeContexts();

                //WebSocket初始化
                var ip = ConfigurationManager.AppSettings["WebSocketLocalIP"];
                var port = ConfigurationManager.AppSettings["WebSocketLocalPort"];
                WebSocketHandler.Current.Start(ip, port);

                //tibco rv
                try
                {
                    var rvServer = TibcoManager.Current;
                    rvServer.InitialTibco("Configuration\\RVConnection.xml");

                    //AreYouThereRequest();
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.DebugFormat(ex.ToString());
                    MessageBox.Show(ex.ToString());
                    Application.Current.Shutdown();
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }

        private void CreateBCAlive()
        {
            try
            {
                DictEIPBCAlives.Clear();
                foreach (var map in Glorysoft.EIPDriver.PLCConfig.Instance.BlockMaps.Values)
                {
                    foreach (var m in map)
                    {
                        if (m.Value.Count > 0 && m.Value.Any(c => c.Name.ToUpper().Contains("BCALIVE") && c.Action.ToUpper() == "WRITE" && c.DeviceCode == Glorysoft.EIPDriver.DeviceCode.Parse("B")))
                        {
                            var bcalivetag = m.Value.FirstOrDefault(c => c.Name.ToUpper().Contains("BCALIVE") && c.Action.ToUpper() == "WRITE" && c.DeviceCode == Glorysoft.EIPDriver.DeviceCode.Parse("B"));
                            if (!DictEIPBCAlives.ContainsKey(m.Key))
                                DictEIPBCAlives.Add(m.Key, bcalivetag);
                        }
                    }
                }

                if (DictEIPBCAlives.Count > 0)
                {
                    SendBCAlive();
                }
            }
            catch
            {
            }
        }

        private void SendBCAlive()
        {
            Task.Factory.StartNew(() =>
            {
                int BCAliveBit = 0;
                while (true)
                {
                    try
                    {
                        BCAliveBit = BCAliveBit == 1 ? 0 : 1;
                        foreach (var bcalivetag in DictEIPBCAlives)
                        {
                            var blk = bcalivetag.Value.BlockCollection["BCCommand"];
                            blk.ItemCollection["BCAlive"].Value = BCAliveBit.ToString();
                            try
                            {
                                PLCContexts.Current.SendCommand(bcalivetag.Value);
                            }
                            catch (Exception ex)
                            {
                                LogHelper.BCLog.Error(ex);
                            }
                            Thread.Sleep(50);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.BCLog.Error(ex);
                    }
                    Thread.Sleep(4000);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private static void AreYouThereRequest()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var eqpids = HostInfo.Current.EQPID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var eqpid in eqpids)
                        {
                            RVAreYouThere data = new RVAreYouThere();
                            data.EQUIPMENTID = eqpid;
                            rvService.SendToMESAreYouThere(eqpid, data, HostInfo.Current.GetTransactionID());
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.MESLog.Error(ex.Message);
                    }
                    Thread.Sleep(10000);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private static void SendS9F13()
        {
            while (true)
            {
                try
                {
                    //foreach (var item in HostInfo.Current.S9F13Message)
                    //{
                    var S9F13Messages = HostInfo.Current.S9F13Message.Where(o => o.Value.AddSeconds(45) < DateTime.Now).ToList();
                    foreach (var item in S9F13Messages)
                    {
                        try
                        {
                            //eisService.SendS9F13(item.Key);
                            HostInfo.Current.S9F13Remove(item.Key);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.BCLog.Info("SendS9F13" + ex.Message);
                        }
                    }
                    //}
                    //foreach (var item in HostInfo.Current.S9F13Message)
                    //{
                    //    try
                    //    {
                    //        if (item.Value.AddSeconds(45) < DateTime.Now)
                    //        {
                    //            eisService.SendS9F13(item.Key);
                    //            HostInfo.Current.S9F13Remove(item.Key);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LogHelper.BCLog.Info("LCAlive" + ex.Message);
                    //    }

                    //}
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.Error(ex);
                }
                Thread.Sleep(1000);
            }

        }

        private void InitNotifyIcon()
        {
            try
            {
                notifyIcon = new System.Windows.Forms.NotifyIcon { BalloonTipText = "LC Server" };
                notifyIcon.ShowBalloonTip(2000);
                notifyIcon.Text = "LC Server";
                notifyIcon.Visible = true;
                //打开菜单项
                var open = new System.Windows.Forms.MenuItem("Open");
                open.Click += Show;
                //关闭菜单项
                var close = new System.Windows.Forms.MenuItem("Close");
                close.Click += Hide;
                //关联托盘控件
                var childen = new[] { open, close };
                notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
                notifyIcon.MouseDoubleClick += (o, e) => { if (e.Button == System.Windows.Forms.MouseButtons.Left) Show(o, e); };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
                Environment.Exit(0);
            }
        }

        private void Show(object sender, EventArgs e)
        {
            try
            {
                Visibility = Visibility.Visible;
                ShowInTaskbar = true;
                Activate();
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }

        private void Hide(object sender, EventArgs e)
        {
            try
            {
                ShowInTaskbar = false;
                Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                //var wConfirm = new ConfirmWin { lblTitle = { Content = "LC Exit Confirm!" } };
                //var dlgResult = wConfirm.ShowDialog();
                //if (dlgResult == null || !dlgResult.Value) e.Cancel = true;
                //var proExe = System.Diagnostics.Process.GetProcessesByName("Glorysoft.BC.Client");
                //foreach (var process in proExe)
                //{
                //    process.Kill();
                //}
                //var lst = System.Diagnostics.Process.GetProcessesByName("Glorysoft.BC.Client.vshost");
                //foreach (var process in lst)
                //{
                //    process.Kill();
                //}
                //Application.Current.Shutdown();
                //Environment.Exit(0);
            }
            catch (Exception ex)
            {

            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wConfirm = new ConfirmWin { lblTitle = { Content = "BC Exit Confirm!" } };
                var dlgResult = wConfirm.ShowDialog();
                if (dlgResult == null || !dlgResult.Value) return;

                #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
                //断开EIP连接
                IEQPService eqpService = CommonContexts.ResolveInstance<IEQPService>();
                eqpService.Terminate();
                #endregion

                //var proExe = System.Diagnostics.Process.GetProcessesByName("Glorysoft.BC.Client");
                //foreach (var process in proExe)
                //{
                //    process.Kill();
                //}
                //var lst = System.Diagnostics.Process.GetProcessesByName("Glorysoft.BC.Client.vshost");
                //foreach (var process in lst)
                //{
                //    process.Kill();
                //}
            }
            catch (Exception ex)
            {

            }
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
        private void MiniWindow_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            //this.ShowInTaskbar = false;
            //this.Visibility = System.Windows.Visibility.Hidden;
            //this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.DragMove();
                    }));
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }
        }
        #region RobotDispath
        private void InitRobotDispath()
        {
            try
            {

                if (robotService != null)
                {
                    robotService.Start("");
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }

        #endregion

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 打印Cache日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_30(object sender, RoutedEventArgs e)
        {
            try
            {
                String cachelog = "";
                foreach (var port in HostInfo.Current.PortList)
                {
                    if (port.GlassInfos.Count > 0)
                    {
                        cachelog += $" \r\n";
                        cachelog += $"UnitID:{port.UnitID} PortID:{port.PortID} \r\n";
                        var glass = port.GlassInfos.OrderBy(c => (c.Position)).ToList();
                        for (int i = 0; i < glass.Count; i++)
                        {
                            cachelog += glass[i].ToString() + $" \r\n";
                        }
                        cachelog += $" \r\n";
                    }
                }
                LogHelper.BCLog.Debug(cachelog);
                return;
                //var wConfirm = new ConfirmWin { lblTitle = { Content = "Permission verification!" } };
                ////wConfirm.IsExit = true;
                //var dlgResult = wConfirm.ShowDialog();
                //if (this.txtTrid.Text.Trim().ToString() == "")
                //{
                //    MessageBox.Show("TRID is not null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                //if (this.txtDsper.Text.Trim().ToString() == "")
                //{
                //    MessageBox.Show("DSPER is not null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                //if (this.txtTotsmp.Text.Trim().ToString() == "")
                //{
                //    MessageBox.Show("TOTSMP is not null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                //if (this.txtRepgsz.Text.Trim().ToString() == "")
                //{
                //    MessageBox.Show("REPGSZ is not null", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                //var trid = this.txtTrid.Text.Trim().ToString();
                //var dsper = this.txtDsper.Text.Trim().ToString();
                //var totsmp = this.txtTotsmp.Text.Trim().ToString();
                //var repgsz = this.txtRepgsz.Text.Trim().ToString();
                ////secsService.SendHSMSTraceInitializeSendS2F23("PhotoTrack", trid, dsper, totsmp, repgsz, new List<string>());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
