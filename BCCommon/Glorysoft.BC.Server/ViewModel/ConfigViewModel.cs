using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.BaseClass;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections;

namespace Glorysoft.BC.Server.ViewModel
{

    public class ConfigViewModel : NotifyPropertyChanged
    {
        protected static readonly IEQPService eqpCmd = CommonContexts.ResolveInstance<IEQPService>();
        public ConfigViewModel()
        {
            try
            {
                foreach (var port in LineInfo.PortList)
                {
                    portIDList.Add(port.PortID);
                }
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           

            //var panel1 = FormatPanelID(1);
            //var panel2 = FormatPanelID(2);
        }
        public static HostInfo LineInfo
        {
            get
            {
                return HostInfo.Current;
            }
        }
        #region PortSetting
        #region Port Command
        private string portID;
        public string PortID
        {
            get
            {
                return portID;
            }

            set
            {
                portID = value;
                Notify("PortID");
            }
        }
        private List<string> portIDList = new List<string>();
        public List<string> PortIDList
        {
            get
            {
                return portIDList;
            }
            set
            {
                portIDList = value;
                Notify("PortIDList");
            }
        }
        public void SendToEQPPortCommand(PortInfo port, string CassetteControl)
        {
            try
            {
                switch (port.PortNo)
                {
                    //case 1:
                    //    eqpCmd.SendPort1CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 2:
                    //    eqpCmd.SendPort2CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 3:
                    //    eqpCmd.SendPort3CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 4:
                    //    eqpCmd.SendPort4CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 5:
                    //    eqpCmd.SendPort5CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 6:
                    //    eqpCmd.SendPort6CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 7:
                    //    eqpCmd.SendPort7CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 8:
                    //    eqpCmd.SendPort8CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 9:
                    //    eqpCmd.SendPort9CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 10:
                    //    eqpCmd.SendPort10CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                    //case 11:
                    //    eqpCmd.SendPort11CassetteControlCommand(port.EQPName, port.PortNo.ToString(), CassetteControl);
                    //    break;
                }
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }
        private DelegateCommand portStartCommand;
        public ICommand PortStartCommand
        {
            get
            {
                if (portStartCommand == null)
                    portStartCommand = new DelegateCommand(PortSatrt);
                return portStartCommand;
            }
        }

        public void PortSatrt()
        {
            try
            {
                //if (MessageBoxResult.Yes ==
                //   MessageBox.Show("您确定要向该port口发送Start命令吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
                //{
                //    var oPort = LineInfo.PortList.FirstOrDefault(f => f.PortID == PortID);
                //    if (string.IsNullOrEmpty(PortID))
                //    {
                //        MessageBox.Show("Please Selcet A Port First", "Infor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //        return;
                //    }
                //    if (oPort.CassetteStatus == (int)EnumCarrierStatus.WaitingforStart)
                //    {
                //        SendToEQPPortCommand(oPort, Consts.PLCCassetteControl.Start.GetHashCode().ToString());
                //        LogHelper.BCLog.Debug(string.Format("Click PortSatrt ;port:{0}", PortID));
                //    }
                //    else
                //    {
                //        MessageBox.Show("Send Satrt Command Port Must At WaitingforStart Status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }
                //    //eqpCmd.SendPort1CassetteControlCommand(port.EQPName, oPort.PortNo.ToString(), Consts.CassetteControl.Cancel.GetHashCode().ToString());

                //}

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
            
        }

        private DelegateCommand portCompleteCommand;
        public ICommand PortCompleteCommand
        {
            get
            {
                if (portCompleteCommand == null)
                    portCompleteCommand = new DelegateCommand(PortComplete);
                return portCompleteCommand;
            }
        }

        public void PortComplete()
        {
            try
            {
                //if (MessageBoxResult.Yes ==
                //   MessageBox.Show("您确定要向该port口发送Complete命令吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
                //{
                //    var oPort = LineInfo.PortList.FirstOrDefault(f => f.PortID == PortID);
                //    if (string.IsNullOrEmpty(PortID))
                //    {
                //        MessageBox.Show("Please Selcet A Port First", "Infor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //        return;
                //    }
                //    if (oPort.PortType == EnumPortType.PL )
                //    {
                //        MessageBox.Show("PL口不能发送Completed命令!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //        return;
                //    }
                //    if (oPort.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing)
                //    {
                //        SendToEQPPortCommand(oPort, Consts.PLCCassetteControl.End.GetHashCode().ToString());
                //        LogHelper.BCLog.Debug(string.Format("Click PortComplete ;port:{0}", PortID));
                //    }
                //    else
                //    {
                //        MessageBox.Show("Send Complete Command Port Must At In Processing Status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }
                //}

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
            
        }
        private DelegateCommand portCancelCommand;
        public ICommand PortCancelCommand
        {
            get
            {
                if (portCancelCommand == null)
                    portCancelCommand = new DelegateCommand(PortCancel);
                return portCancelCommand;
            }
        }

        public void PortCancel()
        {
            try
            {
                //if (MessageBoxResult.Yes ==
                //  MessageBox.Show("您确定要向该port口发送Cancel命令吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
                //{
                //    var oPort = LineInfo.PortList.FirstOrDefault(f => f.PortID == PortID);
                //    if (oPort.CassetteStatus == (int)EnumCarrierStatus.WaitingforProcessing || oPort.CassetteStatus == (int)EnumCarrierStatus.WaitingforStart)
                //    {
                //        SendToEQPPortCommand(oPort, Consts.PLCCassetteControl.Cancel.GetHashCode().ToString());
                //        LogHelper.BCLog.Debug(string.Format("Click PortCancel ;port:{0}", PortID));
                //    }
                //    else
                //    {
                //        MessageBox.Show("Send Cancel Command Port Must At Load Complete or Ready To Start or Wait For Start or Wait For Process Status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    }
                //}
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }

        private DelegateCommand portAbortCommand;
        public ICommand PortAbortCommand
        {
            get
            {
                if (portAbortCommand == null)
                    portAbortCommand = new DelegateCommand(PortAbort);
                return portAbortCommand;
            }
        }

        public void PortAbort()
        {
            try
            {
                if (MessageBoxResult.Yes ==
                   MessageBox.Show("您确定要向该port口发送Abort命令吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    var oPort = LineInfo.PortList.FirstOrDefault(f => f.PortID == PortID);
                    if (string.IsNullOrEmpty(PortID))
                    {
                        MessageBox.Show("Please Selcet A Port First", "Infor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    if (oPort.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing)
                    {
                        SendToEQPPortCommand(oPort, Consts.PLCCassetteControl.Abort.GetHashCode().ToString());
                        LogHelper.BCLog.Debug(string.Format("Click PortAbort ;port:{0}", PortID));
                    }
                    else
                    {
                        MessageBox.Show("Send Abort Command Port Must At In Processing Status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
         }
        #endregion

        #region PanelMap
        //private string portIDByMap;
        //public string PortIDByMap
        //{
        //    get
        //    {
        //        return portIDByMap;
        //    }

        //    set
        //    {

        //        portIDByMap = value;
        //        try
        //        {
        //            var port = LineInfo.PortList.Where(o => o.PortID == portIDByMap).FirstOrDefault();
        //            if (port != null)
        //            {
        //                var count = 0;
        //                if (port.TransferMode == EnumTransferMode.MGVMode) count = 60;//中间 60
        //                else count = 120;//两侧 120

        //                var MapList = new Dictionary<string, object>();
        //                for (int i = 1; i <= count; i++)
        //                {
        //                    MapList.Add(i.ToString(), i.ToString());
        //                }
        //                PanelMapList = MapList;
        //                SelectPanelMap = new Dictionary<string, object>();
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            LogHelper.BCLog.Debug(ex);
        //        }
                
        //        Notify("PortIDByMap");
        //    }
        //}
        private Dictionary<string, object> selectPanelMap=new Dictionary<string, object> ();
        public Dictionary<string, object> SelectPanelMap
        {
            get
            {
                return selectPanelMap;
            }
            set
            {
                selectPanelMap = value;
                Notify("SelectPanelMap");
            }
        }
        private Dictionary<string, object> panelMapList=new Dictionary<string, object> ();
        public Dictionary<string, object> PanelMapList
        {
            get
            {
                return panelMapList;
            }
            set
            {
                panelMapList = value;
                Notify("PanelMapList");
            }
        }

        public string FormatPanelID(string slotid)
        {
            var panelid = DateTime.Now.ToString("MMddHHmmss");
            try
            {
                StringBuilder result = new StringBuilder();
                string strslotid = slotid;//.ToString();
                if (strslotid.Count() < 3)
                {
                    for (int i = 0; i < (3 - strslotid.Count()); i++)
                    {
                        //result = result + "0";
                        result.Append("0");
                    }
                    // result = result + strslotid;
                    result.Append(strslotid);
                }
                else
                {
                    //result = slotid.ToString();
                    result.Append(slotid);
                }
                panelid = "P" + panelid + result.ToString();
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
            
           
            return panelid;
        }

        private DelegateCommand panelMapSaveCommand;
        public ICommand PanelMapSaveCommand
        {
            get
            {
                if (panelMapSaveCommand == null)
                    panelMapSaveCommand = new DelegateCommand(PanelMapSave);
                return panelMapSaveCommand;
            }
        }
        protected static readonly IDBService dBService = CommonContexts.ResolveInstance<IDBService>();
        public void PanelMapSave()
        {
            //try
            //{
            //    if (HostInfo.Current.ControlState != ControlState.OnlineRemote)
            //    {
            //        if (MessageBoxResult.Yes ==
            //          MessageBox.Show("您确定要向该port口保存Panel信息吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
            //        {
            //            var portinfo = LineInfo.PortList.FirstOrDefault(f => f.PortID == PortIDByMap);
            //            if (string.IsNullOrEmpty(PortIDByMap))
            //            {
            //                MessageBox.Show("Please Selcet A Port First", "Infor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //                return;
            //            }
            //            if (portinfo.PortType == EnumPortType.PU.ToString())
            //            {
            //                selectPanelMap = new Dictionary<string, object>();
            //            }
            //            if (portinfo.PortType == EnumPortType.PL.ToString())
            //            {
            //                var panelmap = new Hashtable
            //                {
            //                    { "HPanelID",""},
            //                    { "FPtID",portinfo.PortID},
            //                    { "FCstID",portinfo.CassetteID},
            //                    { "TPtID",""},
            //                    { "TCstID",""},
            //                    { "IsOutPort",false},
                                
            //                    { "TCassetteSequence",""},
            //                    { "FJobSequenceNumber",""},
            //                    { "TJobSequenceNumber",""}
            //                };
            //                dBService.DeletePanelList(panelmap);
            //            }
            //            if (portinfo.CassetteStatus == (int)EnumCarrierStatus.WaitingforStart)
            //            {
            //                //SendToEQPPortCommand(oPort, Consts.PLCCassetteControl.Completed.GetHashCode().ToString());
            //                ObservableCollection<GlassInfo> panelInfos = new ObservableCollection<GlassInfo>();
            //                foreach (var PanelMapItem in SelectPanelMap)
            //                {
            //                    var panel = FormatPanelID(PanelMapItem.Value.ToString());
            //                    GlassInfo panelInfo = new GlassInfo();
            //                    panelInfo.GLSID = panel;
            //                    panelInfo.FSlotNO = Convert.ToInt32(PanelMapItem.Value);
            //                    //panelInfo.FJobSequenceNumber = PanelMapItem.Value.ToString();
            //                    //panelInfo.FCassetteSequence = portinfo.CassetteSequence;
            //                    panelInfo.FCSTID = portinfo.CassetteID;
            //                    panelInfo.FPorTID = portinfo.PortID;
            //                    panelInfo.PPID = HostInfo.Current.CurrentPPID;
            //                    panelInfo.SlotSatus = (int)EnumGlassSlotStatus.W;
            //                    panelInfo.IsExist = true;
            //                    panelInfo.IsOutPort = false;
            //                    panelInfos.Add(panelInfo);
            //                    dBService.InsertPanelInfo(panelInfo);
            //                }
            //                HostInfo.Current.PortPanelInforSave(portinfo, panelInfos);
            //                portinfo.PPID = HostInfo.Current.CurrentPPID;
            //                portinfo.PanelQty = panelInfos.Count.ToString();
            //                dBService.UpdatePortInfo(portinfo);
            //                LogHelper.BCLog.Debug(string.Format("Click PanelMapSave ;port:{0}", PortIDByMap));
            //            }
            //            else
            //            {
            //                MessageBox.Show("Send Strat Command Cassette Must At WaitingforStart Status!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        MessageBox.Show("保存信息必须不是OnlineRemote!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    LogHelper.BCLog.Debug(ex);
            //}
          
        }

        #endregion
        #endregion

        #region Robot Command
        private string commandMotion;
        public string CommandMotion
        {
            get
            {
                return commandMotion;
            }

            set
            {
                commandMotion = value;
                Notify("CommandMotion");
            }
        }
        private string commandStageType;
        public string CommandStageType
        {
            get
            {
                return commandStageType;
            }

            set
            {
                commandStageType = value;
                Notify("CommandStageType");
            }
        }
        private string commandStage;
        public string CommandStage
        {
            get
            {
                return commandStage;
            }

            set
            {
                commandStage = value;
                Notify("CommandStage");
            }
        }
        private string commandSlot;
        public string CommandSlot
        {
            get
            {
                return commandSlot;
            }

            set
            {
                commandSlot = value;
                Notify("CommandSlot");
            }
        }
        private string panelThickness;
        public string PanelThickness
        {
            get
            {
                return panelThickness;
            }

            set
            {
                panelThickness = value;
                Notify("PanelThickness");
            }
        }
        private string commandHand;
        public string CommandHand
        {
            get
            {
                return commandHand;
            }

            set
            {
                commandHand = value;
                Notify("CommandHand");
            }
        }
        private string subCommandMotion="";
        public string SubCommandMotion
        {
            get
            {
                return subCommandMotion;
            }

            set
            {
                subCommandMotion = value;
                Notify("SubCommandMotion");
            }
        }
        private string subCommandStageType = "";
        public string SubCommandStageType
        {
            get
            {
                return subCommandStageType;
            }

            set
            {
                subCommandStageType = value;
                Notify("SubCommandStageType");
            }
        }
        private string subCommandStage = "";
        public string SubCommandStage
        {
            get
            {
                return subCommandStage;
            }

            set
            {
                subCommandStage = value;
                Notify("SubCommandStage");
            }
        }
        private string subCommandSlot = "";
        public string SubCommandSlot
        {
            get
            {
                return subCommandSlot;
            }

            set
            {
                subCommandSlot = value;
                Notify("SubCommandSlot");
            }
        }
        private string subCommandHand = "";
        public string SubCommandHand
        {
            get
            {
                return subCommandHand;
            }

            set
            {
                subCommandHand = value;
                Notify("SubCommandHand");
            }
        }
        private DelegateCommand robotControlCommand;
        public ICommand RobotControlCommand
        {
            get
            {
                if (robotControlCommand == null)
                    robotControlCommand = new DelegateCommand(RobotControl);
                return robotControlCommand;
            }
        }

        public void RobotControl()
        {
           // RobotCommand cmd=new RobotCommand ();
            //ReplyData.ItemCollection[PLCEventItem.CommandMotion] = EnumValueToString(cmd.Command);//1取；2放
            //ReplyData.ItemCollection[PLCEventItem.CommandStageType] = EnumValueToString(cmd.StageType);//1单层；
            //ReplyData.ItemCollection[PLCEventItem.CommandStage] = EnumValueToString(cmd.StagePathNo);//unit path no(db配置的固定的参数)
            //ReplyData.ItemCollection[PLCEventItem.CommandSlot] = EnumValueToString(cmd.SlotNo);//
            //ReplyData.ItemCollection[PLCEventItem.CommandHand] = EnumValueToString(cmd.RobotHand);//0 上手臂；1下手臂
            //ReplyData.ItemCollection[PLCEventItem.PanelThickness] = EnumValueToString(cmd.GlassThickness);
            //ReplyData.ItemCollection[PLCEventItem.SubCommandMotion] = EnumValueToString(cmd.SubCommand);
            //ReplyData.ItemCollection[PLCEventItem.SubCommandStageType] = EnumValueToString(cmd.SubCommandStageType);
            //ReplyData.ItemCollection[PLCEventItem.SubCommandStage] = EnumValueToString(cmd.SubCommandStagePathNo);
            //ReplyData.ItemCollection[PLCEventItem.SubCommandSlot] = EnumValueToString(cmd.SubCommandSlotNo);
            //ReplyData.ItemCollection[PLCEventItem.SubCommandHand] = EnumValueToString(cmd.SubCommandRobotHand);
            try
            {
               // eqpCmd.SendRobotControlCommand(Consts.UnitName.Index.ToString(), CommandMotion, CommandStageType, CommandStage, CommandSlot, CommandHand, PanelThickness, SubCommandMotion, SubCommandStageType, SubCommandStage, SubCommandSlot, SubCommandHand);

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }
        #endregion
    }
}
