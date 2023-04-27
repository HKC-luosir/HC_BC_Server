
using System;
using System.Linq;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using Glorysoft.BC.Entity;

namespace Glorysoft.BC.Server.Infrastructure
{
    public class XmlSerializeManager
    {
        public static T Deserialize<T>(string appSettingID)
        {
            try
            {
                if (!ConfigurationManager.AppSettings.AllKeys.Contains(appSettingID))
                {
                    LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Deserialize] AppSettings not contains {0}", appSettingID));
                    return default(T);
                }
                var configPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings[appSettingID].Trim();
                if (!File.Exists(configPath))
                {
                    LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Deserialize] Configure File not Exist. {0}", appSettingID));
                    return default(T);
                }
                var serializer = new XmlSerializer(typeof(T));
                //LogHelper.PcimwellLog.Debug(string.Format("***XmlSerializeManager*** [Deserialize]  T is {0}", default(T).GetType()));
                using (var stream = new FileStream(configPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var r = (T)serializer.Deserialize(stream);
                    LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Deserialize]  Complete."));
                    return r;
                }
            }
            catch (Exception ee)
            {
                LogHelper.BCLog.Debug(ee);
                return default(T);
            }

        }
        public static bool Serialize<T>(T value, string appSettingID)
        {
            try
            {
                if (value == null)
                {
                    LogHelper.BCLog.Debug("***XmlSerializeManager*** [Serialize] value is null.");
                    return false;
                }
                if (!ConfigurationManager.AppSettings.AllKeys.Contains(appSettingID))
                {
                    LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Serialize] AppSettings not contains {0}", appSettingID));
                    return false;
                }
                var configPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings[appSettingID];
                LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Serialize]  configPath is {0}", configPath));
                LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Serialize]  T is {0}", value.GetType()));
                using (var writer = new FileStream(configPath, FileMode.Create, FileAccess.Write))
                {
                    try
                    {
                        var xs = new XmlSerializer(typeof(T));
                        xs.Serialize(writer, value);
                        LogHelper.BCLog.Debug(string.Format("***XmlSerializeManager*** [Serialize]  Complete."));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.BCLog.Debug(ex);
                        return false;
                    }
                }
            }
            catch (Exception ee)
            {
                LogHelper.BCLog.Debug(ee);
                return false;
            }

        }

    }
}
