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
    public class UnitInfoRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(UnitInfoRequest data,string userName)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            try
            {
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "execuleBcCommand",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                //Hashtable unitmap = new Hashtable
                //{
                //    {"EQPID",data.EQPID},
                //    {"UnitID",data.UnitID }
                //};

                //// var glassInfo = dbService.GetGlassInfoList(glassmap).FirstOrDefault();
                //var unitList = dbService.ViewUnitList(unitmap);
                var unitList = new List<Unit>();
                if(!string.IsNullOrEmpty(data.UnitID))
                {
                    //unitList.Add()
                    var unit=HostInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitID == data.UnitID);
                    if(unit!=null)
                    {
                        unitList.Add(unit);
                    }
                }else
                {
                    unitList.AddRange(HostInfo.EQPInfo.Units);
                }
                UnitInfoReport UnitInfoReport = new UnitInfoReport();
                foreach (var item in unitList)
                {
                    UnitInfoReportUnit UnitInfoReportUnit = new UnitInfoReportUnit();
                    UnitInfoReportUnit.EQPID = item.EQPID; 
                    //UnitInfoReportUnit.UnitPathNo = item.UnitPathNo;
                    UnitInfoReportUnit.UnitType = (int)item.UnitType;
                    UnitInfoReportUnit.UnitCapacity = item.UnitCapacity;
                    UnitInfoReportUnit.UnitID = item.UnitID;
                    UnitInfoReportUnit.UnitName = item.UnitName;
                    UnitInfoReportUnit.CRST = item.CRST.ToString();
                    UnitInfoReportUnit.UnitStatus = item.UnitStatus;
                    UnitInfoReportUnit.UnitSTCode = item.UnitSTCode;
                    UnitInfoReportUnit.ReasonCode = item.ReasonCode;
                    UnitInfoReportUnit.HasSUnit = item.HasSUnit;
                    UnitInfoReportUnit.IsConnect = item.IsConnect;
                    UnitInfoReportUnit.RTCode = item.RTCode;
                    UnitInfoReportUnit.UnitMode = item.UnitMode;
                    Hashtable map = new Hashtable
                    {
                        {"EQPID", item.EQPID},
                        { "UnitID",item.UnitID }
                    };
                    var vcr = dbService.ViewVCRList(map).FirstOrDefault();
                    if(vcr!=null)
                    {
                        UnitInfoReportUnit.VCREnableMode = vcr.VCREnableMode;
                        UnitInfoReportUnit.VCRReadFailOperationMode = vcr.VCRReadFailOperationMode;
                    }
                    
                    UnitInfoReportUnit.IndexerOperationMode = HostInfo.EQPInfo.IndexerOperationMode;
                    UnitInfoReportUnit.CassetteOperationMode = item.CassetteOperationMode;
                    UnitInfoReportUnit.PortQTime = item.PortQTime;
                    UnitInfoReport.UnitList.Add(UnitInfoReportUnit);
                }
                WebSocketResult result = new WebSocketResult() { returnCode="0",returnMessageCH="",returnMessageEN=""};
                WebSocketMessageStr = JsonFormat(UnitInfoReport, UnitInfoReport.TYPE, result);
                return WebSocketMessageStr;
                //SendOPIMessage.SendWebSocketTestMessage();

            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                return WebSocketMessageStr;
            }


        }
    }
}
