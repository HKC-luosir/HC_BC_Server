//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Glorysoft.BC.Entity
//{
//    public delegate void SuspendEvent(object obj, string prodNo);
//    public class Location
//    {
//        public Location(Unit unit)
//        {
//            IsEmpty = true;
//            Parent = unit;
//        }
//        public event SuspendEvent OnSuspendOn;


//        public Unit Parent { get; private set; }
//        public string PositionID { get; set; }
//        public string PositionName { get; set; }
//        public int PositionNo { get; set; }
//        public bool IsEmpty { get; set; }

//        private GlassInfo glsInfo;
//        public GlassInfo GlsInfo
//        {
//            get
//            {
//                return glsInfo;
//            }
//            set
//            {
//                //主要是客户端也会用到，Parent并没有传给Client，所以再Client端，只需要赋值，没有其他的逻辑考虑。
//                if (Parent != null)
//                {
//                    //logger.Debug(string.Format("[LOC_GLS_CHG_1].[{0}---{1}] [{2}]", Parent.Parent.EQPID, Parent.Parent.Type, Parent.UnitID));

//                    //赋值                     
//                    var tempgls = glsInfo;
//                    glsInfo = value;

//                    if (OnSuspendOn != null)
//                    {

//                        if (value != null)//先判断下EQP是否是INS设备。
//                        {
//                            //logger.Debug(string.Format("[LOC_GLS_CHG_2].[INS GlS][{0}---{1}]", this.PositionID, value.GLSID));
//                            return;
//                        }
//                        //Suspend On Check 进机台就检测，而且一直往前，检查到同样的lot就退出。否则继续往前，
//                        //unload比较特殊。CNV2-2的off要检查Unloader的两个Buffer
//                        //Unloader两个buffer分别对应Port1/port2.只要对应的那个port为空就可以suspendOff。
//                        if (value != null && (!string.IsNullOrEmpty(value.PPID)))
//                        {
//                            //logger.Debug(string.Format("[LOC_GLS_CHG_3].[SuspendOn CHK][{0}---{1}]", Parent.Type, value.GLSID));
//                            //try
//                            //{
//                            //    var equipment = Parent.Parent;
//                            //    var canSuspendUnit = equipment.CanSuspendUnit;
//                            //    foreach (var unit in canSuspendUnit)
//                            //    {
//                            //        try
//                            //        {
//                            //            bool isSuspend = false;
//                            //            if (unit.Type == EnumUnitType.Buffer)
//                            //            {
//                            //                isSuspend = (unit as Buffer).IsSuspend;
//                            //                //logger.Debug(string.Format("[LOC_GLS_CHG_4].[BUF ][[{0}]---{1}]", unit.UnitID, isSuspend));
//                            //            }
//                            //            else
//                            //            {
//                            //                var cst = (unit as PortInfo);
//                            //                isSuspend = cst.IsSuspend;
//                            //                //logger.Debug(string.Format("[LOC_GLS_CHG_4].[CST ][{0}]---{1}]", unit.UnitID, isSuspend));
//                            //            }
//                            //            //Suspend Event
//                            //            //if (value.ProductNo != "F" && (!isSuspend))
//                            //            //{
//                            //            //    try
//                            //            //    {
//                            //            //        OnSuspendOn(Parent, value.ProductNo);
//                            //            //    }
//                            //            //    catch (Exception ex)
//                            //            //    {
//                            //            //        logger.Error(ex);
//                            //            //    }
//                            //            //}
//                            //        }
//                            //        catch (Exception ex)
//                            //        {
//                            //            //logger.Error(string.Format("Check Suspend Codi.[{0}]--[{1}]", equipment.EQPID, unit.UnitID), ex);
//                            //        }
//                            //    }
//                            //}
//                            //catch (Exception ex)
//                            //{
//                            //    //logger.Error("Set Glass Error.", ex);
//                            //}

//                        }
//                    }
//                }
//                else
//                    glsInfo = value;
//            }
//        }


//    }
//}
