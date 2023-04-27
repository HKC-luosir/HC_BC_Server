using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Glorysoft.BC.Entity
{
    public class Consts : NotifyPropertyChanged
    {
        //public const string CTSystemConfig = "SystemConfig";
        public static Dictionary<string, string> ViewModelViewMap = new Dictionary<string, string>
        {
            {"TestViewModel","Test"}
        };
        public const string ViewNameSpace = "Glorysoft.BC.Server.View";



        #region matti
        //public static Dictionary<int, string> dicUnitCommandType = new Dictionary<int, string>
        //{
        //    {1,"PLC" },
        //    {2,"EIP" }
        //};
        //1-LOADREADY 2-INUSE 3-UNLOADREADY 4-EMPTY 5-BLOCKED
        //public static Dictionary<int, string> dicPortStatus = new Dictionary<int, string>
        //{
        //    {1,"LOADREADY" },
        //    {2,"INUSE" },
        //    {3,"UNLOADREADY" },
        //    {4,"EMPTY" },
        //    {5,"BLOCKED" }
        //};
        //2-EX实验 3-JC换线 5-UD宕机 6-CM换料 7-ID等待 8-RUN跑货
        //public static Dictionary<int, string> dicEQPStatus = new Dictionary<int, string>
        //{
        //    {2,"EX" },
        //    {3,"JC" },
        //    {5,"DOWN" },
        //    {6,"CM" },
        //    {7,"IDLE" },
        //    {8,"RUN" }
        //};
        //1-Loading Port 2-Unloading Port 3-Both Port 4-Buffer Both Port 5-Buffer Loader Port 6-Buffer Unloader Port 7-Partial Port
        //public static Dictionary<int, string> dicPortType = new Dictionary<int, string>
        //{
        //    {1,"PL" },
        //    {2,"PU" },
        //    {3,"PB" },
        //    {4,"BB" },
        //    {5,"BL" },
        //    {6,"BU" },
        //    {7,"PP" }
        //};
        //public static Dictionary<int, string> dicPortTypeAutoChangeMode = new Dictionary<int, string>
        //{
        //    {1,"Enable Mode" },
        //    {2,"Disable Mode" }
        //};
        //PortMode
        //X XX XX
        //1-Substrate Type   2~3-Job Type 4~5-Judge & Port Use Type
        //public static Dictionary<int, string> dicPortMode_Substrate_Type = new Dictionary<int, string>
        //{
        //    {1,"Glass" },
        //    {2,"Panel" },
        //    {3,"BLU" }
        //};
        //public static Dictionary<int, string> dicPortMode_Job_Type = new Dictionary<int, string>
        //{
        //    {0,"Not Specific" },
        //    {1,"MP Job" },
        //    {2,"Engineer Test" }
        //};
        //public static Dictionary<int, string> dicPortMode_Judge_Port_Use_Type = new Dictionary<int, string>
        //{
        //    {0,"Not Specific" },
        //    {1,"OK" },
        //    {2,"NG" },
        //    {3,"Rework" }
        //};
        //public static Dictionary<int, string> dicPortEnableMode = new Dictionary<int, string>
        //{
        //    {1,"Enabled" },
        //    {2,"Disabled" }
        //};
        //public static Dictionary<int, string> dicPortTransferMode = new Dictionary<int, string>
        //{
        //    {1,"MGV Mode" },
        //    {2,"AGV Mode" },
        //    {3,"Stocker Inline Mode" }
        //};
        //public static Dictionary<int, string> dicPortPauseMode = new Dictionary<int, string>
        //{
        //    {1,"Paused" },
        //    {2,"Normal" }
        //};
        //public static Dictionary<int, string> dicPortCassetteType = new Dictionary<int, string>
        //{
        //    {1,"A_CST" },
        //    {2,"S_CST" },
        //    {3,"A_CST" },
        //    {4,"S_CST" },
        //    {5,"SS_CST" },
        //    {6,"B1_CST" },
        //    {7,"B2_CST" },
        //    {8,"B3_CST" },
        //    {9,"B4_CST" },
        //    {10,"G2_CST" },
        //    {11,"A2_BOX" },
        //    {12,"B_BOX" },
        //    {13,"D_CST" },
        //    {14,"C_CST" },
        //    {15,"A3_BOX_ACP" },
        //    {16,"B1_BOX_ACP" },
        //    {17,"U_BOX" },
        //    {18,"M_BOX" },
        //    {19,"Q_BOX" },
        //    {20,"Q1_BOX_ACP" }
        //};
        //public Dictionary<int, string> dicCSTStatus = new Dictionary<int, string>
        //{
        //    {1,"No Cassette Exist" },
        //    {2,"Waiting for Cassette Data" },
        //    {3,"Waiting For Start Commamd" },
        //    {4,"Waiting for Processing" },
        //    {5,"In Processing" },
        //    {6,"Process Completed" },
        //    {7,"Process Paused" }
        //};
        #endregion

        public enum PackMode
        {
            CSTToCST = 1,
            CSTToBOX = 2,
            BOXToBOX = 3,
            BOXToCST = 4
        }
        public enum UnitName
        {
            Index,
            LaserMarking,
            Packing
        }
        public enum SUnitName
        {
            Index,
            LaserMarking1,
            LaserMarking2,
            //LaserMarking3,
            //LaserMarking4,
            Packing
        }

        public class EQStatusColor
        {
        //     Run = 2,
        //Idle = 1,
        //Down = 3,
        //PM = 4
            public static Hashtable EQColor = new Hashtable
                                              {
                                                   {"2", Brushes.Green},
                                                  {"1", Brushes.Yellow},                                                 
                                                  {"3", Brushes.Red},
                                                  {"4", Brushes.Purple}
                                                  //,
                                                  //{5, Brushes.CadetBlue},
                                                  //{6, Brushes.Orange}
                                              };
        }

        public enum PLCCassetteControl
        {
            //1 : Cassette Process Start;
            //2 : Cassette Process Start By Count;
            //3 : Cassette Process Pause;
            //4 : Cassette Process Resume;
            //5 : Cassette Process Abort;
            //6 : Cassette Process Cancel ;
            //9 : Cassette Process End;
            //11: Cassette Map Download;
            //12 : Reload</param>
            Start = 1,
            StartByCount = 2,
            Pause =3,
            Resume=4,
            Abort=5,
            Cancel=6,
            End = 9,
            MapDownload = 11,
            Reload = 12
        }
        public enum PLCPortControlCommand
        {
            //1: Cassette Map Download;2 : Chuck;3 : Unchuck;4 : Rechuck
            CassetteMapDownload = 1,
            Chuck = 2,
            Unchuck = 3,
            Rechuck = 4
        }
        public enum HSMSCassetteControl
        {
            //1	START 
            //2	CANCEL                                  
            //3	ABORT                                   
            //4	PAUSE                                   
            //5	RESUME                                  
            //6	OPERATOR CALL                           
            //7	Mask Cassette Cancel                    
            //8	Unpacker  BarCodeData (crate-id) result 
            //9	Recycle
            Start = 1,
            Cancel = 2,
            Abort = 3,
            Pause = 4,
            Resume = 5,
            MaskCSTCancel = 7,
            Unpacker = 8,
            Recycle = 9
        }
        public enum PortOperationMode
        {
            AutoMode=1,
            SemiAutoMode = 2,
            ManualMode = 3,
        }
        public enum CommandType
        {
            PLC = 1,
            EIP = 2
        }
        public enum LinkType
        {
            DownstreamLinkSignal = 1,
            UpstreamLinkSignal = 2          
        }
        public const string ItemTypeA = "A";
        public const string ItemTypeI = "I";
        public const string RVMappingConfig = "RVMappingConfig";
        public const string SystemConfig = "SystemConfig";
        public const string SFCDConfig = "SFCDConfig";
        public const string MESRule = "MESRule";
        public const string SECSRule = "SECSRule";
        public const string EQRule = "EQRule";
        public const string LinkSignal = "LinkSignal";
        public enum IsConnect
        {
            Down ,
            Alive
        }


       
        //public static Dictionary<string, string> AntiMap = new Dictionary<string, string>
        //{

        //   {"CT1","VI1"},
        //   {"VI1","CT1"},
        //};
    }
}
