using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.RV.Common;
using Glorysoft.BC.RV.RVService;
using System;
using System.Linq;

namespace Glorysoft.BC.RV.RVMessage.Handlers
{
    public class SPCRateDownloadHandler : AbstractMESMessageHandler
    {
        public SPCRateDownloadHandler(ITibcoContext context)
            : base(context)
        {

        }
        public override void Execute(RVData req)
        {
            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            try
            {
                RVSPCRateDownload sPCRateDownload = XmlSerialization.DeserializeBody<RVSPCRateDownload>(req.StringXml);
                RVHeader requestHeader = new RVHeader();
                XmlSerialization.DeserializeHeaderAndReturn(req.StringXml, "Request", out requestHeader);
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == sPCRateDownload.EQUIPMENTID);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ SPCRateDownloadHandler:{0} Cannot Find EQPInfo +++", sPCRateDownload.EQUIPMENTID);
                    RVSPCRateDownloadReply sPCRateDownloadReply = new RVSPCRateDownloadReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = sPCRateDownloadReply.MessageName;
                    replyHeader.TRANSACTIONID = requestHeader.TRANSACTIONID;
                    replyHeader.RESULT = "FAIL";
                    replyHeader.RESULTMESSAGE = "BC can not find equipmentID:" + sPCRateDownload.EQUIPMENTID;
                    mesService.SendToMESSPCRateDownloadReply(sPCRateDownload.EQUIPMENTID, sPCRateDownloadReply, replyHeader, req.Message);
                    return;
                }
                var unit = oEQP.Units.FirstOrDefault(c => c.UnitID == sPCRateDownload.UNITID);
                if (unit == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ SPCRateDownloadHandler:{0} Cannot Find UnitInfo +++", sPCRateDownload.UNITID);
                    RVSPCRateDownloadReply sPCRateDownloadReply = new RVSPCRateDownloadReply();
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = sPCRateDownloadReply.MessageName;
                    replyHeader.TRANSACTIONID = requestHeader.TRANSACTIONID;
                    replyHeader.RESULT = "FAIL";
                    replyHeader.RESULTMESSAGE = "BC can not find UnitID:" + sPCRateDownload.UNITID;
                    mesService.SendToMESSPCRateDownloadReply(sPCRateDownload.EQUIPMENTID, sPCRateDownloadReply, replyHeader, req.Message);
                    return;
                }
                logicService.MESSPCRateDownload(sPCRateDownload, req.Message, requestHeader.TRANSACTIONID);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(string.Format("[MES to BC][SPCRateDownloadHandler] [Thread:{0}] ex:{1}", CurrentThread, ex));
            }
        }
    }
}
