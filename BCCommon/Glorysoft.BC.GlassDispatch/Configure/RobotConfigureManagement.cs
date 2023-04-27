using Glorysoft.BC.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace  Glorysoft.BC.GlassDispath
{
    public class RobotConfigureManagement
    {
        #region 单例
        private RobotConfigureManagement()
        {
            RobotConfigureList = new Dictionary<string, RobotConfigure>();
        }

        private static readonly Lazy<RobotConfigureManagement> Lazy = new Lazy<RobotConfigureManagement>(() => new RobotConfigureManagement());

        public static RobotConfigureManagement Current
        {
            get
            {
                return Lazy.Value;
            }
        }
        #endregion

        public Dictionary<string, RobotConfigure> RobotConfigureList { get;  set; }
        public bool LoadConfigure(string fromfile)
        {
            try
            {
                RobotConfigureList.Clear();
                var json = File.ReadAllText(fromfile);
                RobotConfigureList = JsonConvert.DeserializeObject<Dictionary<string, RobotConfigure>>(json.Trim());
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
            return true;
        }
        public Dictionary<string, RobotConfigure> LoadDbConfigure(IList<RobotConfigure> listRobotConfigure, IList<RobotPathConfigure> listRobotPathConfigure)
        {
            try
            {
                RobotConfigureList.Clear();
                foreach (var robotConfigure in listRobotConfigure)
                {
                    RobotConfigureList.Add(robotConfigure.IndexerName, robotConfigure);
                    //var groupList = listGroupConfigure.ToList().FindAll(o => o.LineName == robotConfigure.LineName && o.IndexerName == robotConfigure.IndexerName);
                    //if (groupList != null && groupList.Count > 0)
                    //{
                    //    foreach (var group in groupList)
                    //    {
                    //        robotConfigure.GroupList.Add(group);
                    //    }
                    //}
                    var pathList = listRobotPathConfigure.ToList().FindAll(o => o.LineName == robotConfigure.LineName && o.IndexerName == robotConfigure.IndexerName);
                    if (pathList != null && pathList.Count > 0)
                    {
                        var pathListGroup= pathList.GroupBy(o => o.ModePath).ToList();
                        
                        foreach (var item in pathListGroup)
                        {
                            string ModePath = item.Key;
                            var PathListByProcessMode = pathList.Where(o => o.ModePath == item.Key).ToList();
                            robotConfigure.PathList.Add(ModePath, PathListByProcessMode);
                        }
                        //foreach (var val in pathList)
                        //{
                        //    var mode = val.ProductionMode;
                        //    if (!robotConfigure.PathList.ContainsKey(mode))
                        //    {
                        //        robotConfigure.PathList.Add(mode, new List<RobotPathConfigure>());
                        //    }
                        //    var list = robotConfigure.PathList[mode];
                        //    list.Add(val);
                        //}
                    }
                }
                return RobotConfigureList;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }
          
        }
        public bool SaveConfigure(string tofile)
        {
            try
            {
                var json = JsonConvert.SerializeObject(RobotConfigureList);
                File.WriteAllText(tofile, json);
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return false;
            }
           
        }
        public List<string> RobotList
        {
            get { return RobotConfigureList.Keys.ToList(); }
        }
    }
}

