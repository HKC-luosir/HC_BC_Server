using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class SendMESProcessEndData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "sendMESProcessEndData",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                object id;
                InitData.TryGetValue("id", out id);

                #region 上报MES
                var serdata = new Hashtable();
                if (id != null)
                {
                    serdata.Add("id", id);
                }
                var datas = dbService.Viewwip_processendList(serdata);
                var serdataglass = new Hashtable();
                if (id != null)
                {
                    serdataglass.Add("parentid", id);
                }
                var dataglasss = dbService.Viewwip_processend_glassList(serdataglass);

                if (datas != null && datas.Count > 0)
                {
                    var data = datas.FirstOrDefault();
                    RVLotProcessEnd lotProcessEnd = new RVLotProcessEnd();
                    lotProcessEnd.EQUIPMENTID = data.equipmentid;
                    lotProcessEnd.PORTID = data.portid;
                    lotProcessEnd.PORTTYPE = data.porttype;
                    lotProcessEnd.PARTNAME = data.partname;
                    lotProcessEnd.STEPNAME = data.stepname;
                    lotProcessEnd.DURABLEID = data.durableid;
                    lotProcessEnd.OPERATOR = data.operatorid;
                    lotProcessEnd.ACTIONCOMMENT = data.actioncomment;
                    lotProcessEnd.PANELLIST = new List<PANEL>();
                    if (dataglasss != null && dataglasss.Count > 0)
                    {
                        foreach (var panel in dataglasss)
                        {
                            PANEL mesPanel = new PANEL();
                            mesPanel.PANELID = panel.panelid;
                            mesPanel.BONDINGID = panel.blid;
                            mesPanel.GRADE = panel.grade;
                            mesPanel.POSITION = panel.position;
                            mesPanel.ACTIONCOMMENT = panel.actioncomment;
                            mesPanel.ABNORMALCODE = panel.abnormalcode;
                            if (!string.IsNullOrEmpty(panel.defectcode) && !String.IsNullOrEmpty(panel.defectcode.TrimEnd(';')))
                            {
                                mesPanel.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = panel.defectcode.TrimEnd(';'), DEFECTMAIN = "Y" });
                            }
                            //if (!string.IsNullOrEmpty(panel.ProcessingCount) && !String.IsNullOrEmpty(panel.ProcessingCount.TrimEnd(';')))
                            //{
                            //    var processunits = panel.ProcessingCount.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            //    for (int i = 0; i < processunits.Length; i++)
                            //    {
                            //        if (HostInfo.Current.AllEQPInfo.Any(c => c.Units.Any(d => d.LocalNo == Convert.ToInt32(processunits[i]))))
                            //        {
                            //            var unitinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.LocalNo == Convert.ToInt32(processunits[i]))).Units.FirstOrDefault(d => d.LocalNo == Convert.ToInt32(processunits[i]));
                            //            mesPanel.PROCESSEDUNITLIST.Add(new PROCESSEDUNIT() { PROCESSEDUNITNAME = unitinfo.UnitID });
                            //        }
                            //    }
                            //}
                            lotProcessEnd.PANELLIST.Add(mesPanel);
                        }
                    }
                    lotProcessEnd.MAINQTY = lotProcessEnd.PANELLIST.Count.ToString();
                    var replyHeader = rvService.SendToMESLotProcessEnd(lotProcessEnd.EQUIPMENTID, lotProcessEnd, HostInfo.GetTransactionID());
                    if (replyHeader != null)
                    {
                        if (replyHeader.RESULT == MESResult.SUCCESS.ToString())
                        {
                            #region 发送成功后 删除数据
                            dbService.Deletewip_processend(serdata);
                            dbService.Deletewip_processend_glass(serdataglass);
                            #endregion
                        }
                        else//失败则更新数据库
                        {
                            data.returncode = replyHeader.RESULT;
                            data.returnmsg = replyHeader.RESULTMESSAGE;
                            dbService.Updatewip_processend(data);
                        }
                    }
                }
                #endregion

                #region 发送成功后 删除数据
                //var serData = new Hashtable();
                //if (id != null)
                //{
                //    serData.Add("id", id);
                //}
                //dbService.Deletewip_processend(serData);

                //var serGlassData = new Hashtable();
                //if (id != null)
                //{
                //    serGlassData.Add("parentid", id);
                //}
                //dbService.Deletewip_processend_glass(serGlassData);
                #endregion

                WebSocketMessageStr.body = null;
                #endregion

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
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了ProcessEnd数据上报MES操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
