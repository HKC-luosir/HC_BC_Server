using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class Linksignal : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members
        public static int Quanjuid = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        public Linksignal()
        {
            //DownstreamLinkSignal = new DownstreamLinkSignal();
            //UpstreamLinkSignal = new UpstreamLinkSignal();
            //InOutType = EnumInOutType.InOut;
        }
        //public bool IsUpstream { get; set; }
        public string UnitName { get; set; }
        //public int ModeNo { get; set; }
       // public int ModelPosition { get; set; }
        //public string SUnitName { get; set; }
        //public string OUnitName { get; set; }
        //public string OSUnitName { get; set; }
        //public string SendGlassID { get; set; }
        //public string SendLotID { get; set; }
        //public int SendSlotNo { get; set; }
        //public int SendUnitPathNo { get; set; }
        //public SPanelInfo UpSendJobData { get; set; }
        //public SPanelInfo LowSendJobData { get; set; }
        //public string ReceiveGlassID { get; set; }
        //public string ReceiveLotID { get; set; }
        //public int ReceiveSlotNo { get; set; }
        //public int ReceiveUnitPathNo { get; set; }
        //public bool IsPutFirst { get; set; }
        //public bool IsGetDelay { get; set; }
        //public int GetDelayTime { get; set; }
        //public DateTime GetReadyTime { get; set; }
        // public EnumInOutType InOutType { get; set; }
        /// <summary>
        /// 1下手臂 2上手臂
        /// </summary>
        public int LinkType { get; set; }
        public object LinkSignalItem { get; set; }
        //public DownstreamLinkSignal DownstreamLinkSignal { get; set; }
        //public UpstreamLinkSignal UpstreamLinkSignal { get; set; }
        public string LinkName{ get; set; }
        //public bool CanLinksignalPut { get; set; }
        //public bool CanLinksignalGet { get; set; }
        //public void UpdateCanLinkSignalGetPut(Robot robot, ILog logger)
        //{
        //    try
        //    {
        //        //if (UnitName == "Rubbing1" || UnitName == "USC1")
        //        //{
        //        //    if (CheckLinkStatusReceive(this) || (!robot.LowerExistOn && !robot.UpperExistOn))
        //        //    {
        //        //        CanLinksignalPut = true;
        //        //    }
        //        //    else
        //        //        CanLinksignalPut = false;
        //        //}
        //        //else if (UnitName == "LaserCutting")  //sendable , 手臂上的片是要进lasercut
        //        //{
        //        //    var robotGlass = robot.UpperExistOn ? robot.UpHandGlass : robot.LowHandGlass;
        //        //    if (robotGlass == null)
        //        //    {
        //        //        CanLinksignalPut = true;
        //        //    }
        //        //    else
        //        //    {
        //        //        //if (robotGlass.OutEQPName == "Indexer1")
        //        //        //{
        //        //        //    CanLinksignalPut = true;
        //        //        //}
        //        //        //else
        //        //        //    CanLinksignalPut = false;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    CanLinksignalPut = false;
        //        //    CanLinksignalGet = false;
        //        //}
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
           
        //}
        //public bool CheckLinkStatusReceive(Linksignal link)
        //{
        //    try
        //    {
        //        //if (link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble && (link.DownstreamLinkSignal.ReceiveAble && !link.DownstreamLinkSignal.ReceiveStart && link.DownstreamLinkSignal.StageInterlock))
        //        //    return true;
        //        if (link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble )
        //            return true;
        //        //Logger.DebugFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Receive)", link.EQPName + " " + link.UnitName);
        //        //Logger.DebugFormat("+++ Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}, ReceiveAbleOn : {2}, ReceiveStartOn : {3},StageInterlockOn : {4}",
        //        //link.DownstreamLinkSignal.DownstreamInline, link.DownstreamLinkSignal.DownstreamTrouble, link.DownstreamLinkSignal.ReceiveAble, link.DownstreamLinkSignal.ReceiveStart, link.DownstreamLinkSignal.StageInterlock);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger.Error(ex);
        //    }
        //    return false;
        //}

        //public bool CheckLinkStatusSend(Linksignal link)
        //{
        //    try
        //    {
        //        if (link.UpstreamLinkSignal.UpstreamInline
        //            && !link.UpstreamLinkSignal.UpstreamTrouble
        //            && link.UpstreamLinkSignal.SendAble
        //            && !link.UpstreamLinkSignal.SendStart)
        //            return true;
        //        //Logger.DebugFormat("+++ LinkSignal : {0} Status MisMatch (Source Send) ", link.EQPName + " " + link.UnitName);
        //        //Logger.DebugFormat("+++ Current Send Status => UpstreamInline : {0}, UpstreamTrouble :{1}, SendAble : {2}, SendStart : {3},",
        //        //link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger.Error(ex);
        //    }
        //    return false;
        //}
        //public bool CheckLinkStatusReceiveAndExchange(Linksignal link)
        //{
        //    try
        //    {
        //        //if ((link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble && link.DownstreamLinkSignal.ReceiveAble && !link.DownstreamLinkSignal.ReceiveStart && link.DownstreamLinkSignal.StageInterlock) ||
        //        //    (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible && link.UpstreamLinkSignal.StageInterlock))
        //        if ((link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble ) ||
        //            (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible ))
        //            return true;
        //        //Logger.DebugFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Receive and Exchange)", link.EQPName + " " + link.UnitName);
        //        //Logger.DebugFormat("+++ Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}, ReceiveAble : {2}, ReceiveStart : {3}, StageInterlock : {4}", link.DownstreamLinkSignal.DownstreamInline, link.DownstreamLinkSignal.DownstreamTrouble, link.DownstreamLinkSignal.ReceiveAble, link.DownstreamLinkSignal.ReceiveStart, link.DownstreamLinkSignal.StageInterlock);
        //        //Logger.DebugFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}, StageInterlock : {5}", link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart, link.UpstreamLinkSignal.ExchangePossible, link.UpstreamLinkSignal.StageInterlock);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger.Error(ex);
        //    }
        //    return false;
        //}
        //public bool CheckLinkStatusExchange(Linksignal link)
        //{
        //    try
        //    {
        //        //if (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible && link.UpstreamLinkSignal.StageInterlock)
        //        if (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible )
        //        {
        //            //Logger.DebugFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}, StageInterlock : {5}", link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart, link.UpstreamLinkSignal.ExchangePossible, link.UpstreamLinkSignal.StageInterlock);
        //            return true;
        //        }
        //        //Logger.DebugFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Exchange)", link.EQPName + " " + link.UnitName);
        //        //Logger.DebugFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}, StageInterlock : {5}", link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart, link.UpstreamLinkSignal.ExchangePossible, link.UpstreamLinkSignal.StageInterlock);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Logger.Error(ex);
        //    }
        //    return false;
        //}
    }
    [Serializable]
    public class DownstreamLinkSignal : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members
        public static int Quanjuid = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }


        public DownstreamLinkSignal()
        { }

        public bool DownstreamInline { get; set; }
        public bool DownstreamTrouble { get; set; }
        public bool StretchComplete { get; set; }
        public bool SlotNumber1 { get; set; }
        public bool SlotNumber2 { get; set; }
        public bool SlotNumber3 { get; set; }
        public bool SlotNumber4 { get; set; }
        public bool SlotNumber5 { get; set; }
        public bool SlotNumber6 { get; set; }
        public bool ReceiveReady { get; set; }
        public bool SlotPairFlag { get; set; }
        public bool ArmSlotPairFlag { get; set; }
        public bool JobTransferSignal1 { get; set; }
        public bool JobTransferSignal2 { get; set; }
        public bool ReceiveAble { get; set; }
        public bool ReceiveStart { get; set; }
        public bool ReceiveComplete { get; set; }
        public bool ExchangePossible { get; set; }
        public bool ExchangeExecute { get; set; }
        public bool ResumeRequest { get; set; }
        public bool ResumeAck { get; set; }
        public bool ResumeNack { get; set; }
        public bool CancelRequest { get; set; }
        public bool CancelAck { get; set; }
        public bool CancelNack { get; set; }
        public bool ConveyerState { get; set; }
        public bool ShutterState { get; set; }
        public bool PinState { get; set; }
        public bool RobotInterlock { get; set; }
        public bool RobotVacuum { get; set; }
        public bool GripUp { get; set; }
        public bool GripComplete { get; set; }
        public bool PositionFront1 { get; set; }
        public bool PositionBack1 { get; set; }
        public bool PositionFront2 { get; set; }
        public bool PositionBack2 { get; set; }
    }

    public class UpstreamLinkSignal : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members
        public static int Quanjuid = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        public UpstreamLinkSignal()
        { }
        public bool UpstreamInline { get; set; }
        public bool UpstreamTrouble { get; set; }
        public bool StretchComplete { get; set; }
        public bool SlotNumber1 { get; set; }
        public bool SlotNumber2 { get; set; }
        public bool SlotNumber3 { get; set; }
        public bool SlotNumber4 { get; set; }
        public bool SlotNumber5 { get; set; }
        public bool SlotNumber6 { get; set; }
        public bool SendReady { get; set; }
        public bool SlotPairFlag { get; set; }
        public bool ArmSlotPairFlag { get; set; }
        public bool JobTransferSignal1 { get; set; }
        public bool JobTransferSignal2 { get; set; }
        public bool SendAble { get; set; }
        public bool SendStart { get; set; }
        public bool SendComplete { get; set; }
        public bool ExchangePossible { get; set; }
        public bool ExchangeExecute { get; set; }
        public bool ResumeRequest { get; set; }
        public bool ResumeAck { get; set; }
        public bool ResumeNack { get; set; }
        public bool CancelRequest { get; set; }
        public bool CancelAck { get; set; }
        public bool CancelNack { get; set; }
        public bool ConveyerState { get; set; }
        public bool ShutterState { get; set; }
        public bool PinState { get; set; }
        public bool RobotInterlock { get; set; }
        public bool RobotVacuum { get; set; }
        public bool GripUp { get; set; }
        public bool GripComplete { get; set; }
        public bool PositionFront1 { get; set; }
        public bool PositionBack1 { get; set; }
        public bool PositionFront2 { get; set; }
        public bool PositionBack2 { get; set; }
    }
    public class Transfer
    {
        public int UnitPathNo { get; set; }
        public int SlotNo { get; set; }
    }
}

