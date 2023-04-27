
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class ModuleStateReport
    {
        public ModuleStateReport()
        {
            EQPID = "";
            EQPST = "";
            MODULELIST = new List<Unit>();
            MODULEID = "";
            MODULEST = "";
            SUBMODULELIST = new List<SUnit>();
            SUBMODULEID = "";
            SUBMODULEST = "";
        }
    
        public string EQPID { get; set; }
        public string EQPST { get; set; }
        public List<Unit> MODULELIST { get; set; }
        public string MODULEID { get; set; }
        public string MODULEST { get; set; }
        public List<SUnit> SUBMODULELIST { get; set; }
        public string SUBMODULEID { get; set; }
        public string SUBMODULEST { get; set; }
    }
}
