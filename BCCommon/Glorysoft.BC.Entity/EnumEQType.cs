using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public enum MESResult
    {
        SUCCESS = 0,
        FAIL = 1
    }
    [Flags]
    public enum EnumEQType
    {
        [EnumMember]
        Invalid = 0, [EnumMember]
        Normal = 1, [EnumMember]
        Loader = 2, [EnumMember]
        Unloader = 3, [EnumMember]
        VCR = 4, [EnumMember]
        MIO = 5, [EnumMember]
        VCR_MIO = 6
    }

    [Flags]
    public enum EnumPortID
    {
        [EnumMember]
        P01, [EnumMember]
        P02, [EnumMember]
        P03, [EnumMember]
        P04, [EnumMember]
        P05, [EnumMember]
        P06, [EnumMember]
        P07, [EnumMember]
        P08, [EnumMember]
        P09, [EnumMember]
        P10, [EnumMember]
        P11
    }
    [Flags]
    public enum EnumInOutType
    {
        [EnumMember]
        In = 0, [EnumMember]
        Out = 1, [EnumMember]
        InOut = 2
    }
    [Flags]
    public enum EnumLineRunMode
    {
        [EnumMember]
        MassProductionMode = 1, [EnumMember]
        PilotRun = 2, [EnumMember]
        DummyMode = 3, [EnumMember]
        SortingMode = 4
    }
    /// None 0
    /// Robot Home 1
    /// Transfer 2
    /// Move 3
    /// Get 4
    /// Put 5
    /// One Action Exchange 6
    /// Two Action Exchange 7
    /// Command Clear 8
    /// One Action Batch Get 9
    /// One Action Batch Put 10
    /// Two Action Batch Get 11
    /// Two Action Batch Put 12
    [Flags]
    public enum RobotMotion
    {
        [EnumMember]
        None = 0, [EnumMember]
        RobotHome = 1, [EnumMember]
        Transfer = 2, [EnumMember]
        Move = 3, [EnumMember]
        Get = 4, [EnumMember]
        Put = 5, [EnumMember]
        OneActionExchange = 6, [EnumMember]
        TwoActionExchange = 7, [EnumMember]
        CommandClear = 8, [EnumMember]
        OneActionBatchGet = 9, [EnumMember]
        OneActionBatchPut = 10, [EnumMember]
        TwoActionBatchGet = 11, [EnumMember]
        TwoActionBatchPut = 12
    }
    [Flags]
    public enum EStageType
    {
        [EnumMember]
        Single = 0, [EnumMember]
        Multi = 1
    }
    public enum ESubStageType
    {
        [EnumMember]
        One = 1,
        [EnumMember]
        Two = 2
    }
    [Flags]
    public enum RobotHand
    {
        [EnumMember]
        Error = -1, [EnumMember]
        LowHand = 1, [EnumMember]
        UpHand = 2, [EnumMember]
        AllArm = 99
        //[EnumMember]       
        //DoubleHand = 3, [EnumMember]
        //FourHand = 4
    }
    [Flags]
    public enum RobotState
    {
        [EnumMember]
        None = 0, [EnumMember]
        PowerOff = 1, [EnumMember]
        Trouble, [EnumMember]
        ReadyForStart, [EnumMember]
        WaitingForCmd, [EnumMember]
        Busy, [EnumMember]
        Hold
    }
    [Flags]
    public enum EnumRobotDispatchMode
    {
        [EnumMember]
        MANUAL = 1, [EnumMember]
        AUTO = 2
    }
    [Flags]
    public enum EnumUnitType
    {
        [EnumMember]
        Loader = 1,
        [EnumMember]
        Unloader = 2,
        [EnumMember]
        ProcessUnit = 3,
        [EnumMember]
        BufferUnit = 4,
        [EnumMember]
        Stage = 5,
        [EnumMember]
        Robot = 6
    }

    [Flags]
    public enum EnumPortType
    {
        [EnumMember]
        Input = 1, [EnumMember]
        Output = 2, [EnumMember]
        InputOutput = 3, [EnumMember]
        BB = 4, [EnumMember]
        BL = 5, [EnumMember]
        BU = 6
    }

    [Flags]
    public enum EnumTransferMode
    {
        [EnumMember]
        MGVMode = 1, [EnumMember]
        AGVMode = 2, [EnumMember]
        StockerMode = 3, [EnumMember]
        MCTMode = 4, [EnumMember]
        PNPMode = 5
    }
    [Flags]
    public enum EnumPortOperationMode
    {
        [EnumMember]
        AutoMode = 1, [EnumMember]
        SemiAutoMode = 2, [EnumMember]
        ManualMode = 3
    }

    [Flags]
    public enum EnumPortUseType
    {
        [EnumMember]
        GA = 1, [EnumMember]
        PA = 2, [EnumMember]
        SA = 3, [EnumMember]
        QA = 4

    }
    [Flags]
    public enum EnumLoadUnloadType
    {
        [EnumMember]
        Both = 0, [EnumMember]
        Load = 1, [EnumMember]
        Unload = 2
    }

    //public enum EnumEqpStatus
    //{
    //    [EnumMember]
    //    Unkown = 0, [EnumMember]
    //    IDLE = 1, [EnumMember]
    //    RUN = 2, [EnumMember]
    //    DOWN = 3, [EnumMember]
    //    PM = 4
    //}
    [Flags]
    public enum EnumEqpAutoMode
    {
        [EnumMember]
        MANUAL = 2, [EnumMember]
        AUTO = 1
    }
    [Flags]
    public enum EnumLineType
    {
        [EnumMember]
        AGING = 1,
        [EnumMember]
        ASY = 2,
        [EnumMember]
        CAP = 3,
        [EnumMember]
        DOM = 4,
        [EnumMember]
        FTS = 5,
        [EnumMember]
        OLB = 6,
        [EnumMember]
        PRW = 7,
        [EnumMember]
        BRW = 8,
        //[EnumMember]
        //Normal = 101, 
        //[EnumMember]
        //TP = 102, 
        //[EnumMember]
        //V3 = 103,
        //[EnumMember]
        //CF = 104,
        //[EnumMember]
        //NIKON = 105,
        //[EnumMember]
        //ARRAY = 106,
        //[EnumMember]
        //MOD = 107
    }

    [Flags]
    public enum EnumControlState
    {
        [EnumMember]
        OffLine = 0,
        [EnumMember]
        OnLineRemote = 1,
        [EnumMember]
        OnLineLocal = 2
    }
    [Flags]
    public enum EnumGlassSlotStatus
    {
        [EnumMember]
        Empty = 0,//empty
        [EnumMember]
        ProcessEnd = 1,//normal process end
        [EnumMember]
        Wait = 2,//wait
        [EnumMember]
        Skip = 3,//skip
        [EnumMember]
        Fail = 4,//fail
        [EnumMember]
        Processing = 5,//processing
        [EnumMember]
        Recovery = 6,//Recovery 
        [EnumMember]
        Removed = 7,//Removed 
        [EnumMember]
        Scrap = 8,//Scrap 
    }
    [Flags]
    public enum EnumPortStatus
    {
        [EnumMember]
        Empty = 1,
        [EnumMember]
        LoadReady = 2,
        [EnumMember]
        InUse = 3,
        [EnumMember]
        UnloadReady = 4,
        [EnumMember]
        Blocked = 5
    }
    // 1)No Cassette:2)Waiting for Cassette Data: 3)	Waiting for Start Command: 
    //4)	Waiting for Processing;5)	In Processing;6)	Process Completed:7)	Process Paused: 
    [Flags]
    public enum EnumCarrierStatus
    {
        [EnumMember]
        NoCassette = 1, [EnumMember]
        WaitingforCassetteData = 2, [EnumMember]
        WaitingforStartCommand = 3, [EnumMember]
        WaitingforProcessing = 4, [EnumMember]
        InProcessing = 5, [EnumMember]
        ProcessPaused = 6, [EnumMember]
        ProcessCompleted = 7, [EnumMember]
        CassetteProcessAbort = 8, [EnumMember]
        CassetteProcessCancel = 9
    }
    [Flags]
    public enum LineMode
    {
        [EnumMember]
        Normal = 1, 
        [EnumMember]
        Packing = 2,
        [EnumMember]
        UnPacking = 3
    }
    /// <summary>
    ///ASC:层数从小到大取片
    ///DESC:层数从大到小取片
    /// </summary>
    [Flags]
    public enum PortGetType
    {
        [EnumMember]
        ASC = 1,
        [EnumMember]
        DESC = 2
    }
    [Flags]
    public enum EnumCassetteControlCommand
    {
        [EnumMember]
        CassetteProcessStart = 1,
        [EnumMember]
        CassetteProcessStartByCount = 2,
        [EnumMember]
        CassetteProcessPause = 3,
        [EnumMember]
        CassetteProcessResume = 4,
        [EnumMember]
        CassetteProcessCancel = 5,
        [EnumMember]
        CassetteProcessAbort = 6,
        [EnumMember]
        CassetteProcessEnd = 7
    }
    //public class ProcessMode
    //{
    //    public const string Normal = "Normal";
    //    public const string ColdRun = "ColdRun";
    //    public const string OnlyA = "OnlyA";
    //    public const string OnlyB = "OnlyB";
    //    public const string MixRun = "MixRun";
    //}

}
