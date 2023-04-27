using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glorysoft.BC.Client.CommonClass
{
    public class Consts
    {
        static Consts()
        {


        }
        /// <summary>
        /// UI样式不可缺少部分
        /// </summary>
        #region
        private static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string MenuConfig = BaseDirectory + ConfigurationManager.AppSettings["MenuConfig"];
        public const string ViewNameSpace = "Glorysoft.BC.Client.View";
        public static Dictionary<string, string> ViewModelViewMap = new Dictionary<string, string>
        {
            {"MainWindowVM","MainWindow"},
            {"MainFormCutVM","MainFormCut"},
            {"MainFormPOLVM","MainFormPOL"},
            {"MainFormPAKUPK1VM","MainFormPAKUPK1"},
            {"MainFormPAKUPK2VM","MainFormPAKUPK2"},
            {"MainFormLIFT_MF0405VM","MainFormLIFT_MF0405"},
            {"AlarmConfigVM","AlarmConfig"},
            {"LoginVM","Login"},
            {"UserConfigVM","UserConfig"},
            {"AlarmHistoryVM","AlarmHistory"},
                     {"PPIDandRecipeIDMapVM","PPIDandRecipeIDMap"},
            {"PPIDAndRecipeIDForCutVM","PPIDAndRecipeIDForCut"},
            {"PPIDAndRecipeIDForDenseVM","PPIDAndRecipeIDForDense"},
            {"GlassHistoryVM","GlassHistory"},
            {"GlassHistoryForCutVM","GlassHistoryForCut"},
            {"BCCommandVM","BCCommand"},
            {"LotInformationVM","LotInformation" },
            {"ControlStateSettingVM","ControlStateSetting"},
            {"RecipeVM","Recipe" },
            {"MainFormOLB_C6VM","MainFormOLB_C6" },
            {"MainFormOLB_C7VM","MainFormOLB_C7" },
            {"SamplingManageVM","SamplingManage" },
            {"MaterialHistoryVM","MaterialHistory" }
        };


        #endregion
    }
}
