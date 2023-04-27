using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class PanelProcessStartedFDC
    {
        public PanelProcessStartedFDC()
        {
            PRODUCTLIST = new PanelProcessStartedFDCPRODUCTLIST();
            MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }
      
        [XmlElement("PRODUCTLIST")]
        public PanelProcessStartedFDCPRODUCTLIST PRODUCTLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("PRODUCT")]
    public class PanelProcessStartedFDCPRODUCTLIST
    {
        public PanelProcessStartedFDCPRODUCTLIST()
        {
            PRODUCTLIST = new List<PanelProcessStartedFDCPRODUCT>();
        }
        [XmlElement("PRODUCT")]
        public List<PanelProcessStartedFDCPRODUCT> PRODUCTLIST { get; set; }
    }
    public class PanelProcessStartedFDCPRODUCT
    {
        public PanelProcessStartedFDCPRODUCT()
        {
            LOTNAME = "";
            PRODUCTNAME = "";
            PRODUCTSPECNAME = "";
            PRODUCTRECIPE = "";
        }
        /// <summary>
        /// Panel ID
        /// </summary>
        public string LOTNAME { get; set; }
        /// <summary>
        /// Panel ID
        /// </summary>
        public string PRODUCTNAME { get; set; }
        /// <summary>
        /// Product ID
        /// </summary>
        public string PRODUCTSPECNAME { get; set; }
        /// <summary>
        /// Recipe Name(PPID)
        /// </summary>
        public string PRODUCTRECIPE { get; set; }
    }
}
