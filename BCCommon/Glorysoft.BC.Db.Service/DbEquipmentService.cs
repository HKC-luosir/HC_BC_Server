using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System.Linq;
namespace Glorysoft.BC.Db.Service
{
    public class DbEquipmentService : AbstractDbService, IDbEquipmentService
    {
        //public IList<EQPInfo> ViewEQPList(string sLine)
        //{
        //    var lst = ExecuteQueryForList<EQPInfo>("ViewEQPList", sLine);
        //    try
        //    {
        //        foreach (var item in lst.Where(p => p.HasUnit))
        //        {
        //            var units = ExecuteQueryForList<Unit>("ViewUnitList", item.EQPID) ?? new List<Unit>();

        //            var u = units.ToList().FindAll(o => o.EQPName == item.EQPName && o.UnitType != (int)EnumUnitType.Robot);
        //            item.Units.AddRange(u);
        //            var robotList = units.ToList().FindAll(o => o.EQPName == item.EQPName && o.UnitType == (int)EnumUnitType.Robot);
        //            for (int i = 0; i < robotList.Count; i++)
        //            {
        //                Robot robot = new Robot();
        //                robot.UnitID = robotList[i].UnitID;
        //                robot.LineID = robotList[i].LineID;
        //                robot.SUnitPathNo = robotList[i].UnitPathNo;
        //                robot.UnitName = robotList[i].UnitName;
        //                robot.UnitType = robotList[i].UnitType;
        //                robot.Capacity = robotList[i].Capacity;
        //                robot.IsBlockPoint = robotList[i].IsBlockPoint;
        //                robot.EQPID = robotList[i].EQPID;
        //                robot.EQPName = robotList[i].EQPName;
        //                item.Units.Add(robot);
        //            }
        //            var unitinfo = HostInfo.Current.PortList.ToList().FindAll(f => f.EQPName == item.EQPName).ToList();
        //            if (unitinfo.Count > 0)
        //            {
        //                foreach (PortInfo p in unitinfo)
        //                {
        //                    p.EQPID = item.EQPID;
        //                    p.EQPName = item.EQPName;
        //                    p.UnitType = (int)EnumUnitType.Port;
        //                    p.UnitID = p.PortID;
        //                    p.UnitName = p.PortID;

