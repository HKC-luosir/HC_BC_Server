

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class LotProcessStartedFDC
    {
        public LotProcessStartedFDC()
        {
            PRODUCTLIST = new LotProcessStartedFDCProductList();
            MACHINENAME = "";
            CARRIERNAME = "";
            PORTTYPE = "";
            PORTUSETYPE = "";
        }
        public string MACHINENAME { get; set; }
        public string CARRIERNAME { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTUSETYPE { get; set; }
        [XmlElement("PRODUCTLIST")]
        public LotProcessStartedFDCProductList PRODUCTLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("PRODUCT")]
    public class LotProcessStartedFDCProductList
    {
        public LotProcessStartedFDCProductList()
        {
            PRODUCTLIST = new List<LotProcessStartedFDCProduct>();
        }
        [XmlElement("PRODUCT")]
        public List<LotProcessStartedFDCProduct> PRODUCTLIST { get; set; }
    }
    public class LotProcessStartedFDCProduct
    {
        public LotProcessStartedFDCProduct()
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
