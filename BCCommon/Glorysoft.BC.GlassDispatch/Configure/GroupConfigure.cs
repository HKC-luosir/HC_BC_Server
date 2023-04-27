using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.GlassDispath
{
    public class GroupConfigure
    {
        private string groupList;
        public string Name { get; set; }
        public IList<int> List { get; set; }
        public string GroupList
        {
            get
            {
                return groupList;
            }
            set
            {
                groupList = value.Trim();
                List = new List<int>();
                var arr = groupList.Split(',');
                if (arr != null && arr.Length > 0)
                {
                    foreach (var val in arr)
                    {
                        List.Add(HostInfo.Current.StringToInt(val));
                    }
                }
            }
        }
        public int Method { get; set; }     //1:随机，2:顺序
        public string LineName { get; set; }
        public string IndexerName { get; set; }
    }
}

