using System.Collections.Generic;
using System.Collections;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbProcessEndService : IAutoRegister
    {
        object Insertwip_processend(wip_processend data);
        bool Insertwip_processend_glass(wip_processend_glass data);
        IList<wip_processend> Viewwip_processendList(Hashtable data);
        IList<wip_processend_glass> Viewwip_processend_glassList(Hashtable data);
        bool Updatewip_processend(wip_processend data);
        bool Updatewip_processend_glass(wip_processend_glass data);
        bool Deletewip_processend(Hashtable data);
        bool Deletewip_processend_glass(Hashtable data);
    }
}
