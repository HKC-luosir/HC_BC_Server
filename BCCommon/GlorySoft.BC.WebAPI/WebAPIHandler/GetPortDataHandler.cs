using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetPortDataHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getPortData",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion


                Hashtable sunitmap = new Hashtable();
                foreach (var item in InitHistory)
                {
                    sunitmap.Add(item.Key, item.Value);
                }
                IList<cfg_port> list = dbService.Viewcfg_port(sunitmap);

                List<Ports> portdata = new List<Ports>();
                foreach (var li in list)
                {
                    Ports portss = new Ports();
                    portss.equipmentNo = li.eqpid;
                    portss.unitId = li.unitid;
                    portss.unitName = li.unitname;
                    portss.portNo = li.portno.ToString();
                    portss.portId = li.portid;
                    portss.porttype = li.porttype;

                    string sPortMode = li.portmode.ToString().PadLeft(5, '0');
                    //int iSubstrate_Type = Convert.ToInt32(sPortMode.Substring(0, 1));
                    //int iJob_Type = Convert.ToInt32(sPortMode.Substring(1, 2));
                    //int iJudge_Port_Use_Type = Convert.ToInt32(sPortMode.Substring(3, 2));
                    string sPortMode_Substrate_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Substrate_Type, sPortMode.Substring(0, 1));//Consts.dicPortMode_Substrate_Type.ContainsKey(iSubstrate_Type) ? Consts.dicPortMode_Substrate_Type[iSubstrate_Type] : "";
                    string sPortMode_Job_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Job_Type, sPortMode.Substring(1, 2));//Consts.dicPortMode_Job_Type.ContainsKey(iJob_Type) ? Consts.dicPortMode_Job_Type[iJob_Type] : "";
                    string sPortMode_Judge_Port_Use_Type = HostInfo.Current.GetEQToBCValue(MESEventItem.PortMode_Judge_Port_Use_Type, sPortMode.Substring(3, 2));//Consts.dicPortMode_Judge_Port_Use_Type.ContainsKey(iJudge_Port_Use_Type) ? Consts.dicPortMode_Judge_Port_Use_Type[iJudge_Port_Use_Type] : "";
                    portss.portMode = sPortMode_Substrate_Type + "/" + sPortMode_Job_Type + "/" + sPortMode_Judge_Port_Use_Type;

                    portss.portCSTType = HostInfo.Current.GetEQToBCValue(MESEventItem.PortCassetteType, li.portcsttype.ToString());//Consts.dicPortCassetteType.ContainsKey(li.portcsttype) ? Consts.dicPortCassetteType[li.portcsttype] : "";
                    portss.portStatus = HostInfo.Current.GetEQToBCValue(MESEventItem.PortStatus, li.portstatus.ToString());//Consts.dicPortStatus.ContainsKey(li.portstatus) ? Consts.dicPortStatus[li.portstatus] : "";
                    //portss.portOperationMode = li.PortOperationMode.ToString();
                    portss.portTypeAutoChg = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTypeAutoChangeMode, li.porttypeautochangemode.ToString());//Consts.dicPortTypeAutoChangeMode.ContainsKey(li.porttypeautochangemode) ? Consts.dicPortTypeAutoChangeMode[li.porttypeautochangemode] : "";
                    portss.portQTime = li.portqtime;
                    portss.portGrade = li.portgrade;
                    portss.portEnableMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortEnableMode, li.portenablemode.ToString());//Consts.dicPortEnableMode.ContainsKey(li.portenablemode) ? Consts.dicPortEnableMode[li.portenablemode] : "";
                    portss.portPauseMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortPauseMode, li.portpausemode.ToString());//Consts.dicPortPauseMode.ContainsKey(li.portpausemode) ? Consts.dicPortPauseMode[li.portpausemode] : "";
                    portss.transferMode = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTransferMode, li.transfermode);//Consts.dicPortTransferMode.ContainsKey(Convert.ToInt16(li.transfermode)) ? Consts.dicPortTransferMode[Convert.ToInt16(li.transfermode)] : "";
                    portss.cassetteSeq = li.cassettesequenceno;
                    portss.cstid = li.cassetteid;

                    portdata.Add(portss);
                }

                WebSocketMessageStr.body = portdata;

                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };

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
