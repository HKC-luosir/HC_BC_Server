using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotInfoRequestReply : RVBodyBase
    {
        public RVLotInfoRequestReply()
        {
            MessageName = "";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string LOTTYPE { get; set; }
        public string LOTID { get; set; }
        public string DURABLEID { get; set; }
        public string DURABLETYPE { get; set; }
        public string RECIPENAME { get; set; }
        public string GRADE { get; set; }
        public string MAINQTY { get; set; }
        public string SLOTMAP { get; set; }
        [XmlArray("UNITRECIPELIST")]
        [XmlArrayItem("UNITRECIPE")]
        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<PANEL> PANELLIST = new List<PANEL>();
    }
    public class UNITRECIPE
    {
        public UNITRECIPE()
        {
            UNITID = "";
        }
        public string UNITID { get; set; }
        public string UNITRECIPENAME { get; set; }
        public string CHECKFLAG { get; set; }
        public string RECIPENAME { get; set; }
        public string VERSION { get; set; }
        [XmlArray("PARAMALIST")]
        [XmlArrayItem("RECIPEPARAMETER")]
        public List<PARAM> PARAMALIST = new List<PARAM>();
    }
    public class PARAM
    {
        public PARAM()
        {
            NAME = "";
            VALUE = "";
        }
        public string NAME { get; set; }
        public string VALUE { get; set; }
        public string REASONCODE { get; set; }
    }
    public class PANEL
    {
        public PANEL()
        {
            PANELID = "";
            POSITION = "";
        }
        public string PANELID { get; set; }
        public string GRADE { get; set; }
        public string SUBGRADE { get; set; }
        public string ACTIONCOMMENT { get; set; }
        public string WOID { get; set; }
        public string POSITION { get; set; }
        public string BONDINGID { get; set; }
        public string PROCESSFLAG { get; set; }
        [XmlArray("SPECIALCODELIST")]
        [XmlArrayItem("SPECIALCODE")]
        public List<CODE> SPECIALCODELIST = new List<CODE>();
        public string ABNORMALCODE { get; set; }
        [XmlArray("DEFECTLIST")]
        [XmlArrayItem("DEFECT")]
        public List<RVDEFECTCODE> DEFECTLIST = new List<RVDEFECTCODE>();
        [XmlArray("PROCESSEDUNITLIST")]
        [XmlArrayItem("PROCESSEDUNIT")]
        public List<PROCESSEDUNIT> PROCESSEDUNITLIST = new List<PROCESSEDUNIT>();
    }
    public class CODE
    {
        public CODE()
        {
            CODEID = "";
            PANELID = "";
        }
        public string CODEID { get; set; }
        public string PANELID { get; set; }
    }
    public class PROCESSEDUNIT
    {
        public string PROCESSEDUNITNAME { get; set; }
    }
}
