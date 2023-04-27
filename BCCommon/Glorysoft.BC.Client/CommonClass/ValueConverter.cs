using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Glorysoft.BC.Entity;


namespace Glorysoft.BC.Client.CommonClass
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class HostConnect2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Red;
            if ((bool)value)
                color = Brushes.Green;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [ValueConversion(typeof(int), typeof(Brush))]
    public class EQPStatus2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color co = (Color)ColorConverter.ConvertFromString("#e6eded");
            Brush color = new SolidColorBrush(co);
            if ((int)value == 0)
                color = new SolidColorBrush(co);
            else if ((int)value == 1)
                color = Brushes.Yellow;
            else if ((int)value == 2)
                color = Brushes.Green;
            else if ((int)value == 3)
                color = Brushes.Red;
            else if ((int)value == 4)
                color = Brushes.Blue;
            else if ((int)value == 5)
                color = Brushes.CadetBlue;
            else if ((int)value == 6)
                color = Brushes.Orange;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class EQPStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = "";
            if ((int)value == 0)
                status = "IDLE";
            else if ((int)value == 1)
                status = "IDLE";
            else if ((int)value == 2)
                status = "RUN";
            else if ((int)value == 3)
                status = "DOWN";
            else if ((int)value == 4)
                status = "PM";
            else if ((int)value == 5)
                status = "MCHG";
            else if ((int)value == 6)
                status = "ETIME";
            return status;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [ValueConversion(typeof(int), typeof(Brush))]
    public class PortStatus2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Gray;
            if ((string)value == "Empty")
                color = Brushes.Beige;
            else if ((string)value == "Idle")
                color = Brushes.Gold;
            else if ((string)value == "Ready")
                color = Brushes.Brown;
            else if ((string)value == "Wait")
                color = Brushes.DodgerBlue;
            else if ((string)value == "Reserve")
                color = Brushes.AliceBlue;
            else if ((string)value == "Busy")
                color = Brushes.Green;
            else if ((string)value == "Complete")
                color = Brushes.Cyan;
            else if ((string)value == "Abort")
                color = Brushes.Coral;
            else if ((string)value == "Cancel")
                color = Brushes.DodgerBlue;
            else if ((string)value == "Pause")
                color = Brushes.DarkOliveGreen;
            else if ((string)value == "Resume")
                color = Brushes.LightSkyBlue;
            else if ((string)value == "Disable")
                color = Brushes.LightSlateGray;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Brush))]
    public class PortStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Brush))]
    public class CarrierStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = "";
            if ((int)value == 0)
                str = "Empty";
            else if ((int)value == 1)
                str = "Wait For Process";
            else if ((int)value == 2)
                str = "Wait For Start";
            else if ((int)value == 3)
                str = "In Processing";
            else if ((int)value == 4)
                str = "Process End";
            else if ((int)value == 5)
                str = "Process Cancel";
            else if ((int)value == 6)
                str = "Process Abort";
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(string), typeof(Brush))]
    public class RobotPortStatus2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.White;
            if ((string)value == "Empty")
                color = Brushes.White;
            else if ((string)value == "Idle")
                color = Brushes.Yellow;
            else if ((string)value == "Ready")
                color = Brushes.Brown;
            else if ((string)value == "Wait")
                color = Brushes.Blue;
            else if ((string)value == "Reserve")
                color = Brushes.AliceBlue;
            else if ((string)value == "Busy")
                color = Brushes.Green;
            else if ((string)value == "Complete")
                color = Brushes.Cyan;
            else if ((string)value == "Abort")
                color = Brushes.Firebrick;
            else if ((string)value == "Cancel")
                color = Brushes.DodgerBlue;
            else if ((string)value == "Pause")
                color = Brushes.ForestGreen;
            else if ((string)value == "Resume")
                color = Brushes.LightSkyBlue;
            else if ((string)value == "Disable")
                color = Brushes.LightSlateGray;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    [ValueConversion(typeof(string), typeof(Brush))]
    public class UseGrade2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Gray;
            if ((string)value == "P01")
                color = Brushes.SteelBlue;
            else if ((string)value == "P02")
                color = Brushes.Olive;
            else if ((string)value == "P03")
                color = Brushes.Violet;
            else if ((string)value == "P04")
                color = Brushes.Thistle;
            else if ((string)value == "P05")
                color = Brushes.Tomato;
            else if ((string)value == "P06")
                color = Brushes.Turquoise;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class Level2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)(value ?? -1);
            switch (index)
            {
                case 1: return "普通用户";
                case 2: return "管理员";
                default: return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (string)(value ?? "");
            switch (index)
            {
                case "普通用户": return 1;
                case "管理员": return 2;
                default: return null;
            }
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class AlarmStatus2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)(value ?? 0);
            switch (index)
            {
                case 1:
                    return "Set";
                case 2:
                    return "Clear";
                default:
                    return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (string)(value ?? "N/A");
            switch (index)
            {
                case "Set":
                    return 1;

                case "Clear":
                    return 2;
                default:
                    return null;
            }
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class AlarmCode2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)(value ?? 0);
            switch (index)
            {
                case 1:
                    return "Light";
                case 2:
                    return "Serious";
                default:
                    return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (string)(value ?? "N/A");
            switch (index)
            {
                case "Light":
                    return 1;

                case "Serious":
                    return 2;
                default:
                    return null;
            }
        }
    }
    [ValueConversion(typeof(int), typeof(string))]
    public class Int2String : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "0" : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index;
            int.TryParse(value.ToString(), out index);
            return index;
        }

        #endregion
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class Int2Bool : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (int)(value ?? -1);
            return index == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if ((bool) value)
            //    return 1;
            //return 2;

            var index = (bool)(value ?? true);
            return index ? 1 : 2;
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegateValue : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
            //string s = value.ToString();
            //char[] c = new char[] { ' ' };
            //return s.TrimEnd(c);
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(Brush))]
    public class PanelStatus2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Pink;
            if ((string)value == "Wait")
                color = Brushes.Pink;
            else if ((string)value == "ProbeIn")
                color = Brushes.Yellow;
            else if ((string)value == "Testing")
                color = Brushes.AliceBlue;
            else if ((string)value == "ProbeOut")
                color = Brushes.Green;
            else if ((string)value == "TargetIn")
                color = Brushes.DarkGray;
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class HostConnect2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (bool)(value);
            if (result)
                return "Connected";
            else
                return "Connecting";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class VCRMode2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (int)(value);
            if (result == 1)
                return "VCR USE";
            else
                return "VCR No USE";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class EQPMode2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (int)(value);
            switch (result)
            {
                case 1: return "Normal";
                case 2: return "Skip";
                case 3: return "Test";
                default: return "Normal";
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
            var result = (bool)(value);
            switch (result)
            {
                case true: return "Enable";
                case false: return "Disable";
                default: return "Enable";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    [ValueConversion(typeof(bool), typeof(string))]
    public class PCIMUse2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (bool)(value);
            if (result)
                return "Use";
            else
                return "Not Use";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class AntiHostModel : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }

    [ValueConversion(typeof(bool), typeof(String))]
    public class Bool2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (bool)(value);
            if (index)
                return "Connected";
            else
                return "Not Connection";
            //return index == 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class Bool2Brush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Red;
            var colorIndex = (bool)value;
            if (colorIndex)
                color = Brushes.Green;

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(String))]
    public class TargetPlace2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = "";
            switch ((int)value)
            {
                case 1: str = "PPBox Packer";
                    break;
                case 2:str = "Tray Unloader";
                    break;
                case 3:str = "STK Unloader";
                    break;
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int i = 0;
                switch ((string)value)
                {
                    case "PPBox Packer":
                        i = 1;
                        break;
                    case "Tray Unloader":
                        i = 2;
                        break;
                    case "STK Unloader":
                        i = 3;
                        break;
                }
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
    }

    [ValueConversion(typeof(List<int>), typeof(List<string>))]
    public class TargetPlaceList2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> lst = new List<string>();
            foreach (var item in ((List<int>)value))
            {
                string str = "";
                switch ((int)item)
                {
                    case 1:
                        str = "PPBox Packer";
                        break;
                    case 2:
                        str = "Tray Unloader";
                        break;
                    case 3:
                        str = "STK Unloader";
                        break;
                }
                lst.Add(str);
            }
            return lst;
        }
    

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
         }
    }

    [ValueConversion(typeof(string), typeof(String))]
    public class HostControlState2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value==null)
            {
                return "";
            }
            string ControlState = "";
            switch (value.ToString())
            {
                case "F":
                    ControlState = "OffLine";
                    break;
                case "R":
                    ControlState = "Online Remote";
                    break;
                case "L":
                    ControlState = "Online Local";
                    break;
                case "C":
                    ControlState = "LC Control";
                    break;
            }
            return ControlState;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    [ValueConversion(typeof(String), typeof(bool))]
    public class HostMode2Bool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Equals(parameter))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value && parameter.ToString() == ControlState.OnlineRemote.ToString())
                return ControlState.OnlineRemote;
            else if ((bool)value && parameter.ToString() == ControlState.OnlineLocal.ToString())
                return ControlState.OnlineLocal;
            else return ControlState.Offline;
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class PLCConnect2String : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string connect = "";
            var result = (bool)(value);
            if (result)
                connect = "Connected";
            else
                connect = "DisConnect";
            return connect;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class Bool2SheetGlass : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string connect = "";
            var result = (bool)(value);
            if (result)
                connect = "Sheet";
            else
                connect = "Glass";
            return connect;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class LightAlarm2Bool : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = false;
            if ((int)value==1)
            {
                index = true;
            }
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (bool)(value ?? true);
            return index ? "1" : "2";
        }

        #endregion
    }

    [ValueConversion(typeof(string), typeof(bool))]
    public class SeriousAlarm2Bool : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = false;
            if ((int)value == 2)
            {
                index = true;
            }
            return index;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = (bool)(value ?? true);
            return index ? "2" : "1";
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class PortEnable2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush color = Brushes.Gray;
            var result = (bool)(value);
            if (result)
                color = Brushes.Green;
            else
                color = Brushes.Red;

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Brush))]

    [ValueConversion(typeof(bool), typeof(string))]
    public class Bit2Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "1")
                return Brushes.Yellow;
            else
                return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}