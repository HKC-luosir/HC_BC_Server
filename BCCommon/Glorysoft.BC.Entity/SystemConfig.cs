
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("SystemConfig")]
    public class SystemConfig
    {


        public SystemConfig()
        {

        }

        //private string eqpid;
        //[XmlElement("EQPID")]
        //public string EQPID
        //{
        //    get
        //    {
        //        return eqpid;
        //    }
        //    set
        //    {
        //        if (eqpid != value)
        //        {
        //            eqpid = value;

        //        }
        //    }
        //}
        private bool recipecheckenable;
        [XmlElement("RecipeCheckEnable")]
        public bool RecipeCheckEnable
        {
            get
            {
                return recipecheckenable;
            }
            set
            {
                if (recipecheckenable != value)
                {
                    recipecheckenable = value;

                }
            }
        }
        //private int robotdispathmode;
        //[XmlElement("RobotDispathMode")]

        //public int RobotDispathMode
        //{
        //    get
        //    {
        //        return robotdispathmode;
        //    }
        //    set
        //    {
        //        if (robotdispathmode != value)
        //        {
        //            robotdispathmode = value;                   
        //        }
        //    }
        //}

        private bool plcDateTimeEnable;
        [XmlElement("PLCDateTimeEnable")]
        public bool PLCDateTimeEnable
        {
            get
            {
                return plcDateTimeEnable;
            }
            set
            {
                if (plcDateTimeEnable != value)
                {
                    plcDateTimeEnable = value;
                }
            }
        }
        public int DispatchTimeOutDate { get; set; }


        private string userName;
        [XmlElement("UserName")]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName != value)
                {
                    userName = value;
                }
            }
        }

        private string password;
        [XmlElement("Password")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                }
            }
        }


        private int linkSleep;
        [XmlElement("LinkSleep")]
        public int LinkSleep
        {
            get
            {
                return linkSleep;
            }
            set
            {
                if (linkSleep != value)
                {
                    linkSleep = value;
                }
            }
        }

        private string pHTUnitsID;
        [XmlElement("PHTUnitsID")]
        public string PHTUnitsID
        {
            get
            {
                return pHTUnitsID;
            }
            set
            {
                if (pHTUnitsID != value)
                {
                    pHTUnitsID = value;
                }
            }
        }
        private string pHTUnits;
        [XmlElement("PHTUnits")]
        public string PHTUnits
        {
            get
            {
                return pHTUnits;
            }
            set
            {
                if (pHTUnits != value)
                {
                    pHTUnits = value;
                }
            }
        }
        private string pHTUnitName;
        [XmlElement("PHTUnitName")]
        public string PHTUnitName
        {
            get
            {
                return pHTUnitName;
            }
            set
            {
                if (pHTUnitName != value)
                {
                    pHTUnitName = value;
                }

            }
        }
    }
}