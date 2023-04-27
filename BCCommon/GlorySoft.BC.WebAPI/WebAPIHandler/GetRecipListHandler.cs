using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetRecipListHandler: AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SelectRecipeInfoRequestResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                int TotalRowCount = 0;
                object eqpid, pageNum, pageSize, MasterRecipe;
                InitHistory.TryGetValue("eqpid", out eqpid);
                InitHistory.TryGetValue("pageNum", out pageNum);
                InitHistory.TryGetValue("pageSize", out pageSize);
                InitHistory.TryGetValue("MasterRecipe", out MasterRecipe);

                List<opirecipeppidmap> opirecipeppidmapList = new List<opirecipeppidmap>();
                
                PPIDAndRecipe PPIDAndRecipe = new PPIDAndRecipe();
                PPIDAndRecipe.EQPID = eqpid.ToString();
                PPIDAndRecipe.PPID = MasterRecipe == null ? "" : MasterRecipe.ToString();
                List<PPIDAndRecipe> PPIDAndRecipeList = dbService.GetPPIDAndRecipeList(PPIDAndRecipe).ToList();
                var recipeGroups = PPIDAndRecipeList.GroupBy(o => o.PPID).ToList();
                TotalRowCount = recipeGroups.Count;//总数
                recipeGroups = recipeGroups.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize).ToList();
                foreach (var item in recipeGroups)
                {
                    opirecipeppidmap opirecipeppidmap = new opirecipeppidmap();
                    var recipeList= PPIDAndRecipeList.Where(o => o.PPID == item.Key).OrderBy(o=>o.LocalID).ToList();
                    opirecipeppidmap.eqpid = eqpid.ToString();
                    opirecipeppidmap.ppid = item.Key;
                    opirecipeppidmap.value = item.Key;
                    var recipeArray = new string[30] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" , "", "", "", "", "", "", "", "", "", "" };
                    for (int i = 0; i < recipeList.Count(); i++)
                    {
                        recipeArray[recipeList[i].LocalID - 2] = recipeList[i].RecipeID;
                    }
                    opirecipeppidmap.unit1recipeid = recipeArray[0];
                    opirecipeppidmap.unit2recipeid = recipeArray[1];
                    opirecipeppidmap.unit3recipeid = recipeArray[2];
                    opirecipeppidmap.unit4recipeid = recipeArray[3];
                    opirecipeppidmap.unit5recipeid = recipeArray[4];
                    opirecipeppidmap.unit6recipeid = recipeArray[5];
                    opirecipeppidmap.unit7recipeid = recipeArray[6];
                    opirecipeppidmap.unit8recipeid = recipeArray[7];
                    opirecipeppidmap.unit9recipeid = recipeArray[8];
                    opirecipeppidmap.unit10recipeid = recipeArray[9];
                    opirecipeppidmap.unit11recipeid = recipeArray[10];
                    opirecipeppidmap.unit12recipeid = recipeArray[11];
                    opirecipeppidmap.unit13recipeid = recipeArray[12];
                    opirecipeppidmap.unit14recipeid = recipeArray[13];
                    opirecipeppidmap.unit15recipeid = recipeArray[14];
                    opirecipeppidmap.unit16recipeid = recipeArray[15];
                    opirecipeppidmap.unit17recipeid = recipeArray[16];
                    opirecipeppidmap.unit18recipeid = recipeArray[17];
                    opirecipeppidmap.unit19recipeid = recipeArray[18];
                    opirecipeppidmap.unit20recipeid = recipeArray[19];
                    opirecipeppidmap.unit21recipeid = recipeArray[20];
                    opirecipeppidmap.unit22recipeid = recipeArray[21];
                    opirecipeppidmap.unit23recipeid = recipeArray[22];
                    opirecipeppidmap.unit24recipeid = recipeArray[23];
                    opirecipeppidmap.unit25recipeid = recipeArray[24];
                    opirecipeppidmap.unit26recipeid = recipeArray[25];
                    opirecipeppidmap.unit27recipeid = recipeArray[26];
                    opirecipeppidmap.unit28recipeid = recipeArray[27];
                    opirecipeppidmap.unit29recipeid = recipeArray[28];
                    opirecipeppidmap.unit30recipeid = recipeArray[29];
                    Hashtable map = new Hashtable();
                    map.Add("EQPID", eqpid.ToString());
                    //Logger.Info(string.Format("[EQPID]:{0}", eqpid.ToString()));
                    map.Add("MachineRecipeName", item.Key);
                    //Logger.Info(string.Format("[MachineRecipeName]:{0}", item.Key));
                    ProcessModeMap processModeMap = new ProcessModeMap();
                    //processModeMap.EQPID = eqpid.ToString();
                    //processModeMap.MachineRecipeName = item.Key;
                    processModeMap = dbService.GetProcessModeMapList(map).FirstOrDefault();

                    //Logger.Info(string.Format("[processModeMap]"));
                    if (processModeMap!=null)
                    {
                        opirecipeppidmap.modepath = processModeMap.ModePath;//((int)processModeMap.ModePath).ToString();
                        opirecipeppidmap.hascvd = processModeMap.HasCVD;
                        opirecipeppidmap.createdate = processModeMap.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                        opirecipeppidmap.remark = processModeMap.Remark;
                    }

                    opirecipeppidmapList.Add(opirecipeppidmap);
                }

                InitHistory.Add("total", TotalRowCount);
                InitHistory.Add("rows", opirecipeppidmapList);

                WebSocketMessageStr.body = InitHistory;
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
