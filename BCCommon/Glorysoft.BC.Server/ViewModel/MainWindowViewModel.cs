using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.BaseClass;
using Glorysoft.BC.Server.Infrastructure;
using Glorysoft.BC.Server.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Server.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        public MainWindowViewModel()
        {

            testCommand = new DelegateCommand(ShowTest);
            lineModeCommand = new DelegateCommand(LineMode);
            var name = Assembly.GetEntryAssembly().GetName();
            var ver = name.Version;
            //portList.Add(new PortInfo() { PortID = "port" });
            BCVersion = string.Format("BOE3 BC System ({0})", ver);
        }
        private string bcVersion;
        public string BCVersion
        {
            get
            {
                return bcVersion;
            }
            set
            {
                if (value != bcVersion)
                {
                    bcVersion = value;
                    Notify("BCVersion");
                }
            }
        }
        private DelegateCommand testCommand;
        public DelegateCommand TestCommand
        {
            get
            {
                return testCommand;
            }
        }
        public HostInfo LineInfo
        {
            get
            {
                return HostInfo.Current;
            }
            set
            {
                Notify("LineInfo");
            }
        }
        //private ObservableCollection<PortInfo> portList = new ObservableCollection<PortInfo>();

        //public ObservableCollection<PortInfo> PortList
        //{
        //    get { return portList; }
        //    set
        //    {
        //        portList = value;
        //        Notify("PortList");
        //    }
        //}
        /// <sum
        private void ShowTest()
        {
            //InforController.ShowSubWindow<ConfigurationViewModel>(new ConfigurationViewModel());
            InforController.ShowSubWindow<TestViewModel>(new TestViewModel());
        }
        private DelegateCommand lineModeCommand;
        public DelegateCommand LineModeCommand
        {
            get
            {
                return lineModeCommand;
            }
        }
        private void LineMode()
        {
            try
            {
                //var LineMode = new LineMode();
                //LineMode.ShowDialog();
            }
            catch (System.Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }

        private DelegateCommand configurationCommand;
        public DelegateCommand ConfigurationCommand
        {
            get
            {
                if (configurationCommand == null)
                    configurationCommand = new DelegateCommand(Configuration);
                return configurationCommand;
            }
        }
        private void Configuration()
        {
            try
            {
                #region 登陆校验
                var wConfirm = new ConfirmWin { lblTitle = { Content = "Configuration" } };
                var dlgResult = wConfirm.ShowDialog();
                if (dlgResult != null && dlgResult.Value)
                {
                    var Config = new Config();
                    Config.ShowDialog();
                }
                #endregion               
            }
            catch (System.Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
    }
}
