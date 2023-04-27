using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Service
{
    public class DbPalletService : AbstractDbService, IDbPalletService
    {
        public IList<his_pallet> Viewhis_palletList(Hashtable map)
        {
            return ExecuteQueryForList<his_pallet>("Viewhis_palletList", map) ?? new List<his_pallet>();
        }
        public IList<his_pallet> Viewhis_palletListCount(Hashtable map)
        {
            return ExecuteQueryForList<his_pallet>("Viewhis_palletListCount", map) ?? new List<his_pallet>();
        }
        public object Inserthis_pallet(his_pallet data)
        {
            return ExecuteQueryForObject("Inserthis_pallet", data);
        }
        public bool Updatehis_pallet(his_pallet data)
        {
            return ExecuteUpdate("Updatehis_pallet", data) == 1 ? true : false;
        }
    }
}
