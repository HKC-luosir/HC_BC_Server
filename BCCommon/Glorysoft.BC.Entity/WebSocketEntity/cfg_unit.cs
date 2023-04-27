using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_unit
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string unitname { get; set; }
        public int unittype { get; set; }
        public int unitcapacity { get; set; }
        public string reasoncode { get; set; }
        public string unitstcode { get; set; }
     
     
        public int hassunit { get; set; }
        public int unitmode { get; set; }
        public int cassetteoperationmode { get; set; }
        /// <summary>
        /// �Ƿ��Ǹ��������һ���豸
        /// </summary>
        public bool iseqpend { get; set; } = false;
        /// <summary>
        /// �Ƿ��Ǹ������һ���豸
        /// </summary>
        public bool iseqpstart { get; set; } = false;
        /// <summary>
        /// �Ƿ�У��recipeid
        /// </summary>
        public bool currentrecipeidcheck { get; set; } = true;
        /// <summary>
        /// �Ƿ��Ǳؾ��豸
        /// </summary>
        public bool isprocessend { get; set; } = false;
        /// <summary>
        /// �Ƿ���������
        /// </summary>
        public bool isjobdatarequest { get; set; } = false;
        /// <summary>
        /// EIP DeviceType
        /// </summary>
        public string devicetype { get; set; } = "";
        /// <summary>
        /// EIP Class3IP
        /// </summary>
        public string class3ip { get; set; } = "";
        /// <summary>
        /// EIP PLCIP
        /// </summary>
        public string plcip { get; set; } = "";
        /// <summary>
        /// EIP PLCPort
        /// </summary>
        public string plcport { get; set; } = "";
        /// <summary>
        /// VCR״̬ number:status;
        /// </summary>
        public string vcrstatus { get; set; }
        public int localno { get; set; }
        public int portqtime { get; set; }
        public int commandtype { get; set; }
        public int currentrecipeid { get; set; }
        public int downstreaminlinemode { get; set; }
        public int upstreaminlinemode { get; set; }
        public string unitno { get; set; }
        public int loadingstop { get; set; }
        public int crst { get; set; }
        public string unitstatus{ get; set; }
        public int cimmode { get; set; }
    }
}
