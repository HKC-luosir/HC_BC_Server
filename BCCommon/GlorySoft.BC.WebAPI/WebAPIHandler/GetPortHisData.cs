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
   public class GetPortHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName,Dictionary<string, object> unitHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "GetPortHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, portid, unitid, firstdate, lastdate;
                unitHis.TryGetValue("pageNum", out pageNum);
                unitHis.TryGetValue("pageSize", out pageSize);
                unitHis.TryGetValue("unitid", out unitid);
                unitHis.TryGetValue("portid", out portid);
                unitHis.TryGetValue("firstdate", out firstdate);
                unitHis.TryGetValue("lastdate", out lastdate);

                var serdata = new Hashtable();
                if (unitid != null)
                {
                    serdata.Add("unitid", unitid);
                }
                if (portid != null)
                {
                    serdata.Add("portid", portid);
                }
                if (firstdate != null)
                {
                    serdata.Add("startcreatedate", firstdate);
                }
                if (lastdate != null)
                {
                    serdata.Add("endcreatedate", lastdate);
                }
                var portcount = dbService.Viewhis_portCount(serdata);
                if (pageNum != null)
                {
                    serdata.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    serdata.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var port = dbService.Viewhis_port(serdata);
                //var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                unitHis.Add("total", portcount.Count);

                List<Ports> portdata = new List<Ports>();
                if (port != null && port.Count() > 0)
                {
                    foreach (var li in port)
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
                        portss.updateDate = li.updatedate;

                        portdata.Add(portss);
                    }
                }
                unitHis.Add("rows", portdata);

                WebSocketMessageStr.body = unitHis;
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
