using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Glorysoft.BC.Entity;
using System.Collections;
using Glorysoft.BC.Client.CommonClass;
using System.Collections.ObjectModel;
using GlorySoft.UI;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Windows.Input;
using System.Windows.Forms;

namespace Glorysoft.BC.Client.ViewModel
{
    class AlarmConfigVM: PopupWindow
    {
        private HostInfo OClient;
        private readonly ExcelService excelService;
        public AlarmConfigVM()
        {
            OClient = ClientInfo.Current.OClient;
            excelService = new ExcelService();
        }
        #region Properties
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

        private string toolTip = "Excel首行必须为标题[AlarmID，EQP，AlarmCode，AlarmText]，Sheet名为[Sheet1]!";
        public string ToolTip
        {
            get { return toolTip; }
        }

        private bool isChecked = true;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        private ObservableCollection<AlarmInfo> oAlarm = new ObservableCollection<AlarmInfo>();
        public ObservableCollection<AlarmInfo> OAlarm
        {
            get { return oAlarm; }
            set
            {
                oAlarm = value;
                RaisePropertyChanged("OAlarm");
            }
        }

        private AlarmInfo selectedItem = new AlarmInfo();
        public AlarmInfo SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged("SelectedItem");
                if (selectedItem == null) return;

                var tmp = new AlarmInfo
                {
                    EQPName = selectedItem.EQPName,
                    AlarmID = selectedItem.AlarmID,
                    AlarmLevel = selectedItem.AlarmLevel,
                    AlarmText = selectedItem.AlarmText,
                    AlarmStatus = selectedItem.AlarmStatus,
                    AlarmEnable = selectedItem.AlarmEnable,
                    CreateDate = selectedItem.CreateDate
                };
                UpdatingItem = tmp;
            }
        }

        private AlarmInfo updatingItem = new AlarmInfo();
        public AlarmInfo UpdatingItem
        {
            get { return updatingItem; }
            set
            {
                updatingItem = value;
                RaisePropertyChanged("UpdatingItem");
            }
        }

        #endregion

        #region Commands
        private DelegateCommand importExcelCommand;
        public ICommand ImportExcelCommand
        {
            get
            {
                return importExcelCommand ??
                       (importExcelCommand = new DelegateCommand(ImportExcel));
            }
        }


        private DelegateCommand modifyCommand;
        public ICommand ModifyCommand
        {
            get
            {
                if (modifyCommand == null)
                {
                    modifyCommand = new DelegateCommand(Modify);
                }
                return modifyCommand;
            }
        }

        private DelegateCommand<object> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = new DelegateCommand<object>(Delete);
                return deleteCommand;
            }
        }

        private DelegateCommand exportExcelCommand;
        public ICommand ExportExcelCommand
        {
            get
            {
                return exportExcelCommand ??
                       (exportExcelCommand = new DelegateCommand(ExportExcel));
            }
        }

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


        private DelegateCommand clearCommand;
        public ICommand ClearCommand
        {
            get { return clearCommand ?? (clearCommand = new DelegateCommand(ClearEQPAlarmList)); }
        }

        private DelegateCommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new DelegateCommand(Add);
                }
                return addCommand;
            }
        }
        #endregion

        #region Functions
        private bool Change()
        {
            if (updatingItem.AlarmID != "0") return true;
            if (updatingItem.EQPName.Length == 0) return true;
            return false;
        }

        private void Search()
        {
            try
            {
                var eqpName = updatingItem.EQPName.Trim();
                var eqpID = OClient.EQPList[eqpName].EQPID;

                var lst = ClientRequest.ViewAlarmList(eqpID);
                OAlarm = new ObservableCollection<AlarmInfo>(lst);

            }
            catch (Exception ex)
            {

            }
        }


        private void ImportExcel()
        {
            // Let factory create the IOpenFileDialog
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    DefaultExt = "xls",
                    Filter = "2010xlsx|*.xlsx|2003xls|*.xls"
                };
                if (System.Windows.Forms.MessageBox.Show("Excel首行必须为标题[AlarmID，EQPNamee，AlarmLevel，AlarmText]，Sheet名为[Sheet1]!", "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes) return;
                var result = openFileDialog.ShowDialog();

                if (result != DialogResult.OK) return;

                var fileName = openFileDialog.FileName;
                if (fileName.Length == 0) return;

                var lst = excelService.ReadAlarmExcel(fileName, openFileDialog.FilterIndex);
                bool bRet;

                bRet = ClientRequest.ImportAlarmList(lst);


                System.Windows.MessageBox.Show(
                    string.Format("导入Excel: {0}" + (bRet ? "成功！" : "失败！"),
                                  openFileDialog.SafeFileName), "Info");
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("导入失败：" + exp.Message, "Error",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
        }

        private void Add()
        {
            try
            {
                if (updatingItem.AlarmID == "0")
                {
                    System.Windows.MessageBox.Show("AlarmID isn't Empty");
                    return;
                }
                if (updatingItem.EQPName == "*")
                {
                    System.Windows.MessageBox.Show("EQP isn't Empty");
                    return;
                }
                bool bRet;
                updatingItem.EQPID = ClientInfo.Current.OClient.EQPList[updatingItem.EQPName].EQPID;
                updatingItem.CreateDate = DateTime.Now;
                bRet = ClientRequest.InsertAlarmInfo(updatingItem);

                System.Windows.MessageBox.Show(
                    string.Format("AlarmID: {0}，Text: {1}，新增" + (bRet ? "成功！" : "失败！"),
                                  updatingItem.AlarmID, updatingItem.AlarmText), "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                if (bRet)
                    OAlarm.Add(updatingItem);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("新增失败：" + exp.Message, "Error",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
        }

        private void Modify()
        {
            try
            {
                if (updatingItem.AlarmID == "0")
                {
                    System.Windows.MessageBox.Show("AlarmID is Empty");
                    return;
                }
                if (string.IsNullOrEmpty(updatingItem.EQPName))
                {
                    System.Windows.MessageBox.Show("EQPName is Empty");
                    return;
                }
                bool bRet;
                updatingItem.EQPID = ClientInfo.Current.OClient.EQPList[updatingItem.EQPName].EQPID;
                bRet = ClientRequest.UpdateAlarmInfo(updatingItem);

                System.Windows.MessageBox.Show(
                    string.Format("AlarmID: {0}，Text: {1}，修改" + (bRet ? "成功！" : "失败！"),
                                  updatingItem.AlarmID, updatingItem.AlarmText), "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                if (!bRet) return;

                var index =
                    oAlarm.IndexOf(oAlarm.First(p => p.AlarmID == updatingItem.AlarmID));
                oAlarm.RemoveAt(index);
                OAlarm.Insert(index, updatingItem);
                Search();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("修改失败：" + exp.Message, "Error",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
        }

        private void Delete(object items)
        {
            try
            {
                var selectedItems = items as IEnumerable;
                if (selectedItems == null) return;
                if (System.Windows.MessageBox.Show("你确定删除选中的Alarm？", "Info",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question) ==
                    MessageBoxResult.No)
                {
                    return;
                }
                var iRet = false;
                var myItems = selectedItems.Cast<AlarmInfo>().ToList();
                var count = myItems.Count;
                for (var i = 0; i < count; i++)
                {
                    myItems[i].EQPID = ClientInfo.Current.OClient.EQPList[myItems[i].EQPName].EQPID;
                    iRet = ClientRequest.DeleteAlarmInfo(myItems[i]);
                    OAlarm.Remove(myItems[i]);
                }
                System.Windows.MessageBox.Show(string.Format("删除{0}条Alarm!", iRet), "Info",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("删除失败：" + exp.Message, "Error",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
        }

        private void ExportExcel()
        {
            try
            {
                if (System.Windows.MessageBox.Show("你确定导出Alarm?", "Alarm List",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
                excelService.ExportAlarmToCSV(oAlarm.ToList());

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("导出失败：" + exp.Message, "Error",
                                               MessageBoxButton.OK,
                                               MessageBoxImage.Information);
            }
        }


        private void ClearEQPAlarmList()
        {
            try
            {
                if (updatingItem.EQPName == "*") return;
                if (
                    System.Windows.MessageBox.Show(
                        string.Format("你确定清除设备：[{0}] Alarm List?", updatingItem.EQPName),
                        "Info", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
                    MessageBoxResult.No)
                {
                    return;
                }
                var eqpName = updatingItem.EQPName;
                var eqpID = OClient.EQPList[eqpName].EQPID;
                ClientRequest.ClearAlarmList(eqpID);
                var lst = ClientRequest.ViewAlarmList(eqpID);
                OAlarm = new ObservableCollection<AlarmInfo>(lst);
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion
    }
}
