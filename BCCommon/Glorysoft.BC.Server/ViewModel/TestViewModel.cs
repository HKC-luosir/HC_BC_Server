using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Contract.PLC;

using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.BaseClass;
using Glorysoft.BC.EQP.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Server.ViewModel
{

    public class TestViewModel : PopupWindowViewModel
    {
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        
        public TestViewModel()
        {
           // s6F1TraceDataCommand = new DelegateCommand(S6F1TraceData);        
        }
        private IPLCContext GetPLCContextByEQPName(string eqpid)
        {
            return CommonContexts.GetPLCContextByName(eqpid);
        }
        private IPLCContext GetCCLinkPLCContext()
        {
            var plcContext = GetPLCContextByEQPName(HostInfo.Current.EQPID);
            return plcContext;
        }
        //private DelegateCommand eqpAlarmCommand;
        //public DelegateCommand EqpAlarmCommand
        //{
        //    get
        //    {
        //        if (eqpAlarmCommand == null) eqpAlarmCommand = new DelegateCommand(EqpAlarm);
        //        return eqpAlarmCommand;
        //       // return s6F1TraceDataCommand;
        //    }
        //}
        //private void EqpAlarm()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var EQPAlarmReportHandler = new EQPAlarmReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.CTST.ToString(), Consts.EQP.CTST.ToString() + "EQPAlarmReport");
        //    ReplyData.ItemCollection[PLCEventItem.AlarmID] = "1";
        //    ReplyData.ItemCollection[PLCEventItem.AlarmLevel] = "1";
        //    ReplyData.ItemCollection[PLCEventItem.AlarmStatus] = "1";
        //    EQPAlarmReportHandler.Execute(context, ReplyData);
        //}

        //private DelegateCommand eqpStatusChangeCommandCommand;
        //public DelegateCommand EQPStatusChangeCommandCommand
        //{
        //    get
        //    {
        //        if (eqpStatusChangeCommandCommand == null) eqpStatusChangeCommandCommand = new DelegateCommand(EQPStatusChangeCommand);
        //        return eqpStatusChangeCommandCommand;
        //    }
        //}
        //private void EQPStatusChangeCommand()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var EQPStatusChangeReportHandler = new EQPStatusChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.CTST.ToString(), Consts.EQP.CTST.ToString() + "EQPStatusChangeReport");
        //    ReplyData.ItemCollection[PLCEventItem.EQPStatus] = "1";
        //    ReplyData.ItemCollection[PLCEventItem.ReasonCode] = "1";
        //    ReplyData.ItemCollection["UnitStatus1"] = "1";
        //    ReplyData.ItemCollection["ReasonCode1"] = "1";
        //    ReplyData.ItemCollection["UnitStatus2"] = "1";
        //    ReplyData.ItemCollection["ReasonCode2"] = "1";
        //    EQPStatusChangeReportHandler.Execute(context, ReplyData);
        //}

        //private DelegateCommand fetchOutJobCommand;
        //public DelegateCommand FetchOutJobCommand
        //{
        //    get
        //    {
        //        if (fetchOutJobCommand == null) fetchOutJobCommand = new DelegateCommand(FetchOutJob);
        //        return fetchOutJobCommand;
        //    }
        //}
        //private void FetchOutJob()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var FetchOutJobReportHandler = new FetchOutJobReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "FetchOutJobReport");
        //    ReplyData.ItemCollection["PanelID"] = "1";
        //    ReplyData.ItemCollection["VCRReadPanelID"] = "1";
        //    ReplyData.ItemCollection["JobSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["PanelGrade"] = "1";
        //    ReplyData.ItemCollection["UnitPathNo"] = "1";
        //    ReplyData.ItemCollection["PTID"] = "1";
        //    ReplyData.ItemCollection["CSTID"] = "1";
        //    ReplyData.ItemCollection["QTY"] = "1";
        //    ReplyData.ItemCollection["SLOTSEL"] = "1";
        //    ReplyData.ItemCollection["SLOTNO"] = "1";
        //    ReplyData.ItemCollection["PNLID"] = "1";
        //    ReplyData.ItemCollection["PPID"] = "1";
        //    ReplyData.ItemCollection["LOTID"] = "1";
        //    ReplyData.ItemCollection["LOTJUDGE"] = "1";
        //    ReplyData.ItemCollection["LOTSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["OPERID"] = "1";
        //    ReplyData.ItemCollection["PRODID"] = "1";
        //    ReplyData.ItemCollection["SMPLFLAG"] = "1";
        //    ReplyData.ItemCollection["RWKCNT"] = "1";
        //    ReplyData.ItemCollection["CFGLSID"] = "1";
        //    ReplyData.ItemCollection["LOTTYPE"] = "1";
        //    ReplyData.ItemCollection["SPNLID"] = "1";
        //    ReplyData.ItemCollection["PNLJUDGE"] = "1";
        //    ReplyData.ItemCollection["PNLGRADE"] = "1";
        //    ReplyData.ItemCollection["PNLSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["ATPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["ASSYPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["OSREPAIR"] = "1";
        //    ReplyData.ItemCollection["ATREPAIR"] = "1";
        //    ReplyData.ItemCollection["UNITID"] = "1";
        //    FetchOutJobReportHandler.Execute(context, ReplyData);
        //}

        //private DelegateCommand inUnitReportCommand;
        //public DelegateCommand InUnitReportCommand
        //{
        //    get
        //    {
        //        if (inUnitReportCommand == null) inUnitReportCommand = new DelegateCommand(InUnitReport);
        //        return inUnitReportCommand;
        //    }
        //}
        //private void InUnitReport()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var InUnitReportHandler = new InUnitReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.CTST.ToString(), Consts.EQP.CTST.ToString() + "InUnitReport");
        //    ReplyData.ItemCollection["PanelID"] = "1";
        //    ReplyData.ItemCollection["VCRReadPanelID"] = "1";
        //    ReplyData.ItemCollection["JobSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["PanelGrade"] = "1";
        //    ReplyData.ItemCollection["UnitPathNo"] = "1";
        //    ReplyData.ItemCollection["PTID"] = "1";
        //    ReplyData.ItemCollection["CSTID"] = "1";
        //    ReplyData.ItemCollection["QTY"] = "1";
        //    ReplyData.ItemCollection["SLOTSEL"] = "1";
        //    ReplyData.ItemCollection["SLOTNO"] = "1";
        //    ReplyData.ItemCollection["PNLID"] = "1";
        //    ReplyData.ItemCollection["PPID"] = "1";
        //    ReplyData.ItemCollection["LOTID"] = "1";
        //    ReplyData.ItemCollection["LOTJUDGE"] = "1";
        //    ReplyData.ItemCollection["LOTSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["OPERID"] = "1";
        //    ReplyData.ItemCollection["PRODID"] = "1";
        //    ReplyData.ItemCollection["SMPLFLAG"] = "1";
        //    ReplyData.ItemCollection["RWKCNT"] = "1";
        //    ReplyData.ItemCollection["CFGLSID"] = "1";
        //    ReplyData.ItemCollection["LOTTYPE"] = "1";
        //    ReplyData.ItemCollection["SPNLID"] = "1";
        //    ReplyData.ItemCollection["PNLJUDGE"] = "1";
        //    ReplyData.ItemCollection["PNLGRADE"] = "1";
        //    ReplyData.ItemCollection["PNLSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["ATPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["ASSYPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["OSREPAIR"] = "1";
        //    ReplyData.ItemCollection["ATREPAIR"] = "1";
        //    ReplyData.ItemCollection["UNITID"] = "1";
        //    InUnitReportHandler.Execute(context, ReplyData);
        //}

        
        //private DelegateCommand outUnitReportCommand;
        //public DelegateCommand OutUnitReportCommand
        //{
        //    get
        //    {
        //        if (outUnitReportCommand == null) outUnitReportCommand = new DelegateCommand(OutUnitReport);
        //        return outUnitReportCommand;
        //    }
        //}
        //private void OutUnitReport()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var OutUnitReportHandler = new OutUnitReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.CTST.ToString(), Consts.EQP.CTST.ToString() + "OutUnitReport");
        //    ReplyData.ItemCollection["PanelID"] = "1";
        //    ReplyData.ItemCollection["VCRReadPanelID"] = "1";
        //    ReplyData.ItemCollection["JobSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["PanelGrade"] = "1";
        //    ReplyData.ItemCollection["UnitPathNo"] = "1";
        //    ReplyData.ItemCollection["PTID"] = "1";
        //    ReplyData.ItemCollection["CSTID"] = "1";
        //    ReplyData.ItemCollection["QTY"] = "1";
        //    ReplyData.ItemCollection["SLOTSEL"] = "1";
        //    ReplyData.ItemCollection["SLOTNO"] = "1";
        //    ReplyData.ItemCollection["PNLID"] = "1";
        //    ReplyData.ItemCollection["PPID"] = "1";
        //    ReplyData.ItemCollection["LOTID"] = "1";
        //    ReplyData.ItemCollection["LOTJUDGE"] = "1";
        //    ReplyData.ItemCollection["LOTSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["OPERID"] = "1";
        //    ReplyData.ItemCollection["PRODID"] = "1";
        //    ReplyData.ItemCollection["SMPLFLAG"] = "1";
        //    ReplyData.ItemCollection["RWKCNT"] = "1";
        //    ReplyData.ItemCollection["CFGLSID"] = "1";
        //    ReplyData.ItemCollection["LOTTYPE"] = "1";
        //    ReplyData.ItemCollection["SPNLID"] = "1";
        //    ReplyData.ItemCollection["PNLJUDGE"] = "1";
        //    ReplyData.ItemCollection["PNLGRADE"] = "1";
        //    ReplyData.ItemCollection["PNLSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["ATPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["ASSYPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["OSREPAIR"] = "1";
        //    ReplyData.ItemCollection["ATREPAIR"] = "1";
        //    ReplyData.ItemCollection["UNITID"] = "1";
        //    OutUnitReportHandler.Execute(context, ReplyData);
        //}

        
        //private DelegateCommand port1CassetteStatusCommand;
        //public DelegateCommand Port1CassetteStatusCommand
        //{
        //    get
        //    {
        //        if (port1CassetteStatusCommand == null) port1CassetteStatusCommand = new DelegateCommand(Port1CassetteStatus);
        //        return port1CassetteStatusCommand;
        //    }
        //}
        //private void Port1CassetteStatus()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var Port1CassetteStatusChangeReportHandler = new Port1CassetteStatusChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "Port1CassetteStatusChangeReport");
        //    ReplyData.ItemCollection["UnitID"] = "1";
        //    ReplyData.ItemCollection["PortNo"] = "1";
        //    ReplyData.ItemCollection["CassetteStatus"] = "1";
        //    ReplyData.ItemCollection["CassetteID"] = "1";
        //    ReplyData.ItemCollection["PanelQTY"] = "1";
        //    ReplyData.ItemCollection["CassetteSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["CompleteCode"] = "1";        
        //    Port1CassetteStatusChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand port1EnableModeCommand;
        //public DelegateCommand Port1EnableModeCommand
        //{
        //    get
        //    {
        //        if (port1EnableModeCommand == null) port1EnableModeCommand = new DelegateCommand(Port1EnableMode);
        //        return port1EnableModeCommand;
        //    }
        //}
        //private void Port1EnableMode()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var Port1EnableModeChangeReportHandler = new Port1EnableModeChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "Port1EnableModeChangeReport");
        //    ReplyData.ItemCollection["PortMode"] = "1";
        
        //    Port1EnableModeChangeReportHandler.Execute(context, ReplyData);
        //}

        //private DelegateCommand port1StatusCommand;
        //public DelegateCommand Port1StatusCommand
        //{
        //    get
        //    {
        //        if (port1StatusCommand == null) port1StatusCommand = new DelegateCommand(Port1Status);
        //        return port1StatusCommand;
        //    }
        //}
        //private void Port1Status()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var Port1StatusChangeReportHandler = new Port1StatusChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "Port1StatusChangeReport");
        //    ReplyData.ItemCollection["UnitID"] = "1";
        //    ReplyData.ItemCollection["PortNo"] = "1";
        //    ReplyData.ItemCollection["PortStatus"] = "1";
        //    ReplyData.ItemCollection["CassetteID"] = "1";
        //    ReplyData.ItemCollection["PanelQTY"] = "1";
        //    ReplyData.ItemCollection["CassetteSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["PanelInSlotExistedOrNot"] = "1";

        //    Port1StatusChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand port1TransferModeCommand;
        //public DelegateCommand Port1TransferModeCommand
        //{
        //    get
        //    {
        //        if (port1TransferModeCommand == null) port1TransferModeCommand = new DelegateCommand(Port1TransferMode);
        //        return port1TransferModeCommand;
        //    }
        //}
        //private void Port1TransferMode()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var Port1TransferModeChangeReportHandler = new Port1TransferModeChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "Port1TransferModeChangeReport");
        //    ReplyData.ItemCollection["TransferMode"] = "1";
          
        //    Port1TransferModeChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand portTypeChangeCommand;
        //public DelegateCommand PortTypeChangeCommand
        //{
        //    get
        //    {
        //        if (portTypeChangeCommand == null) portTypeChangeCommand = new DelegateCommand(PortTypeChange);
        //        return portTypeChangeCommand;
        //    }
        //}
        //private void PortTypeChange()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var PortTypeChangeReportHandler = new PortTypeChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "PortTypeChangeReport");
        //    ReplyData.ItemCollection["PortNo"] = "1";
        //    ReplyData.ItemCollection["PortType"] = "1";
        //    PortTypeChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand portUseTypeCommand;
        //public DelegateCommand PortUseTypeCommand
        //{
        //    get
        //    {
        //        if (portUseTypeCommand == null) portUseTypeCommand = new DelegateCommand(PortUseType);
        //        return portUseTypeCommand;
        //    }
        //}
        //private void PortUseType()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var PortUseTypeChangeReportHandler = new PortUseTypeChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "PortUseTypeChangeReport");
        //    ReplyData.ItemCollection["PortNo"] = "1";
        //    ReplyData.ItemCollection["PortUseType"] = "1";
        //    PortUseTypeChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand recipeChangeCommand;
        //public DelegateCommand RecipeChangeCommand
        //{
        //    get
        //    {
        //        if (recipeChangeCommand == null) recipeChangeCommand = new DelegateCommand(RecipeChange);
        //        return recipeChangeCommand;
        //    }
        //}
        //private void RecipeChange()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var RecipeChangeReportHandler = new RecipeChangeReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.CTST.ToString(), Consts.EQP.CTST.ToString() + "RecipeChangeReport");
        //    ReplyData.ItemCollection["RecipeID"] = "1";
        //    ReplyData.ItemCollection["Type"] = "1";
        //    RecipeChangeReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand removeJobCommand;
        //public DelegateCommand RemoveJobCommand
        //{
        //    get
        //    {
        //        if (removeJobCommand == null) removeJobCommand = new DelegateCommand(RemoveJob);
        //        return removeJobCommand;
        //    }
        //}
        //private void RemoveJob()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var RemoveJobReportHandler = new RemoveJobReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "RemoveJobReport");
        //    ReplyData.ItemCollection["PanelID"] = "1";
        //    ReplyData.ItemCollection["UnitID"] = "1";
        //    ReplyData.ItemCollection["RemovedFlag"] = "1";
        //    RemoveJobReportHandler.Execute(context, ReplyData);
        //}
        //private DelegateCommand storeJobCommand;
        //public DelegateCommand StoreJobCommand
        //{
        //    get
        //    {
        //        if (storeJobCommand == null) storeJobCommand = new DelegateCommand(StoreJob);
        //        return storeJobCommand;
        //    }
        //}
        //private void StoreJob()
        //{
        //    var context = GetCCLinkPLCContext();
        //    var StoreJobReportHandler = new StoreJobReportHandler();
        //    PLCData ReplyData = new PLCData(Consts.EQP.Robot.ToString(), Consts.EQP.Robot.ToString() + "StoreJobReport");
        //    ReplyData.ItemCollection["PanelID"] = "1";
        //    ReplyData.ItemCollection["VCRReadPanelID"] = "1";
        //    ReplyData.ItemCollection["JobSequenceNumber"] = "1";
        //    ReplyData.ItemCollection["PanelGrade"] = "1";
        //    ReplyData.ItemCollection["UnitPathNo"] = "1";
        //    ReplyData.ItemCollection["PTID"] = "1";
        //    ReplyData.ItemCollection["CSTID"] = "1";
        //    ReplyData.ItemCollection["QTY"] = "1";
        //    ReplyData.ItemCollection["SLOTSEL"] = "1";
        //    ReplyData.ItemCollection["SLOTNO"] = "1";
        //    ReplyData.ItemCollection["PNLID"] = "1";
        //    ReplyData.ItemCollection["PPID"] = "1";
        //    ReplyData.ItemCollection["LOTID"] = "1";
        //    ReplyData.ItemCollection["LOTJUDGE"] = "1";
        //    ReplyData.ItemCollection["LOTSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["OPERID"] = "1";
        //    ReplyData.ItemCollection["PRODID"] = "1";
        //    ReplyData.ItemCollection["SMPLFLAG"] = "1";
        //    ReplyData.ItemCollection["RWKCNT"] = "1";
        //    ReplyData.ItemCollection["CFGLSID"] = "1";
        //    ReplyData.ItemCollection["LOTTYPE"] = "1";
        //    ReplyData.ItemCollection["SPNLID"] = "1";
        //    ReplyData.ItemCollection["PNLJUDGE"] = "1";
        //    ReplyData.ItemCollection["PNLGRADE"] = "1";
        //    ReplyData.ItemCollection["PNLSORTTYPE"] = "1";
        //    ReplyData.ItemCollection["ATPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["ASSYPNLGRADE"] = "1";
        //    ReplyData.ItemCollection["OSREPAIR"] = "1";
        //    ReplyData.ItemCollection["ATREPAIR"] = "1";
        //    ReplyData.ItemCollection["UNITID"] = "1";
        //    StoreJobReportHandler.Execute(context, ReplyData);
        //}
        
    }
}
