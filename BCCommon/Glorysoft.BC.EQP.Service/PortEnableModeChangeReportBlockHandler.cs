
using Glorysoft.Auto.Contract.PLC;
using Glorysoft.BC.EQP.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.Test;

namespace Glorysoft.BC.EQP.Service
{
    class PortEnableModeChangeReportBlockHandler : AbstractEventHandler
    {
        public override void Execute(IPLCContext context, PLCData data)
        {
            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            try
            {
                BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} begin", CurrentThread, data.EQPName, this.GetType().Name));

                DateTime BeginTime = DateTime.Now;
                var portEnableMode = data.GetItemInt(PLCEventItem.PortEnableMode);
                var portName = data.Name.Substring(0, 5);
                var unit = HostInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitName == data.EQPName);
                PortInfo port;
                if (unit.UnitType == EnumUnitType.Loader)
                    port = HostInfo.PortList.FirstOrDefault(o => o.PortID == HostInfo.GetEQToBCValue(MESEventItem.PortName, portName));
                else
                    port = HostInfo.PortList.FirstOrDefault(o => o.PortID == HostInfo.GetEQToBCValue(MESEventItem.UnPortName, portName));
                port.PortEnableMode = portEnableMode;

                BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1}  UpdatePortInfo", CurrentThread, data.EQPName, this.GetType().Name));
                dbService.UpdatePortInfo(port);
                BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1}  InsertHisPortInfoResult", CurrentThread, data.EQPName, this.GetType().Name));
                dbService.InsertHisPortInfoResult(port);
                BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} end", CurrentThread, data.EQPName, this.GetType().Name));
            }
            catch (Exception ex)
            {
                BCLog.Error(string.Format("[HandlerName:{2}] [Thread:{0}]ex:{1}", CurrentThread, ex, this.GetType().Name));
            }

        }
    }
}