        //                    item.Units.Add(p);
        //                }
        //            }
        //            foreach (var unit in units)
        //            {
        //                item.UnitList.Add(unit.UnitName, unit);
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //    return lst;
        //}
        public List<EQPInfo> ViewEQP(string eqpid)
        {
            //  var varss = ExecuteQueryForList<EQPInfo>("ViewEQPList", eqpid);
            //var eQPInfo = ExecuteQueryForList<EQPInfo>("ViewEQPList", eqpid).FirstOrDefault();
            Hashtable data = new Hashtable();
            data.Add("EQPID", eqpid);
            var alleQPInfo = ExecuteQueryForList<EQPInfo>("ViewEQPList", data);
            try
            {
                foreach (var eQPInfo in alleQPInfo)
                {
                    //var units = ExecuteQueryForList<Unit>("ViewUnitList", eQPInfo.EQPID) ?? new List<Unit>();
                    Hashtable unitmap = new Hashtable
                {
                    {"EQPID",eQPInfo.EQPID },
                    {"UnitID","" }
                };
                    var units = ViewUnitList(unitmap);
                    //foreach (var item in units)
                    //{
                    //    eQPInfo.UnitList.Add(item.UnitName, item);
                    //}
                    var u = units.ToList().FindAll(o => o.UnitType != EnumUnitType.Robot);
                    eQPInfo.Units.AddRange(u);
                    var robotList = units.ToList().FindAll(o => o.UnitType == EnumUnitType.Robot);
                    for (int i = 0; i < robotList.Count; i++)
                    {
                        Robot robot = new Robot();
                        robot = robotList[i].SetRobotInfo(robot, robotList[i]);
                        eQPInfo.Units.Add(robot);
                    }
                    //foreach (PortInfo p in HostInfo.Current.PortList)
                    //{
                    //    var port = (PortInfo)p.Clone();
                    //    port.EQPID = p.EQPID;
                    //    port.UnitType = (int)EnumUnitType.Port;
                    //    port.UnitID = p.PortID;
                    //    port.UnitName = p.PortID;
                    //    eQPInfo.Units.Add(port);
                    //}


                    foreach (var item in eQPInfo.Units)
                    {
                        //var sunits = ExecuteQueryForList<SUnit>("ViewSUnitList", item.UnitID) ?? new List<SUnit>();
                        Hashtable sunitmap = new Hashtable
                    {
                        {"EQPID",eQPInfo.EQPID},
                        {"UnitID",item.UnitID },
                        {"SUnitID",""}
                    };
                        var sunits = ViewSUnitList(sunitmap);
                        //LaserMarking1
                        //var testa = sunits.ToList().FindAll(o => o.UnitName == item.UnitName);
                        //var testb = testa.ToList().FindAll(o => o.SUnitType != (int)EnumUnitType.Robot);
                        // var u = sunits.ToList().FindAll(o => o.UnitName == item.UnitName && o.SUnitType != (int)EnumUnitType.Robot);
                        if (sunits != null && sunits.Count > 0)
                        {
                            item.SUnitList.AddRange(sunits);

                        //    foreach (var sunititem in item.SUnitList.Where(o => o.HasSSUnit))
                        //    {
                        //        Hashtable ssunitmap = new Hashtable
                        //{
                        //    {"EQPID",eQPInfo.EQPID},
                        //    {"UnitID",item.UnitID },
                        //    {"SUnitID",sunititem.SUnitID},
                        //    {"SSUnitID",""}
                        //};
                        //        var ssunits = ViewSSUnitList(ssunitmap);
                        //        if (ssunits != null)
                        //        {
                        //            sunititem.SSUnitList.AddRange(ssunits);
                        //        }
                        //    }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return alleQPInfo.ToList();
        }
        public bool UpdateEQPInfo(EQPInfo EQPInfo)
        {
            return ExecuteUpdate("UpdateEQPInfo", EQPInfo) == 1 ? true : false;
        }
        public bool InsertHisEQPInfo(EQPInfo EQPInfo)
        {
            return ExecuteInsert("InsertHisEQPInfo", EQPInfo);
        }


        public bool InsertHisUnitResult(Unit Unit)
        {
            return ExecuteInsert("InsertHisUnitResult", Unit);
        }
        public IList<Unit> ViewUnitList(Hashtable map)
        {
            return ExecuteQueryForList<Unit>("ViewUnitList", map);
        }
        public bool UpdateUnitInfo(Unit Unit)
        {
            return ExecuteUpdate("UpdateUnitInfo", Unit) == 1 ? true : false;
        }
        public IList<SUnit> ViewSUnitList(Hashtable map)
        {
            return ExecuteQueryForList<SUnit>("ViewSUnitList", map);
        }
        public bool UpdateSUnitInfo(SUnit SUnit)
        {
            return ExecuteUpdate("UpdateSUnitInfo", SUnit) == 1 ? true : false;
        }
        public bool InsertHisSUnitResult(SUnit SUnit)
        {
            return ExecuteInsert("InsertHisSUnitResult", SUnit);
        }

        public IList<SSUnit> ViewSSUnitList(Hashtable map)
        {
            return ExecuteQueryForList<SSUnit>("ViewSSUnitList", map);
        }
        public bool UpdateSSUnitInfo(SSUnit SSUnit)
        {
            return ExecuteUpdate("UpdateSSUnitInfo", SSUnit) == 1 ? true : false;
        }

        public User FindUser(string userID)
        {
            return ExecuteQueryForObject<User>("FindUser", userID);
        }
        public IList<User> GetUserList()
        {
            return ExecuteQueryForList<User>("GetUserList", null);
        }
        public bool InsertUser(User user)
        {
            return ExecuteInsert("InsertUser", user);
        }
        public bool UpdateUser(User user)
        {
            return ExecuteUpdate("UpdateUser", user) == 1 ? true : false;
        }
        public bool DeleteUser(string userID)
        {
            return ExecuteDelete("DeleteUser", userID) == 1 ? true : false;
        }

        public bool UpdateDenseAllLotQTY(int qty)
        {
            return ExecuteUpdate("UpdateDenseAllLotQTY", qty) == 1 ? true : false;
        }
        public bool UpdateDenseLotStatus(int status)
        {
            return ExecuteUpdate("UpdateDenseLotStatus", status) == 1 ? true : false;
        }
        public bool UpdateDenseCurrentLotQTY(int qty)
        {
            return ExecuteUpdate("UpdateDenseCurrentLotQTY", qty) == 1 ? true : false;
        }
        public HostInfo FindDenseLotInfo()
        {
            return ExecuteQueryForObject<HostInfo>("FindDenseLotInfo", null) ?? HostInfo.Current;
        }
        public IList<ECInfo> ViewECList()
        {
            return ExecuteQueryForList<ECInfo>("ViewECList", null);
        }

        public int UpdateECInfo(IList<ECInfo> lst)
        {
            int i = 0;
            foreach (var item in lst)
            {
                i = ExecuteUpdate("UpdateECInfo", item);
            }
            return i;
        }
        public IList<EventInfo> ViewEventList(string CEED)
        {
            return ExecuteQueryForList<EventInfo>("ViewEventList", CEED);
        }
        public IList<EventInfo> ViewAllEventList()
        {
            return ExecuteQueryForList<EventInfo>("ViewAllEventList", null);
        }
        public int UpdateAllEventInfo(string ceed)
        {
            int i = 0;
            i = ExecuteUpdate("UpdateAllEventInfo", ceed);
            return i;
        }

        public int UpdateEventInfo(Hashtable map)
        {
            int i = 0;
            i = ExecuteUpdate("UpdateEventInfo", map);
            return i;
        }

        //public IList<OQASamplingRule> ViewOQASamplingRuleList(string lineID)
        //{
        //    return ExecuteQueryForList<OQASamplingRule>("ViewOQASamplingRuleList", lineID);
        //}

        //public bool UpdateOQASamplingRule(OQASamplingRule oqaSamplingRule)
        //{
        //    return ExecuteUpdate("UpdateOQASamplingRule", oqaSamplingRule) == 1 ? true : false;
        //}


        public IList<EQPStatusRule> ViewEQPStatusRuleList(Hashtable map)
        {
            return ExecuteQueryForList<EQPStatusRule>("ViewEQPStatusRuleList", map);
        }
        public bool InsertEQPStatusRule(EQPStatusRule EQPStatusRule)
        {          
            return ExecuteInsert("InsertEQPStatusRule", EQPStatusRule);
        }
        public int DeleteEQPStatusRule(Hashtable map)
        {
            return ExecuteDelete("DeleteEQPStatusRule", map);
        }



        public IList<EQPStatusGroup> ViewEQPStatusGroupList(Hashtable map)
        {
            return ExecuteQueryForList<EQPStatusGroup>("ViewEQPStatusGroupList", map);
        }
        public bool InsertEQPStatusGroup(EQPStatusGroup EQPStatusGroup)
        {
            return ExecuteInsert("InsertEQPStatusGroup", EQPStatusGroup);
        }
        public int DeleteEQPStatusGroup(Hashtable map)
        {
            return ExecuteDelete("DeleteEQPStatusGroup", map);
        }



        public IList<VCR> ViewVCRList(Hashtable map)
        {
            return ExecuteQueryForList<VCR>("ViewVCRList", map);
        }
        public int UpdateVCR(VCR VCR)
        {        
            return ExecuteUpdate("UpdateVCR", VCR);
        }
        public bool InsertHisVCRResult(VCR VCR)
        {
            return ExecuteInsert("InsertHisVCRResult", VCR);
        }

    }
}
