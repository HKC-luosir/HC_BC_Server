using System.Windows.Forms;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Client.CommonClass;
using GlorySoft.UI;
using System.Windows.Input;
using System.Collections.Generic;

namespace Glorysoft.BC.Client.ViewModel
{
    public class LotInformationVM:PopupWindow
    {
        public LotInformationVM(CarrierInfo carrierInfo)
        {
            OCarrier = carrierInfo ?? new CarrierInfo();
        }
        private CarrierInfo oCarrier;
        public CarrierInfo OCarrier
        {
            get
            {
                return oCarrier;
            }
            set
            {
                oCarrier = value;
                RaisePropertyChanged("OCarrier");
            }
        }

        private GlassInfo selectedItem = new GlassInfo();
        public GlassInfo SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        private DelegateCommand modifyInfoCommand;
        public ICommand ModifyInfoCommand
        {
            get
            {
                if (modifyInfoCommand == null)
                    modifyInfoCommand = new DelegateCommand(Modify);
                return modifyInfoCommand;
            }
        }
        private void Modify()
        {
            foreach (var item in oCarrier.GlassList)
            {
                if (item.GlassJudge=="N")
                {
                    item.Selected = false;
                }
            }
            var result = ClientRequest.UpdateCarrierInfo(oCarrier) && ClientRequest.InsertCarrierInfo(OCarrier);
            if (!result)
            {
                MessageBox.Show("更新失败，请确认数据是否正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("更新成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
        private DelegateCommand downloadCarrierCommand;
        public ICommand DownloadCarrierCommand
        {
            get
            {
                if (downloadCarrierCommand == null)
                    downloadCarrierCommand = new DelegateCommand(Download);
                return downloadCarrierCommand;
            }
        }

        private void Download()
        {
            ClientRequest.SendCarrierInfo(oCarrier);
        }

        private DelegateCommand waitForStartCommand;
        public ICommand WaitForStartCommand
        {
            get
            {
                if (waitForStartCommand == null)
                    waitForStartCommand = new DelegateCommand(WaitForStart);
                return waitForStartCommand;
            }
        }

        private void WaitForStart()
        {
            ClientRequest.SendWaitForStart(oCarrier);
        }

        private DelegateCommand startCarrierCommand;
        public ICommand StartCarrierCommand
        {
            get
            {
                if (startCarrierCommand == null)
                    startCarrierCommand = new DelegateCommand(Start);
                return startCarrierCommand;
            }
        }

        private void Start()
        {
            ClientRequest.SendCarrierControl(oCarrier, CarrierControl.Start);
        }

        private DelegateCommand cancelCarrierCommand;
        public ICommand CancelCarrierCommand
        {
            get
            {
                if (cancelCarrierCommand == null)
                    cancelCarrierCommand = new DelegateCommand(Cancel);
                return cancelCarrierCommand;
            }
        }

        private void Cancel()
        {
            ClientRequest.SendCarrierControl(oCarrier, CarrierControl.Cancel);
        }

        private DelegateCommand abortCarrierCommand;
        public ICommand AbortCarrierCommand
        {
            get
            {
                if (abortCarrierCommand == null)
                    abortCarrierCommand = new DelegateCommand(Abort);
                return abortCarrierCommand;
            }
        }

        private void Abort()
        {
            ClientRequest.SendCarrierControl(oCarrier, CarrierControl.Abort);
        }

    }
}
