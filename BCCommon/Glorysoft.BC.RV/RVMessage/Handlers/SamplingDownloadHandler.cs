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
    public class SamplingDownloadHandler : AbstractMESMessageHandler
    {
        public SamplingDownloadHandler(ITibcoContext context)
            : base(context)
        {

        }
        public override void Execute(RVData req)
        {
            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            try
            {
                RVSamplingDownload samplingDownload = XmlSerialization.DeserializeBody<RVSamplingDownload>(req.StringXml);
                RVHeader requestHeader = new RVHeader();
                XmlSerialization.DeserializeHeaderAndReturn(req.StringXml, "Request", out requestHeader);
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == samplingDownload.EQUIPMENTID);
                if (oEQP != null)
                {
                    logicService.MESSamplingDownload(samplingDownload, req.Message, requestHeader.TRANSACTIONID);
                }
                else
                {
                    LogHelper.EIPLog.ErrorFormat("+++ SamplingDownloadHandler:{0} Cannot Find EQPInfo +++", samplingDownload.EQUIPMENTID);
                    RVSamplingDownloadReply samplingDownloadReply = new RVSamplingDownloadReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = samplingDownloadReply.MessageName;
                    replyHeader.TRANSACTIONID = requestHeader.TRANSACTIONID;
                    replyHeader.RESULT = "FAIL";
                    replyHeader.RESULTMESSAGE = "BC can not find equipmentID:" + samplingDownload.EQUIPMENTID;
                    mesService.SendToMESSamplingDownloadReply(samplingDownload.EQUIPMENTID, samplingDownloadReply, replyHeader, req.Message);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(string.Format("[MES to BC][SamplingDownloadHandler] [Thread:{0}] ex:{1}", CurrentThread, ex));
            }
        }
    }
}
