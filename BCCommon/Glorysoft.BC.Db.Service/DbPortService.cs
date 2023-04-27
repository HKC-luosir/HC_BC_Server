using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System.Linq;
using System.Collections.ObjectModel;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Service
{
    public class DbPortService : AbstractDbService, IDbPortService
    {

        public IList<PortInfo> ViewPortList(Hashtable Hashtable)
        {
            return ExecuteQueryForList<PortInfo>("ViewPortList", Hashtable) ?? new List<PortInfo>();
        }
        public bool UpdatePortInfo(PortInfo oPort)
        {
            //UpdateCassette(oPort.CassetteInfo);
            return ExecuteUpdate("UpdatePortInfo", oPort) == 1 ? true : false;
        }
        public int UpdatePortWaitingforProcessingTime(Hashtable Hashtable)
        {
            return ExecuteUpdate("UpdatePortWaitingforProcessingTime", Hashtable);
        }
        public bool InsertHisPortInfoResult(PortInfo PortInfo)
        {
            return ExecuteInsert("InsertHisPortInfoResult", PortInfo);
        }

        public object InsertCassette(Cassette item)
        {
            return ExecuteQueryForObject("InsertCassette", item);
        }
        public bool UpdateCassette(Cassette item)
        {
            return ExecuteUpdate("UpdateCassette", item) == 1 ? true : false;
        }
        public bool UpdateHisCassette(Cassette item)
        {
            return ExecuteUpdate("UpdateHisCassette", item) == 1 ? true : false;
        }
        public bool UpdateCassetteHasCVD(Cassette item)
        {
            return ExecuteUpdate("UpdateCassetteHasCVD", item) == 1 ? true : false;
        }
        public bool UpdateCassetteStartTime(Cassette item)
        {
            return ExecuteUpdate("UpdateCassetteStartTime", item) == 1 ? true : false;
        }
        public bool UpdateCassetteEndTime(Cassette item)
        {
            return ExecuteUpdate("UpdateCassetteEndTime", item) == 1 ? true : false;
        }
        public int DeleteCassetteList(Hashtable map)
        {
            return ExecuteDelete("DeleteCassetteList", map);
        }
        public int DeleteCassetteByDateTime()
        {
            DateTime datetime = DateTime.Now.AddDays(-3);
            var stringTime = datetime.ToString("yyyy-MM-dd");
            return ExecuteDelete("DeleteCassetteByDateTime", stringTime);
        }
        public IList<Cassette> GetCassetteList(Hashtable Hashtable)
        {
            return ExecuteQueryForList<Cassette>("GetCassetteList", Hashtable) ?? new List<Cassette>();
        }
        public bool InsertHisCassette(Cassette item)
        {
            return ExecuteInsert("InsertHisCassette", item);
        }
        //public bool InsertCassetteInfo(Cassette Cassette)
        //{
        //    try
        //    {

        //        //var Hashtable = new Hashtable
        //        //{
        //        //    {"CassetteID",Cassette.CassetteID },
        //        //    {"PortNo",Cassette.PortNo },
        //        //    {"CassetteSequence",Cassette.CassetteSequence}
        //        //};
        //        //var CassetteList = ViewCassetteList(Hashtable);
        //        //if (CassetteList.Count >= 1)
        //        //{
        //        //    return UpdateCassetteInfo(Cassette);
        //        //}
        //        //else
        //        //{
        //            return ExecuteInsert("InsertCassetteInfo", Cassette);
        //        //}
        //        //return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        //public IList<Cassette> ViewCassetteList(Hashtable Hashtable)
        //{
        //    return ExecuteQueryForList<Cassette>("ViewCassetteList", Hashtable) ?? new List<Cassette>();
        //}
        //public bool UpdateCassetteInfo(Cassette Cassette)
        //{
        //    return ExecuteUpdate("UpdateCassetteInfo", Cassette) == 1 ? true : false;
        //}

        public bool Insertcfg_portgradegroup(cfg_portgradegroup data)
        {
            return ExecuteInsert("Insertcfg_portgradegroup", data);
        }

        public IList<cfg_portgradegroup> Viewcfg_portgradegroup(Hashtable map)
        {
            return ExecuteQueryForList<cfg_portgradegroup>("Viewcfg_portgradegroup", map) ?? new List<cfg_portgradegroup>();
        }

        public bool Updatecfg_portgradegroup(cfg_portgradegroup data)
        {
            return ExecuteUpdate("Updatecfg_portgradegroup", data) == 1 ? true : false;
        }

        public bool Deletecfg_portgradegroup(Hashtable data)
        {
            return ExecuteDelete("Deletecfg_portgradegroup", data) == 1 ? true : false;
        }
    }
}
