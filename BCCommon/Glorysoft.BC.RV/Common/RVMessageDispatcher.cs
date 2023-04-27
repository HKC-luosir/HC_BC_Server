using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.RV.RVMessage.Handlers;
using Glorysoft.BC.RV.RVService;
using System;
using System.Collections.Generic;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.Common
{
    public class RVMessageDispatcher : ITibcoDispather
    {
        private Dictionary<string, AbstractMESMessageHandler> mesHandlers;
        private Dictionary<string, AbstractFDCMessageHandler> fdcHandlers;
        private readonly HostInfo hostInfo = HostInfo.Current;
        private TibcoContext context;
        public RVMessageDispatcher(TibcoContext _context)
        {
            context = _context;
            mesHandlers = new Dictionary<string, AbstractMESMessageHandler>
            {
                {"MES.RECIPEPARAMREQUEST",new RecipeParamRequestHandler(context)},
                {"M2.SAMPLINGDOWNLOAD",new SamplingDownloadHandler(context)},
                {"SPCRATEDOWNLOAD",new SPCRateDownloadHandler(context)}
            };
        }
        public void Dispath(string rvName, object rvMessage, Message requestMessage)
        {
            try
            {
                var msgStr = rvMessage.ToString();
                RVHeader header = new RVHeader();
                XmlSerialization.DeserializeHeaderAndReturn(msgStr, "Request", out header);
                string log = XmlSerialization.ToXmlFormat(msgStr);
                var messageName = header.MESSAGENAME;
                var tranID = header.TRANSACTIONID;
                LogHelper.MESLog.Info($"[{tranID}] [Receive From {rvName}] [{messageName}]{System.Environment.NewLine}{log}");

                RVData mesMsg = new RVData();
                mesMsg.StringXml = rvMessage.ToString().Trim();
                mesMsg.Message = requestMessage;
                if (mesHandlers.ContainsKey(messageName))
                {
                    var handlerName = messageName;
                    mesHandlers[handlerName].Execute(mesMsg);
                }
                else
                {
                    LogHelper.MESLog.Error("MessageHandler不存在:" + messageName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("Receive From Tibco Error:" + ex.Message);
            }
        }
    }
}
