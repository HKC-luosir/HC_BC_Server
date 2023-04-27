using System;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using log4net;

namespace Glorysoft.BC.GlassDispath
{
    internal class CommandFactory
    {
        public string Name { get; private set; }
        public CommandFactory(string name)
        {
            Name = name;
        }
        #region Create Robot Command

        public RobotCommand CreateOnlyBPortPutCommand(PortInfo port, int targetModelPosition, int PositionA, RobotHand robotHandA, int SlotPostionA,
             int PositionB, RobotHand robotHandB, int SlotPostionB, int priority)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Put,
                    STArmNo1 = robotHandB,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = PositionB,
                    STPutSlotPostion1 = SlotPostionB,

                    NDRCMD2 = RobotMotion.Put,
                    NDArmNo2 = robotHandA,
                    NDPutPosition2 = targetModelPosition,
                    NDPutSlotNo2 = PositionA,
                    NDPutSlotPostion2 = SlotPostionA,

                    Name = Name,
                    Priority = priority,
                    RobotHand = robotHandA,
                    Unit = port
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }


        public RobotCommand CreateMultiChamberPortPutCommand(PortInfo port, int ModelPositionA, int ModelPositionB,
            GlassInfo glassA, GlassInfo glassB, RobotHand handA, RobotHand handB, int priority, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Put,
                    //SlotNo = slotNo,
                    //StagePathNo = port.UnitPathNo,
                    //StageType = EStageType.Multi,
                    //SubCommandStageType = port.TransferMode == "1" ? ESubStageType.One : ESubStageType.Two,
                    STRCMD1 = RobotMotion.Put,
                    STArmNo1 = handA,
                    STPutPosition1 = ModelPositionA,
                    STPutSlotNo1 = glassA.Position,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

                    NDRCMD2 = RobotMotion.Put,
                    NDArmNo2 = handB,
                    NDPutPosition2 = ModelPositionB,
                    NDPutSlotNo2 = glassB.Position,
                    NDPutSlotPostion2 = SlotPostion,

                    //GlassThickness = handJob.GlassThickNess,
                    // JobData = handJob,
                    Name = Name,
                    Priority = priority,
                    RobotHand = RobotHand.AllArm,
                    Unit = port
                };
                return cmd;
                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2},STPutSlotNo1:{3},STPutSlotPostion1:{4}",
                //cmd.STRCMD1,cmd.STArmNo1,cmd.STPutPosition1,cmd.STPutSlotNo1,cmd.STPutSlotPostion1);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }

        public RobotCommand CreateMultiChamberEQPToPortCommand(PortInfo port, int GetModelPosition,
            int GetSlotNo, int PutModelPositionA, int PutModelPositionB, GlassInfo glassA, GlassInfo glassB, int priority, int SlotPostion)
        {
            try
            {
                if (Math.Abs(glassA.Position - glassB.Position) == 1 && PutModelPositionA == PutModelPositionB)
                {
                    var cmd = new RobotCommand
                    {

                        STRCMD1 = RobotMotion.TwoActionBatchGet,
                        STArmNo1 = RobotHand.AllArm,
                        STGetPosition1 = GetModelPosition,
                        STGetSlotNo1 = GetSlotNo,
                        STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

                        NDRCMD2 = RobotMotion.TwoActionBatchPut,
                        NDArmNo2 = RobotHand.AllArm,
                        NDPutPosition2 = PutModelPositionA,
                        NDPutSlotNo2 = glassA.Position,
                        NDPutSlotPostion2 = SlotPostion,

                        //GlassThickness = handJob.GlassThickNess,
                        // JobData = handJob,
                        Name = Name,
                        Priority = priority,
                        RobotHand = RobotHand.AllArm,
                        Unit = port,
                        TargetModelPosition = PutModelPositionA
                    };
                    return cmd;
                }
                else
                {
                    var cmd = new RobotCommand
                    {

                        STRCMD1 = RobotMotion.TwoActionBatchGet,
                        STArmNo1 = RobotHand.AllArm,
                        STGetPosition1 = GetModelPosition,
                        STGetSlotNo1 = GetSlotNo,
                        STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

                        NDRCMD2 = RobotMotion.Put,
                        NDArmNo2 = RobotHand.UpHand,
                        NDPutPosition2 = PutModelPositionA,
                        NDPutSlotNo2 = glassA.Position,
                        NDPutSlotPostion2 = SlotPostion,

                        RDRCMD3 = RobotMotion.Put,
                        RDArmNo3 = RobotHand.LowHand,
                        RDPutPosition3 = PutModelPositionB,
                        RDPutSlotNo3 = glassB.Position,
                        RDPutSlotPostion3 = SlotPostion,
                        //GlassThickness = handJob.GlassThickNess,
                        // JobData = handJob,
                        Name = Name,
                        Priority = priority,
                        RobotHand = RobotHand.AllArm,
                        Unit = port,
                        TargetModelPosition = PutModelPositionA
                    };
                    return cmd;
                }

                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2},STPutSlotNo1:{3},STPutSlotPostion1:{4}",
                //cmd.STRCMD1,cmd.STArmNo1,cmd.STPutPosition1,cmd.STPutSlotNo1,cmd.STPutSlotPostion1);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand CreateEQPToPortCommand(PortInfo port, int getModelPosition, int putModelPosition,
          int GetSlotNo, GlassInfo Glass, RobotHand robotHand, int priority, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Put,
                    //SlotNo = slotNo,
                    //StagePathNo = port.UnitPathNo,
                    //StageType = EStageType.Multi,
                    //SubCommandStageType = port.TransferMode == "1" ? ESubStageType.One : ESubStageType.Two,
                    STRCMD1 = RobotMotion.Get,
                    STArmNo1 = robotHand,
                    STGetPosition1 = getModelPosition,
                    STGetSlotNo1 = GetSlotNo,
                    STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

                    NDRCMD2 = RobotMotion.Put,
                    NDArmNo2 = robotHand,
                    NDPutPosition2 = putModelPosition,
                    NDPutSlotNo2 = Glass.Position,
                    NDPutSlotPostion2 = SlotPostion,

                    //GlassThickness = handJob.GlassThickNess,
                    // JobData = handJob,
                    Name = Name,
                    Priority = priority,
                    RobotHand = robotHand,
                    Unit = port,
                    TargetModelPosition = putModelPosition
                };
                return cmd;
                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2},STPutSlotNo1:{3},STPutSlotPostion1:{4}",
                //cmd.STRCMD1,cmd.STArmNo1,cmd.STPutPosition1,cmd.STPutSlotNo1,cmd.STPutSlotPostion1);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand OnlyBCreatePortGetCommand(PortInfo port, int getModelPosition, RobotHand robotHandA, int PositionA, int SlotPostionA,
           RobotHand robotHandB, int PositionB, int SlotPostionB, int priority)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Get,
                    STArmNo1 = robotHandA,
                    STGetPosition1 = getModelPosition,
                    STGetSlotNo1 = PositionA,
                    STGetSlotPostion1 = SlotPostionA,

                    NDRCMD2 = RobotMotion.Get,
                    NDArmNo2 = robotHandB,
                    NDGetPosition2 = getModelPosition,
                    NDGetSlotNo2 = PositionB,
                    NDGetSlotPostion2 = SlotPostionB,

                    // JobData = info,
                    Name = Name,
                    Priority = priority,
                    RobotHand = robotHandA,
                    Unit = port
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        #region MultiChamber
        public RobotCommand CreateMultiChamberProcessGetCommand(Unit unit, RobotHand robotHand, int priotity, int targetModelPosition, int GetSlotNo)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Get,
                    //SlotNo = slotNo,
                    //StagePathNo = UnitPathNo,
                    //StageType = EStageType.Single,
                    STRCMD1 = RobotMotion.Get,
                    STArmNo1 = robotHand,
                    STGetPosition1 = targetModelPosition,
                    STGetSlotNo1 = GetSlotNo,// info.Position,
                    STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),


                    //JobData = info,
                    Name = Name,
                    Priority = priotity,
                    RobotHand = robotHand,
                    Unit = unit
                };

                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand CreateMultiChamberProcessGetCommand(Unit unit, int priotity, int targetModelPosition, int GetSlotNoA, int GetSlotNoB)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Get,
                    //SlotNo = slotNo,
                    //StagePathNo = UnitPathNo,
                    //StageType = EStageType.Single,
                    STRCMD1 = RobotMotion.TwoActionBatchGet,
                    STArmNo1 = RobotHand.AllArm,
                    STGetPosition1 = targetModelPosition,
                    STGetSlotNo1 = GetSlotNoA,// info.Position,
                    STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),

                    //NDRCMD2 = RobotMotion.Get,
                    //NDArmNo2 = robotHandB,
                    //NDGetPosition2 = targetModelPosition,
                    //NDGetSlotNo2 = GetSlotNoB,// info.Position,
                    //NDGetSlotPostion2 = 0,//Convert.ToInt32(info.FSlotPosition),


                    //JobData = info,
                    Name = Name,
                    Priority = priotity,
                    //RobotHand = robotHand,
                    Unit = unit
                };

                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotCommand CreateMultiChamberProcessGetCommand(Unit unit, RobotHand robotHandA, RobotHand robotHandB, 
        //    int priotity, int targetModelPosition, int GetSlotNoA, int GetSlotNoB)
        //{
        //    try
        //    {
        //        var cmd = new RobotCommand
        //        {
        //            //Command = RobotMotion.Get,
        //            //SlotNo = slotNo,
        //            //StagePathNo = UnitPathNo,
        //            //StageType = EStageType.Single,
        //            STRCMD1 = RobotMotion.Get,
        //            STArmNo1 = robotHandA,
        //            STGetPosition1 = targetModelPosition,
        //            STGetSlotNo1 = GetSlotNoA,// info.Position,
        //            STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),

        //            NDRCMD2 = RobotMotion.Get,
        //            NDArmNo2 = robotHandB,
        //            NDGetPosition2 = targetModelPosition,
        //            NDGetSlotNo2 = GetSlotNoB,// info.Position,
        //            NDGetSlotPostion2 = 0,//Convert.ToInt32(info.FSlotPosition),


        //            //JobData = info,
        //            Name = Name,
        //            Priority = priotity,
        //            //RobotHand = robotHand,
        //            Unit = unit
        //        };

        //        return cmd;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        public RobotCommand CreateMultiChamberPortGetCommand(PortInfo port, int getModelPosition, int putModelPosition,
          GlassInfo glassA, GlassInfo glassB, RobotHand handA, RobotHand handB,
          int PutSlotNoA, int PutSlotNoB, int priority, int SlotPostion)
        {
            try
            {
                if (Math.Abs(glassA.Position - glassB.Position) == 1)
                {
                    var cmd = new RobotCommand
                    {

                        STRCMD1 = RobotMotion.TwoActionBatchGet,
                        STArmNo1 = RobotHand.AllArm,
                        STGetPosition1 = getModelPosition,
                        STGetSlotNo1 = glassA.Position,
                        STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

                        NDRCMD2 = RobotMotion.TwoActionBatchPut,
                        NDArmNo2 = RobotHand.AllArm,
                        NDPutPosition2 = putModelPosition,
                        NDPutSlotNo2 = PutSlotNoA,
                        NDPutSlotPostion2 = SlotPostion,

                        // JobData = info,
                        Name = Name,
                        Priority = priority,
                        //RobotHand = robotHand,
                        Unit = port,
                        TargetModelPosition = putModelPosition
                    };
                    return cmd;
                }
                else
                {
                    var cmd = new RobotCommand
                    {

                        STRCMD1 = RobotMotion.Get,
                        STArmNo1 = handB,
                        STGetPosition1 = getModelPosition,
                        STGetSlotNo1 = glassA.Position,
                        STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

                        NDRCMD2 = RobotMotion.Get,
                        NDArmNo2 = handA,
                        NDGetPosition2 = getModelPosition,
                        NDGetSlotNo2 = glassB.Position,// info.Position,
                        NDGetSlotPostion2 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

                        RDRCMD3 = RobotMotion.TwoActionBatchPut,
                        RDArmNo3 = RobotHand.AllArm,
                        RDPutPosition3 = putModelPosition,
                        RDPutSlotNo3 = PutSlotNoA,
                        RDPutSlotPostion3 = SlotPostion,

                        //NDRCMD2 = RobotMotion.Get,
                        //NDArmNo2 = robotHandB,
                        //NDGetPosition2 = getModelPosition,
                        //NDGetSlotNo2 = glassB.Position,// info.Position,
                        //NDGetSlotPostion2 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),


                        //RDRCMD3 = RobotMotion.Put,
                        //RDArmNo3 = robotHandA,
                        //RDPutPosition3 = putModelPosition,
                        //RDPutSlotNo3 = PutSlotNoA,
                        //RDPutSlotPostion3 = SlotPostion,


                        //THRCMD4 = RobotMotion.Put,
                        //THArmNo4 = robotHandB,
                        //THPutPosition4 = putModelPosition,
                        //THPutSlotNo4 = PutSlotNoB,
                        //THPutSlotPostion4 = SlotPostion,

                        // JobData = info,
                        Name = Name,
                        Priority = priority,
                        //RobotHand = robotHand,
                        Unit = port
                    };
                    return cmd;
                }


            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotCommand CreateMultiChamberPortGetCommand(PortInfo port, int getModelPosition, int putModelPosition, 
        //    GlassInfo glassA, GlassInfo glassB,RobotHand robotHandA, RobotHand robotHandB, 
        //    int PutSlotNoA, int PutSlotNoB, int priority, int SlotPostion)
        //{
        //    try
        //    {
        //        var cmd = new RobotCommand
        //        {

        //            STRCMD1 = RobotMotion.Get,
        //            STArmNo1 = robotHandA,
        //            STGetPosition1 = getModelPosition,
        //            STGetSlotNo1 = glassA.Position,
        //            STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),


        //            NDRCMD2 = RobotMotion.Get,
        //            NDArmNo2 = robotHandB,
        //            NDGetPosition2 = getModelPosition,
        //            NDGetSlotNo2 = glassB.Position,// info.Position,
        //            NDGetSlotPostion2 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),


        //            RDRCMD3 = RobotMotion.Put,
        //            RDArmNo3 = robotHandA,
        //            RDPutPosition3 = putModelPosition,
        //            RDPutSlotNo3 = PutSlotNoA,
        //            RDPutSlotPostion3 = SlotPostion,


        //            THRCMD4 = RobotMotion.Put,
        //            THArmNo4 = robotHandB,
        //            THPutPosition4 = putModelPosition,
        //            THPutSlotNo4 = PutSlotNoB,
        //            THPutSlotPostion4 = SlotPostion,

        //            // JobData = info,
        //            Name = Name,
        //            Priority = priority,
        //            //RobotHand = robotHand,
        //            Unit = port
        //        };
        //        return cmd;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        public RobotCommand CreateMultiChamberPortGetCommand(PortInfo port, int getModelPosition, int putModelPosition,
            GlassInfo glass, RobotHand robotHand, int PutSlotNo, int priority, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Get,
                    STArmNo1 = robotHand,
                    STGetPosition1 = getModelPosition,
                    STGetSlotNo1 = glass.Position,
                    STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

                    NDRCMD2 = RobotMotion.Put,
                    NDArmNo2 = robotHand,
                    NDPutPosition2 = putModelPosition,
                    NDPutSlotNo2 = PutSlotNo,
                    NDPutSlotPostion2 = SlotPostion,

                    // JobData = info,
                    Name = Name,
                    Priority = priority,
                    RobotHand = robotHand,
                    Unit = port,
                    TargetModelPosition = putModelPosition
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }

        public RobotCommand CreateMultiChamberPortPutCommand(PortInfo port, int targetModelPosition,
           GlassInfo GlassA, GlassInfo GlassB, int priority, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.TwoActionBatchPut,
                    STArmNo1 = RobotHand.AllArm,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = GlassA.Position,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

                    //NDRCMD2 = RobotMotion.Put,
                    //NDArmNo2 = robotHandB,
                    //NDPutPosition2 = targetModelPosition,
                    //NDPutSlotNo2 = GlassB.Position,
                    //NDPutSlotPostion2 = SlotPostion,

                    Name = Name,
                    Priority = priority,
                    // RobotHand = robotHand,
                    Unit = port
                };
                return cmd;
                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2},STPutSlotNo1:{3},STPutSlotPostion1:{4}",
                //cmd.STRCMD1,cmd.STArmNo1,cmd.STPutPosition1,cmd.STPutSlotNo1,cmd.STPutSlotPostion1);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotCommand CreateMultiChamberPortPutCommand(PortInfo port, int targetModelPosition,
        //    GlassInfo GlassA, GlassInfo GlassB, RobotHand robotHandA, RobotHand robotHandB, int priority, int SlotPostion)
        //{
        //    try
        //    {
        //        var cmd = new RobotCommand
        //        {

        //            STRCMD1 = RobotMotion.Put,
        //            STArmNo1 = robotHandA,
        //            STPutPosition1 = targetModelPosition,
        //            STPutSlotNo1 = GlassA.Position,
        //            STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(handJob.FSlotPosition),

        //            NDRCMD2 = RobotMotion.Put,
        //            NDArmNo2 = robotHandB,
        //            NDPutPosition2 = targetModelPosition,
        //            NDPutSlotNo2 = GlassB.Position,
        //            NDPutSlotPostion2 = SlotPostion,

        //            Name = Name,
        //            Priority = priority,
        //           // RobotHand = robotHand,
        //            Unit = port
        //        };
        //        return cmd;
        //        // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2},STPutSlotNo1:{3},STPutSlotPostion1:{4}",
        //        //cmd.STRCMD1,cmd.STArmNo1,cmd.STPutPosition1,cmd.STPutSlotNo1,cmd.STPutSlotPostion1);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        public RobotCommand CreateMultiChamberEQPToEQPCommand(Unit unit, int GetModelPosition, int GetSlotNoA,
            int PutSlotNoA, int priotity, int targetModelPosition, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.TwoActionBatchGet,
                    STArmNo1 = RobotHand.AllArm,
                    STGetPosition1 = GetModelPosition,
                    STGetSlotNo1 = GetSlotNoA,// info.Position,
                    STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),

                    NDRCMD2 = RobotMotion.TwoActionBatchPut,
                    NDArmNo2 = RobotHand.AllArm,
                    NDPutPosition2 = targetModelPosition,
                    NDPutSlotNo2 = PutSlotNoA,
                    NDPutSlotPostion2 = SlotPostion,

                    // JobData = info,
                    Name = Name,
                    Priority = priotity,
                    //RobotHand = robotHand,
                    Unit = unit,
                    TargetModelPosition = targetModelPosition
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand CreateMultiChamberEQPToEQPCommand(Unit unit, int GetModelPosition, int GetSlotNoA, RobotHand robot,
           int PutSlotNoA, int priotity, int targetModelPosition, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Get,
                    STArmNo1 = robot,
                    STGetPosition1 = GetModelPosition,
                    STGetSlotNo1 = GetSlotNoA,// info.Position,
                    STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),

                    NDRCMD2 = RobotMotion.Put,
                    NDArmNo2 = robot,
                    NDPutPosition2 = targetModelPosition,
                    NDPutSlotNo2 = PutSlotNoA,
                    NDPutSlotPostion2 = SlotPostion,

                    // JobData = info,
                    Name = Name,
                    Priority = priotity,
                    //RobotHand = robotHand,
                    Unit = unit,
                    TargetModelPosition = targetModelPosition
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand CreateMultiChamberProcessPutCommand(Unit unit, int PutSlotNo1, int PutSlotNo2, int priotity, int targetModelPosition, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.TwoActionBatchPut,
                    STArmNo1 = RobotHand.AllArm,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = PutSlotNo1,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

                    //NDRCMD2 = RobotMotion.Put,
                    //NDArmNo2 = robotHandB,
                    //NDPutPosition2 = targetModelPosition,
                    //NDPutSlotNo2 = PutSlotNo2,
                    //NDPutSlotPostion2 = SlotPostion,

                    // JobData = info,
                    Name = Name,
                    Priority = priotity,
                    //RobotHand = robotHand,
                    Unit = unit
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotCommand CreateMultiChamberProcessPutCommand(Unit unit,  RobotHand robotHandA, RobotHand robotHandB, 
        //    int PutSlotNo1,int PutSlotNo2, int priotity, int targetModelPosition, int SlotPostion)
        //{
        //    try
        //    {
        //        var cmd = new RobotCommand
        //        {

        //            STRCMD1 = RobotMotion.Put,
        //            STArmNo1 = robotHandA,
        //            STPutPosition1 = targetModelPosition,
        //            STPutSlotNo1 = PutSlotNo1,
        //            STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),

        //            NDRCMD2 = RobotMotion.Put,
        //            NDArmNo2 = robotHandB,
        //            NDPutPosition2 = targetModelPosition,
        //            NDPutSlotNo2 = PutSlotNo2,
        //            NDPutSlotPostion2 = SlotPostion,

        //            // JobData = info,
        //            Name = Name,
        //            Priority = priotity,
        //            //RobotHand = robotHand,
        //            Unit = unit
        //        };
        //        return cmd;

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}

        public RobotCommand CreateMultiChamberProcessPutCommand(Unit unit, GlassInfo Glass, RobotHand robotHand,
            int priotity, int targetModelPosition, int PutSlotNo, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Put,
                    //SlotNo = slotNo,
                    //StagePathNo = UnitPathNo,
                    //StageType = EStageType.Single,
                    STRCMD1 = RobotMotion.Put,
                    STArmNo1 = robotHand,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = PutSlotNo,//info.Position,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),




                    // JobData = info,
                    Name = Name,
                    Priority = priotity,
                    RobotHand = robotHand,
                    Unit = unit
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="robotHand"></param>
        /// <param name="priotity"></param>
        /// <param name="targetModelPosition"></param>
        /// <param name="slotPostion">0-None 1-前片 2-后片 99-前后片</param>
        /// <returns></returns>
        public List<RobotCommand> CreateProcessGetCommand(Unit unit, RobotHand robotHand, RobotModel robotmodel,ref string errinfo)
        {
            List<RobotCommand> cmds = new List<RobotCommand>();
            try
            {
                //var cmd = new RobotCommand
                //{
                //    //Command = RobotMotion.Get,
                //    //SlotNo = slotNo,
                //    //StagePathNo = UnitPathNo,
                //    //StageType = EStageType.Single,
                //    STRCMD1 = RobotMotion.Get,
                //    //STArmNo1 = robotHand,
                //    STGetPosition1 = robotmodel.ModelPosition,
                //    //STGetSlotNo1 = 1,// info.Position,
                //    //STGetSlotPostion1 = slotPostion,//Convert.ToInt32(info.FSlotPosition),
                //    //JobData = info,
                //    Name = Name,
                //    Priority = robotmodel.OutPriority,
                //    RobotHand = robotHand,
                //    Unit = unit
                //};

                string errinfo1 = "";
                string errinfo2 = "";
                if (robotHand == RobotHand.AllArm)//两个手臂都有空
                {
                    if (robotmodel.EQPSendSlotNoA != 0 && robotmodel.EQPSendSlotNoB != 0)//有两层
                    {
                        //判断两片是否同等级
                        var issamegrade1 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassA, robotmodel.GlassB, ref errinfo1);
                        var issamegrade2 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassC, robotmodel.GlassD, ref errinfo2);


                        if (issamegrade1 == true && issamegrade2 == true && String.IsNullOrEmpty(errinfo1) && String.IsNullOrEmpty(errinfo2))//两组片都是各自同级的 直接双取
                        {
                            var cmd = new RobotCommand
                            {
                                STRCMD1 = RobotMotion.OneActionBatchGet,
                                STArmNo1 = RobotHand.AllArm,
                                STGetPosition1 = robotmodel.ModelPosition,
                                STGetSlotNo1 = 2,// info.Position,
                                STGetSlotPostion1 = 99,//Convert.ToInt32(info.FSlotPosition),
                                Name = Name,
                                Priority = robotmodel.OutPriority,
                                RobotHand = RobotHand.AllArm,
                                Unit = unit
                            };
                            if (robotmodel.GlassA != null)
                                cmd.GetGlassA = robotmodel.GlassA;
                            if (robotmodel.GlassB != null)
                                cmd.GetGlassB = robotmodel.GlassB;
                            if (robotmodel.GlassC != null)
                                cmd.GetGlassC = robotmodel.GlassC;
                            if (robotmodel.GlassD != null)
                                cmd.GetGlassD = robotmodel.GlassD;
                            cmds.Add(cmd);                            
                        }
                        else//某层有不同级情况 则分两条命令取
                        {
                            if (String.IsNullOrEmpty(errinfo1))
                            {
                                //下手臂取下层
                                var cmd = new RobotCommand
                                {
                                    STRCMD1 = RobotMotion.Get,
                                    STArmNo1 = RobotHand.LowHand,
                                    STGetPosition1 = robotmodel.ModelPosition,
                                    STGetSlotNo1 = robotmodel.EQPSendSlotNoA,// info.Position,
                                    STGetSlotPostion1 = !issamegrade1 ? (robotmodel.GlassA != null ? 1 : 2) : ((robotmodel.GlassA != null && robotmodel.GlassB != null) ? 99 : (robotmodel.GlassA != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                    Name = Name,
                                    Priority = robotmodel.OutPriority + 1,
                                    RobotHand = RobotHand.LowHand,
                                    Unit = unit
                                };
                                if (robotmodel.GlassA != null)
                                    cmd.GetGlassA = robotmodel.GlassA;
                                if (robotmodel.GlassB != null)
                                    cmd.GetGlassB = robotmodel.GlassB;
                                cmds.Add(cmd);
                            }
                            if (String.IsNullOrEmpty(errinfo2))
                            {
                                //上手臂取上层
                                var cmd2 = new RobotCommand
                                {
                                    STRCMD1 = RobotMotion.Get,
                                    STArmNo1 = RobotHand.UpHand,
                                    STGetPosition1 = robotmodel.ModelPosition,
                                    STGetSlotNo1 = robotmodel.EQPSendSlotNoB,// info.Position,
                                    STGetSlotPostion1 = !issamegrade2 ? (robotmodel.GlassC != null ? 1 : 2) : ((robotmodel.GlassC != null && robotmodel.GlassD != null) ? 99 : (robotmodel.GlassC != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                    Name = Name,
                                    Priority = robotmodel.OutPriority,
                                    RobotHand = RobotHand.UpHand,
                                    Unit = unit
                                };
                                if (robotmodel.GlassC != null)
                                    cmd2.GetGlassC = robotmodel.GlassC;
                                if (robotmodel.GlassD != null)
                                    cmd2.GetGlassD = robotmodel.GlassD;
                                cmds.Add(cmd2);
                            }
                        }
                    }
                    else//只有一层
                    {
                        //优先下手臂去取
                        if (robotmodel.EQPSendSlotNoA != 0)
                        {
                            //判断两片是否同等级
                            var issamegrade1 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassA, robotmodel.GlassB,ref errinfo1);
                            if (String.IsNullOrEmpty(errinfo1))
                            {
                                var cmd = new RobotCommand
                                {
                                    STRCMD1 = RobotMotion.Get,
                                    STArmNo1 = RobotHand.LowHand,
                                    STGetPosition1 = robotmodel.ModelPosition,
                                    STGetSlotNo1 = robotmodel.EQPSendSlotNoA,// info.Position,
                                    STGetSlotPostion1 = !issamegrade1 ? (robotmodel.GlassA != null ? 1 : 2) : ((robotmodel.GlassA != null && robotmodel.GlassB != null) ? 99 : (robotmodel.GlassA != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                    Name = Name,
                                    Priority = robotmodel.OutPriority,
                                    RobotHand = RobotHand.LowHand,
                                    Unit = unit
                                };
                                if (robotmodel.GlassA != null)
                                    cmd.GetGlassA = robotmodel.GlassA;
                                if (robotmodel.GlassB != null)
                                    cmd.GetGlassB = robotmodel.GlassB;
                                cmds.Add(cmd);
                            }
                        }
                        else if (robotmodel.EQPSendSlotNoB != 0)
                        {
                            //判断两片是否同等级
                            var issamegrade1 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassC, robotmodel.GlassD, ref errinfo1);
                            if (String.IsNullOrEmpty(errinfo1))
                            {
                                var cmd = new RobotCommand
                                {
                                    STRCMD1 = RobotMotion.Get,
                                    STArmNo1 = RobotHand.LowHand,
                                    STGetPosition1 = robotmodel.ModelPosition,
                                    STGetSlotNo1 = robotmodel.EQPSendSlotNoB,// info.Position,
                                    STGetSlotPostion1 = !issamegrade1 ? (robotmodel.GlassC != null ? 1 : 2) : ((robotmodel.GlassC != null && robotmodel.GlassD != null) ? 99 : (robotmodel.GlassC != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                    Name = Name,
                                    Priority = robotmodel.OutPriority,
                                    RobotHand = RobotHand.LowHand,
                                    Unit = unit
                                };
                                if (robotmodel.GlassC != null)
                                    cmd.GetGlassC = robotmodel.GlassC;
                                if (robotmodel.GlassD != null)
                                    cmd.GetGlassD = robotmodel.GlassD;
                                cmds.Add(cmd);
                            }
                        }
                    }
                }
                else
                {
                    if (robotmodel.EQPSendSlotNoA != 0)
                    {
                        //判断两片是否同等级
                        var issamegrade1 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassA, robotmodel.GlassB, ref errinfo1);
                        if (String.IsNullOrEmpty(errinfo1))
                        {
                            var cmd = new RobotCommand
                            {
                                STRCMD1 = RobotMotion.Get,
                                STArmNo1 = robotHand,
                                STGetPosition1 = robotmodel.ModelPosition,
                                STGetSlotNo1 = robotmodel.EQPSendSlotNoA,// info.Position,
                                STGetSlotPostion1 = !issamegrade1 ? (robotmodel.GlassA != null ? 1 : 2) : ((robotmodel.GlassA != null && robotmodel.GlassB != null) ? 99 : (robotmodel.GlassA != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                Name = Name,
                                Priority = robotmodel.OutPriority,
                                RobotHand = robotHand,
                                Unit = unit
                            };
                            if (robotmodel.GlassA != null)
                                cmd.GetGlassA = robotmodel.GlassA;
                            if (robotmodel.GlassB != null)
                                cmd.GetGlassB = robotmodel.GlassB;
                            cmds.Add(cmd);
                        }
                    }
                    else if (robotmodel.EQPSendSlotNoB != 0)
                    {
                        //判断两片是否同等级
                        var issamegrade1 = CheckTwoGlassGrade(unit.EQPID, robotmodel.GlassC, robotmodel.GlassD,ref errinfo1);
                        if (String.IsNullOrEmpty(errinfo1))
                        {
                            var cmd = new RobotCommand
                            {
                                STRCMD1 = RobotMotion.Get,
                                STArmNo1 = robotHand,
                                STGetPosition1 = robotmodel.ModelPosition,
                                STGetSlotNo1 = robotmodel.EQPSendSlotNoB,// info.Position,
                                STGetSlotPostion1 = !issamegrade1 ? (robotmodel.GlassC != null ? 1 : 2) : ((robotmodel.GlassC != null && robotmodel.GlassD != null) ? 99 : (robotmodel.GlassC != null ? 1 : 2)),//Convert.ToInt32(info.FSlotPosition),
                                Name = Name,
                                Priority = robotmodel.OutPriority,
                                RobotHand = robotHand,
                                Unit = unit
                            };
                            if (robotmodel.GlassC != null)
                                cmd.GetGlassC = robotmodel.GlassC;
                            if (robotmodel.GlassD != null)
                                cmd.GetGlassD = robotmodel.GlassD;
                            cmds.Add(cmd);
                        }
                    }
                }

                errinfo = errinfo1 + errinfo2;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
            return cmds;
        }

        /// <summary>
        /// 判断两片是否同等级
        /// </summary>
        /// <param name="glassA"></param>
        /// <param name="glassB"></param>
        /// <returns></returns>
        private bool CheckTwoGlassGrade(string EQPID, GlassInfo GlassA, GlassInfo GlassB, ref string errorinfo)
        {
            try
            {
                if (GlassA != null && GlassB != null)
                {
                    var portGradeGroups = HostInfo.Current.PortGradeGroupList.FirstOrDefault(o => o.Key == EQPID).Value;
                    if (portGradeGroups != null && portGradeGroups.Count > 0)
                    {
                        var glassAPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassA.GlassGradeCode) && o.enabled == 0);
                        var glassBPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassB.GlassGradeCode) && o.enabled == 0);
                        #region 需求6 7.修改取片时Panel等级没有设定在Port Grade Group中不取片 yindeyu 20222024
                        if (glassAPortGradeGroups.Count() == 0)
                        {
                            errorinfo += $"GlassID:{GlassA.GlassID} cstseqno:{GlassA.CassetteSequenceNo} slotseqno:{GlassA.SlotSequenceNo} grade:{GlassA.GlassGradeCode} can not find BC PortGrade Group;";
                        }
                        if (glassBPortGradeGroups.Count() == 0)
                        {
                            errorinfo += $"GlassID:{GlassB.GlassID} cstseqno:{GlassB.CassetteSequenceNo} slotseqno:{GlassB.SlotSequenceNo} grade:{GlassB.GlassGradeCode} can not find BC PortGrade Group;";
                        }

                        if (glassAPortGradeGroups.Count() > 0 && glassBPortGradeGroups.Count() > 0)
                        #endregion
                        {
                            var checkResult = false;
                            foreach (var glassAPGG in glassAPortGradeGroups)
                            {
                                checkResult = glassBPortGradeGroups.Any(c => c.portgradegroup == glassAPGG.portgradegroup);
                                if (checkResult)
                                {
                                    LogHelper.BCLog.Debug($"FirstGlass {GlassA.GlassGradeCode} portgrade match SecondGlass {GlassB.GlassGradeCode}");
                                    break;
                                }
                                else
                                    continue;
                            }
                            return checkResult;
                        }
                    }
                    else
                    {
                        errorinfo += $"EQPID:{EQPID} PortGradeGroupList not found;";
                    }
                }
                else
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return true;
            }
        }

        public List<RobotCommand> CreatePortGetCommand(PortInfo port, int getModelPosition, GlassInfo glassA, GlassInfo glassB, GlassInfo glassC, GlassInfo glassD, RobotHand robotHand, int priority)
        {
            List<RobotCommand> cmds = new List<RobotCommand>();
            try
            {
                if (robotHand == RobotHand.AllArm)//两个手臂都有空
                {
                    if ((glassA != null || glassB != null) && (glassC != null || glassD != null))//有两层
                    {
                        var glass1 = glassA != null ? glassA : glassB;
                        var glass2 = glassC != null ? glassC : glassD;
                        var cmd = new RobotCommand
                        {
                            STRCMD1 = RobotMotion.TwoActionBatchGet,
                            STArmNo1 = RobotHand.AllArm,
                            STGetPosition1 = getModelPosition,
                            STPutPosition1 = getModelPosition,
                            STGetSlotNo1 = glass1.Position,
                            STGetSlotPostion1 = (glassA != null && glassB != null) ? 99 : (glassA != null ? 1 : 2),
                            STPutSlotNo1 = glass2.Position,
                            STPutSlotPostion1 = (glassC != null && glassD != null) ? 99 : (glassC != null ? 1 : 2),
                            Name = Name,
                            Priority = priority,
                            RobotHand = RobotHand.AllArm,
                            Unit = port
                        };
                        if (glassA != null)
                            cmd.GetGlassA = glassA;
                        if (glassB != null)
                            cmd.GetGlassB = glassB;
                        if (glassC != null)
                            cmd.GetGlassC = glassC;
                        if (glassD != null)
                            cmd.GetGlassD = glassD;
                        cmds.Add(cmd);

                        ////默认从下往上取
                        ////上手臂取最下层
                        //var glass1 = glassA != null ? glassA : glassB;
                        //var cmd = new RobotCommand
                        //{
                        //    STRCMD1 = RobotMotion.Get,
                        //    STGetPosition1 = getModelPosition,
                        //    STArmNo1 = RobotHand.UpHand,
                        //    STGetSlotNo1 = glass1.Position,
                        //    STGetSlotPostion1 = (glassA != null && glassB != null) ? 99 : (glassA != null ? 1 : 2),
                        //    Name = Name,
                        //    Priority = priority,
                        //    RobotHand = RobotHand.UpHand,
                        //    Unit = port
                        //}; 
                        //if (glassA != null)
                        //    cmd.GetGlassA = glassA;
                        //if (glassB != null)
                        //    cmd.GetGlassB = glassB;
                        //cmds.Add(cmd);

                        ////下手臂取第二层
                        //var glass2 = glassC != null ? glassC : glassD;
                        //var cmd2 = new RobotCommand
                        //{
                        //    STRCMD1 = RobotMotion.Get,
                        //    STGetPosition1 = getModelPosition,
                        //    STArmNo1 = RobotHand.LowHand,
                        //    STGetSlotNo1 = glass2.Position,
                        //    STGetSlotPostion1 = (glassC != null && glassD != null) ? 99 : (glassC != null ? 1 : 2),
                        //    Name = Name,
                        //    Priority = priority,
                        //    RobotHand = RobotHand.LowHand,
                        //    Unit = port
                        //};
                        //if (glassC != null)
                        //    cmd2.GetGlassC = glassC;
                        //if (glassD != null)
                        //    cmd2.GetGlassD = glassD;
                        //cmds.Add(cmd2);
                    }
                    else//只有一层
                    {
                        //默认从下往上取
                        //上手臂取最下层
                        var glass1 = glassA != null ? glassA : glassB;
                        var cmd = new RobotCommand
                        {
                            STRCMD1 = RobotMotion.Get,
                            STGetPosition1 = getModelPosition,
                            STArmNo1 = RobotHand.UpHand,
                            STGetSlotNo1 = glass1.Position,
                            STGetSlotPostion1 = (glassA != null && glassB != null) ? 99 : (glassA != null ? 1 : 2),
                            Name = Name,
                            Priority = priority,
                            RobotHand = RobotHand.UpHand,
                            Unit = port
                        };
                        if (glassA != null)
                            cmd.GetGlassA = glassA;
                        if (glassB != null)
                            cmd.GetGlassB = glassB;
                        cmds.Add(cmd);
                    }
                }
                else
                {
                    //默认从下往上取
                    //手臂取最下层
                    var glass1 = glassA != null ? glassA : glassB;
                    var cmd = new RobotCommand
                    {
                        STRCMD1 = RobotMotion.Get,
                        STGetPosition1 = getModelPosition,
                        STArmNo1 = robotHand,
                        STGetSlotNo1 = glass1.Position,
                        STGetSlotPostion1 = (glassA != null && glassB != null) ? 99 : (glassA != null ? 1 : 2),
                        Name = Name,
                        Priority = priority,
                        RobotHand = robotHand,
                        Unit = port
                    };
                    if (glassA != null)
                        cmd.GetGlassA = glassA;
                    if (glassB != null)
                        cmd.GetGlassB = glassB;
                    cmds.Add(cmd);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
            return cmds;
        }


        public RobotCommand CreateProcessPutCommand(Unit unit, RobotHand robotHand, RobotModel tagetmodel, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    //Command = RobotMotion.Put,
                    //SlotNo = slotNo,
                    //StagePathNo = UnitPathNo,
                    //StageType = EStageType.Single,
                    STRCMD1 = RobotMotion.Put,
                    STArmNo1 = robotHand,
                    STPutPosition1 = tagetmodel.ModelPosition,
                    STPutSlotNo1 = tagetmodel.EQPReciveSlotNoA == 1 ? 1 : (tagetmodel.EQPReciveSlotNoB == 2 ? 2 : 1),//info.Position,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(info.FSlotPosition),
                    // JobData = info,
                    Name = Name,
                    Priority = tagetmodel.INPriority,
                    RobotHand = robotHand,
                    Unit = unit
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
        }

        public RobotCommand CreatePortPutCommand(PortInfo port, int targetModelPosition, RobotHand robotHand, int priority, int Position, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    STRCMD1 = RobotMotion.Put,
                    STArmNo1 = robotHand,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = Position,
                    STPutSlotPostion1 = SlotPostion,
                    Name = Name,
                    Priority = priority,
                    RobotHand = robotHand,
                    Unit = port
                };
                return cmd;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }

        public RobotCommand CreateProcessExchangeCommand(RobotHand putRobotHand, Unit getJobStage, GlassInfo putJobData, int priotity, int targetModelPosition, int GetSlotPostion, int PutSlotPostion, RobotMotion RobotMotion)
        {
            var cmd = new RobotCommand();
            try
            {
                RobotHand getRobotHand;
                if (putRobotHand == RobotHand.LowHand)
                {
                    getRobotHand = RobotHand.UpHand;
                }
                else
                {
                    getRobotHand = RobotHand.LowHand;
                }
                cmd.STRCMD1 = RobotMotion;//RobotMotion.OneActionExchange;
                cmd.STArmNo1 = getRobotHand;
                cmd.STGetPosition1 = targetModelPosition;
                cmd.STGetSlotNo1 = 1;//putJobData.Position;
                cmd.STGetSlotPostion1 = GetSlotPostion;//Convert.ToInt32(putJobData.FSlotPosition);


                cmd.STPutPosition1 = targetModelPosition;
                cmd.STPutSlotNo1 = 1;//info.Position,
                cmd.STPutSlotPostion1 = PutSlotPostion;//Convert.ToInt32(info.FSlotPosition),

                cmd.RobotHand = getRobotHand;
                cmd.Name = Name;
                cmd.Priority = priotity;
                // cmd.JobData = getJobData;
                //cmd.PutJobData = putJobData;
                cmd.Unit = getJobStage;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return cmd;
        }


        public RobotCommand CreateTransferIndexToEQPCommand(RobotHand robotHand, PortInfo port, int getModelPosition, GlassInfo getJobData, RobotModel targetmode, int SlotPostion)
        {

            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Transfer,
                    STArmNo1 = robotHand,
                    STGetPosition1 = getModelPosition,
                    STGetSlotNo1 = getJobData.Position,
                    STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(getJobData.FSlotPosition),


                    STPutPosition1 = targetmode.ModelPosition,
                    STPutSlotNo1 = targetmode.EQPReciveSlotNoA == 1 ? 1 : (targetmode.EQPReciveSlotNoB == 2 ? 2 : 1),//getJobData.Position,
                    STPutSlotPostion1 = SlotPostion, //Convert.ToInt32(getJobData.FSlotPosition),


                    //JobData = getJobData,
                    Name = Name,
                    Priority = targetmode.TransferPriority,
                    RobotHand = robotHand,
                    Unit = port
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }


        }
        public List<RobotCommand> CreateTransferEQPToIndexCommand(Unit unitItem, RobotModel getRobotModel, RobotModel putModel, RobotHand STArmNo1, int STGetPosition1, int STGetSlotNo1, int STGetSlotPostion1, int STPutPosition1, int STPutSlotNo1, int STPutSlotPostion1)//, RobotHand NDArmNo2, int NDGetPosition2, int NDGetSlotNo2, int NDGetSlotPostion2, int NDPutPosition2, int NDPutSlotNo2, int NDPutSlotPostion2
        {
            List<RobotCommand> cmds = new List<RobotCommand>();
            try
            {
                var cmd = new RobotCommand
                {
                    Name = Name,
                    STRCMD1 = RobotMotion.Transfer,
                    Priority = getRobotModel.TransferPriority,
                    Unit = unitItem

                    //STArmNo1 = robotHand,
                    //STGetPosition1 = sourceModelPosition,
                    //STGetSlotNo1 = 1,//getJobData.Position,
                    //STGetSlotPostion1 = SlotPostion, //Convert.ToInt32(getJobData.FSlotPosition),


                    //STPutPosition1 = targetModelPosition,
                    //STPutSlotNo1 = getJobData.Position,
                    //STPutSlotPostion1 = SlotPostion,// Convert.ToInt32(getJobData.FSlotPosition),


                    ////  JobData = getJobData,
                    //RobotHand = robotHand,
                };

                if (STGetPosition1 != 0)
                {
                    cmd.STArmNo1 = STArmNo1;
                    cmd.STGetPosition1 = STGetPosition1;
                    cmd.STGetSlotNo1 = STGetSlotNo1;
                    cmd.STGetSlotPostion1 = STGetSlotPostion1;
                    cmd.STPutPosition1 = STPutPosition1;
                    cmd.STPutSlotNo1 = STPutSlotNo1;
                    cmd.STPutSlotPostion1 = STPutSlotPostion1;
                    cmd.RobotHand = STArmNo1; 
                }
                cmds.Add(cmd);

                ////port只能单放 所以这里改成 两条命令执行
                //var cmd2 = new RobotCommand
                //{
                //    Name = Name,
                //    STRCMD1 = RobotMotion.Transfer,
                //    Priority = getRobotModel.TransferPriority,
                //    Unit = unitItem
                //};
                //if (NDGetPosition2 != 0)
                //{
                //    cmd2.STArmNo1 = NDArmNo2;
                //    cmd2.STGetPosition1 = NDGetPosition2;
                //    cmd2.STGetSlotNo1 = NDGetSlotNo2;
                //    cmd2.STGetSlotPostion1 = NDGetSlotPostion2;
                //    cmd2.STPutPosition1 = NDPutPosition2;
                //    cmd2.STPutSlotNo1 = NDPutSlotNo2;
                //    cmd2.STPutSlotPostion1 = NDPutSlotPostion2;
                //    cmd2.RobotHand = NDArmNo2;
                //}
                //cmds.Add(cmd2);

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
            return cmds;
        }
        public RobotCommand CreateTransferEQPToEQPCommand(RobotHand robotHand, Unit unit, GlassInfo getJobData, int priotity, int sourceModelPosition, int targetModelPosition, int SlotPostion)
        {

            try
            {
                var cmd = new RobotCommand
                {

                    STRCMD1 = RobotMotion.Transfer,
                    STArmNo1 = robotHand,
                    STGetPosition1 = sourceModelPosition,
                    STGetSlotNo1 = 1,//getJobData.Position,
                    STGetSlotPostion1 = SlotPostion,//Convert.ToInt32(getJobData.FSlotPosition),


                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = 1,// getJobData.Position,
                    STPutSlotPostion1 = SlotPostion,//Convert.ToInt32(getJobData.FSlotPosition),


                    // JobData = getJobData,
                    Name = Name,
                    Priority = priotity,
                    RobotHand = robotHand,
                    Unit = unit
                };
                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }


        }


        public RobotCommand CreateProcessPutWaitCommand(Unit unit, RobotHand robotHand, int priotity, int targetModelPosition, int SlotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    STRCMD1 = RobotMotion.Move,
                    STArmNo1 = robotHand,
                    STSubCommand1 = 2,
                    STPutPosition1 = targetModelPosition,
                    STPutSlotNo1 = 1,
                    STPutSlotPostion1 = 0,

                    // JobData = info,
                    Name = Name,
                    Priority = priotity,
                    RobotHand = robotHand,
                    Unit = unit
                };
                return cmd;
                //Logger.InfoFormat("[CreateProcessPutCommand][PutWait] STRCMD1:{0},STArmNo1:{1},STPutPosition1:{2} STPutSlotNo1:{3},STPutSlotPostion1:{4},STSubCommand1:{5} ",
                //cmd.STRCMD1.ToString(), cmd.STArmNo1.ToString(), cmd.STPutPosition1, cmd.STPutSlotNo1, cmd.STPutSlotPostion1,cmd.STSubCommand1);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotCommand CreateProcessGetWaitCommand(Unit unit, RobotHand robotHand, int priotity, int targetModelPosition, int slotPostion)
        {
            try
            {
                var cmd = new RobotCommand
                {
                    STRCMD1 = RobotMotion.Move,
                    STArmNo1 = robotHand,
                    STSubCommand1 = 1,
                    STGetPosition1 = targetModelPosition,
                    STGetSlotNo1 = 1,// info.Position,
                    STGetSlotPostion1 = 0,//Convert.ToInt32(info.FSlotPosition),

                    //JobData = info,
                    Name = Name,
                    Priority = priotity,
                    RobotHand = robotHand,
                    Unit = unit
                };

                return cmd;

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotCommand CreateProcessPutWaitCommand(Unit unit, GlassInfo info, int slotNo, RobotHand robotHand, int priotity)
        //{
        //    try
        //    {
        //        var cmd = new RobotCommand
        //        {
        //            //Command = RobotMotion.PutWaitP,
        //            //JobData = info,
        //            //Name = Name,
        //            //Priority = priotity,
        //            //RobotHand = robotHand,
        //            //SlotNo = slotNo,
        //            //StagePathNo = unit.UnitPathNo,
        //            //StageType = unit.Capacity == 1 ? EStageType.Single : EStageType.Multi,
        //            //Unit = unit
        //        };
        //        return cmd;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        //public RobotCommand CreateBufferGetCommand(Buffer buffer, PanelInfo info, int slotNo, RobotHand robotHand, int priority)
        //{
        //    var cmd = new RobotCommand
        //    {
        //        Command = RobotMotion.Get,
        //        JobData = info,
        //        Name = Name,
        //        Priority = priority,
        //        RobotHand = robotHand,
        //        SlotNo = slotNo,
        //        StagePathNo = buffer.UnitNo,
        //        StageType = EStageType.Multi,
        //        Unit = buffer
        //    };
        //    return cmd;
        //}
        //public RobotCommand CreateBufferPutCommand(Buffer buffer, GlassInfo handJob, int slotNo, RobotHand robotHand, int priority)
        //{
        //    var cmd = new RobotCommand
        //    {
        //        Command = RobotMotion.Put,
        //        GlassThickness = handJob.GlassThickNess,
        //        JobData = handJob,
        //        Name = Name,
        //        Priority = priority,
        //        RobotHand = robotHand,
        //        SlotNo = slotNo,
        //        StagePathNo = buffer.UnitNo,
        //        StageType = EStageType.Multi,
        //        Unit = buffer
        //    };
        //    return cmd;
        //}
        #endregion
    }
}
