using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{
    #region 需求3 1.EIP通讯状态变化 liuyusen 20221010
    public class MachineAliveHandler : AbstractEventHandler
    {
        public MachineAliveHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                //LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.BitItem.Name, args.Name);
                //var plcmsg = args.Message;
                //var txid = args.Message.TransactionID;
                var eqpName = args.Name;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    //LogHelper.EIPLog.ErrorFormat("+++ CIMModeHandler:{0} Cannot Find EQPInfo +++", args.Name);
                    return;
                }
                //var MachineAlive = args.BitItem.Value;
                oEQP.AliveUpdate = DateTime.Now;
                //logicService.CIMModeChange(oEQP, CIMMode);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ MachineAliveHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
    #endregion
}
