
using System;
using System.Collections.Generic;
using System.Collections;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDBConfigService : IAutoRegister
    {
        //SystemConfig ViewSystemConfig(string eqpid);

         IList<CFGS1F5> ViewCFGS1F5(Hashtable map);
         bool InsertCFGS1F5(CFGS1F5 CFGS1F5);
         int UpdateCFGS1F5(CFGS1F5 CFGS1F5);
         int DeleteCFGS1F5(CFGS1F5 CFGS1F5);

        IList<GlassExistencePosition> ViewGlassExistencePosition(Hashtable map);
        // int SaveGlassExistencePosition(GlassExistencePosition GlassExistencePosition);
        int UpdateGlassExistencePosition(GlassExistencePosition GlassExistencePosition);

        IList<OPILink> ViewOPILink(Hashtable map);


        IList<CFGOLDPriority> ViewCFGOLDPriority(Hashtable map);
        int UpdateCFGOLDPriority(CFGOLDPriority CFGOLDPriority);
    }
}
