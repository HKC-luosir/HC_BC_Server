using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class OperationMode
    {
        //  <result property = "EQPID" column ="eqpid"/>
        //<result property = "Equipmentvalue" column ="equipmentvalue"/>
        //<result property = "OperationModeName" column ="operationmodename"/>
        //<result property = "UpdateTime" column ="updatetime"/>
        //<result property = "UpdateUser" column ="updateuser"/>
        //<result property = "HostName" column ="hostname"/>
        //<result property = "SendHost" column ="sendhost"/>
        public string EQPID { get; set; }
        public int Equipmentvalue { get; set; }
        public string OperationModeName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public string HostName { get; set; }
        public bool SendHost { get; set; }
    }
}
