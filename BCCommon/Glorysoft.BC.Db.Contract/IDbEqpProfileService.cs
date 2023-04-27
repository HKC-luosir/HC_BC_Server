using System.Collections.Generic;
using System.Collections;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbEqpProfileService : IAutoRegister
    {
        object Insertcfg_eqpprofile(cfg_eqpprofile data);
        object Insertcfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data);
        object Insertcfg_eqpprofile_item(cfg_eqpprofile_item data);

        IList<cfg_eqpprofile> Viewcfg_eqpprofile(Hashtable Hashtable);
        IList<cfg_eqpprofile_itemgroup> Viewcfg_eqpprofile_itemgroup(Hashtable Hashtable);
        IList<cfg_eqpprofile_item> Viewcfg_eqpprofile_item(Hashtable Hashtable);

        bool Updatecfg_eqpprofile(cfg_eqpprofile data);
        bool Updatecfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data);
        bool Updatecfg_eqpprofile_item(cfg_eqpprofile_item data);

        bool Deletecfg_eqpprofile(Hashtable Hashtable);
        bool Deletecfg_eqpprofile_itemgroup(Hashtable Hashtable);
        bool Deletecfg_eqpprofile_item(Hashtable Hashtable);
    }
}
