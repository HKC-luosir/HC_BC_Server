using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class GlassInfoRequest : BaseClass
    {
        public GlassInfoRequest() { }
        //       
        //  GLSID
        //FPorTID
        //TPorTID
        //FCSTID
        //TCSTID
        public string GLSID { get; set; }
        public string FPorTID { get; set; }
        public string TPorTID { get; set; }
        public string FCSTID { get; set; }
        public string TCSTID { get; set; }
        //IsOutPort
        //FCassetteSequence
        //TCassetteSequence
        //FSlotNO
        //TSlotNO
        //public string IsOutPort { get; set; }
        public string FCassetteSequence { get; set; }
        public string TCassetteSequence { get; set; }
        public string FSlotNO { get; set; }
        public string TSlotNO { get; set; }
    }
}
