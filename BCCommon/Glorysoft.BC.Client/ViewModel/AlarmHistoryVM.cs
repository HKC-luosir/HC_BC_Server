using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Client.CommonClass;
using GlorySoft.UI;
using System.Windows.Input;
using System.Collections;
using System.Windows;

namespace Glorysoft.BC.Client.ViewModel
{
    class AlarmHistoryVM : PopupWindow
    {
        private HostInfo OClient;
        public AlarmHistoryVM()
        {
            OClient = ClientInfo.Current.OClient;
            LoadData();
        }
        #region property 
        
        List<string> eqpList = null;
        public List<string> EQPList
        {
            get
            {
                if (eqpList == null)
                {
                    eqpList = new List<string>();
                    eqpList.Add("*");
                    var oEQP = OClient.EQPList;
                    foreach (var item in oEQP)
                    {
                        //eqpList.Add(item.Value.EQPName);
                    }
                }
                return eqpList;
            }
        }
        private IList<AlarmInfo> alarmList;
        public IList<AlarmInfo> AlarmList
        {
            get { return alarmList; }
            set
            {
                alarmList = value;
                RaisePropertyChanged("AlarmList");
            }
        }
        private string eqpID;
        public string EqpID
        {
            get { return eqpID; }
            set
            {
                eqpID = value;
                RaisePropertyChanged("EqpID");
            }
        }

        private string eqpName;
        public string EQPName
        {
            get { return eqpName; }
            set
            {
                eqpName = value;
                RaisePropertyChanged("EQPName");
            }
        }

        private List<string> hourSelect;
        public List<string> HourSelect
        {
            get { return hourSelect; }
            set
            {
                hourSelect = value;
                RaisePropertyChanged("HourSelect");
            }
        }

        private string hourSelectFrom;
        public string HourSelectFrom
        {
            get { return hourSelectFrom; }
            set
            {
                hourSelectFrom = value;
                RaisePropertyChanged("HourSelectFrom");
            }
        }

        private string hourSelectTo;
        public string HourSelectTo
        {
            get { return hourSelectTo; }
            set
            {
                hourSelectTo = value;
                RaisePropertyChanged("HourSelectTo");
            }
        }

        private DateTime selectDateFrom;
        public DateTime SelectDateFrom
        {
            get { return selectDateFrom; }
            set
            {
                selectDateFrom = value;
                RaisePropertyChanged("SelectDateFrom");
            }
        }

        private DateTime selectDateTo;
        public DateTime SelectDateTo
        {
            get { return selectDateTo; }
            set
            {
                selectDateTo = value;
                RaisePropertyChanged("SelectDateTo");
            }
        }
        #endregion
        private void LoadData()
        {
            hourSelect = new List<string>();
            for (var i = 0; i < 25; i++)
            {
                hourSelect.Add(i.ToString("00"));
            }

            SelectDateFrom = DateTime.Today;
            SelectDateTo = DateTime.Today;
            HourSelectFrom = "00";
            HourSelectTo = "24";
        }

        #region SearchCommand
        private DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                    searchCommand = new DelegateCommand(Search);
                return searchCommand;
            }
        }
        private void Search()
        {
            var sFrom = string.Format("{0} {1}:00:00", selectDateFrom.ToString("yyyy-MM-dd"), hourSelectFrom);
            var sTo = string.Format("{0} {1}:00:00", SelectDateTo.ToString("yyyy-MM-dd"), HourSelectTo);
            var map = new Hashtable
                          {
                              {"EQPName", eqpName},
                              {"FromDate", sFrom},
                              {"ToDate", sTo}
                          };
            var lst = ClientRequest.ViewAlarmHistory(map);
            AlarmList = lst;
        }

        private DelegateCommand exportCommand;
        public ICommand ExportCommand
        {
            get
            {
                if (exportCommand == null)
                    exportCommand = new DelegateCommand(ExportToExcel);
                return exportCommand;
            }
        }

        private void ExportToExcel()
        {
            try
            {
                if (MessageBox.Show("你确定导出Alarm?", "Alarm List", MessageBoxButton.YesNo,
                                   MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }

                if (AlarmList.Count == 0) return;

                Mouse.SetCursor(Cursors.Wait);

                var xlsService = new ExcelService();
                xlsService.ExportAlarmHistory(AlarmList);

                Mouse.SetCursor(Cursors.Arrow);
            }
            catch (Exception ex)
            {
                Mouse.SetCursor(Cursors.Arrow);
                MessageBox.Show("导出失败：" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion
    }
}
