
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;

namespace Glorysoft.BC.Entity
{

    public class SSUnit : NotifyPropertyChanged
    {

        public SSUnit()
        {

        }
      // <result property = "EQPID" column="eqpid"/>     
      //<result property = "UnitID" column="unitid"/>
      //<result property = "UnitName" column="unitname"/>
      //<result property = "SUnitID" column="sunitid"/>
      //<result property = "SUnitName" column="sunitname"/>
      //<result property = "SSUnitID" column="ssunitid"/>
      //<result property = "SSUnitName" column="ssunitname"/>
      //<result property = "SSUnitStatus" column="ssunitstatus"/>
      //<result property = "SSUnitSTCode" column="ssunitstcode"/>
      //<result property = "SSUnitNo" column="ssunitno"/>
      //<result property = "SSUnitType" column="ssunittype"/>
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public string SUnitID { get; set; }
        public string SUnitName { get; set; }
        public string SSUnitID { get; set; }
        public string SSUnitName { get; set; }

        public string SSUnitStatus { get; set; }
        public string SSUnitSTCode { get; set; }
        public string SSUnitNo { get; set; }
        public string SSUnitType { get; set; }

    }
}