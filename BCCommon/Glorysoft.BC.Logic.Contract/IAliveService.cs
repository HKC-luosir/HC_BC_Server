using Glorysoft.Auto.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{
    public interface IAliveService : IAutoRegister
    {
        void MonitorPLCAlive();
    }
}
