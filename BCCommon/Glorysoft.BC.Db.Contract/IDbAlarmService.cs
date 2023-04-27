using System;
using System.Collections.Generic;
using System.Collections;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbAlarmService:IAutoRegister
    {
        IList<AlarmInfo> ViewAlarmList(Hashtable map);
        AlarmInfo FindOneAlarm(Hashtable map);
        bool InsertAlarmInfo(AlarmInfo alarmInfo);
        //bool ImportAlarmList(IList<AlarmInfo> lst);
        ////bool UpdateAlarmEnable(AlarmInfo AlarmInfo);
        ////bool UpdateAlarmInfo(Hashtable item);
        bool DeleteAlarmInfo(Hashtable alarm);
        //bool ClearAlarmList(string eqpID);
        IList<AlarmInfo> ViewAlarmHistory(Hashtable map);
        bool InsertAlarmHistory(AlarmInfo item);
        //bool DeleteAlarmHistory(DateTime dtTime);




         bool InsertWipAlarmInfo(AlarmInfo item);
         int DeleteWipAlarmInfo(Hashtable map);
        int DeleteWipAlarmMinInfo(Hashtable map);
        IList<AlarmInfo> ViewWipAlarmList(Hashtable map);
    }
}
