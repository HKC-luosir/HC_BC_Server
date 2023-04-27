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
    public class GetPPIDRecipeInfoRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getHistoryTableInformation",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                object eqpid, ppid;
                InitHistory.TryGetValue("eqpid", out eqpid);
                InitHistory.TryGetValue("ppid", out ppid);

                PPIDAndRecipe PPIDAndRecipe = new PPIDAndRecipe();
                PPIDAndRecipe.EQPID = eqpid.ToString();
                PPIDAndRecipe.PPID = ppid.ToString();
                List<PPIDAndRecipe> PPIDAndRecipeList = dbService.GetPPIDAndRecipeList(PPIDAndRecipe).ToList();

                List<RecipeRowData> recipeRowDatas = new List<RecipeRowData>();

                for (int i = 0; i < PPIDAndRecipeList.Count; i++)
                {
                    RecipeRowData recipeRowData = new RecipeRowData()
                    {
                        No = PPIDAndRecipeList[i].LocalID.ToString(),
                        equipmentNo = PPIDAndRecipeList[i].UnitID,
                        name = PPIDAndRecipeList[i].PPID
                    };
                    recipeRowDatas.Add(recipeRowData);
                }




                WebSocketMessageStr.body = recipeRowDatas;

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
