using GalaSoft.MvvmLight;
using GlorySoft.UI;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Framework;
using Glorysoft.Auto.Contract.WCF;
using Glorysoft.BC.WCF.Contract;
using System.Configuration;
using System.Windows.Input;
using Glorysoft.BC.Client.CommonClass;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System;
using Glorysoft.Auto.WCF;

namespace Glorysoft.BC.Client.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private static string SubscriberEndpointConfigName = ConfigurationManager.AppSettings["SubscriberEndpointConfigName"].ToString();
        public MainWindowVM()
        {
            menuItems = XmlSerializeManager.Deserialize<List<MenuItemInfo>>(Glorysoft.BC.Client.CommonClass.Consts.MenuConfig);
            Startup applicationStartup = new Startup();
            applicationStartup.Initialization();
            SubscriberService();
            try
            {
                ClientInfo.Current.OClient = ClientRequest.GetHostInfo();
            }
            catch (Exception ex)
            { }
            if (OClient == null || !OClient.IsHostOpen)
            {
                MessageBox.Show("BC_Server未启动，请先启动BC_Server!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }
            LoadContent();
        }


        private void LoadContent()
        {
            if (ClientInfo.Current.OClient.LineType==LineType.Cut)
            {
                currentMainContent = new MainFormCutVM();
            }
            else if (ClientInfo.Current.OClient.LineType==LineType.POL)
            {
                currentMainContent = new MainFormPOLVM();
            }
            else if (ClientInfo.Current.OClient.LineType == LineType.POL_LG)
            {
                currentMainContent = new MainFormLGVM();
            }
            else if (ClientInfo.Current.OClient.LineType == LineType.OLB_C6)
            {
                currentMainContent = new MainFormOLB_C6VM();
            }
            else if (ClientInfo.Current.OClient.LineType == LineType.OLB_C7)
            {
                currentMainContent = new MainFormOLB_C7VM();
            }
            else if (ClientInfo.Current.OClient.LineType==LineType.OCPacking)
            {
                currentMainContent = new MainFormOCPackingVM();
            }
        }



        public HostInfo OClient
        {
            get
            {
                return ClientInfo.Current.OClient;
            }
            set
            {
                RaisePropertyChanged("OClient");
            }
        }
        private string message="";
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }
        private CarrierInfo selectCarrierInfo = new CarrierInfo();
        public CarrierInfo SelectCarrierInfo
        {
            get
            {
                return selectCarrierInfo;
            }
            set
            {
                selectCarrierInfo = value;
                RaisePropertyChanged("SelectCarrierInfo");
            }
        }
        private ObservableCollection<string> operationMode = new ObservableCollection<string>();
        public ObservableCollection<string> OperationMode
        {
            get
            {
                return operationMode;
            }
            set
            {
                operationMode = value;
                RaisePropertyChanged("OperationMode");
            }
        }

        private ObservableCollection<string> vcrMode = new ObservableCollection<string>();
        public ObservableCollection<string> VCRMode
        {
            get
            {
                return vcrMode;
            }
            set
            {
                vcrMode = value;
                RaisePropertyChanged("VCRMode");
            }
        }

        private string selectOperationMode;
        public string SelectOperationMode
        {
            get
            {
                return selectOperationMode;
            }
            set
            {
                selectOperationMode = value;
                RaisePropertyChanged("SelectOperationMode");
            }
        }

        private string selectVCRMode;
        public string SelectVCRMode
        {
            get
            {
                return selectVCRMode;
            }
            set
            {
                selectVCRMode = value;
                RaisePropertyChanged("SelectVCRMode");
            }
        }

        private ViewModelBase currentMainContent;
        public ViewModelBase CurrentMainContent
        {
            get { return currentMainContent; }
            set
            {
                if (value == currentMainContent) return;
                currentMainContent = value;
                RaisePropertyChanged("CurrentMainContent");
            }
        }
        private List<MenuItemInfo> menuItems=new List<MenuItemInfo>();
        public List<MenuItemInfo> MenuList
        {
            get
            {
                return menuItems;
            }
            set
            {
                menuItems = value;
            }
        }

        private DelegateCommand sendCommand;
        public ICommand SendCommand
        {
            get
            {
                if (sendCommand == null) sendCommand = new DelegateCommand(Send);
                return sendCommand;
            }
        }
        private void Send()
        {
            if (message.Trim()!="")
            {
               ClientRequest.SendHostMsg(message);
            }
        }
        private DelegateCommand setControlStateCommand;
        public ICommand SetControlStateCommand
        {
            get
            {
                if (setControlStateCommand == null) setControlStateCommand = new DelegateCommand(SetControlState);
                return setControlStateCommand;
            }
        }
        private void SetControlState()
        {
            Controller.ShowSubWindow<ControlStateSettingVM>(new ControlStateSettingVM());
        }

        private DelegateCommand viewLotInfoCommand;
        public ICommand ViewLotInfoCommand
        {
            get
            {
                if (viewLotInfoCommand == null) viewLotInfoCommand = new DelegateCommand(viewLotInfo);
                return viewLotInfoCommand;
            }
        }
        private void viewLotInfo()
        {
            var oCarrier = ClientInfo.Current.OClient.CarrierList.FirstOrDefault(f => f.CarrierID == selectCarrierInfo.CarrierID);
            Controller.ShowSubWindow<LotInformationVM>(new LotInformationVM(oCarrier));
        }


        private DelegateCommand operationModeChangeCommand;
        public ICommand OperationModeChangeCommand
        {
            get
            {
                if (operationModeChangeCommand == null) operationModeChangeCommand = new DelegateCommand(OperationModeChange);
                return operationModeChangeCommand;
            }
        }
        private void OperationModeChange()
        {
            OClient.OperationMode = SelectOperationMode == OperationText.Normal ? 1 : 2;
            ClientRequest.UpdateOperationMode(OClient.OperationMode);

        }

        private DelegateCommand vcrModeChangeoCmmand;
        public ICommand VCRModeChangeCommand
        {
            get
            {
                if (vcrModeChangeoCmmand == null) vcrModeChangeoCmmand = new DelegateCommand(VCRModeChange);
                return vcrModeChangeoCmmand;
            }
        }
        private void VCRModeChange()
        {
            var mode = "";
            switch (selectVCRMode)
            {
                case "VCROn–ErrorSkip":
                     mode=VCRReadingMode.ErrorSkip;
                    break;
                case "VCROn–ErrorKeyIn":
                    mode = VCRReadingMode.ErrorKeyin;
                    break;
                case "VCROff–ReadingSkip":
                    mode = VCRReadingMode.ReadingSkip;
                    break;
                case "VCROff–AlwaysKeyIn":
                    mode = VCRReadingMode.AlwayskeyIn;
                    break;
            }
            ClientRequest.UpdateVCRMode(mode);
        }

        private DelegateCommand changeCanChangeOnlineCommand;
        public ICommand ChangeCanChangeOnlineCommand
        {
            get
            {
                if (changeCanChangeOnlineCommand == null) changeCanChangeOnlineCommand = new DelegateCommand(ChangeOnlineCommand);
                return changeCanChangeOnlineCommand;
            }
        }

        private void ChangeOnlineCommand()
        {
            ClientRequest.ChangeCanOnlineCommand(OClient.CanChangeOnline);
        }
        private static INotifyClient SubscriberService()
        {
            INotifyClient callback = new NotifyClient();
            ClientSubscriber<ISubscription>.Subsriber(callback, SubscriberEndpointConfigName);
            return callback;
        }
    }
}
