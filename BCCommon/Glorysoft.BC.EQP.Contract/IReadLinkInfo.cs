using System;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;
using log4net;

namespace Glorysoft.BC.EQP.Contract
{

    public interface IReadLinkInfo : IAutoRegister
    {
        void Start();
    }
}
