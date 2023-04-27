using Glorysoft.BC.Entity.Configuration;
using Glorysoft.BC.Entity.WebSocketEntity;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class HostInfo : NotifyPropertyChanged
    {


        #region 单例模式
        private static readonly Lazy<HostInfo> Lazy = new Lazy<HostInfo>(() => new HostInfo());
        public static HostInfo Current
        {
            get
            {
                return Lazy.Value;
            }
        }
        public HostInfo()
        {

            LineRunMode = EnumLineRunMode.MassProductionMode;

            //Thread alive = new Thread(SendLCAlive);
            //alive.Start();
            // eqpList=
        }
        //public CancellationTokenSource AreYouThereRequest;
        #endregion
        public EnumLineRunMode LineRunMode { get; set; }
        public string EQPID { get; set; }
        public CFGOLDPriority OldPriority { get; set; }
        //public string EQPType;
        //public string EQPName;
        //private string cclinkName;
        //public string CCLinkName
        //{
        //    get
        //    {
        //        return cclinkName;
        //    }
        //    set
        //    {
        //        if (cclinkName != value)
        //        {
        //            cclinkName = value;
        //            Notify("CCLinkName");
        //        }
        //    }
        //}
        //private string lineID;
        //public string LineID
        //{
        //    get
        //    {
        //        return lineID;
        //    }
        //    set
        //    {
        //        if (lineID != value)
        //        {
        //            lineID = value;
        //            Notify("LineID");
        //        }
        //    }
        //}
        public string GetLogMessage()
        {
            try
            {
                DateTime sendDataTime = new DateTime();
                sendDataTime = DateTime.Now;
                string sTime = sendDataTime.ToString("hh:mm:ss");
                string sMon = sendDataTime.Month.ToString().PadLeft(2, '0');
                string sDay = sendDataTime.Day.ToString().PadLeft(2, '0');
                string txtMessage = "";
                txtMessage = sMon + "/" + sDay + "_" + sTime + ":";
                return txtMessage;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return "";
            }

        }
        #region ppid check
        //public void CheckPPID(PortInfo port)
        //{
        //    try
        //    {
        //        lock (syncConv)
        //        {
        //            try
        //            {
        //                //ISECSCommandService secsCmd = CommonContexts.ResolveInstance<ISECSCommandService>();
        //                List<Unit> units = new List<Unit>();
        //                int RtCodeResult = 0;
        //                for (int i = 0; i < 10; i++)
        //                {
        //                    units = EQPInfo.UnitList.Values.Where(o => o.RTCode == "0").ToList();
        //                    RtCodeResult = units.Count();
        //                    if (RtCodeResult == 2)
        //                    {
        //                        //var port = PortList.Where(o => o.PortNo == portno).FirstOrDefault();

        //                        S2F104ReplyMessage("0", port);
        //                        return;
        //                    }
        //                    Thread.Sleep(1000);
        //                }
        //                units = EQPInfo.UnitList.Values.Where(o => o.RTCode == "0").ToList();
        //                RtCodeResult = units.Count();
        //                if (RtCodeResult < 2)
        //                {
        //                    S2F104ReplyMessage("3", port);
        //                }
        //                foreach (var eqpItem in EQPInfo.UnitList)
        //                {
        //                    eqpItem.Value.RTCode = "";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.BCLog.Debug(ex);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //}
        #endregion
        #region S2F104
        public void S2F104ReplyMessage(string ack, PortInfo port)
        {

            if (S2F104ReplyMessageFunction != null)
            {
                S2F104ReplyMessageFunction(ack, port);
            }
        }
        public delegate void S2F104ReplyMessageDelegate(string ack, PortInfo port);
        /// <summary>
        /// S2F104 回复ReplyMessage
        /// </summary>
        public S2F104ReplyMessageDelegate S2F104ReplyMessageFunction;

        #endregion
        #region OPCallList
        private ObservableCollection<string> _opCallList=new ObservableCollection<string> ();
        public ObservableCollection<string> OPCallList
        {
            get { return _opCallList; }
            set { _opCallList = value; Notify("OPCallList"); }
        }
        public void OPCallListAdd(string opcallInfor)
        {
            opcallInfor = GetLogMessage() + opcallInfor;
            if (OPCallListAddFun != null)
            {
                OPCallListAddFun(opcallInfor);
            }
        }
        public delegate void OPCallListAddDelegate(string opcallInfor);
        public OPCallListAddDelegate OPCallListAddFun;
        #endregion
        #region S9F13
        public void S9F13Add(string portid)
        {

            if (S9F13AddFunction != null)
            {
                S9F13AddFunction(portid);
            }
        }
        public delegate void S9F13AddDelegate(string portid);
        public S9F13AddDelegate S9F13AddFunction;
        public void S9F13Remove(string portid)
        {

            if (S9F13RemoveFunction != null)
            {
                S9F13RemoveFunction(portid);
            }
        }
        public delegate void S9F13RemoveDelegate(string portid);
        public S9F13RemoveDelegate S9F13RemoveFunction;
        #endregion
        #region port 信息处理
        public void PortPanelInforSave(string UnitID, string PortID, List<GlassInfo> panelInfos)
        {

            if (PortPanelInforSaveFunction != null)
            {
                PortPanelInforSaveFunction(UnitID, PortID, panelInfos);
            }
        }
        public delegate void PortPanelInforSaveDelegate(string UnitID, string PortID, List<GlassInfo> panelInfos);
        public PortPanelInforSaveDelegate PortPanelInforSaveFunction;
        public void PortPanelInforUpdate(PortInfo port, GlassInfo panelInfo)
        {

            if (PortPanelInforUpdateFunction != null)
            {
                PortPanelInforUpdateFunction(port, panelInfo);
            }
        }
        public delegate void PortPanelInforUpdateDelegate(PortInfo port, GlassInfo panelInfo);
        public PortPanelInforUpdateDelegate PortPanelInforUpdateFunction;

        //public void PortPanelInforReload(PortInfo port, ObservableCollection<PanelInfo> panelInfos)
        //{

        //    if (PortPanelInforReloadFunction != null)
        //    {
        //        PortPanelInforReloadFunction(port, panelInfos);
        //    }
        //}
        //public delegate void PortPanelInforReloadDelegate(PortInfo port, ObservableCollection<PanelInfo> panelInfos);
        //public PortPanelInforReloadDelegate PortPanelInforReloadFunction;
        public void PortPanelInforAdd(PortInfo port, GlassInfo panelInfo)
        {

            if (PortPanelInforAddFunction != null)
            {
                PortPanelInforAddFunction(port, panelInfo);
            }
        }
        public delegate void PortPanelInforAddDelegate(PortInfo port, GlassInfo panelInfo);
        public PortPanelInforAddDelegate PortPanelInforAddFunction;


        //public void PortPanelInforRemove(PortInfo port, GlassInfo panelInfo)
        //{

        //    if (PortPanelInforRemoveFunction != null)
        //    {
        //        PortPanelInforRemoveFunction(port, panelInfo);
        //    }
        //}
        //public delegate void PortPanelInforRemoveDelegate(PortInfo port, GlassInfo panelInfo);
        //public PortPanelInforRemoveDelegate PortPanelInforRemoveFunction;
        #endregion


        #region 字符串处理
        public string FormatSlotID(int slotid)
        {
            StringBuilder result = new StringBuilder();
            try
            {
                string strslotid = slotid.ToString();
                if (strslotid.Count() < 3)
                {
                    for (int i = 0; i < (3 - strslotid.Count()); i++)
                    {
                        //result = result + "0";
                        result.Append("0");
                    }
                    //result = result + strslotid;
                    result.Append(strslotid);
                }
                else
                {
                    //result = slotid.ToString();
                    result.Append(slotid.ToString());
                }
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return result.ToString();
        }
        public int StringToInt(string item)
        {
            try
            {
                if(string.IsNullOrEmpty(item))
                {
                    return 0;
                }
                return Convert.ToInt32(item);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return 0;
            }
        }
        //public double StringToDouble(string item, string fieldName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(item))
        //        {
        //            return 0;
        //        }
        //        return Convert.ToDouble(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(string.Format("fieldName:{0};Value:{1}; StringToDouble Error!", fieldName, item));
        //        LogHelper.BCLog.Debug(ex);
        //        return 0;
        //    }
        //}
        /// <summary>
        /// 如果参数是0会返回空
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string IntToString(int item)
        {
            try
            {
                return item == 0 ? "" : item.ToString();//Convert.ToInt32(item);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return "";
            }
        }
        public string FormatSlotPosition(int slotposition)
        {
            switch(slotposition)
            {
                case 0:
                    return "";
                case 1:
                    return "A";
                case 2:
                    return "B";
            }
            return "";
        }
        /// <summary>
        /// double 转科学计数法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string FormatScientificNotation(double item)
        {
            string itemstr = item.ToString("e");
            var ss = itemstr.Split('e');
            var aa = ss[1].Substring(1);
            var bb = Convert.ToInt32(aa);
            var result = string.Format("{0}e{1}{2}", ss[0], ss[1].Substring(0, 1), bb);
            return result;
        }
        #endregion
        #region 
        //public List<OPILink> OPILinkList { get; set; }
        private List<OPILink> opiLinkList=new List<OPILink>();
        public List<OPILink> OPILinkList
        {
            get
            {
                return opiLinkList;
            }
            set
            {
                if (opiLinkList != value)
                {
                    opiLinkList = value;
                    Notify("OPILinkList");
                }
            }
        }
        #region CurrentTraceListInfo
        private Dictionary<string, Dictionary<string, UnitTraceListInfo>> unitTraceListInfoDic = new Dictionary<string, Dictionary<string, UnitTraceListInfo>>();
        /// <summary>
        /// unitid   trid  CurrentTraceListInfo
        /// </summary>
        public Dictionary<string, Dictionary<string, UnitTraceListInfo>> UnitTraceListInfoDic
        {
            get { return unitTraceListInfoDic; }
            set
            {
                unitTraceListInfoDic = value;
                Notify("UnitTraceListInfoDic");
            }
        }
        private List<CurrentSVData> ccLinkSVDataList = new List<CurrentSVData>();
        public List<CurrentSVData> CCLinkSVDataList
        {
            get { return ccLinkSVDataList; }
            set
            {
                ccLinkSVDataList = value;
                Notify("CCLinkSVDataList");
            }
        }
        /// <summary>
        /// 查看UnitTraceListInfoDic 对应的unit信息里是否存在相应的trid数据,
        /// 如果不存在,添加新的trid,并且启动相对应的新的线程
        /// 如果存在,修改trid数据
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="trid"></param>
        /// <param name="CurrentTraceListInfo"></param>
        public void SaveUnitTraceListInfo(string unitid, string trid, UnitTraceListInfo UnitTraceListInfo)
        {

            if (SaveUnitTraceListInfoFunction != null)
            {
                SaveUnitTraceListInfoFunction(unitid, trid, UnitTraceListInfo);
            }
        }
        public delegate void SaveUnitTraceListInfoDelegate(string unitid, string trid, UnitTraceListInfo UnitTraceListInfo);
        public SaveUnitTraceListInfoDelegate SaveUnitTraceListInfoFunction;

        //public void RemoveCurrentTraceListInfo(CurrentTraceListInfo CurrentTraceListInfo)
        //{

        //    if (RemoveCurrentTraceListInfoFunction != null)
        //    {
        //        RemoveCurrentTraceListInfoFunction(CurrentTraceListInfo);
        //    }
        //}
        //public delegate void RemoveCurrentTraceListInfoDelegate(CurrentTraceListInfo CurrentTraceListInfo);
        //public RemoveCurrentTraceListInfoDelegate RemoveCurrentTraceListInfoFunction;

        //public void RemoveOldCurrentTraceListInfo()
        //{

        //    if (RemoveOldCurrentTraceListInfoFunction != null)
        //    {
        //        RemoveOldCurrentTraceListInfoFunction();
        //    }
        //}
        //public delegate void RemoveOldCurrentTraceListInfoDelegate();
        //public RemoveOldCurrentTraceListInfoDelegate RemoveOldCurrentTraceListInfoFunction;

        #endregion
        #region CurrentPanelInfoList
        private List<GlassInfo> currentPanelInfoList = new List<GlassInfo>();
        public List<GlassInfo> CurrentPanelInfoList
        {
            get { return currentPanelInfoList; }
            set
            {
                currentPanelInfoList = value;
                Notify("CurrentPanelInfoList");
            }
        }
        //public void UpdateCurrentPanelInfor(GlassInfo panelInfo)
        //{

        //    if (UpdateCurrentPanelInforFunction != null)
        //    {
        //        UpdateCurrentPanelInforFunction(panelInfo);
        //    }
        //}
        //public delegate void UpdateCurrentPanelInforDelegate(GlassInfo panelInfo);
        //public UpdateCurrentPanelInforDelegate UpdateCurrentPanelInforFunction;


        //public void ADDCurrentPanelInfoList(GlassInfo panelInfo)
        //{

        //    if (ADDCurrentPanelInfoListFunction != null)
        //    {
        //        ADDCurrentPanelInfoListFunction(panelInfo);
        //    }
        //}
        //public delegate void ADDCurrentPanelInfoListDelegate(GlassInfo panelInfo);
        //public ADDCurrentPanelInfoListDelegate ADDCurrentPanelInfoListFunction;

        //public void RemoveCurrentPanelInfoList(GlassInfo panelInfo)
        //{

        //    if (RemoveCurrentPanelInfoListFunction != null)
        //    {
        //        RemoveCurrentPanelInfoListFunction(panelInfo);
        //    }
        //}
        //public delegate void RemoveCurrentPanelInfoListDelegate(GlassInfo panelInfo);
        //public RemoveCurrentPanelInfoListDelegate RemoveCurrentPanelInfoListFunction;

        //public void RemoveOldCurrentPanelInfoList()
        //{

        //    if (RemoveOldCurrentPanelInfoListFunction != null)
        //    {
        //        RemoveOldCurrentPanelInfoListFunction();
        //    }
        //}
        //public delegate void RemoveOldCurrentPanelInfoListDelegate();
        //public RemoveOldCurrentPanelInfoListDelegate RemoveOldCurrentPanelInfoListFunction;

        #endregion
        #endregion

        //private int lineStatus;
        //public int LineStatus
        //{
        //    get
        //    {
        //        return lineStatus;
        //    }
        //    set
        //    {
        //        if (lineStatus != value)
        //        {
        //            lineStatus = value;
        //            Notify("LineStatus");
        //        }
        //    }
        //}

        //private string lineReasonCode;
        //public string LineReasonCode
        //{
        //    get
        //    {
        //        return lineReasonCode;
        //    }
        //    set
        //    {
        //        if (lineReasonCode != value)
        //        {
        //            lineReasonCode = value;
        //            Notify("LineReasonCode");
        //        }
        //    }
        //}
        private Dictionary<string, List<string>> currentAlarmList = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> CurrentAlarmList
        {
            get { return currentAlarmList; }
            set
            {
                currentAlarmList = value;
                Notify("CurrentAlarmList");
            }
        }


        private bool canChangeOnline = true;
        public bool CanChangeOnline
        {
            get
            {
                return canChangeOnline;
            }
            set
            {
                canChangeOnline = value;
                Notify("CanChangeOnline");
            }
        }
        public bool HaveFeeder { get; set; }
        public string UserID { get; set; }
        public string LineType { get; set; }
        public int ParaListBit { get; set; }


        public int IsAbort { get; set; }
        // public string SlotToProcess { get; set; }
        public List<Recipe> RecipeParameter { get; set; }
        public List<RecipeDownload> RecipeDownLoad { get; set; }
        //public string SlotInfo { get; set; }
        private int operationMode = 1;
        public int OperationMode
        {
            get
            {
                return operationMode;
            }

            set
            {
                operationMode = value;
                Notify("OperationMode");
            }
        }

        //public int LotFlag { get; set; }

        //private bool isHostOpen;
        //public bool IsHostOpen
        //{
        //    get
        //    {
        //        return isHostOpen;
        //    }
        //    set
        //    {
        //        isHostOpen = value;
        //    }
        //}

        public RVMappingConfig RVMappingConfig { get; set; }
        public readonly Dictionary<string, ConversationObject> convList = new Dictionary<string, ConversationObject>();
        private static readonly object syncConv = new object();
        //public void AddConversation(string ObjectId, string SnFx)
        //{
        //    var conv = new ConversationObject(ObjectId.Trim(), SnFx);

        //    lock (syncConv)
        //    {
        //        if (!convList.ContainsKey(ObjectId.Trim()))
        //            convList.Add(ObjectId.Trim(), conv);
        //    }
        //}
        ///// <summary>
        ///// Check Conversation TimeOut List, Reomve Item.
        ///// </summary>
        ///// <param name="sEQID"></param>
        ///// <param name="ObjectId"></param>
        ///// <param name="SnFx"></param>
        //public void RemoveConversation(string ObjectId, string SnFx)
        //{
        //    if (!convList.ContainsKey(ObjectId)) return;

        //    var tempConv = convList[ObjectId];
        //    lock (syncConv)
        //    {
        //        if (tempConv.StreamFunc == SnFx)
        //            convList.Remove(ObjectId);
        //    }
        //}

        //private Dictionary<string, PLCRequestInfo> plcRequestInfoList = new Dictionary<string, PLCRequestInfo>();
        //public Dictionary<string,PLCRequestInfo> PLCRequestInfoList
        //{
        //    get { return plcRequestInfoList; }
        //    set { plcRequestInfoList = value; }
        //}

        //private Dictionary<string, PLCRequestInfo> matRequestInfoList = new Dictionary<string, PLCRequestInfo>();
        //public Dictionary<string, PLCRequestInfo> MatRequestInfoList
        //{
        //    get { return matRequestInfoList; }
        //    set { matRequestInfoList = value; }
        //}

        private SystemConfig systemConfig = new SystemConfig();
        public SystemConfig SystemConfig
        {
            get { return systemConfig; }
            set
            {
                if (systemConfig != value)
                {
                    systemConfig = value;
                    Notify("SystemConfig");
                }
            }
        }
        private MESRule mesRule = new MESRule();
        public MESRule MESRule
        {
            get { return mesRule; }
            set
            {
                if (mesRule != value)
                {
                    mesRule = value;
                    Notify("MESRule");
                }
            }
        }
        private SECSRule secsRule = new SECSRule();
        public SECSRule SECSRule
        {
            get { return secsRule; }
            set
            {
                if (secsRule != value)
                {
                    secsRule = value;
                    Notify("SECSRule");
                }
            }
        }
        private LinkSignal linkSignal = new LinkSignal();
        public LinkSignal LinkSignal
        {
            get { return linkSignal; }
            set
            {
                if (linkSignal != value)
                {
                    linkSignal = value;
                    Notify("LinkSignal");
                }
            }
        }
        private EQRule eqRule = new EQRule();
        public EQRule EQRule
        {
            get { return eqRule; }
            set
            {
                if (eqRule != value)
                {
                    eqRule = value;
                    Notify("EQRule");
                }
            }
        }

        private EQPInfo eqpInfo = new  EQPInfo();
        /// <summary>
        /// 用于Robot控制使用的Line
        /// </summary>
        public  EQPInfo EQPInfo
        {
            get { return eqpInfo; }
            set
            {
                if (eqpInfo != value)
                {
                    eqpInfo = value;
                    Notify("EQPInfo");
                }
            }
        }

        private List<EQPInfo> allEQPInfo = new List<EQPInfo>();
        /// <summary>
        /// BC上的所有Line
        /// </summary>
        public List<EQPInfo> AllEQPInfo
        {
            get { return allEQPInfo; }
            set
            {
                if (allEQPInfo != value)
                {
                    allEQPInfo = value;
                    Notify("AllEQPInfo");
                }
            }
        }

        /// <summary>
        /// Robot CurrentPosition
        /// </summary>
        public int CurrentPosition { get; set; }
        private Dictionary<string, DateTime> s9F13Message = new Dictionary<string, DateTime>();
        public Dictionary<string, DateTime> S9F13Message
        {
            get { return s9F13Message; }
            set
            {
                if (s9F13Message != value)
                {
                    s9F13Message = value;
                    Notify("S9F13Message");
                }
            }
        }

        private List<PortInfo> portList = new List<PortInfo>();

        public List<PortInfo> PortList
        {
            get { return portList; }
            set
            {
                portList = value;
                Notify("PortList");
            }
        }
        //private List<Material> materialList = new List<Material>();

        //public List<Material> MaterialList
        //{
        //    get { return materialList; }
        //    set
        //    {
        //        materialList = value;
        //        Notify("MaterialList");
        //    }
        //}
        private Dictionary<string, int> currentPortCommand = new Dictionary<string, int>();
        /// <summary>
        /// 1: Cassette Map Download;2 : Chuck;3 : Unchuck;4 : Rechuck
        /// </summary>
        public Dictionary<string, int> CurrentPortCommand
        {
            get { return currentPortCommand; }
            set
            {
                currentPortCommand = value;
                Notify("CurrentPortCommand");
            }
        }
        //public string MasterRecipeToPort1 { get; set; }
        //public string MasterRecipeToPort2 { get; set; }
        //public string MasterRecipeToPort3 { get; set; }
        //public string MasterRecipeToPort4 { get; set; }

        /// <summary>
        /// s2f103 recipe check 使用
        /// </summary>
        public int CurrentPortNo { get; set; }

        public int RobotControlCommandReplyReturnCode { get; set; }
        private ObservableCollection<CarrierInfo> carrierList = new ObservableCollection<CarrierInfo>();

        public ObservableCollection<CarrierInfo> CarrierList
        {
            get { return carrierList; }
            set
            {
                carrierList = value;
                Notify("CarrierList");
            }
        }

        //private Dictionary<string, TraceData> traceDataReport = new Dictionary<string, TraceData>();
        //public Dictionary<string, TraceData> TraceDataReport
        //{
        //    get { return traceDataReport; }
        //    set
        //    {
        //        if (traceDataReport!=value)
        //        {
        //            traceDataReport = value;
        //        }
        //    }
        //}
        private Dictionary<string, List<SVInfo>> eqpSVList = new Dictionary<string, List<SVInfo>>();
        public Dictionary<string, List<SVInfo>> EQPSVList
        {
            get
            {
                return eqpSVList;
            }
            set
            {
                if (eqpSVList != value)
                {
                    eqpSVList = value;
                }
            }
        }
        private Dictionary<string, List<SVInfo>> unitSVList = new Dictionary<string, List<SVInfo>>();
        public Dictionary<string, List<SVInfo>> UnitSVList
        {
            get
            {
                return unitSVList;
            }
            set
            {
                if (unitSVList != value)
                {
                    unitSVList = value;
                }
            }
        }


        private Dictionary<string, List<MaterialInfo>> eqpCurrentMaterialList = new Dictionary<string, List<MaterialInfo>>();
        public Dictionary<string, List<MaterialInfo>> EQPCurrentMaterialList
        {
            get
            {
                return eqpCurrentMaterialList;
            }
            set
            {
                eqpCurrentMaterialList = value;
                Notify("EQPCurrentMaterialList");
            }
        }

        //private bool isFACConnect;
        //public bool IsFACConnect
        //{
        //    get { return isFACConnect; }
        //    set
        //    {
        //        if (isFACConnect != value)
        //        {
        //            isFACConnect = value;
        //            Notify("IsFACConnect");
        //        }
        //    }
        //}

        private bool isNGLot;
        public bool IsNGLot
        {
            get { return isNGLot; }
            set { isNGLot = value; Notify("IsNGLot"); }
        }

        //private int currentOQAResult;
        //public int CurrentOQAResult
        //{
        //    get { return currentOQAResult; }
        //    set { currentOQAResult = value; Notify("CurrentOQAResult"); }
        //}

        //private ObservableCollection<string> currentOQALot = new ObservableCollection<string>();
        //public ObservableCollection<string> CurrentOQALot
        //{
        //    get { return currentOQALot; }
        //    set { currentOQALot = value; Notify("CurrentOQALot"); }
        //}

        //private int settingOQANGLotCount = 4;
        //public int SettingOQANGLotCount
        //{
        //    get { return settingOQANGLotCount; }
        //    set { settingOQANGLotCount = value; Notify("SettingOQANGLotCount"); }
        //}

        //private int currentOQANGLotCount;
        //public int CurrentOQANGLotCount
        //{
        //    get { return currentOQANGLotCount; }
        //    set { currentOQANGLotCount = value; Notify("CurrentOQANGLotCount"); }
        //}

        //private ObservableCollection<OQASamplingRule> oqaSamplingRuleList = new ObservableCollection<OQASamplingRule>();
        //public ObservableCollection<OQASamplingRule> OQASamplingRuleList
        //{
        //    get { return oqaSamplingRuleList; }
        //    set { oqaSamplingRuleList = value; Notify("OQASamplingRuleList"); }
        //}

        //private ObservableCollection<string> msgList = new ObservableCollection<string>();
        //public ObservableCollection<string> MsgList
        //{
        //    get
        //    {
        //        return msgList;
        //    }
        //    set
        //    {
        //        msgList = value;
        //        Notify("MsgList");
        //    }
        //}
        //private ObservableCollection<EventInfo> eventList = new ObservableCollection<EventInfo>();
        //public ObservableCollection<EventInfo> EventList
        //{
        //    get
        //    {
        //        return eventList;
        //    }
        //    set
        //    {
        //        eventList = value;
        //        Notify("EventList");
        //    }
        //}

        //private string currentWorkOrder = "";
        //public string CurrentWorkOrder
        //{
        //    get { return currentWorkOrder; }
        //    set { currentWorkOrder = value;Notify("CurrentWorkOrder"); }
        //}

        //private string currentFGCode = "";
        //public string CurrentFGCode
        //{
        //    get { return currentFGCode; }
        //    set { currentFGCode = value; Notify("CurrentFGCode"); }
        //}

        //private int processDataCount;//POL精度99片报一次
        //public int ProcessDataCount
        //{
        //    get { return processDataCount; }
        //    set { processDataCount = value; Notify("ProcessDataCount"); }
        //}
        //private string currentRecipeNo;
        //public string CurrentRecipeNo
        //{
        //    get { return currentRecipeNo; }
        //    set
        //    {
        //        if (currentRecipeNo != value)
        //        {
        //            currentRecipeNo = value;
        //            Notify("CurrentRecipeNo");
        //        }
        //    }
        //}

        private Dictionary<string, Dictionary<string, string>> mainRecpieRequestOriginalSourceSubjectNameDic = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> MainRecpieRequestOriginalSourceSubjectNameDic
        {
            get { return mainRecpieRequestOriginalSourceSubjectNameDic; }
            set
            {
                if (mainRecpieRequestOriginalSourceSubjectNameDic != value)
                {
                    mainRecpieRequestOriginalSourceSubjectNameDic = value;
                    Notify("MainRecpieRequestOriginalSourceSubjectNameDic");
                }
            }
        }
        private Dictionary<string, Dictionary<string, string>> recipeParameterRequestOriginalSourceSubjectNameDic = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> RecipeParameterRequestOriginalSourceSubjectNameDic
        {
            get { return recipeParameterRequestOriginalSourceSubjectNameDic; }
            set
            {
                if (recipeParameterRequestOriginalSourceSubjectNameDic != value)
                {
                    recipeParameterRequestOriginalSourceSubjectNameDic = value;
                    Notify("RecipeParameterRequestOriginalSourceSubjectNameDic");
                }
            }
        }
        //public string MainRecpieRequestORIGINALSOURCESUBJECTNAME { get; set; }
        //public string RecipeParameterRequestORIGINALSOURCESUBJECTNAME { get; set; }

        private int currentPackMode;

        /// <summary>
        /// //1：CST→CST，2，CST→BOX，3：BOX→BOX，4：BOX→CST 
        /// </summary>
        public int CurrentPackMode
        {
            get { return currentPackMode; }
            set
            {
                if (currentPackMode != value)
                {
                    currentPackMode = value;
                    Notify("CurrentPackMode");
                }
            }
        }
        private string panelSize;
        public string PanelSize
        {
            get { return panelSize; }
            set
            {
                if (panelSize != value)
                {
                    panelSize = value;
                    Notify("PanelSize");
                }
            }
        }
        private string pnlsize;
        public string PNLSIZE
        {
            get { return pnlsize; }
            set
            {
                if (pnlsize != value)
                {
                    pnlsize = value;
                    Notify("PNLSIZE");
                }
            }
        }
        //private bool hsmsSendTraceData=false;
        //public bool HsmsSendTraceData
        //{
        //    get { return hsmsSendTraceData; }
        //    set
        //    {
        //        if (hsmsSendTraceData != value)
        //        {
        //            hsmsSendTraceData = value;
        //            Notify("HsmsSendTraceData");
        //        }
        //    }
        //}
        private ConcurrentDictionary<string, CancellationObject> cancellationObjectDic = new ConcurrentDictionary<string, CancellationObject>();
        public ConcurrentDictionary<string, CancellationObject> CancellationObjectDic//ConcurrentDictionary是线程安全的1.0.6.1
        {

            get { return cancellationObjectDic; }
            set
            {
                if (cancellationObjectDic != value)
                {
                    cancellationObjectDic = value;
                }
            }
        }
        #region Robot Dispatch
        private int sequenceNo = 0;
        public int SequenceNo
        {
            get
            {
                if (sequenceNo == 10000)
                {
                    sequenceNo = 0;
                }
                sequenceNo++;
                return sequenceNo;
            }
        }

        private List<object> list = new List<object>();
        public List<object> DispatchList
        {
            get
            {
                return list;
            }
            set
            {
                list = value;
            }
        }
        private Dictionary<string, SPanelInfo> panelList = new Dictionary<string, SPanelInfo>();
        public Dictionary<string, SPanelInfo> PanelList
        {
            get
            {
                return panelList;
            }
            set
            {
                if (panelList != value)
                {
                    panelList = value;
                    Notify("PanelList");
                }
            }
        }
        //public void AddGlassList(SPanelInfo Info)
        //{
        //    Info.CreateDate = DateTime.Now;
        //    if (PanelList.ContainsKey(Info.PanelID))
        //    {
        //        PanelList[Info.PanelID] = Info;
        //    }
        //    else
        //    {
        //        PanelList.Add(Info.PanelID, Info);

        //    }
        //}
        public void UpdateLinkSignalValue(Type linktype, object linkobject, string propertyname, bool val)
        {
            try
            {
                PropertyInfo info = linktype.GetProperty(propertyname.Replace("#", ""));
                if (info != null)
                    info.SetValue(linkobject, val);
            }
            catch (Exception ex)
            {
                // LogHelper.AppLog.Error(ex);
            }
        }
        //private InlineTool inlineTool = new InlineTool();
        //public InlineTool LineInfo
        //{
        //    get
        //    {
        //        return inlineTool;
        //    }
        //    set
        //    {
        //        if (inlineTool != value)
        //        {
        //            inlineTool = value;
        //        }
        //    }
        //}
        //private bool bcConnected;

        public bool isSendOPIInforamtion { get; set; }
        public Line LineMode { get; set; }
        #endregion

        private int messageSequenceNo = 0;
        public int MessageSequenceNo
        {
            get
            {
                if(messageSequenceNo==9000)
                {
                    messageSequenceNo = 0;
                }
                messageSequenceNo++;
                return messageSequenceNo;
            }
        }

        #region  MES Rule
        public string GetBCToMESValue(string key, string value)
        {
            string result = "";
            var mappingItem = MESRule.mappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (mappingItem != null)
            {
                var mappingValueClass = mappingItem.mappingValueList.FirstOrDefault(o => o.value == value);
                if (mappingValueClass != null)
                {
                    result = mappingValueClass.mappingValue;
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = value;
            }
            if(string.IsNullOrEmpty(result))
            {
                result = "";
            }
            return result;
        }
        public string GetMESToBCValue(string key, string name)
        {
            string result = "";
            var mappingItem = MESRule.mappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (mappingItem != null)
            {
                var mappingValueClass = mappingItem.mappingValueList.FirstOrDefault(o => o.mappingValue == name);
                if (mappingValueClass != null)
                {
                    result = mappingValueClass.value;
                }
            }

            if (string.IsNullOrEmpty(result))
            {
                result = name;
            }
            return result;
        }

        public string GetSECSToBCValue(string key, string name)
        {
            string result = "";
            var SECSMappingItem = SECSRule.SECSMappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (SECSMappingItem != null)
            {
                var SECSMappingValue = SECSMappingItem.SECSMappingValueList.FirstOrDefault(o => o.value == name);
                if (SECSMappingValue != null)
                {
                    result = SECSMappingValue.mappingValue;
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = name;
            }
            return result;
        }
        public string GetBCToSECSValue(string key, string name)
        {
            string result = "";
            var SECSMappingItem = SECSRule.SECSMappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (SECSMappingItem != null)
            {
                var SECSMappingValue = SECSMappingItem.SECSMappingValueList.FirstOrDefault(o => o.mappingValue == name);
                if (SECSMappingValue != null)
                {
                    result = SECSMappingValue.value;
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = name;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "";
            }
            return result;
        }

        public string GetEQToBCValue(string key, string name, bool needmap = false)
        {
            string result = "";
            var EQMappingItem = EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (EQMappingItem != null)
            {
                var EQMappingValue = EQMappingItem.EQMappingValueList.FirstOrDefault(o => o.value == name);
                if (EQMappingValue != null)
                {
                    result = EQMappingValue.mappingValue;
                }
            }
            if (string.IsNullOrEmpty(result) && !needmap)
            {
                result = name;
            }
            return result;
        }
        public string GetBCToEQValue(string key, string name)
        {
            string result = "";
            var EQMappingItem = EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == key);
            if (EQMappingItem != null)
            {
                var EQMappingValue = EQMappingItem.EQMappingValueList.FirstOrDefault(o => o.mappingValue == name);
                if (EQMappingValue != null)
                {
                    result = EQMappingValue.value;
                }
            }
            if (string.IsNullOrEmpty(result))
            {
                result = name;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = "";
            }
            return result;
        }
        public void AddCancellation(CancellationTokenSource thread, string messageName)
        {
            CancellationObject objects = new CancellationObject();
            objects.CancellationTokenSource = thread;
            if (CancellationObjectDic.ContainsKey(messageName))
            {
                CancellationObjectDic[messageName] = objects;
            }
            else
            {
                CancellationObjectDic.TryAdd(messageName, objects);
            }
        }
        public string FormatString(string item)
        {
            try
            {
                if (string.IsNullOrEmpty(item)) return "";

                return item.Trim();
                //return Convert.ToInt32(item);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return "";
            }
        }
        //处理科学计数
        public string ChangeDataToD(string strData)
        {
            try
            {
                Decimal dData = 0.0M;
                if (strData.Contains("E") || strData.Contains("e"))
                {
                    dData = Decimal.Parse(strData, System.Globalization.NumberStyles.Float);
                    return dData.ToString();
                }
                else
                {
                    return strData;
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(string.Format("[ChangeDataToD]  ChangeDataToD ex:{0}", ex));
                return strData;
            }

        }
        public double StringToDouble(string item, string fieldName)
        {
            try
            {
                if (string.IsNullOrEmpty(item))
                {
                    return 0;
                }
                return Convert.ToDouble(item);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(string.Format("fieldName:{0};Value:{1}; StringToDouble Error!", fieldName, item));
                LogHelper.BCLog.Debug(ex);
                return 0;
            }
        }
        public int FormatStringToInt(string item)
        {
            //LogHelper.BCLog.Debug("item:" + item);
            int value = 0;
            int.TryParse(item, out value);
            return value;
            //try
            //{
            //    if (string.IsNullOrEmpty(item)) return 0;

            //    return Convert.ToInt32(item);
            //    //return Convert.ToInt32(item);
            //}
            //catch (Exception ex)
            //{
            //    //LogHelper.BCLog.Error(ex);
            //    return 0;
            //}
        }
        #endregion

        #region RemainedGlassFlag
       //public string ChangeDataToD(string strData)
       //{
       //    try
       //    {
       //        Decimal dData = 0.0M;
       //        if (strData.Contains("E") || strData.Contains("e"))
       //        {
       //            dData = Decimal.Parse(strData, System.Globalization.NumberStyles.Float);
       //            return dData.ToString();
       //        }
       //        else
       //        {
       //            return strData;
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        LogHelper.BCLog.Error(string.Format("[ChangeDataToD]  ChangeDataToD ex:{0}", ex));
       //        return strData;
       //    }
       //
       //}
        public List<PortInfo> GetProcessPortList()
        {
            List<PortInfo> portList = new List<PortInfo>();
            foreach (var portItem in PortList)
            {
                if(portItem.PortStatus== (int)EnumPortStatus.InUse)
                {
                    if (portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing || portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing)
                    {
                        var glassList = portItem.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait);
                        if (glassList.Count() > 0)
                        {
                            portList.Add(portItem);
                        }
                    }
                }
            }
            return portList;
        }
        public void RemainedGlassFlagModify()
        {

            if (RemainedGlassFlagModifyFunction != null)
            {
                RemainedGlassFlagModifyFunction();
            }
        }
        public delegate void RemainedGlassFlagModifyDelegate();
        public RemainedGlassFlagModifyDelegate RemainedGlassFlagModifyFunction;
        #endregion

        #region CVDGlassFlag
     
        public List<PortInfo> GetCVDProcessPortList()
        {
            List<PortInfo> portList = new List<PortInfo>();
            foreach (var portItem in PortList)
            {
                if (portItem.PortStatus == (int)EnumPortStatus.InUse && portItem.CassetteInfo.HasCVD)
                {
                    LogHelper.BCLog.Debug(string.Format("[GetCVDProcessPortList]portid:{0}; portStatus:{1}; cassetteHasCVD:{2}", portItem.PortID, portItem.PortStatus, portItem.CassetteInfo.HasCVD));
                    if (portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing || portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing)
                    {
                        LogHelper.BCLog.Debug(string.Format("[GetCVDProcessPortList]portid:{0}; CassetteStatus:{1}", portItem.PortID, portItem.CassetteInfo.CassetteStatus));
                        if (CheckPortByGlassCVDFlag(portItem, "GetCVDProcessPortList"))
                        {
                            portList.Add(portItem);
                        }
                    }
                    else
                    {
                        LogHelper.BCLog.Debug(string.Format("[GetCVDProcessPortList]portid:{0}; CassetteStatus:{1}", portItem.PortID, portItem.CassetteInfo.CassetteStatus));
                    }
                }
                else
                {
                    LogHelper.BCLog.Debug(string.Format( "[GetCVDProcessPortList]portid:{0}; portStatus:{1}; cassetteHasCVD:{2}", portItem.PortID, portItem.PortStatus, portItem.CassetteInfo.HasCVD));
                }
            }
            return portList;
        }
        public List<PortInfo> GetCVDProcessPortListByGlass(string portID,int lastGlag)
        {
            List<PortInfo> portList = new List<PortInfo>();
            foreach (var portItem in PortList)
            {
                if (portItem.PortStatus == (int)EnumPortStatus.InUse && portItem.CassetteInfo.HasCVD)
                {
                    if (portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing || portItem.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing)
                    {

                        if (portItem.PortID == portID && lastGlag == 1)
                        {
                            LogHelper.BCLog.Debug(string.Format(" Check CVDFlag; port{0} glass is Last:", portID));
                        }
                        else
                        {
                            if(CheckPortByGlassCVDFlag( portItem, "GetCVDProcessPortListByGlass"))
                            {
                                portList.Add(portItem);
                            }                          
                            //var glassList = portItem.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait||o.SlotSatus== EnumGlassSlotStatus.Processing);
                            //if (glassList.Count() > 0)
                            //{
                            //    LogHelper.BCLog.Debug(string.Format("Check CVDFlag; Port:{0} has wait or Processing Glass.", portID));
                            //    portList.Add(portItem);
                            //}
                        }
                    }
                    //portList.Add(portItem);
                }
            }
            return portList;
        }

        private bool CheckPortByGlassCVDFlag(  PortInfo portItem,string functionName)
        {
            bool result = false;
            var glass = portItem.GlassInfos.FirstOrDefault(o => o.LastGlassFlag == 1);
            if (glass != null)
            {
                LogHelper.BCLog.Debug(string.Format("[{1}] Check CVDFlag; Port:{0}; LastGlass[{2},{3}]", portItem.PortID, functionName, glass.CassetteSequenceNo, glass.SlotSequenceNo));
                if (glass.CVDFlag != 1)
                {
                    LogHelper.BCLog.Debug(string.Format("[{1}] Check CVDFlag; Port:{0}; LastGlass[{2},{3}]; CVDFlag!=1; ", portItem.PortID, functionName, glass.CassetteSequenceNo, glass.SlotSequenceNo));
                    result = true;
                }                 
            }
            else
            {
                LogHelper.BCLog.Debug(string.Format("[{1}] Check CVDFlag; Port:{0};  not exist LastGlassFlag is 1 Glass", portItem.PortID, functionName));                
            }
            return result;
        }

        public void CVDGlassFlagModify()
        {

            if (CVDGlassFlagModifyFunction != null)
            {
                CVDGlassFlagModifyFunction();
            }
        }
        public delegate void CVDGlassFlagModifyDelegate();
        public CVDGlassFlagModifyDelegate CVDGlassFlagModifyFunction;
        #endregion
        public string GetHSMSUnitName(string UnitName)
        {
            string unitName = UnitName;
            if (UnitName.Contains("HSMS"))
            {
                unitName = unitName.Replace("HSMS", "");
            }
            return unitName;
        }
        #region Test
        //private Dictionary<string, Dictionary<string, DateTime>> testMessageBeginInfoDic = new Dictionary<string, Dictionary<string, DateTime>>();
        ///// <summary>
        ///// unitname,messagename,beginTime
        ///// </summary>
        //public Dictionary<string, Dictionary<string, DateTime>> TestMessageBeginInfoDic
        //{
        //    get { return testMessageBeginInfoDic; }
        //    set
        //    {
        //        if (testMessageBeginInfoDic != value)
        //        {
        //            testMessageBeginInfoDic = value;
        //            Notify("TestMessageBeginInfoDic");
        //        }
        //    }
        //}
        #endregion

        #region matti
        Random r = new Random();
        public string GetTransactionID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(1000000, 9999999);
        }
        public List<cfg_operationmode> operationmodelist { get; set; } = new List<cfg_operationmode>();
        public List<bc_sys_setting> SystemSetting { get; set; } = new List<bc_sys_setting>();
        public ConcurrentDictionary<string, List<DVData>> DVDataList { get; set; } = new ConcurrentDictionary<string, List<DVData>>();
        public ConcurrentDictionary<string, List<SVData>> SVDataList { get; set; } = new ConcurrentDictionary<string, List<SVData>>();
        public ConcurrentDictionary<string, List<RecipeParameter>> RecipeParameterList { get; set; } = new ConcurrentDictionary<string, List<RecipeParameter>>();
        public ConcurrentDictionary<string, List<cfg_portgradegroup>> PortGradeGroupList { get; set; } = new ConcurrentDictionary<string, List<cfg_portgradegroup>>();
        private Dictionary<string, string> rvMessageList = new Dictionary<string, string>();
        public Dictionary<string, string> RVMessageList
        {
            get
            {
                return rvMessageList;
            }
            set
            {
                if (rvMessageList != value)
                {
                    rvMessageList = value;
                }
            }
        }
        public ConcurrentDictionary<string, List<BoxCache>> BoxListCache { get; set; } = new ConcurrentDictionary<string, List<BoxCache>>();
        public object lockdvdat { get; set; } = new object();//单例锁 防止写dv文件异常
        public ConcurrentDictionary<string, Task> AllT9Task { get; set; } = new ConcurrentDictionary<string, Task>();
        public ConcurrentDictionary<string, CancellationTokenSource> AllT9CancelToken { get; set; } = new ConcurrentDictionary<string, CancellationTokenSource>();
        public ConcurrentDictionary<string, string> OPICommandTrans { get; set; } = new ConcurrentDictionary<string, string>();
        #endregion
    }
}
