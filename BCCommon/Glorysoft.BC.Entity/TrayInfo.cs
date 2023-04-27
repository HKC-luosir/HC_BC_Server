
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class TrayInfo : NotifyPropertyChanged
    {
        public TrayInfo()
        {
            GlassInfoList = new List<GlassInfo>();
            //TrayInfoList = new List<TrayInfo>();
        }
        public DateTime CurrentDateTime { get; set; }
        public string TRAYID { get; set; }
        public string PROCESSFLOWNAME { get; set; }
        public string PROCESSFLOWVERSION { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }
        public string PROCESSOPERATIONVERSION { get; set; }
        //<result property = "TRAYID" column="trayid"/>
        //<result property = "PROCESSFLOWNAME" column="processflowname"/>
        //<result property = "PROCESSFLOWVERSION" column="processflowversion"/>
        //<result property = "PROCESSOPERATIONNAME" column="processoperationname"/>
        //<result property = "PROCESSOPERATIONVERSION" column="processoperationversion"/>

      
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTSPECVERSION { get; set; }
        public string PRODUCTIONTYPE { get; set; }
        //<result property = "PortType" column="porttype"/>
        //<result property = "PortUseType" column="portusetype"/>
        //<result property = "PRODUCTSPECNAME" column="productspecname"/>
        //<result property = "PRODUCTSPECVERSION" column="productspecversion"/>
        //<result property = "PRODUCTIONTYPE" column="productiontype"/>
        public string PRODUCTQUANTITY { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PRODUCTREQUESTNAME { get; set; }
        //<result property = "PRODUCTQUANTITY" column="productquantity"/>
        //<result property = "MACHINERECIPENAME" column="machinerecipename"/>
        //<result property = "PRODUCTREQUESTNAME" column="productrequestname"/>

        public string LotID { get; set; }
        public string Operid { get; set; }
        public string Prodid { get; set; }
        public string LotJudge { get; set; }
      //<result property = "LotID" column="lotid"/>
      //<result property = "Operid" column="operid"/>
      //<result property = "Prodid" column="prodid"/>
      //<result property = "LotJudge" column="lotjudge"/>
        public string POSITION { get; set; }
        public string PPID { get; set; }
        public string CELLCOUNT { get; set; }
        public string SLOTNO { get; set; }
        public DateTime CreateDate { get; set; }
        //<result property = "POSITION" column="position"/>
        // <result property = "PPID" column="ppid"/>
        // <result property = "CELLCOUNT" column="cellcount"/>
        // <result property = "SLOTNO" column="slotno"/>  
        // <result property = "CreateDate" column="createdate"/>

        public string PorTID { get; set; }
        public string PortType { get; set; }
        public string PortUseType { get; set; }
        // <result property = "PorTID" column="portid"/>
        // <result property = "PortType" column="porttype"/>
        // <result property = "PortUseType" column="portusetype"/>
        public List<GlassInfo> GlassInfoList { get; set; }





        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}

