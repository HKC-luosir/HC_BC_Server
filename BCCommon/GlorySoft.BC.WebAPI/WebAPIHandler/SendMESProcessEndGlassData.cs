using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class SendMESProcessEndGlassData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "sendMESProcessEndGlassData",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                object id;
                InitData.TryGetValue("id", out id);

                var serdata = new Hashtable();
                if (id != null)
                {
                    serdata.Add("id", id);
                }
                var datas = dbService.Viewwip_processend_glassList(serdata);

                if (datas != null && datas.Count > 0)
                {
                    var data = datas.FirstOrDefault();
                    #region 上报MES
                    RVPanelTrackInOut paneltrackinout = new RVPanelTrackInOut();
                    paneltrackinout.EQUIPMENTID = data.equipmentid;
                    paneltrackinout.PANELID = data.panelid;
                    paneltrackinout.LOTTYPE = data.lottype;
                    paneltrackinout.BONDINGID = data.blid;
                    paneltrackinout.GRADE = data.grade;
                    paneltrackinout.POSITION = data.position;
                    paneltrackinout.ABNORMALCODE = data.abnormalcode;
                    if (!String.IsNullOrEmpty(data.defectcode.TrimEnd(';')))
                        paneltrackinout.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = data.defectcode.TrimEnd(';'), DEFECTMAIN = "Y" });
                    var resMes = rvService.SendToMESPanelTrackInOutReport(data.equipmentid, paneltrackinout, HostInfo.GetTransactionID());

                    if (resMes != null)
                    {
                        if (resMes.RESULT == MESResult.SUCCESS.ToString())
                        {
                            #region 发送成功后 删除数据
                            var serGlassData = new Hashtable();
                            if (id != null)
                            {
                                serGlassData.Add("id", id);
                            }
                            dbService.Deletewip_processend_glass(serGlassData);
                            #endregion
                        }
                        else//失败则更新数据库
                        {
                            data.returncode = resMes.RESULT;
                            data.returnmsg = resMes.RESULTMESSAGE;
                            dbService.Updatewip_processend_glass(data);
                        }
                    }
                    #endregion
                }

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
            opiHis.Add("operating", "进行了ProcessEndGlass数据上报MES操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
