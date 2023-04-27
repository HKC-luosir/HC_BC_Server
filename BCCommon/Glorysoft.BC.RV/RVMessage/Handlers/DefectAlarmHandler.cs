using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.RV.Common;
using Glorysoft.BC.RV.RVService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.RV.RVMessage.Handlers
{
    public class DefectAlarmHandler : AbstractMESMessageHandler
    {
        public DefectAlarmHandler(ITibcoContext context)
            : base(context)
        {

        }
        public override void Execute(RVData req)
        {
            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            try
            {
                RVDefectAlarm samplingDownload = XmlSerialization.DeserializeBody<RVDefectAlarm>(req.StringXml);
                RVHeader requestHeader = new RVHeader();
                XmlSerialization.DeserializeHeaderAndReturn(req.StringXml, "Request", out requestHeader);
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == samplingDownload.EQUIPMENTID);
                if (oEQP != null)
                {
                    //叫停设备，TBD
                    //logicService.MESSamplingDownload(samplingDownload, req.Message, requestHeader.TRANSACTIONID);
                    RVDefectAlarmReply defectAlarmReply = new RVDefectAlarmReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.RESULT = "SUCCESS";
                    //replyHeader.RESULTMESSAGE = "BC can not find equipmentID:" + samplingDownload.EQUIPMENTID;
                    mesService.SendToMESDefectAlarmReply(samplingDownload.EQUIPMENTID, defectAlarmReply, replyHeader, req.Message);
                }
                else
                {
                    LogHelper.EIPLog.ErrorFormat("+++ SamplingDownloadHandler:{0} Cannot Find EQPInfo +++", samplingDownload.EQUIPMENTID);
                    RVDefectAlarmReply defectAlarmReply = new RVDefectAlarmReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.RESULT = "FAIL";
                    replyHeader.RESULTMESSAGE = "BC can not find equipmentID:" + samplingDownload.EQUIPMENTID;
                    mesService.SendToMESDefectAlarmReply(samplingDownload.EQUIPMENTID, defectAlarmReply, replyHeader, req.Message);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(string.Format("[MES to BC][SamplingDownloadHandler] [Thread:{0}] ex:{1}", CurrentThread, ex));
            }
        }
    }
}
