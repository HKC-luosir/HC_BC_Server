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
    public class RecipeParamRequestHandler : AbstractMESMessageHandler
    {
        public RecipeParamRequestHandler(ITibcoContext context)
            : base(context)
        {

        }
        public override void Execute(RVData req)
        {
            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
            try
            {
                RVRecipeParameterRequest recipeParameterRequest = XmlSerialization.DeserializeBody<RVRecipeParameterRequest>(req.StringXml);
                RVHeader requestHeader = new RVHeader();
                XmlSerialization.DeserializeHeaderAndReturn(req.StringXml, "Request", out requestHeader);
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == recipeParameterRequest.EQUIPMENTID);
                if (oEQP != null)
                {
                    logicService.MESRecipeParamRequest(recipeParameterRequest, req.Message, requestHeader.TRANSACTIONID);
                }
                else
                {
                    LogHelper.EIPLog.ErrorFormat("+++ RecipeParamRequestHandler:{0} Cannot Find EQPInfo +++", recipeParameterRequest.EQUIPMENTID);
                    RVRecipeParameterRequestReply recipeParameterRequestReply = new RVRecipeParameterRequestReply();
                    recipeParameterRequestReply.EQUIPMENTID = recipeParameterRequest.EQUIPMENTID;
                    RVHeader replyHeader = new RVHeader();
                    replyHeader.MESSAGENAME = recipeParameterRequestReply.MessageName;
                    replyHeader.TRANSACTIONID = requestHeader.TRANSACTIONID;
                    replyHeader.RESULT = "FAIL";
                    replyHeader.RESULTMESSAGE = "BC can not find equipmentID:" + recipeParameterRequest.EQUIPMENTID;
                    mesService.SendToMESRecipeParameterRequestReply(recipeParameterRequest.EQUIPMENTID, recipeParameterRequestReply, replyHeader, req.Message);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(string.Format("[MES to BC][RecipeParamRequestHandler] [Thread:{0}] ex:{1}", CurrentThread, ex));
            }
        }
    }
}
