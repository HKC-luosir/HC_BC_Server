
using System;
using System.Collections.Generic;
using System.Collections;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDBDVDataService : IAutoRegister
    {
        IList<DVData> ViewDVDataList(Hashtable map);      
        bool InsertDVData(DVData DVData);

        int UpdateDVData(DVData DVData);

        int DeleteDVData(Hashtable map);




        IList<SVData> ViewSVDataList(Hashtable map);
        bool InsertSVData(SVData SVData);

        int UpdateSVData(SVData SVData);

        int DeleteSVData(Hashtable map);

    }
}
