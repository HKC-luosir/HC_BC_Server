using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Service
{
    public class DbProcessEndService : AbstractDbService, IDbProcessEndService
    {
        public object Insertwip_processend(wip_processend data)
        {
            return ExecuteQueryForObject("Insertwip_processend", data);
        }
        public bool Insertwip_processend_glass(wip_processend_glass data)
        {
            return ExecuteInsert("Insertwip_processend_glass", data);
        }
        public IList<wip_processend> Viewwip_processendList(Hashtable data)
        {
            return ExecuteQueryForList<wip_processend>("Viewwip_processendList", data) ?? new List<wip_processend>();
        }
        public IList<wip_processend_glass> Viewwip_processend_glassList(Hashtable data)
        {
            return ExecuteQueryForList<wip_processend_glass>("Viewwip_processend_glassList", data) ?? new List<wip_processend_glass>();
        }
        public bool Updatewip_processend(wip_processend data)
        {
            return ExecuteUpdate("Updatewip_processend", data) == 1 ? true : false;
        }
        public bool Updatewip_processend_glass(wip_processend_glass data)
        {
            return ExecuteUpdate("Updatewip_processend_glass", data) == 1 ? true : false;
        }
        public bool Deletewip_processend(Hashtable data)
        {
            return ExecuteUpdate("Deletewip_processend", data) == 1 ? true : false;
        }
        public bool Deletewip_processend_glass(Hashtable data)
        {
            return ExecuteUpdate("Deletewip_processend_glass", data) == 1 ? true : false;
        }
    }
}
