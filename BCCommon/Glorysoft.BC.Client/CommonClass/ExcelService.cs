using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Glorysoft.BC.Client.CommonClass
{
    public class ExcelService
    {
        /// <summary>
        /// 从Excel中读取AlarmList
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="filterIndex"> </param>
        /// <returns>Alarm列表</returns>
        public IList<AlarmInfo> ReadAlarmExcel(string excelFilePath, int filterIndex)
        {
            IList<AlarmInfo> lst = new List<AlarmInfo>();
            try
            {
                var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

                if (filterIndex == 1)
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";

                //HDR ( HeaDer Row )设置 若指定值为Yes，代表 Excel 档中的工作表第一行是栏位名称
                //IMEX ( Import Export mode )设置,0 is Export mode,1 is Import mode,2 is Linked mode (full update capabilities)

                var conn = new System.Data.OleDb.OleDbConnection(connectionString);
                conn.Open();
                var tables = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                var sheet = tables.Rows[0]["Table_Name"].ToString();

                var cmd = conn.CreateCommand();
                var strSQL = string.Format("select * from [{0}]", sheet);
                cmd.CommandText = strSQL;

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["AlarmID"].Equals(DBNull.Value)) continue;
                    if (reader["EQPName"].ToString().Trim().Length == 0) continue;

                    var oAlarm = new AlarmInfo
                    {
                        AlarmID = reader["AlarmID"].ToString(),
                        EQPName = reader["EQPName"].ToString().Trim(),
                        EQPID = ClientInfo.Current.OClient.EQPList[reader["EQPName"].ToString().Trim()].EQPID,
                        AlarmText = reader["AlarmText"].ToString().Trim()
                        
                    };
                    if (!reader["AlarmLevel"].Equals(DBNull.Value))
                        oAlarm.AlarmLevel = Convert.ToInt32(reader["AlarmLevel"]);
                    lst.Add(oAlarm);
                }
                conn.Close();

                return lst;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(string.Format(ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 导出Alarm到CSV
        /// </summary>
        /// <param name="lst"></param>
        public void ExportAlarmToCSV(IList<AlarmInfo> lst)
        {
            var headers = new[] { "ALID", "EQPName", "ALCD", "ALTX" };
            var columns = new[] { "A", "B", "C", "D" };

            try
            {
                // Get the class type and instantiate Excel. 
                var objClassType = Type.GetTypeFromProgID("Excel.Application");
                var objApp_Late = Activator.CreateInstance(objClassType);

                //Get the workbooks collection. 
                var objBooks_Late = objApp_Late.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, objApp_Late, null);

                //Add a new workbook. 
                var objBook_Late = objBooks_Late.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objBooks_Late, null);

                //Get the worksheets collection. 
                var objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, objBook_Late, null);

                //Get the first worksheet. 
                var Parameters = new Object[1];
                Parameters[0] = 1;
                var objSheet_Late = objSheets_Late.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, objSheets_Late, Parameters);

                // Create the headers in the first row of the sheet 
                object objRange_Late;
                for (var i = 0; i < 4; i++)
                {
                    //Get a range object that contains cell. 
                    Parameters = new Object[2];
                    Parameters[0] = columns[i] + "1";
                    Parameters[1] = Missing.Value;
                    objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, Parameters);
                    //Write Headers in cell. 
                    Parameters = new Object[1];
                    Parameters[0] = headers[i];
                    objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objRange_Late, Parameters);
                }

                // Now add the data from the grid to the sheet starting in row 2 
                var row = 1;
                foreach (AlarmInfo item in lst)
                {
                    row++;
                    for (var j = 0; j < 4; j++)
                    {
                        //Get a range object that contains cell. 
                        Parameters = new Object[2];
                        Parameters[0] = columns[j] + row;
                        Parameters[1] = Missing.Value;
                        objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, Parameters);

                        //Write Headers in cell. 
                        Parameters = new Object[1];
                        switch (j)
                        {
                            case 0:
                                Parameters[0] = item.AlarmID;
                                break;
                            case 1:
                                Parameters[0] = item.EQPName;
                                break;
                            case 2:
                                Parameters[0] = item.AlarmLevel;
                                break;
                            case 3:
                                Parameters[0] = item.AlarmText;
                                break;
                        }
                        objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objRange_Late, Parameters);
                    }
                }

                //Return control of Excel to the user. 
                Parameters = new Object[1];
                Parameters[0] = true;
                objApp_Late.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, objApp_Late, Parameters);
                objApp_Late.GetType().InvokeMember("UserControl", BindingFlags.SetProperty, null, objApp_Late, Parameters);
            }
            catch (Exception ex)
            {

            }
        }

        public void ExportAlarmHistory(IEnumerable<AlarmInfo> lst)
        {
            var headers = new[] { "AlarmID", "Equipment", "AlarmLevel", "AlarmStatus", "AlarmText", "Time" };
            var columns = new[] { "A", "B", "C", "D", "E", "F" };

            try
            {
                // Get the class type and instantiate Excel. 
                var objClassType = Type.GetTypeFromProgID("Excel.Application");
                var objApp_Late = Activator.CreateInstance(objClassType);

                //Get the workbooks collection. 
                var objBooks_Late = objApp_Late.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, objApp_Late, null);

                //Add a new workbook. 
                var objBook_Late = objBooks_Late.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, objBooks_Late, null);

                //Get the worksheets collection. 
                var objSheets_Late = objBook_Late.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, objBook_Late, null);

                //Get the first worksheet. 
                var Parameters = new Object[1];
                Parameters[0] = 1;
                var objSheet_Late = objSheets_Late.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, objSheets_Late, Parameters);

                // Create the headers in the first row of the sheet 
                object objRange_Late;
                for (var i = 0; i < 6; i++)
                {
                    //Get a range object that contains cell. 
                    Parameters = new Object[2];
                    Parameters[0] = columns[i] + "1";
                    Parameters[1] = Missing.Value;
                    objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, Parameters);
                    //Write Headers in cell. 
                    Parameters = new Object[1];
                    Parameters[0] = headers[i];
                    objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objRange_Late, Parameters);
                }

                // Now add the data from the grid to the sheet starting in row 2 
                var row = 1;
                foreach (AlarmInfo item in lst)
                {
                    row++;
                    for (var j = 0; j < 6; j++)
                    {
                        //Get a range object that contains cell. 
                        Parameters = new Object[2];
                        Parameters[0] = columns[j] + row;
                        Parameters[1] = Missing.Value;
                        objRange_Late = objSheet_Late.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, objSheet_Late, Parameters);

                        //Write Headers in cell. 
                        Parameters = new Object[1];
                        switch (j)
                        {
                            case 0:
                                Parameters[0] = item.AlarmID; break;
                            case 1:
                                Parameters[0] = item.EQPName; break;
                            case 2:
                                Parameters[0] = item.AlarmLevel; break;
                            case 3:
                                Parameters[0] = item.AlarmStatus; break;
                            case 4:
                                Parameters[0] = item.AlarmText; break;
                            case 5:
                                Parameters[0] = item.CreateDate; break;
                        }
                        objRange_Late.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, objRange_Late, Parameters);
                    }
                }

                //Return control of Excel to the user. 
                Parameters = new Object[1];
                Parameters[0] = true;
                objApp_Late.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null, objApp_Late, Parameters);
                objApp_Late.GetType().InvokeMember("UserControl", BindingFlags.SetProperty, null, objApp_Late, Parameters);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
