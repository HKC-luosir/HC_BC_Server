using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class PackingBoxInfo
    {
        public string EQPID { get; set; }
        public string BoxID { get; set; }
        public string BoxSpecID { get; set; }
        public string BoxOperationID { get; set; }
        public string BoxProductionType { get; set; }
        public string BoxWorkOrder { get; set; }
        public string BoxGroupID { get; set; }
        public string BoxCheckInCode { get; set; }
        public string BoxWeight { get; set; }
        public string BoxRevisionCode { get; set; }
        public string BoxFGCode { get; set; }
        public string BoxGrade { get; set; }
        public int PanelQTY { get; set; }
        public string PalletID { get; set; }
        public string BoxSerial { get; set; }
        public string Time { get; set; }
        public string TranID { get; set; }
    }
}
