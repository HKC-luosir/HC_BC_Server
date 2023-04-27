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
    public class AddRecipeInfoRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, AddRecipeInfo InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "AddRecipeInfoRequestResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                for (int i = 0; i < InitData.recipeValueList.Count; i++)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("eqpid", InitData.eqpid);
                    hashtable.Add("ppid", InitData.ppid);
                    hashtable.Add("recipeid", InitData.recipeValueList[i].name == "" ? "0" : InitData.recipeValueList[i].name);
                    hashtable.Add("createuser", InitData.createUser);
                    var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(o => o.UnitName == InitData.recipeValueList[i].equipmentNo));
                    var unit = eqpinfo.Units.FirstOrDefault(o => o.UnitName == InitData.recipeValueList[i].equipmentNo);
                    hashtable.Add("unitid", unit.UnitID);
                    hashtable.Add("localid", unit.LocalNo);//Convert.ToInt32(InitData.recipeValueList[i].No)+1
                    dbService.Insertcfg_recipeppidmap(hashtable);
                }

                Hashtable hashtable2 = new Hashtable();
                hashtable2.Add("modepath", InitData.processMode);
                hashtable2.Add("machinerecipename", InitData.ppid);
                hashtable2.Add("eqpid", InitData.eqpid);
                hashtable2.Add("hascvd", InitData.hascvd);
                hashtable2.Add("remark", InitData.remark);
                dbService.Insertcfg_processmodemap(hashtable2);

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
                //    if(item.name!="0")
                //    {
                //        Parameter parameter = new Parameter();
                //        var unit = eqpinfo.Units.FirstOrDefault(o => o.UnitName == item.equipmentNo);
                //        parameter.ParameterName = unit.UnitID;
                //        parameter.ParameterValue = item.name;
                //        recipe.ParameterList.Add(parameter);
                //    }                    
                //}               
                //logicService.RecipeChanged(InitData.eqpid, recipe, "4");
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
            opiHis.Add("operating", "进行了RecipeInfo数据添加操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion

            return WebSocketMessageStr;
        }

    }
}
