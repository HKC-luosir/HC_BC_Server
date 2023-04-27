using System;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;
using System.Threading;
using Glorysoft.BC.Entity.SECSEntity;

namespace Glorysoft.BC.SECS.Service
{
    public class SECSCommandService : AbstractSECSHandler, ISECSCommandService
    {
        /// <summary>
        /// 建立通讯连接
        /// </summary>
        public bool S1F1Command(string eqpName, string eqptype)
        {
            string functionname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            try
            {
                var context = CommonContexts.GetSECSContextByName(eqpName);
                if (context == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "context=NULL"}");
                    return false;
                }
                int s = 1;
                int f = 1;
                var trans = context.GetTransaction(s, f, 0, $"S{s}F{f}_H");
                if (trans == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "trans=NULL"}");
                    return false;
                }
                if (!(trans.OrignalMessage is SECSTransaction Data))
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "Data=NULL"}");
                    return false;
                }
                var msg = Data.Primary;
                if (msg == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "msg=NULL"}");
                    return false;
                }
                return context.SendMessage(trans);
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{eqpName}, MessageName: {functionname}", e);
                return false;
            }
        }

        /// <summary>
        /// 查询SVID
        /// </summary>
        public SelectSVIDResponse S1F3Command_Sync(SelectSVIDRequest data)
        {
            string functionname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var are = new AutoResetEvent(false);//线程终止信号
            SelectSVIDResponse reply = new SelectSVIDResponse();

            var eqpName = data.eqpid;
            var eqptype = data.eqptype;
            var parameters = data.SVIDList;
            try
            {
                var context = CommonContexts.GetSECSContextByName(eqpName);
                if (context == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "context=NULL"}");
                    return new SelectSVIDResponse() { ack = 63 };
                }
                int s = 1;
                int f = 3;
                var trans = context.GetTransaction(s, f, 0, $"S{s}F{f}_H");
                if (trans == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "trans=NULL"}");
                    return new SelectSVIDResponse() { ack = 63 };
                }
                if (!(trans.OrignalMessage is SECSTransaction Data))
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "Data=NULL"}");
                    return new SelectSVIDResponse() { ack = 63 };
                }
                var msg = Data.Primary;
                if (msg == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "msg=NULL"}");
                    return new SelectSVIDResponse() { ack = 63 };
                }
                Data.Tag = are;
                foreach (var pam in parameters)
                {
                    {
                        var secs = new SECSItem();
                        switch (eqptype) //通过设备类型来分别
                        {
                            case "TYPE2":
                                secs = new SECSItem(eSECS_FORMAT.ASCII)
                                {
                                    Value = pam.parameter_id
                                };
                                break;
                            default:
                                secs = new SECSItem(eSECS_FORMAT.U4)
                                {
                                    Value = pam.parameter_id
                                };
                                break;
                        }
                        msg.Root.Add(secs);
                    }
                }
                context.SendMessage(trans);
                if (are.WaitOne(45000, false))
                {
                    //有回复
                    var reply_msg = Data.Secondary;
                    for (int i = 1; i <= reply_msg.Root.ItemCount; i++)
                    {
                        parameters[i - 1].value = reply_msg.Root.Item(i).Value;
                    }
                    reply.ValueList = parameters;
                    are.Dispose();
                    are = null;
                    return reply;
                }
                //回复超时
                SECSLogger.Info($"{eqpName}, MessageName: {functionname} SECSTIMEOUT");
                are.Dispose();
                are = null;
                return new SelectSVIDResponse() { ack = 63, msg = eqpName + "EQ Not Reply S1F3" };
            }
            catch (Exception e)
            {
                are = null;
                are = null;
                SECSLogger.Error($"{eqpName}, MessageName: {functionname}", e);
                return new SelectSVIDResponse() { ack = 63 };
            }
        }

        /// <summary>
        /// 建立通讯连接
        /// </summary>
        public EstablishCommResponse S1F13Command_Sync(EstablishCommRequest data)
        {
            string functionname = System.Reflection.MethodBase.GetCurrentMethod().Name;
            var are = new AutoResetEvent(false);//线程终止信号
            EstablishCommResponse reply = new EstablishCommResponse();

            var eqpName = data.eqpid;
            var eqptype = data.eqptype;
            try
            {
                var context = CommonContexts.GetSECSContextByName(eqpName);
                if (context == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "context=NULL"}");
                    return new EstablishCommResponse() { ack = 63 };
                }
                int s = 1;
                int f = 13;
                var trans = context.GetTransaction(s, f, 0, $"S{s}F{f}_H");
                if (trans == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "trans=NULL"}");
                    return new EstablishCommResponse() { ack = 63 };
                }
                if (!(trans.OrignalMessage is SECSTransaction Data))
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "Data=NULL"}");
                    return new EstablishCommResponse() { ack = 63 };
                }
                var msg = Data.Primary;
                if (msg == null)
                {
                    SECSLogger.Error($"{eqpName}, MessageName: {functionname + "msg=NULL"}");
                    return new EstablishCommResponse() { ack = 63 };
                }
                Data.Tag = are;
                context.SendMessage(trans);
                if (are.WaitOne(45000, false))
                {
                    //有回复
                    var reply_msg = Data.Secondary;
                    var ack = reply_msg.Root.Item(1).Value.ToBi();
                    reply.ack = ack[0];
                    are.Dispose();
                    are = null;
                    return reply;
                }
                //回复超时
                SECSLogger.Info($"{eqpName}, MessageName: {functionname} SECSTIMEOUT");
                are.Dispose();
                are = null;
                return new EstablishCommResponse() { ack = 63, msg = eqpName + "EQ Not Reply S1F13" };
            }
            catch (Exception e)
            {
                are.Dispose();
                are = null;
                SECSLogger.Error($"{eqpName}, MessageName: {functionname}", e);
                return new EstablishCommResponse() { ack = 63 };
            }
        }
    }
}
