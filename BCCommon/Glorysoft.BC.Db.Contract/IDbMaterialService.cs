using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbMaterialService:IAutoRegister
    {
        IList<MaterialInfo> ViewMaterialInfo(MaterialInfo data);
        bool InsertMaterialHistory(MaterialInfo data);
        bool InsertMaterialInfo(MaterialInfo data);
        bool UpdateMaterialInfo(MaterialInfo data);
        bool DeleteMaterialInfo(MaterialInfo data);
    }
}
