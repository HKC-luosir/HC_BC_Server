using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
  
    public class CFGS1F5 : NotifyPropertyChanged
    {


        public CFGS1F5()
        {

        }

        private string eqpid;
        public string EQPID
        {
            get
            {
                return eqpid;
            }
            set
            {
                if (eqpid != value)
                {
                    eqpid = value;
                    Notify("EQPID");
                }
            }
        }
        public string UnitID { get; set; }
        public string SFCDName { get; set; }
        public bool Enable { get; set; }
        //      eqpid character varying(50),
        //unitid character varying(50),
        //sfcdname character varying(50),
        //enable boolean
    }
}
