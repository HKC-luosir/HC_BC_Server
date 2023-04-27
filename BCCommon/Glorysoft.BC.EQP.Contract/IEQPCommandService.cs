using System;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;
using log4net;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.EQP.Contract
{
    public interface IEQPCommandService : IAutoRegister
    {
        #region Yuan
        bool CassetteMapDownloadCommand(string eqpName, List<GlassInfo> glassInfoList, string portNo, int capacity, int iMapDownLoadDelay);
        #endregion
    }
}
