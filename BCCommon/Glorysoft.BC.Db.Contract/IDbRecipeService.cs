using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbRecipeService : IAutoRegister
    {

         IList<RecipeParameter> GetRecipeParameterList(RecipeParameter RecipeParameter);
         bool InsertRecipeParameter(RecipeParameter item);
         int UpdateRecipeParameter(RecipeParameter item);
         int DeleteRecipeParameter(RecipeParameter item);




         IList<PPIDAndRecipe> GetPPIDAndRecipeList(PPIDAndRecipe PPIDAndRecipe);
         bool InsertPPIDAndRecipe(PPIDAndRecipe item);
         int DeletePPIDAndRecipe(PPIDAndRecipe item);



         IList<ProcessModeMap> GetProcessModeMapList(Hashtable Hashtable);
         bool InsertProcessModeMap(ProcessModeMap item);
         int DeleteProcessModeMap(ProcessModeMap item);



        bool InsertMIXRunConfig(MIXRunConfig item);
        int DeleteMIXRunConfig(Hashtable item);
        IList<MIXRunConfig> GetMIXRunConfigList(Hashtable Hashtable);
        int UpdateMIXRunConfig(MIXRunConfig item);


        bool InsertMIXRunInputRatio(MIXRunInputRatio item);
        int DeleteMIXRunInputRatio(Hashtable item);
        IList<MIXRunInputRatio> GetMIXRunInputRatioList(Hashtable Hashtable);
        int UpdateMIXRunInputRatio(MIXRunInputRatio item);




        IList<OperationMode> GetOperationModeList(Hashtable Hashtable);

        IList<UseRecipeList> GetUseRecipeList(Hashtable Hashtable);
        bool InsertUseRecipeList(UseRecipeList item);
        int DeleteUseRecipeList(UseRecipeList Hashtable);
    }
}
