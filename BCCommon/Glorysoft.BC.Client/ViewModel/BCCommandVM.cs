using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glorysoft.BC.Client.ViewModel
{
    class BCCommandVM: PopupWindow
    {

        private HostInfo OClient;
        #region init
        public BCCommandVM()
        {
            OClient = ClientInfo.Current.OClient;
           // eqpList = OClient.EQPList.Values.Select(f => f.EQPName).ToList();
        }
        #endregion

        #region property
        private List<string> eqpList = new List<string>();
        public List<string> EQPList
        {
            get
            {
                return eqpList;
            }
            set
            {
                eqpList = value;
                RaisePropertyChanged("EQPList");
            }
        }

        private List<string> unitList = new List<string>();
        public List<string> UnitList
        {
            get
            {
                return unitList;
            }
            set
            {
                unitList = value;
                RaisePropertyChanged("UnitList");
            }
        }
        private string keyinTime = "";
        public string KeyinTime
        {
            get
            {
                return keyinTime;
            }
            set
            {
                keyinTime = value;
                RaisePropertyChanged("KeyinTime");
            }
        }

        private string currentTime = DateTime.Now.ToString(DateFormat.NoSpace);//DateTime.Now.ToString("yyyyMMddHHmmss");
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }

        private string eqpName = "";
        public string EQPName
        {
            get { return eqpName; }
            set
            {
                eqpName = value;
                RaisePropertyChanged("EQPName");
            }
        }

        private string tracingEQPName = "";
        public string TracingEQPName
        {
            get { return tracingEQPName; }
            set
            {
                tracingEQPName = value;
                if (!string.IsNullOrEmpty(tracingEQPName))
                {
                  // UnitList = ClientInfo.Current.OClient.EQPList.Values.FirstOrDefault(f => f.EQPName == tracingEQPName).UnitList.Select(f => f.Value.UnitName).ToList();
                }
                RaisePropertyChanged("TracingEQPName");
            }
        }

        private string tracingUnitName = "";
        public string TracingUnitName
        {
            get { return tracingUnitName; }
            set
            {
                tracingUnitName = value;
                RaisePropertyChanged("TracingUnitName");
            }
        }

        private string frequency = "";
        public string Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                RaisePropertyChanged("Frequency");
            }
        }

        private string times = "";
        public string Times
        {
            get
            {
                return times;
            }
            set
            {
                times = value;
                RaisePropertyChanged("Times");
            }
        }

        private string cimMessage = "";
        public string CIMMessage
        {
            get { return cimMessage; }
            set
            {
                cimMessage = value;
                RaisePropertyChanged("CIMMessage");
            }
        }
        #endregion

        #region CurrentSetCommand
        private DelegateCommand keyinTimeSetCommand;
        public ICommand KeyinTimeSetCommand
        {
            get
            {
                if (keyinTimeSetCommand == null)
                    keyinTimeSetCommand = new DelegateCommand(KeyinSet);
                return keyinTimeSetCommand;
            }
        }
        private void KeyinSet()
        {
            try
            {
                if (string.IsNullOrEmpty(keyinTime))
                {
                    MessageBox.Show("时间没有输入，请输入14位的数字", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                Regex reg = new Regex("^[0-9]+$");
                Match ma = reg.Match(keyinTime);
                if (ma.Success && keyinTime.Length == 14)
                {
                    ClientRequest.SendTimeToEQP(eqpName, keyinTime);
                }
                else
                {
                    MessageBox.Show("时间格式不对，请输入14位的数字", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private DelegateCommand currentTimeSetCommand;
        public ICommand CurrentTimeSetCommand
        {
            get
            {
                if (currentTimeSetCommand == null)
                    currentTimeSetCommand = new DelegateCommand(CurrentSet);
                return currentTimeSetCommand;
            }
        }
        private void CurrentSet()
        {
            try
            {
                ClientRequest.SendTimeToEQP(eqpName, DateTime.Now.ToString(DateFormat.NoSpace));
            }
            catch (Exception ex)
            {

            }
            
        }
        #endregion

        #region SendCommand
        private DelegateCommand sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                if (sendMessageCommand == null)
                    sendMessageCommand = new DelegateCommand(Send);
                return sendMessageCommand;
            }
        }
        private void Send()
        {
            if (string.IsNullOrEmpty(cimMessage))
            {
                MessageBox.Show("请输入Message的内容", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(eqpName))
            {
                MessageBox.Show("请选择EQPName或者选择发送给所有设备", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var msgList = new List<string>();
                msgList.Add(cimMessage);
                ClientRequest.SendMsgToEQP(eqpName, msgList);
            }
        }
        #endregion

        #region SendAllCommand
        private DelegateCommand sendMessageToAllCommand;
        public ICommand SendMessageToAllCommand
        {
            get
            {
                if (sendMessageToAllCommand == null)
                    sendMessageToAllCommand = new DelegateCommand(SendAllEqp);
                return sendMessageToAllCommand;
            }
        }
        private void SendAllEqp()
        {
            var msgList = new List<string>();
            msgList.Add(cimMessage);
            foreach (var item in OClient.EQPList.Values)
            {
              //  ClientRequest.SendMsgToEQP(item.EQPName, msgList);
            }
        }
        #endregion
        #region TracingDataCommand
        private DelegateCommand sendTracingDataCommand;
        public ICommand SendTracingDataCommand
        {
            get
            {
                if (sendTracingDataCommand == null)
                    sendTracingDataCommand = new DelegateCommand(SendTracingData);
                return sendTracingDataCommand;
            }
        }
        private void SendTracingData()
        {
            try
            {
              //  var oEQP = ClientInfo.Current.OClient.EQPList.Values.FirstOrDefault(f => f.EQPName == tracingEQPName);
                if (string.IsNullOrEmpty(frequency) || string.IsNullOrEmpty(times))
                {
                    MessageBox.Show("请输入内容", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //if (oEQP.HasUnit && string.IsNullOrEmpty(tracingUnitName))
                //{
                //    MessageBox.Show("请选择unit", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
                Regex reg = new Regex("^[0-9]+$");
                Match ma = reg.Match(frequency);

                if (ma.Success)
                {
                    int fre = int.Parse(frequency);
                    int tim = int.Parse(times);
                    ClientRequest.SendTracingCommandToEQP(tracingEQPName, tracingUnitName, fre, tim);
                }
                else
                {
                    MessageBox.Show("数据格式不对，请重新输入", "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Waring", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            
        }
        #endregion
    }
}
