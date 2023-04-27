using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;

namespace Glorysoft.BC.Db.Service
{
    public class DbAlarmService : AbstractDbService, IDbAlarmService
    {
        public IList<AlarmInfo> ViewAlarmList(Hashtable map)
        {
            return ExecuteQueryForList<AlarmInfo>("ViewAlarmList", map);
        }
        public AlarmInfo FindOneAlarm(Hashtable map)
        {
            return ExecuteQueryForObject<AlarmInfo>("FindOneAlarm", map);
        }
        public bool InsertAlarmInfo(AlarmInfo alarmInfo)
        {
            return ExecuteInsert("InsertAlarmInfo", alarmInfo);
        }
        //public bool ImportAlarmList(IList<AlarmInfo> lst)
        //{
        //    foreach (AlarmInfo item in lst)
        //    {
        //        // VALUES (#EQPID#, #AlarmText#, #AlarmEnable#, #UNITID#, #SUNITID#, #AlarmCode#, #AlarmLevel#);
        //        //Hashtable map = new Hashtable
        //        //{
        //        //    {"EQPID",item.EQPID },
        //        //    //{"EQPName",item.EQPName },
        //        //    {"AlarmText",item.AlarmText },
        //        //    {"AlarmEnable",item.AlarmEnable },
        //        //    {"UNITID",item.UNITID },
        //        //    {"SUNITID",item.SUNITID },
        //        //    {"AlarmCode",item.AlarmCode },
        //        //    {"AlarmLevel",item.AlarmLevel }
        //        //};
        //        ExecuteInsert("InsertAlarmInfo", item);
        //    }
        //    return true;
        //}
        //public bool UpdateAlarmEnable(AlarmInfo AlarmInfo)
        //{
        //    return ExecuteUpdate("UpdateAlarmEnable", AlarmInfo) == 1 ? true : false;
        //}
        //public bool UpdateAlarmInfo(Hashtable item)
        //{
        //    return ExecuteUpdate("UpdateAlarmInfo", item) == 1 ? true : false;
        //}
        public bool DeleteAlarmInfo(Hashtable alarm)
        {
            return ExecuteDelete("DeleteAlarmInfo", alarm) == 1 ? true : false;
        }
        //public bool ClearAlarmList(string eqpID)
        //{
        //    return ExecuteDelete("ClearAlarmList", eqpID) == 1 ? true : false;
        //}
        public IList<AlarmInfo> ViewAlarmHistory(Hashtable map)
        {
            return ExecuteQueryForList<AlarmInfo>("ViewAlarmHistory", map);
        }
        public bool InsertAlarmHistory(AlarmInfo item)
        {
            return ExecuteInsert("InsertAlarmHistory", item);
        }






        public bool InsertWipAlarmInfo(AlarmInfo item)
        {
            return ExecuteInsert("InsertWipAlarmInfo", item);
        }
        public int DeleteWipAlarmInfo(Hashtable map)
        {
            return ExecuteDelete("DeleteWipAlarmInfo", map);
        }
        public int DeleteWipAlarmMinInfo(Hashtable map)
        {
            return ExecuteDelete("DeleteWipAlarmMinInfo", map);
        }        
        public IList<AlarmInfo> ViewWipAlarmList(Hashtable map)
        {
            return ExecuteQueryForList<AlarmInfo>("ViewWipAlarmList", map);
        }

    }
}
