using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Glorysoft.BC.Db.Service
{
    public class DbRecipeService : AbstractDbService, IDbRecipeService
    {

        public IList<RecipeParameter> GetRecipeParameterList(RecipeParameter RecipeParameter)
        {           
            var result = ExecuteQueryForList<RecipeParameter>("GetRecipeParameterList", RecipeParameter);            
            return result;
        }
        public bool InsertRecipeParameter(RecipeParameter item)
        {
            var iRet = false;
            iRet = ExecuteInsert("InsertRecipeParameter", item);
            return iRet;
        }
        public int UpdateRecipeParameter(RecipeParameter item)
        {  
            return ExecuteUpdate("UpdateRecipeParameter", item);
        }
        public int DeleteRecipeParameter(RecipeParameter item)
        {
            return ExecuteDelete("DeleteRecipeParameter", item);
        }




        public IList<PPIDAndRecipe> GetPPIDAndRecipeList(PPIDAndRecipe PPIDAndRecipe)
        {
            var result = ExecuteQueryForList<PPIDAndRecipe>("GetPPIDAndRecipeList", PPIDAndRecipe);
            return result;
        }
        public bool InsertPPIDAndRecipe(PPIDAndRecipe item)
        {
            var iRet = false;
            iRet = ExecuteInsert("InsertPPIDAndRecipe", item);
            return iRet;
        }        
        public int DeletePPIDAndRecipe(PPIDAndRecipe item)
        {
            return ExecuteDelete("DeletePPIDAndRecipe", item);
        }





        public IList<ProcessModeMap> GetProcessModeMapList(Hashtable Hashtable)
        {
            var result = ExecuteQueryForList<ProcessModeMap>("GetProcessModeMapList", Hashtable);
            return result;
        }
        public bool InsertProcessModeMap(ProcessModeMap item)
        {
            var iRet = false;
            iRet = ExecuteInsert("InsertProcessModeMap", item);
            return iRet;
        }
        public int DeleteProcessModeMap(ProcessModeMap item)
        {
            return ExecuteDelete("DeleteProcessModeMap", item);
        }



        public bool InsertMIXRunConfig(MIXRunConfig item)
        {
            var iRet = false;
            iRet = ExecuteInsert("InsertMIXRunConfig", item);
            return iRet;
        }
        public int DeleteMIXRunConfig(Hashtable item)
        {
            return ExecuteDelete("DeleteMIXRunConfig", item);
        }
        public IList<MIXRunConfig> GetMIXRunConfigList(Hashtable Hashtable)
        {
            var MIXRunConfigList = ExecuteQueryForList<MIXRunConfig>("GetMIXRunConfigList", Hashtable);
            foreach (var MIXRunConfigItem in MIXRunConfigList)
            {
                Hashtable ht = new Hashtable();
                ht.Add("EQPID", MIXRunConfigItem.EQPID);
                ht.Add("MachineRecipeName", MIXRunConfigItem.MachineRecipeName);
                IList<MIXRunInputRatio> MIXRunInputRatioList = GetMIXRunInputRatioList(ht);
                MIXRunConfigItem.MIXRunInputRatioList = MIXRunInputRatioList.ToList();
            }
            return MIXRunConfigList;
        }
        public int UpdateMIXRunConfig(MIXRunConfig item)
        {
            return ExecuteUpdate("UpdateMIXRunConfig", item);
        }



        public bool InsertMIXRunInputRatio(MIXRunInputRatio item)
        {
            var iRet = false;
            iRet = ExecuteInsert("InsertMIXRunInputRatio", item);
            return iRet;
        }
        public int DeleteMIXRunInputRatio(Hashtable item)
        {
            return ExecuteDelete("DeleteMIXRunInputRatio", item);
        }
        public IList<MIXRunInputRatio> GetMIXRunInputRatioList(Hashtable Hashtable)
        {
            var result = ExecuteQueryForList<MIXRunInputRatio>("GetMIXRunInputRatioList", Hashtable);
            return result;
        }
        public int UpdateMIXRunInputRatio(MIXRunInputRatio item)
        {
            return ExecuteUpdate("UpdateMIXRunInputRatio", item);
        }



        public IList<OperationMode> GetOperationModeList(Hashtable Hashtable)
        {
            var result = ExecuteQueryForList<OperationMode>("GetOperationModeList", Hashtable);
            return result;
        }

        public IList<UseRecipeList> GetUseRecipeList(Hashtable Hashtable)
        {

            var result = ExecuteQueryForList<UseRecipeList>("GetUseRecipeList", Hashtable);
            return result;
        }
        public bool InsertUseRecipeList(UseRecipeList item)
        {

            var iRet = false;
            iRet = ExecuteInsert("InsertUseRecipeList", item);
            return iRet;
        }
        public int DeleteUseRecipeList(UseRecipeList item)
        {
            return ExecuteDelete("DeleteUseRecipeList", item);
        }
    }
}