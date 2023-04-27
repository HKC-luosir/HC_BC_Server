using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class PLCRequestInfo
    {
        public PLCRequestInfo()
        {
            ProductID = "";
            ProductType = "";
            EQPID = "";
            EQPName = "";
        }
        public string ProductID { get; set; }
        public string ProductType { get; set; }
        public string EQPID { get; set; }
        public string EQPName { get; set; }
        public int RequestNo { get; set; }  //记录设备用PanelInformationRequest1/2来请求的
    }
}
