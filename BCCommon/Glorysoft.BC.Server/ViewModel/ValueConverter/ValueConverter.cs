using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Glorysoft.BC.Server.ViewModel.ValueConverter
{
    [ValueConversion(typeof(bool), typeof(String))]
    public class Bool2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var index = (bool)(value);
                if (index)
                    return "Connected";
                else
                    return "Not Connection";
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                 return null;
            }
            
            //return index == 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(string), typeof(Brush))]
    public class PortStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //0:Empty Port
            //1:Load Requet
            //2:Load Complete
            //3:Unload Request
            //4:Unload Complete
            //5:In Processing
            //6:Down
             try
            {
                string str = "";
                if ((int)value == 0)
                    str = "Empty";
                else if ((int)value == 1)
                    str = "Load Request";
                else if ((int)value == 2)
                    str = "Load Complete";
                else if ((int)value == 3)
                    str = "Unload Request";
                else if ((int)value == 4)
                    str = "Unload Complete";
                else if ((int)value == 5)
                    str = "In Processing";
                else if ((int)value == 6)
                    str = "Down";
                return str;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
      [ValueConversion(typeof(string), typeof(Brush))]
    public class CSTStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //1 : No Cassette Exist
            //2: Waiting for Start Command
            //3 : Waiting for Processing
            //4 : In Processing
            //5 : Process Paused
            //6 : Process Completed
             try
            {
                string str = "";
                if ((int)value == 0)
                    str = "Empty";
                else if ((int)value == 1)
                    str = "Empty";
                else if ((int)value == 2)
                    str = "WaitingForStart";
                else if ((int)value == 3)
                    str = "WaitingForProcessing";
                else if ((int)value == 4)
                    str = "InProcessing";
                else if ((int)value == 5)
                    str = "ProcessPaused";
                else if ((int)value == 6)
                    str = "Completed";
                return str;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class PortEnable2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //1.Enable
            //2.Disable
             try
            {
                Brush color = Brushes.Gray;
                var result = (string)(value);
                if (result == "1")
                    color = Brushes.Green;
                else
                    color = Brushes.Red;

                return color;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(int), typeof(string))]
    public class EQPEnable2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                var result = (string)(value);
                switch (result == "1")
                {
                    case true: return "Enable";
                    case false: return "Disable";
                    default: return "Enable";
                }
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Brush))]
    public class Int2FillColor : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                // LogHelperTCim.TCIMLogger.Error("Int2FillColor1");
                Brush color = Brushes.YellowGreen;
                var colorIndex = (string)(value);// value;
                                                 //if (colorIndex > 0)
                                                 //{
                color = (Brush)Glorysoft.BC.Entity.Consts.EQStatusColor.EQColor[colorIndex];
                //}

                return color;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class Bool2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                Brush color = Brushes.Red;
                var colorIndex = (bool)value;
                if (colorIndex)
                    color = Brushes.Green;

                return color;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class PLCConnect2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                string connect = "";
                var result = (bool)(value);
                if (result)
                    connect = "Connected";
                else
                    connect = "DisConnet";
                return connect;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(bool), typeof(string))]
    public class EQPPackModeString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string connect = "";
                //1：CST→CST，2，CST→BOX，3：BOX→BOX，4：BOX→CST 
                var result = (int)(value);
                  switch(result)
                  {
                      case 0:
                          connect = "";
                          break;
                      case 1:
                          connect = "CST→CST";
                          break;
                      case 2:
                          connect = "CST→BOX";
                          break;
                      case 3:
                          connect = "BOX→BOX";
                          break;
                      case 4:
                          connect = "BOX→CST";
                          break;
                  }
                return connect;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(int), typeof(String))]
    public class HostControlState2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                Brush color = Brushes.Red;
                switch ((EnumControlState)value)
                {
                    case EnumControlState.OffLine:
                        color = Brushes.Red;
                        break;
                    case EnumControlState.OnLineRemote:
                        color = Brushes.Green;
                        break;
                    case EnumControlState.OnLineLocal:
                        color = Brushes.Yellow;
                        break;
                }
                return color;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    [ValueConversion(typeof(int), typeof(String))]
    public class HostControlState2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             try
            {
                string State = "";
                switch ((EnumControlState)value)
                {
                   case EnumControlState.OffLine:
                        State = "Offline";
                        break;
                    case EnumControlState.OnLineRemote:
                        State = "OnlineRemote";
                        break;
                    case EnumControlState.OnLineLocal:
                        State = "OnlineLocal";
                        break;
                }
                return State;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
