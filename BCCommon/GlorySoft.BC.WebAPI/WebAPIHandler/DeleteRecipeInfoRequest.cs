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
    public class DeleteRecipeInfoRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
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

                object eqpid, ppid;
                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("ppid", out ppid);

                Hashtable hashtable = new Hashtable();
                hashtable.Add("eqpid", eqpid);
                hashtable.Add("ppid", ppid);


                PPIDAndRecipe PPIDAndRecipe = new PPIDAndRecipe();
                PPIDAndRecipe.EQPID = eqpid.ToString();
                PPIDAndRecipe.PPID = ppid.ToString();
                var PPIDAndRecipeList= dbService.GetPPIDAndRecipeList(PPIDAndRecipe);


                dbService.Deletecfg_recipeppidmap(hashtable);


                Hashtable hashtable2 = new Hashtable();
                hashtable2.Add("eqpid", eqpid);
                hashtable2.Add("machinerecipename", ppid);
                dbService.Deletecfg_processmodemap(hashtable2);

                //#region send to mes RecipeChanged
                //Recipe recipe = new Recipe();
                //recipe.RecipeNo = ppid.ToString();
                //recipe.RecipeVersion = DateTime.Now.ToString("yyyyMMddHHmmss");
                //recipe.ParameterCount = "0";
                //recipe.EventID = "";
                //recipe.RecipeType = "E";
                //recipe.PreviousRecipeNo = "";//HostInfo.IntToString(PreviousRecipeNo);
                //recipe.RecipeChangeTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                //foreach (var item in PPIDAndRecipeList)
                //{
                //    if(item.LocalID!=2)
                //    {
                //        Parameter Parameter = new Parameter();
                //        Parameter.ParameterName = item.UnitID;
                //        Parameter.ParameterValue = item.RecipeID;
                //        recipe.ParameterList.Add(Parameter);
                //    }                   
                //}
                
                //logicService.RecipeChanged(eqpid.ToString(), recipe, "3");
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
            opiHis.Add("operating", "进行了RecipeInfo数据删除操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion

            return WebSocketMessageStr;
        }
    }
}
