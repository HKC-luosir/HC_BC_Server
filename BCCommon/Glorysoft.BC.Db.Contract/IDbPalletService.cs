using System.Collections.Generic;
using System.Collections;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbPalletService : IAutoRegister
    {
        IList<his_pallet> Viewhis_palletList(Hashtable Hashtable);
        IList<his_pallet> Viewhis_palletListCount(Hashtable Hashtable);
        object Inserthis_pallet(his_pallet data);
        bool Updatehis_pallet(his_pallet data);
    }
}
