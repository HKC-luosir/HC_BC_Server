using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class UnitInformationRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(Units Units)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "UnitInformationResponse",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = "T7-0.0.2",
                uuid = null,
                userName = "admin"
            };
            #endregion
            try
            {
                #region Body
                Units units = new Units();
                if (HostInfo.Current.AllEQPInfo.Any(c => c.Units.Any(d => d.SUnitList.Any(e => e.SUnitID == Units.subUnitId))))
                {
                    var eqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.SUnitList.Any(e => e.SUnitID == Units.subUnitId)));
                    var unit = eqp.Units.FirstOrDefault(d => d.SUnitList.Any(e => e.SUnitID == Units.subUnitId));
                    var sunit = unit.SUnitList.FirstOrDefault(e => e.SUnitID == Units.subUnitId);
                    units.unitId = sunit.UnitID;
                    units.subUnitId = sunit.SUnitID;
                    units.subUnitName = sunit.SUnitName;
                    units.subUnitNo = sunit.SubUnitNo;
                    units.sUnitStatus = sunit.SUnitStatus;
                    units.sUnitSTCode = sunit.SUnitSTCode;
                    units.unitNo = sunit.SubUnitNo;
                    units.currentStauts = sunit.SUnitStatus;
                }

                //Units units = new Units();
                //for (int i = 0; i < HostInfo.Current.EQPInfo.Units.Count; i++)
                //{
                //    var type = HostInfo.Current.EQPInfo.Units[i].GetType().Name;
                //    if (type != "Unit" && type != "Robot")
                //    {
                //        continue;
                //    }
                //    var sunit = HostInfo.Current.EQPInfo.Units[i].SUnitList.Where(t => t.SUnitID == Units.subUnitId).ToList();
                //    for (int k = 0; k < sunit.Count; k++)
                //    {
                //        units.unitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].UnitID;
                //        units.subUnitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitID;
                //        units.subUnitName = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitName;
                //        units.subUnitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
                //        units.sUnitStatus = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;
                //        units.sUnitSTCode = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitSTCode;

                //        units.unitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
                //        units.currentStauts = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;
                //    }

                //}
                //    HostInfo.Current.EQPInfo.Units[i].SUnitList


                UnitInformationResponseReport UnitInformationResponseReport = new UnitInformationResponseReport()
                {
                    unit = units
                };




                WebSocketMessageStr.body = UnitInformationResponseReport;

                #endregion
                #region result;
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Operation failed !",
                    returnMessageCH = "操作失败！"
                };
            }
            return WebSocketMessageStr;
        }
    }
}
