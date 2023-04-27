using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.EIP.Common
{
    [XmlRoot("configuration")]
    public class EIPConnections
    {
        [XmlElement("EIPConnection")]
        public List<EIPConnection> EIPConnectionList { get; set; }
    }
    [Serializable]
    [XmlRoot("EIPConnection")]
    public class EIPConnection
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("EIPType")]
        public string EIPType { get; set; }
        [XmlElement("DeviceType")]
        public string DeviceType { get; set; }
        [XmlElement("LogLevel")]
        public string LogLevel { get; set; }
        [XmlElement("LogPath")]
        public string LogPath { get; set; }
        [XmlElement("ConnectMode")]
        public string ConnectMode { get; set; }
        [XmlElement("Protocol")]
        public string Protocol { get; set; }
        [XmlElement("RemoteIPAddress")]
        public string RemoteIPAddress { get; set; }
        [XmlElement("RemoteIPPort")]
        public string RemoteIPPort { get; set; }
        [XmlElement("Timeout")]
        public string Timeout { get; set; }
        [XmlElement("ConnectInterval")]
        public string ConnectInterval { get; set; }
        [XmlElement("WzoneScanIntervalInMsec")]
        public string WzoneScanIntervalInMsec { get; set; }
        [XmlElement("BzoneScanIntervalInMsec")]
        public string BzoneScanIntervalInMsec { get; set; }
        [XmlElement("MapFile")]
        public string MapFile { get; set; }
        [XmlElement("WorkflowFile")]
        public string WorkflowFile { get; set; }
        #region 需求7 2.EIP Driver增加LogTitleName,变更日志文件夹名称 liuyusen 20221017
        [XmlElement("LogTitleName")]
        public string LogTitleName { get; set; }
        #endregion
    }
}
