using Glorysoft.SECSwell;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.SECS.Service
{
    /// <summary>
    /// Extensions类
    /// </summary>
    public static class Extensions
    {
        #region 类型转换
        #region String -> SECSFormat
        public static eSECS_FORMAT ToSECSType(this string value)
        {
            return (eSECS_FORMAT)Enum.Parse(typeof(eSECS_FORMAT), value, true);
        }
        #endregion

        #region String -> SECSType
        public static byte ToU1(this string value)
        {
            byte result = 0;
            if (Byte.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static ushort ToU2(this string value)
        {
            ushort result = 0;
            if (UInt16.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static uint ToU4(this string value)
        {
            uint result = 0;
            if (UInt32.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static ulong ToU8(this string value)
        {
            ulong result = 0;
            if (UInt64.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }

        public static sbyte ToI1(this string value)
        {
            sbyte result = 0;
            if (SByte.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static short ToI2(this string value)
        {
            short result = 0;
            if (Int16.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static int ToI4(this string value)
        {
            int result = 0;
            if (Int32.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static long ToI8(this string value)
        {
            long result = 0;
            if (Int64.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }

        public static float ToF4(this string value)
        {
            float result = 0.0f;
            if (Single.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        public static double ToF8(this string value)
        {
            double result = 0.0;
            if (Double.TryParse(value, out result))
            {
                return result;
            }
            return result;
        }
        /// <summary>
        /// 将字符串转换为Boolean类型(支持大小写,简写T/F,1/0)
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static bool ToBo(this string value)
        {
            bool result = false;
            switch (value)
            {
                default:
                case "0":
                case "FALSE":
                case "false":
                case "False":
                case "F": result = false; break;
                case "1":
                case "TRUE":
                case "true":
                case "True":
                case "T": result = true; break;
            }
            return result;
        }
        #endregion

        #region String -> DateTime
        /// <summary>
        /// HHmmss
        /// </summary>
        public static DateTime ToTime(this string value)
        {
            DateTime result;
            result = DateTime.ParseExact(value, "HHmmss", CultureInfo.CurrentCulture);
            return result;
        }
        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        public static DateTime ToDateTime(this string value)
        {
            DateTime result;
            result = DateTime.ParseExact(value, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);
            return result;
        }
        /// <summary>
        /// 自定义格式
        /// </summary>
        /// <param name="format">格式字符串</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value, string format)
        {
            DateTime result;
            result = DateTime.ParseExact(value, format, CultureInfo.CurrentCulture);
            return result;
        }
        #endregion

        #region Object -> SECSType
        public static byte ToU1(this object value)
        {
            return ToU1(value.ToString());
        }
        public static ushort ToU2(this object value)
        {
            return ToU2(value.ToString());
        }
        public static uint ToU4(this object value)
        {
            return ToU4(value.ToString());
        }
        public static ulong ToU8(this object value)
        {
            return ToU8(value.ToString());
        }

        public static sbyte ToI1(this object value)
        {
            return ToI1(value.ToString());
        }
        public static short ToI2(this object value)
        {
            return ToI2(value.ToString());
        }
        public static int ToI4(this object value)
        {
            return ToI4(value.ToString());
        }
        public static long ToI8(this object value)
        {
            return ToI8(value.ToString());
        }

        public static float ToF4(this object value)
        {
            return ToF4(value.ToString());
        }
        public static double ToF8(this object value)
        {
            return ToF8(value.ToString());
        }
        /// <summary>
        /// 将字符串转换为Boolean类型(支持大小写,简写T/F,1/0)
        /// </summary>
        /// <param name="value">要转换的对象</param>
        /// <returns></returns>
        public static bool ToBo(this object value)
        {
            return ToBo(value.ToString());
        }
        /// <summary>
        /// object转换为byte[](注: 直接byte[]强制转换,object默认应该byte[])
        /// </summary>
        /// <param name="value">装箱的byte[]对象</param>
        /// <returns></returns>
        public static byte[] ToBi(this object value)
        {
            if (value is byte[])
            {
                return ((byte[])value);
            }
            if (value is int)
            {
                return BitConverter.GetBytes(value.ToI4());
            }
            else
            {
                return Encoding.Default.GetBytes(value.ToString());
            }
        }
        #endregion

        #region Object -> Bi
        public static byte[] ToBi(this int value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] ToBi(this uint value)
        {
            return BitConverter.GetBytes(value);
        }
        #endregion

        #region Boolean -> String
        /// <summary>
        /// 将Boolean转换为字符串简写(T/F)
        /// </summary>
        /// <param name="value">要转换的Boolean对象</param>
        /// <returns>返回T或者F</returns>
        public static string ToBoStr(this bool value)
        {
            return (value ? "T" : "F");
        }
        #endregion

        #region String -> Second
        /// <summary>
        /// 将HHmmss格式的字符串转换为秒数
        /// </summary>
        public static int ToSeconds(this string value)
        {
            int len = value.Length;
            int hour = 0;
            int minute = 0;
            int second = 0;
            if (len > 5)
            {
                second = value.Substring(4, 2).ToI4();
            }
            if (len > 3)
            {
                minute = value.Substring(2, 2).ToI4();
            }
            if (len > 1)
            {
                hour = value.Substring(0, 2).ToI4();
            }
            return ((int)(new TimeSpan(hour, minute, second).TotalSeconds));
        }
        /// <summary>
        /// 将HHmmss格式的对象转换为秒数
        /// </summary>
        public static int ToSeconds(this object value)
        {
            return value.ToString().ToSeconds();
        }
        #endregion

        #endregion

        #region 扩展方法
        public static void SetValue(this SECSItem item, object value, int length = 0, eSECS_FORMAT fmt = eSECS_FORMAT.ASCII, string name = "")
        {
            item.SetValue(fmt, value);
            item.Length = length;
            item.Name = name;
        }

        #region Xml Serialization
        /// <summary>
        /// 将对象序列化为Xml字符串
        /// </summary>
        /// <typeparam name="TObject">对象的类型</typeparam>
        /// <param name="this">要序列化的对象</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>成功返回Xml字符串, 失败返回 null</returns>
        public static string SerializeXml<TObject>(this TObject @this, Encoding encoding = null)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(@this.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, @this);
                if (encoding == null)
                {
                    return Encoding.Default.GetString(memoryStream.ToArray());
                }
                else
                {
                    return encoding.GetString(memoryStream.ToArray());
                }
            }
        }
        /// <summary>
        /// 将Xml符串反序列化为对象
        /// </summary>
        /// <typeparam name="TObject">对象的类型</typeparam>
        /// <param name="this">要反序列化的Xml字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>成功返回 TObject 对象, 失败返回 null</returns>
        public static TObject DeserializeXml<TObject>(this string @this, Encoding encoding = null)
        {
            using (MemoryStream memoryStream = new MemoryStream((encoding == null ? Encoding.Default.GetBytes(@this) : encoding.GetBytes(@this))))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TObject));
                return (TObject)xmlSerializer.Deserialize(memoryStream);
            }
        }
        #endregion
        #endregion
    }
}
