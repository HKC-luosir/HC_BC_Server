using GalaSoft.MvvmLight;
using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Client.ViewModel;
using GlorySoft.UI;
using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Glorysoft.BC.Client.View
{
    public class WindowBase : GloryWindow
    {
        //WindowBase类继承GloryWindow,重写CommandAction方法，cmmandname为Menu中MenuItem的name
        public WindowBase()
        {

        }
        public override void CommandAction(string cmmandname)
        {
            switch (cmmandname)
            {
                case "Open":
                    OpenFile();
                    break;
                case "AlarmConfig":
                    OpenAlarmConfig();
                    break;
                case "UserConfig":
                    OpenUserConfig();
                    break;
                case "GlassHistory":
                    OpenGlassHistory();
                    break;
                case "AlarmHistory":
                    OpenAlarmHistory();
                    break;
                case "PPID":
                    OpenPPIDandRecipeID();
                    break;
                case "BCCommand":
                    OpenCommand();
                    break;
                case "RecipeHistory":
                    OpenRecipe();
                    break;
                case "OQA":
                    OpenOQA();
                    break;
                case "MaterialHistory":
                    OpenMaterial();
                    break;
            }
        }
        private void OpenFile()
        {

        }

        private void OpenPPIDandRecipeID()
        {
            Controller.ShowSubWindow<PPIDandRecipeIDMapVM>(new PPIDandRecipeIDMapVM());
        }

        private void OpenAlarmHistory()
        {
            Controller.ShowSubWindow<AlarmHistoryVM>(new AlarmHistoryVM());
        }

        private void OpenAlarmConfig()
        {
            Controller.ShowSubWindow<AlarmConfigVM>(new AlarmConfigVM());
        }

        private void OpenGlassHistory()
        {
            if (ClientInfo.Current.OClient.LineType == LineType.Cut) Controller.ShowSubWindow<GlassHistoryForCutVM>(new GlassHistoryForCutVM());
            else Controller.ShowSubWindow<GlassHistoryVM>(new GlassHistoryVM());
        }

        private void OpenUserConfig()
        {
            Login login = new Login();
            login.ShowDialog();
            if ((bool)login.DialogResult)
            {
                Controller.ShowSubWindow<UserConfigVM>(new UserConfigVM());
            }
        }

        private void OpenCommand()
        {
            Controller.ShowSubWindow<BCCommandVM>(new BCCommandVM());
        }

        private void OpenRecipe()
        {
            Controller.ShowSubWindow<RecipeVM>(new RecipeVM());
        }

        private void OpenOQA()
        {
            Controller.ShowSubWindow<SamplingManageVM>(new SamplingManageVM());
        }

        private void OpenMaterial()
        {
            Controller.ShowSubWindow<MaterialHistoryVM>(new MaterialHistoryVM());
        }
    }
}
