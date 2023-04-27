using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class GlassProcessData
    {
        public GlassProcessData()
        {
            EQPID = "";
            MODULEID = "";
            LOTID = "";
            CSTID = "";
            GLSID = "";
            PROCESSOPERATION = "";
            PRODUCTSPEC = "";
            RECIPEID = "";
            DCOLLNAME = "";
            SITENAME = "";
            DCOLLVALUE = "";
            DCOLLIST = new List<string>();
            SITELIST = new List<string>();
        }
        public string EQPID { get; set; }
        public string MODULEID { get; set; }
        public string LOTID { get; set; }
        public string CSTID { get; set; }
        public string GLSID { get; set; }
        public string PROCESSOPERATION { get; set; }
        public string PRODUCTSPEC { get; set; }
        public string RECIPEID { get; set; }
        public List<string> DCOLLIST { get; set; }
        public string DCOLLNAME { get; set; }
        public List<string> SITELIST { get; set; }
        public string SITENAME { get; set; }

        public string DCOLLVALUE { get; set; }

    }
    
}
