using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Db.Service
{

    public class DBDVDataService : AbstractDbService, IDBDVDataService
    {
        public IList<DVData> ViewDVDataList(Hashtable map)
        {
            return ExecuteQueryForList<DVData>("ViewDVDataList", map);
        }
        public bool InsertDVData(DVData DVData)
        {
            return ExecuteInsert("InsertDVData", DVData);
        }
        public int UpdateDVData(DVData DVData)
        {
            return ExecuteUpdate("UpdateDVData", DVData);
        }
        public int DeleteDVData(Hashtable map)
        {
            return ExecuteDelete("DeleteDVData", map);
        }



        public IList<SVData> ViewSVDataList(Hashtable map)
        {
            return ExecuteQueryForList<SVData>("ViewSVDataList", map);
        }
        public bool InsertSVData(SVData SVData)
        {
            return ExecuteInsert("InsertSVData", SVData);
        }
        public int UpdateSVData(SVData SVData)
        {
            return ExecuteUpdate("UpdateSVData", SVData);
        }
        public int DeleteSVData(Hashtable map)
        {
            return ExecuteDelete("DeleteSVData", map);
        }
    }
}
