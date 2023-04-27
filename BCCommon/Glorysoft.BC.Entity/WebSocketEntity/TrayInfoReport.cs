using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
 
    public class TrayInfoReport : BaseClass
    {
        public TrayInfoReport()
        {
            TrayList = new List<TrayInfoReportTray>();

        }
        public List<TrayInfoReportTray> TrayList { get; set; }
    }
    public class TrayInfoReportTray
    {
        public string TRAYID { get; set; }
        public string PROCESSFLOWNAME { get; set; }
        public string PROCESSFLOWVERSION { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }
        public string PROCESSOPERATIONVERSION { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTSPECVERSION { get; set; }
        public string PRODUCTIONTYPE { get; set; }
        public string PRODUCTQUANTITY { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PRODUCTREQUESTNAME { get; set; }
        public string LotID { get; set; }
        public string Operid { get; set; }
        public string Prodid { get; set; }
        public string LotJudge { get; set; }
        public string POSITION { get; set; }
        public string PPID { get; set; }
        public string CELLCOUNT { get; set; }
        public string SLOTNO { get; set; }
        public DateTime CreateDate { get; set; }
        public string PorTID { get; set; }
        public string PortType { get; set; }
        public string PortUseType { get; set; }
    }
}
