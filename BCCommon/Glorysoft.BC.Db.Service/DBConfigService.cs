using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Db.Service
{    
    public class DBConfigService : AbstractDbService, IDBConfigService
    {
        //public SystemConfig ViewSystemConfig(string eqpid)
        //{
        //    return ExecuteQueryForObject<SystemConfig>("ViewSystemConfig", eqpid);
        //}


        public IList<CFGS1F5> ViewCFGS1F5(Hashtable map)
        {
            return ExecuteQueryForList<CFGS1F5>("ViewCFGS1F5", map);
        }
        public bool InsertCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return ExecuteInsert("InsertCFGS1F5", CFGS1F5);
        }
        public int UpdateCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return ExecuteUpdate("UpdateCFGS1F5", CFGS1F5);
        }
        
        public int DeleteCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return ExecuteDelete("DeleteCFGS1F5", CFGS1F5);
        }

        public IList<GlassExistencePosition> ViewGlassExistencePosition(Hashtable map)
        {
            return ExecuteQueryForList<GlassExistencePosition>("ViewGlassExistencePosition", map);
        }
        //public bool InsertGlassExistencePosition(GlassExistencePosition GlassExistencePosition)
        //{
        //    return ExecuteInsert("InsertGlassExistencePosition", GlassExistencePosition);
        //}
        //public int SaveGlassExistencePosition(GlassExistencePosition GlassExistencePosition)
        //{
        //    Hashtable map = new Hashtable();
        //    map.Add("CassetteSequenceNo", GlassExistencePosition.CassetteSequenceNo);
        //    map.Add("SlotSequenceNo", GlassExistencePosition.SlotSequenceNo);
        //    var GlassExistencePositionList=  ViewGlassExistencePosition(map);
        //    var GlassExistencePositionItem = GlassExistencePositionList.FirstOrDefault();
        //    if(GlassExistencePositionItem!=null)
        //    {
        //        return ExecuteUpdate("UpdateGlassExistencePosition", GlassExistencePosition);
        //    }
        //   else
        //    {
        //        return ExecuteInsert("InsertGlassExistencePosition", GlassExistencePosition)==true?1:0;
        //    }
        //}
        public int UpdateGlassExistencePosition(GlassExistencePosition GlassExistencePosition)
        {
            return ExecuteUpdate("UpdateGlassExistencePosition", GlassExistencePosition);
        }


        public IList<OPILink> ViewOPILink(Hashtable map)
        {
            return ExecuteQueryForList<OPILink>("ViewOPILink", map);
        }


        public IList<CFGOLDPriority> ViewCFGOLDPriority(Hashtable map)
        {
            return ExecuteQueryForList<CFGOLDPriority>("ViewCFGOLDPriority", map);
        }
        public int UpdateCFGOLDPriority(CFGOLDPriority CFGOLDPriority)
        {
            return ExecuteUpdate("UpdateCFGOLDPriority", CFGOLDPriority);
        }
    }
}
