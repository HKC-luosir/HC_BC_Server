using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
   public class VCR
    {

        //eqpid character varying(50),
        //unitid character varying(50),
        //unitname character varying(50),
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        //<result property = "EQPID" column="eqpid"/>
        //<result property = "UnitID" column="unitid"/>
        //<result property = "UnitName" column="unitname"/>


        //vcrenablemode integer,
        //vcrno integer,
        //vcrreadfailoperationmode integer
        public int VCREnableMode { get; set; }
        public int VCRNO { get; set; }
        public int VCRReadFailOperationMode { get; set; }
        //<result property = "VCREnableMode" column="vcrenablemode"/>
        //<result property = "VCRNO" column="vcrno"/>
        //<result property = "VCRReadFailOperationMode" column="vcrreadfailoperationmode"/>

        public DateTime CreateDate { get; set; }


    }
}
