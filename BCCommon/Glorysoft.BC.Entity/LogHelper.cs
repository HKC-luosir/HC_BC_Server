using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Glorysoft.BC.Entity
{
    public static class LogHelper
    {
        public static string logbasepath { get; set; } = "";
        public static readonly ILog BCLog;
        public static readonly ILog MESLog;
        public static readonly ILog WebSocketLog;
        public static readonly ILog WebAPILog;
        public static readonly ILog EIPLog;
        public static readonly ILog DBLog;
        public static readonly ILog LinkSignalLog;
        public static readonly ILog CVDataLog;
        public static readonly ILog EIPERROR;
        public static readonly ILog SECSLog;
        //public static readonly ILog DVDataLog;
        static LogHelper()
        {
            if (ConfigurationManager.AppSettings["LogPath"] != null && !String.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]))
            {
                logbasepath = ConfigurationManager.AppSettings["LogPath"].TrimEnd('\\').TrimEnd('/');
            }
            else
            {
                logbasepath = "logs";
            }
            //var sPath = ConfigurationManager.AppSettings["LogPath"];
            //LogManager.ConfigureLogger("BCLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //BCLog = LogManager.GetConfigureLogger("BCLog");
            //LogManager.ConfigureLogger("MESLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //MESLog = LogManager.GetConfigureLogger("MESLog");
            //LogManager.ConfigureLogger("DBLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //DBLog = LogManager.GetConfigureLogger("DBLog");
            //LogManager.ConfigureLogger("WebSocketLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //WebSocketLog = LogManager.GetConfigureLogger("WebSocketLog");
            //LogManager.ConfigureLogger("WebAPILog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //WebAPILog = LogManager.GetConfigureLogger("WebAPILog");
            //LogManager.ConfigureLogger("EIPLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //EIPLog = LogManager.GetConfigureLogger("EIPLog");
            //LogManager.ConfigureLogger("LinkSignalLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //LinkSignalLog = LogManager.GetConfigureLogger("LinkSignalLog");
            //LogManager.ConfigureLogger("CVDataLog", sPath + "\\ProcessData", eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //CVDataLog = LogManager.GetConfigureLogger("CVDataLog");
            //LogManager.ConfigureLogger("SECSLog", sPath, eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //SECSLog = LogManager.GetConfigureLogger("SECSLog");
            BCLog = AddLogger("BCLog");
            MESLog = AddLogger("MESLog");
            WebSocketLog = AddLogger("WebSocketLog");
            WebAPILog = AddLogger("WebAPILog");
            EIPLog = AddLogger("EIPLog");
            DBLog = AddLogger("DBLog");
            LinkSignalLog = AddLogger("LinkSignalLog");
            CVDataLog = AddLogger("CVDataLog", "ProcessData");
            //LogManager.ConfigureLogger("DVDataLog", sPath + "\\ProcessData", eLogFilter.DEBUG, eLogFilter.DEBUG | eLogFilter.INFO | eLogFilter.ERROR, eFileFomart.Date, null);
            //DVDataLog = LogManager.GetConfigureLogger("DVDataLog");
            EIPERROR = AddLogger("EIPERROR", "EIPLog");
            SECSLog = AddLogger("SECSLog");
            AddLogger("PLCERROR", "PLCLog");
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Configured = true;
        }

        public static ILog AddLogger(string LoggerName, string logpath = null)
        {
            try
            {
                // 写入文档时的日志格式
                var patternLayout = new PatternLayout
                {
                    ConversionPattern = "%d %m%n"
                };
                patternLayout.ActivateOptions();


                var logger = (log4net.Repository.Hierarchy.Logger)log4net.LogManager.GetRepository().GetLogger(LoggerName);
                //logger.Hierarchy = hierarchy;
                logger.Level = log4net.Core.Level.Debug;
                // 文档日志
                string logs = String.IsNullOrEmpty(logpath) ? logbasepath : logbasepath + "\\" + logpath.TrimEnd('/');
                string[] LogLevels = new string[4] { "DEBUG", "INFO", "ERROR", "WARN" };
                foreach (var LogLevel in LogLevels)
                {
                    var roller = new RollingFileAppender
                    {
                        Name = $"{LoggerName}{LogLevel}",
                        File = $"{logs}/{LoggerName}/{LogLevel}/{LoggerName}.{LogLevel}.log",
                        Encoding = Encoding.UTF8,
                        PreserveLogFileNameExtension = true,
                        AppendToFile = true,
                        StaticLogFileName = true,
                        DatePattern = ".yyyy-MM-dd",
                        MaxSizeRollBackups = -1,
                        MaximumFileSize = "30MB",
                        RollingStyle = RollingFileAppender.RollingMode.Composite,
                        Layout = patternLayout
                    };
                    var LevelMin = log4net.Core.Level.Debug;
                    switch (LogLevel)
                    {
                        case "DEBUG":
                            LevelMin = log4net.Core.Level.Debug;
                            break;
                        case "INFO":
                            LevelMin = log4net.Core.Level.Info;
                            break;
                        case "ERROR":
                            LevelMin = log4net.Core.Level.Error;
                            break;
                        case "WARN":
                            LevelMin = log4net.Core.Level.Warn;
                            break;
                        default:
                            break;
                    }
                    roller.AddFilter(new log4net.Filter.LevelRangeFilter() { LevelMin = LevelMin, LevelMax = LevelMin });
                    roller.ActivateOptions();
                    logger.AddAppender(roller);
                }
                return new log4net.Core.LogImpl(logger);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// CreatDVFile
        /// </summary>
        /// <param name="ftphelper"></param>
        /// <param name="data"></param>
        public static void CreatDVFile(string UnitID, string glassid, string data)
        {
            lock (HostInfo.Current.lockdvdat)
            {
                try
                {
                    string DateTimeDir = DateTime.Now.ToString("yyyy-MM-dd");
                    string Path = logbasepath + "\\ProcessData\\DVDataLog\\" + DateTimeDir;

                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                        Thread.Sleep(50);
                    }

                    string file = Path + "\\" + glassid + ".txt";

                    using (StreamWriter txt = new StreamWriter(file, true, Encoding.UTF8))
                    {
                        //txt.Flush();
                        txt.WriteLine("---------------------------\r\n"
                         + "UnitID: " + UnitID + " Time: "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n"
                         + "GlassID: " + glassid + " " + "\r\n"
                        + data
                        + "---------------------------\r\n\r\n"
                            );
                        txt.Close();
                    }
                }
                catch (System.Exception ex)
                {
                    LogHelper.BCLog.Error(ex.ToString());
                }
            }
        }
    }
}
