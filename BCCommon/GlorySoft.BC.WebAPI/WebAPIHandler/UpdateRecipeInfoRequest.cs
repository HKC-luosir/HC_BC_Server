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
    public class UpdateRecipeInfoRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, AddRecipeInfo InitData,string type)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = type,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == InitData.eqpid);
                for (int i = 0; i < InitData.recipeValueList.Count; i++)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("recipeid", InitData.recipeValueList[i].name == "" ? "0" : InitData.recipeValueList[i].name);

                    hashtable.Add("eqpid", InitData.eqpid);
                    var unit = eqpinfo.Units.FirstOrDefault(o => o.UnitName == InitData.recipeValueList[i].equipmentNo);
                    hashtable.Add("unitid", unit.UnitID);
                    hashtable.Add("ppid", InitData.ppid);
                    dbService.Updatecfg_recipeppidmap(hashtable);
                }

                Hashtable hashtable2 = new Hashtable();
                hashtable2.Add("modepath", InitData.processMode);
                hashtable2.Add("machinerecipename", InitData.ppid);
                hashtable2.Add("eqpid", InitData.eqpid);
                hashtable2.Add("hascvd", InitData.hascvd);
                hashtable2.Add("remark", InitData.remark);
                dbService.Updatecfg_processmodemap(hashtable2);

                //#region send to mes RecipeChanged
                //Recipe recipe = new Recipe();
                //recipe.RecipeNo = InitData.ppid;
                //recipe.RecipeVersion = DateTime.Now.ToString("yyyyMMddHHmmss");
                //recipe.ParameterCount = InitData.recipeValueList.Count().ToString();
                //recipe.RecipeType = "E";
                //recipe.EventID = "";
                //recipe.PreviousRecipeNo = "";//HostInfo.IntToString(PreviousRecipeNo);
                //recipe.RecipeChangeTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //foreach (var item in InitData.recipeValueList)
                //{
                //    if (item.name != "0")
                //    {
                //        Parameter parameter = new Parameter();
                //        var unit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == item.equipmentNo);
                //        parameter.ParameterName = unit.UnitID;
                //        parameter.ParameterValue = item.name;
                //        recipe.ParameterList.Add(parameter);
                //    }                      
                //}
                //logicService.RecipeChanged(InitData.eqpid, recipe, "2");
                //#endregion

                WebSocketMessageStr.body = null;
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
            opiHis.Add("operating", "进行了RecipeInfo数据更新操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
