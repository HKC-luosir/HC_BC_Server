using GalaSoft.MvvmLight;
using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glorysoft.BC.Client.ViewModel
{
    public class SamplingManageVM : PopupWindow
    {
        
        public SamplingManageVM()
        {
            LoadData();
        }
        private void LoadData()
        {
            currentNGLotCount = OClient.SettingOQANGLotCount.ToString();
            oqaSamplingRuleList = OClient.OQASamplingRuleList;
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

        private ObservableCollection<OQASamplingRule> oqaSamplingRuleList = new ObservableCollection<OQASamplingRule>();
        public ObservableCollection<OQASamplingRule> OQASamplingRuleList
        {
            get { return oqaSamplingRuleList; }
            set { oqaSamplingRuleList = value; RaisePropertyChanged("OQASamplingRuleList"); }
        }

        private string currentNGLotCount;
        public string CurrentNGLotCount
        {
            get { return currentNGLotCount; }
            set { currentNGLotCount = value; RaisePropertyChanged("CurrentNGLotCount"); }
        }

        private DelegateCommand updateNGLotCountCommand;
        public ICommand UpdateNGLotCountCommand
        {
            get
            {
                if (updateNGLotCountCommand == null) updateNGLotCountCommand = new DelegateCommand(UpdateNGLotCount);
                return updateNGLotCountCommand;
            }
        }
        private void UpdateNGLotCount()
        {
            try
            {
                if (string.IsNullOrEmpty(currentNGLotCount))
                {
                    MessageBox.Show("未正确填写数据，请检查", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    Regex reg = new Regex("^[0-9]+$");
                    Match ma = reg.Match(currentNGLotCount);
                    if (ma.Success)
                    {
                        int count = int.Parse(currentNGLotCount);
                        if(count<=0) MessageBox.Show("未正确填写数据，请检查", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                      //  ClientRequest.UpdateOQANGLotCount(count);
                    }
                    else
                    {
                        MessageBox.Show("未正确填写数据，请检查", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private DelegateCommand updateSampingRuleCommand;
        public ICommand UpdateSampingRuleCommand
        {
            get
            {
                if (updateSampingRuleCommand == null) updateSampingRuleCommand = new DelegateCommand(UpdateSampingRule);
                return updateSampingRuleCommand;
            }
        }
        private void UpdateSampingRule()
        {
            try
            {
                foreach (var item in OQASamplingRuleList)
                {
                   // var result = ClientRequest.UpdateOQASamplingRule(item);
                    //if (result) MessageBox.Show("更新成功", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    //else MessageBox.Show("更新失败", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
