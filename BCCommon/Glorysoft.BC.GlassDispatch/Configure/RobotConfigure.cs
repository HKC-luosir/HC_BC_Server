using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using log4net;

namespace Glorysoft.BC.GlassDispath
{
    public class RobotConfigure
    {
        public string LineName { get; set; }
        public string IndexerName { get; set; }
        public RobotConfigure()
        {
            //GroupList = new List<GroupConfigure>();
            PathList = new Dictionary<string, List<RobotPathConfigure>>();
        }
        public string Name { get; set; }
        public string LogPath { get; set; }
        public string LogName { get; set; }
        public bool IsCheckDoublePut { get; set; }
        public bool IsCheckDoubleGet { get; set; }
        public bool IsLensGlassFirst { get; set; }
       // public List<GroupConfigure> GroupList { get; set; }
        public Dictionary<string, List<RobotPathConfigure>> PathList { get; set; }
        //public RobotPathConfigure GetPathConfigure(string mode, string sname, string ruleID)
        //{
        //    if (PathList.ContainsKey(mode))
        //    {
        //        var lst = PathList[mode];
        //        foreach (var cfg in lst)
        //        {
        //            if (GroupList.Exists(o => o.Name == cfg.SourcePathName))
        //            {
        //                var group = GroupList.Find(o => o.Name == cfg.SourcePathName);
        //                if (group != null)
        //                {
        //                    foreach (var name in group.List)
        //                    {
        //                        if (CheckPathConfigure(name, sname, cfg.RuleID, ruleID))
        //                        {
        //                            return cfg;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (CheckPathConfigure(cfg.SourcePathName, sname, cfg.RuleID, ruleID))
        //                {
        //                    return cfg;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
        //public RobotPathConfigure GetPathConfigure(string mode, RobotHand hand, string sname, string ruleID)
        //{
        //    if (PathList.ContainsKey(mode))
        //    {
        //        var lst = PathList[mode];
        //        foreach (var cfg in lst)
        //        {
        //            if (!cfg.RobotFixed || (cfg.RobotFixed && cfg.RobotArm == hand))
        //            {
        //                if (GroupList.Exists(o => o.Name == cfg.SourcePathName))
        //                {
        //                    var group = GroupList.Find(o => o.Name == cfg.SourcePathName);
        //                    if (group != null)
        //                    {
        //                        foreach (var name in group.List)
        //                        {
        //                            if(CheckPathConfigure(name, sname, cfg.RuleID, ruleID))
        //                            {
        //                                return cfg;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (CheckPathConfigure(cfg.SourcePathName, sname, cfg.RuleID, ruleID))
        //                    {
        //                        return cfg;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
        public List<RobotPathConfigure> GetPathConfigureList(string ModePath, int CurrentModelPosition, string ruleID, ILog Logger)
        {
            //Logger.Info("[GetPathConfigureList] begin ");
            var pathlist = new List<RobotPathConfigure>();
            if (PathList.ContainsKey(ModePath))
            {
                Logger.Info(string.Format("[GetPathConfigureList] PathList.ContainsKey(ModePath);ModePath:{0} ", ModePath));
                var lst = PathList[ModePath];

                foreach (var cfg in lst)
                {

                    Logger.Info(string.Format("[GetPathConfigureList] cfg.SourcePathName:{0};CurrentModelPosition:{1} ", cfg.SourcePathName, CurrentModelPosition));
                    //if (CheckPathConfigure(cfg.SourcePathName, CurrentModelPosition, cfg.RuleID, ruleID))
                    if(cfg.SourcePathName== CurrentModelPosition)
                    {
                        pathlist.Add(cfg);
                        Logger.Info(string.Format("[GetPathConfigureList]pathlist Add;  cfg.SourcePathName:{0};CurrentModelPosition:{1} ", cfg.SourcePathName, CurrentModelPosition));
                    }
                    else
                    {
                        Logger.Info(string.Format("[GetPathConfigureList]pathlist Not Add;  cfg.SourcePathName:{0};CurrentModelPosition:{1} ", cfg.SourcePathName, CurrentModelPosition));
                    }
                }
            }
            else
            {
                Logger.Info(string.Format("[GetPathConfigureList] PathList.ContainsKey(ModePath)=false;ModePath:{0} ", ModePath));
            }
           // Logger.Info("[GetPathConfigureList] end ");
            return pathlist;
        }
        //public List<RobotPathConfigure> GetPathConfigureList(string ModePath, RobotHand hand, int CurrentModelPosition, string ruleID)
        //{
        //    var pathlist = new List<RobotPathConfigure>();
        //    if (PathList.ContainsKey(ModePath))
        //    {
        //        var lst = PathList[ModePath];
        //        foreach (var cfg in lst)
        //        {
        //            if (!cfg.RobotFixed || (cfg.RobotFixed && cfg.RobotArm == hand))
        //            {
        //                //if (GroupList.Exists(o => o.Name == cfg.SourcePathName))
        //                //{
        //                //    var group = GroupList.Find(o => o.Name == cfg.SourcePathName);
        //                //    if (group != null)
        //                //    {
        //                //        foreach (var cfgSourceModelPosition in group.List)
        //                //        {
        //                //            if (CheckPathConfigure(cfgSourceModelPosition, CurrentModelPosition, cfg.RuleID, ruleID))
        //                //            {
        //                //                pathlist.Add(cfg);
        //                //            }
        //                //        }
        //                //    }
        //                //}
        //                //else
        //                //{
        //                    if (CheckPathConfigure(cfg.SourcePathName, CurrentModelPosition, cfg.RuleID, ruleID))
        //                    {
        //                        pathlist.Add(cfg);
        //                    }
        //               // }
        //            }
        //        }
        //    }
        //    return pathlist;
        //}
        //private bool CheckPathConfigure(int cfgSourceModelPosition, int CurrentModelPosition, string cfgRuleID, string glassRuleID)
        //{
        //    if (cfgSourceModelPosition == CurrentModelPosition)
        //    {
        //        return true;
        //        //if (!string.IsNullOrEmpty(glassRuleID) && glassRuleID == ConstDef.BY_PASS)
        //        //{
        //        //    if (!string.IsNullOrEmpty(cfgRuleID) && cfgRuleID == ConstDef.BY_PASS)
        //        //    {
        //        //        return true;
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    if (string.IsNullOrEmpty(cfgRuleID))
        //        //    {
        //        //        return true;
        //        //    }
        //        //    if (cfgRuleID == glassRuleID)
        //        //    {
        //        //        return true;
        //        //    }
        //        //}
        //    }
        //    return false;
        //}
    }
}

