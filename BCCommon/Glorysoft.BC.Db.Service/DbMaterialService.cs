using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Db.Service
{
    public class DbMaterialService : AbstractDbService, IDbMaterialService
    {
        public IList<MaterialInfo> ViewMaterialInfo(MaterialInfo data)
        {
            return ExecuteQueryForList<MaterialInfo>("ViewMaterialInfo", data);
        }
        public bool InsertMaterialHistory(MaterialInfo data)
        {
            return ExecuteInsert("InsertMaterialHistory", data);
        }
        public bool InsertMaterialInfo(MaterialInfo data)
        {
            return ExecuteInsert("InsertMaterialInfo", data);
        }
        public bool UpdateMaterialInfo(MaterialInfo data)
        {
            return ExecuteUpdate("UpdateMaterialInfo", data) == 1 ? true : false;
        }
        public bool DeleteMaterialInfo(MaterialInfo data)
        {
            return ExecuteDelete("DeleteMaterialInfo", data) == 1 ? true : false;
        }
    }
}
