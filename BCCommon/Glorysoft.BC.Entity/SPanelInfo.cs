using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Glorysoft.BC.Entity
{
    public class SPanelInfo : NotifyPropertyChanged
    {

        

      
        public SPanelInfo()
        {

        }
       
        public string ReadPanelID { get; set; }
        public string HPanelID { get; set; }
        public string PanelGrade { get; set; }
        public string FPtID { get; set; }
        public string TPtID { get; set; }
        public int FSlotNO { get; set; }
        public int TSlotNO { get; set; }
        public string LotID { get; set; }
        public string FCstID { get; set; }
        public string TCstID { get; set; }
        public DateTime CreateDate { get; set; }
        public string QTY { get; set; }
        public string SlotSel { get; set; }
        /// <summary>
        /// slot no
        /// </summary>
        public string FJobSequenceNumber { get; set; }
        public string TJobSequenceNumber { get; set; }
        public string FCassetteSequence { get; set; }
        public string TCassetteSequence { get; set; }
        public string PPID { get; set; }
        public string LotJudge { get; set; }
        public string LotSortType { get; set; }
        public string UnitPathNo { get; set; }
        public string Operid { get; set; }
        public string Prodid { get; set; }
        public string SmplFlag { get; set; }
        public string RwkCnt { get; set; }
        public string CfGlsID { get; set; }
        public string LotType { get; set; }
        public string SPnlID { get; set; }
        public string PnlJudge { get; set; }
        public string PnlGrade { get; set; }
        public string PnlSortType { get; set; }
        public string AtPnlGrade { get; set; }
        public string AssyPnlGrade { get; set; }
        public string OsRepair { get; set; }
        public string AtRepair { get; set; }
      
        

       
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
