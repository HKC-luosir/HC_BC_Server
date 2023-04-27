using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class LotProcessEndFDC
    {
        public LotProcessEndFDC()
        {
            PRODUCTLIST = new LotProcessEndFDCProductList();
               MACHINENAME ="";
              CARRIERNAME ="";
              PORTNAME ="";
              PORTTYPE ="";
              PORTUSETYPE ="";
              LOTJUDGE ="";
        }
        public string MACHINENAME { get; set; }
        public string CARRIERNAME { get; set; }
        public string PORTNAME { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTUSETYPE { get; set; }

        /// <summary>
        /// [ G | N | R | P ]
        /// </summary>
        public string LOTJUDGE { get; set; }

        //[XmlArray("PRODUCTLIST")]
        //[XmlArrayItem("PRODUCT ")]
        [XmlElement("PRODUCTLIST")]
        public LotProcessEndFDCProductList PRODUCTLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("PRODUCT")]
    public class LotProcessEndFDCProductList
    {
        public LotProcessEndFDCProductList()
        {
            PRODUCTLIST = new List<LotProcessEndFDCProduct>();
        }
        [XmlElement("PRODUCT")]
        public List<LotProcessEndFDCProduct> PRODUCTLIST { get; set; }
    }
    public class LotProcessEndFDCProduct
    {
        public LotProcessEndFDCProduct()
        {
            LOTNAME = "";
            PRODUCTSPECNAME = "";
            PRODUCTRECIPE = "";
        }
        public string LOTNAME { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTRECIPE { get; set; }

    }
}
