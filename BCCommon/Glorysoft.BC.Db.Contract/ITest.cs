
using System;
using System.Collections.Generic;
using System.Collections;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.ServiceModel;

namespace Glorysoft.BC.Db.Contract
{
    public interface ITest : IAutoRegister
    {
       
        bool InsertTestLog(TestLog item);
    }
}
