//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;
//namespace Glorysoft.BC.Entity
//{
//    [Serializable]
//    [XmlRoot("SystemConfig")]
//    public  class SFCDConfig
//    {

//        private List<SFCDEqpInfo> sfcdEqpInfoList=new List<SFCDEqpInfo> ();
//        [XmlElement("EQPInfo")]
//        public List<SFCDEqpInfo> SFCDEqpInfoList
//        {
//            get
//            {
//                return sfcdEqpInfoList;
//            }
//            set
//            {
//                if (sfcdEqpInfoList != value)
//                {
//                    sfcdEqpInfoList = value;                   
//                }
//            }
//        }
//    }
//    [Serializable]
//    public class SFCDEqpInfo
//    {
//        private List<SFCDItem> sfcdItemList = new List<SFCDItem>();
//        [XmlElement("SFCDItem")]
//        public List<SFCDItem> SFCDItemList
//        {
//            get
//            {
//                return sfcdItemList;
//            }
//            set
//            {
//                if (sfcdItemList != value)
//                {
//                    sfcdItemList = value;
//                }
//            }
//        }
//    }
//    [Serializable]
//    public class SFCDItem
//    {
//        private string sfcd;
//        [XmlAttribute("SFCD")]
//        public string SFCD
//        {
//            get
//            {
//                return sfcd;
//            }
//            set
//            {
//                if (sfcd != value)
//                {
//                    sfcd = value;
//                }
//            }
//        }
//        private bool enable;
//        [XmlAttribute("Enable")]
//        public bool Enable
//        {
//            get
//            {
//                return enable;
//            }
//            set
//            {
//                if (enable != value)
//                {
//                    enable = value;
//                }
//            }
//        }
//    }
//}
