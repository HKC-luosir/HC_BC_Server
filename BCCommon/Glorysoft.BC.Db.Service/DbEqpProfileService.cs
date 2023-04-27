using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Service
{
    public class DbEqpProfileService : AbstractDbService, IDbEqpProfileService
    {
        public object Insertcfg_eqpprofile(cfg_eqpprofile data)
        {
            return ExecuteQueryForObject("Insertcfg_eqpprofile", data);
        }
        public object Insertcfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data)
        {
            return ExecuteQueryForObject("Insertcfg_eqpprofile_itemgroup", data);
        }
        public object Insertcfg_eqpprofile_item(cfg_eqpprofile_item data)
        {
            return ExecuteQueryForObject("Insertcfg_eqpprofile_item", data);
        }

        public IList<cfg_eqpprofile> Viewcfg_eqpprofile(Hashtable map)
        {
            return ExecuteQueryForList<cfg_eqpprofile>("Viewcfg_eqpprofile", map) ?? new List<cfg_eqpprofile>();
        }
        public IList<cfg_eqpprofile_itemgroup> Viewcfg_eqpprofile_itemgroup(Hashtable map)
        {
            return ExecuteQueryForList<cfg_eqpprofile_itemgroup>("Viewcfg_eqpprofile_itemgroup", map) ?? new List<cfg_eqpprofile_itemgroup>();
        }
        public IList<cfg_eqpprofile_item> Viewcfg_eqpprofile_item(Hashtable map)
        {
            return ExecuteQueryForList<cfg_eqpprofile_item>("Viewcfg_eqpprofile_item", map) ?? new List<cfg_eqpprofile_item>();
        }

        public bool Updatecfg_eqpprofile(cfg_eqpprofile data)
        {
            return ExecuteUpdate("Updatecfg_eqpprofile", data) == 1 ? true : false;
        }
        public bool Updatecfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data)
        {
            return ExecuteUpdate("Updatecfg_eqpprofile_itemgroup", data) == 1 ? true : false;
        }
        public bool Updatecfg_eqpprofile_item(cfg_eqpprofile_item data)
        {
            return ExecuteUpdate("Updatecfg_eqpprofile_item", data) == 1 ? true : false;
        }

        public bool Deletecfg_eqpprofile(Hashtable data)
        {
            return ExecuteDelete("Deletecfg_eqpprofile", data) == 1 ? true : false;
        }
        public bool Deletecfg_eqpprofile_itemgroup(Hashtable data)
        {
            return ExecuteDelete("Deletecfg_eqpprofile_itemgroup", data) == 1 ? true : false;
        }
        public bool Deletecfg_eqpprofile_item(Hashtable data)
        {
            return ExecuteDelete("Deletecfg_eqpprofile_item", data) == 1 ? true : false;
        }
    }
}
