using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbEquipmentService : IAutoRegister
    {
        // IList<EQPInfo> ViewEQPList(string sLine);
        List<EQPInfo> ViewEQP(string eqpid);
         bool UpdateEQPInfo(EQPInfo EQPInfo);
        bool InsertHisEQPInfo(EQPInfo EQPInfo);
        bool InsertHisUnitResult(Unit Unit);
        IList<Unit> ViewUnitList(Hashtable map);
         bool UpdateUnitInfo(Unit Unit);
        IList<SUnit> ViewSUnitList(Hashtable map);
        bool UpdateSUnitInfo(SUnit SUnit);
        bool InsertHisSUnitResult(SUnit SUnit);
        IList<SSUnit> ViewSSUnitList(Hashtable map);
        bool UpdateSSUnitInfo(SSUnit SSUnit);
        User FindUser(string userID);
        IList<User> GetUserList();
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(string userID);
       
        IList<ECInfo> ViewECList();
        bool UpdateDenseAllLotQTY(int qty);
        bool UpdateDenseLotStatus(int status);
        bool UpdateDenseCurrentLotQTY(int qty);
        HostInfo FindDenseLotInfo();
        IList<EventInfo> ViewEventList(string CEED);
        int UpdateECInfo(IList<ECInfo> lst);
        IList<EventInfo> ViewAllEventList();
        int UpdateAllEventInfo(string ceed);
        int UpdateEventInfo(Hashtable map);



         IList<EQPStatusRule> ViewEQPStatusRuleList(Hashtable map);
         bool InsertEQPStatusRule(EQPStatusRule EQPStatusRule);
         int DeleteEQPStatusRule(Hashtable map);



         IList<EQPStatusGroup> ViewEQPStatusGroupList(Hashtable map);
         bool InsertEQPStatusGroup(EQPStatusGroup EQPStatusGroup);
         int DeleteEQPStatusGroup(Hashtable map);




        IList<VCR> ViewVCRList(Hashtable map);
        int UpdateVCR(VCR VCR);
        bool InsertHisVCRResult(VCR VCR);
    }
}
