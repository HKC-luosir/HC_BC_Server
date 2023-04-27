
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class PanelProcessEndFDC
    {
        public PanelProcessEndFDC()
        {
            PRODUCTLIST = new PanelProcessEndFDCPRODUCTLIST();
            MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }

        [XmlElement("PRODUCTLIST")]
        public PanelProcessEndFDCPRODUCTLIST PRODUCTLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("PRODUCT")]
    public class PanelProcessEndFDCPRODUCTLIST
    {
        public PanelProcessEndFDCPRODUCTLIST()
        {
            PRODUCTLIST = new List<PanelProcessEndFDCPRODUCT>();
        }
        [XmlElement("PRODUCT")]
        public List<PanelProcessEndFDCPRODUCT> PRODUCTLIST { get; set; }
    }
    public class PanelProcessEndFDCPRODUCT
    {
        public PanelProcessEndFDCPRODUCT()
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
