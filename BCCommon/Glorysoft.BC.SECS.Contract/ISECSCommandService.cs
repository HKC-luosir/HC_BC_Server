using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.SECSEntity;

namespace Glorysoft.BC.SECS.Contract
{
    public interface ISECSCommandService : IAutoRegister
    {
        /// <summary>
        /// 建立通讯连接
        /// </summary>
        bool S1F1Command(string eqpName, string eqptype);
        /// <summary>
        /// 查询SVID
        /// </summary>
        SelectSVIDResponse S1F3Command_Sync(SelectSVIDRequest data);

        /// <summary>
        /// 建立通讯连接
        /// </summary>
        EstablishCommResponse S1F13Command_Sync(EstablishCommRequest data);
    }
}
