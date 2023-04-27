﻿using System;
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
    public class GetUnitList : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SelectLineStatusSpecResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                //Hashtable hashtable = new Hashtable();
                //foreach (var item in InitData)
                //{
                //    hashtable.Add(item.Key, item.Value);
                //}
                //var body = dbService.Viewcfg_unit(hashtable);
                List<cfg_unit> allunit = new List<cfg_unit>();
                var eqpids = HostInfo.Current.EQPID.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var eqpid in eqpids)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("eqpid", eqpid);
                    var eqpunit = dbService.Viewcfg_unit(hashtable);
                    if (eqpunit != null && eqpunit.Count > 0)
                    {
                        eqpunit = eqpunit.OrderBy(c => c.localno).ToList();
                        allunit.AddRange(eqpunit);
                    }
                }

                WebSocketMessageStr.body = allunit;
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
