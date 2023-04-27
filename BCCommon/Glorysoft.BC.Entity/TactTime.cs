using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class TactTime
    {
        private string lineId; //产线ID
        private string eqId; //设备ID
        private string eqname;//设备名称
        private double eqTactTime;//设备tacttime时间
        public string LineID
        {
            get { return lineId; }
            set { lineId = value; }
        }

        public string EQPName
        {
            get { return eqname; }
            set { eqname = value; }
        }
        public string EQPID
        {
            get { return eqId; }
            set { eqId = value; }
        }
        public double EQPTactTime
        {
            get { return eqTactTime; }
            set { eqTactTime = value; }
        }
    }
}
