using System;

namespace Glorysoft.BC.Entity
{
    public class MaterialInfo
    {       
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string MaterialType { get; set; }
        public string MaterialID { get; set; }
        public string MaterialState { get; set; }
        /// <summary>
        /// 物料使用数量
        /// </summary>
        public string MaterialUseCount { get; set; }//
        public string MaterialPosition { get; set; }
        public string MaterialTarget { get; set; }
        public string MaterialSource { get; set; }

        //public string GLSID { get; set; }
        //public string MaterialPartID { get; set; }
        public string MaterialLotID { get; set; }
        public string MaterialName { get; set; }


        public int MaterialQty { get; set; }        
        public int MaterialQTime { get; set; }        

        //public string OperatorID { get; set; }
        //public int? ValidationResult { get; set; }
        public DateTime CreateDate { get; set; }
        //   <result property = "EQPID" column="eqpid"/>
        //   <result property = "UnitID" column="unitid"/>
        //<result property = "MaterialType" column="materialtype"/>
        //<result property = "MaterialID" column="materialid"/>
        //<result property = "MaterialState" column="materialstate"/>
        //<result property = "MaterialUseDcnt" column="materialusedcnt"/>
        //<result property = "MaterialPosition" column="materialposition"/>
        //<result property = "CreateDate" column="createdate"/>
    }
}