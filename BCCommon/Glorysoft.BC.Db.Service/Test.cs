using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;

namespace Glorysoft.BC.Db.Service
{

    public class Test : AbstractDbService, ITest
    {
        public bool InsertTestLog(TestLog item)
        {
            //return ExecuteInsert("InsertTestLog", item);
            return true;
        }
    }
}
