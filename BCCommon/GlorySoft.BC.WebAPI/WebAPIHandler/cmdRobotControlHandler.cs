using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class cmdRobotControlHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, commandForm InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "CimmessageCommandResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body

                RobotCommand rc = new RobotCommand();
                if (!String.IsNullOrEmpty(InitData.cmd1st.rcmd))
                    rc.STRCMD1 = (RobotMotion)Enum.Parse(typeof(RobotMotion), InitData.cmd1st.rcmd);
                if (!String.IsNullOrEmpty(InitData.cmd1st.armNo))
                    rc.STArmNo1 = (RobotHand)Enum.Parse(typeof(RobotHand), InitData.cmd1st.armNo);
                if (!String.IsNullOrEmpty(InitData.cmd1st.getPosition))
                    rc.STGetPosition1 = Convert.ToInt32(InitData.cmd1st.getPosition);
                if (!String.IsNullOrEmpty(InitData.cmd1st.putPosition))
                    rc.STPutPosition1 = Convert.ToInt32(InitData.cmd1st.putPosition);
                if (!String.IsNullOrEmpty(InitData.cmd1st.getSlotNo))
                    rc.STGetSlotNo1 = Convert.ToInt32(InitData.cmd1st.getSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd1st.putSlotNo))
                    rc.STPutSlotNo1 = Convert.ToInt32(InitData.cmd1st.putSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd1st.subcommand))
                    rc.STSubCommand1 = Convert.ToInt32(InitData.cmd1st.subcommand);
                if (!String.IsNullOrEmpty(InitData.cmd1st.getSlotPosition))
                    rc.STGetSlotPostion1 = Convert.ToInt32(InitData.cmd1st.getSlotPosition);
                if (!String.IsNullOrEmpty(InitData.cmd1st.putSlotPosition))
                    rc.STPutSlotPostion1 = Convert.ToInt32(InitData.cmd1st.putSlotPosition);

                if (!String.IsNullOrEmpty(InitData.cmd2st.rcmd))
                    rc.NDRCMD2 = (RobotMotion)Enum.Parse(typeof(RobotMotion), InitData.cmd2st.rcmd);
                if (!String.IsNullOrEmpty(InitData.cmd2st.armNo))
                    rc.NDArmNo2 = (RobotHand)Enum.Parse(typeof(RobotHand), InitData.cmd2st.armNo);
                if (!String.IsNullOrEmpty(InitData.cmd2st.getPosition))
                    rc.NDGetPosition2 = Convert.ToInt32(InitData.cmd2st.getPosition);
                if (!String.IsNullOrEmpty(InitData.cmd2st.putPosition))
                    rc.NDPutPosition2 = Convert.ToInt32(InitData.cmd2st.putPosition);
                if (!String.IsNullOrEmpty(InitData.cmd2st.getSlotNo))
                    rc.NDGetSlotNo2 = Convert.ToInt32(InitData.cmd2st.getSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd2st.putSlotNo))
                    rc.NDPutSlotNo2 = Convert.ToInt32(InitData.cmd2st.putSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd2st.subcommand))
                    rc.NDSubCommand2 = Convert.ToInt32(InitData.cmd2st.subcommand);
                if (!String.IsNullOrEmpty(InitData.cmd2st.getSlotPosition))
                    rc.NDGetSlotPostion2 = Convert.ToInt32(InitData.cmd2st.getSlotPosition);
                if (!String.IsNullOrEmpty(InitData.cmd2st.putSlotPosition))
                    rc.NDPutSlotPostion2 = Convert.ToInt32(InitData.cmd2st.putSlotPosition);

                if (!String.IsNullOrEmpty(InitData.cmd3st.rcmd))
                    rc.RDRCMD3 = (RobotMotion)Enum.Parse(typeof(RobotMotion), InitData.cmd3st.rcmd);
                if (!String.IsNullOrEmpty(InitData.cmd3st.armNo))
                    rc.RDArmNo3 = (RobotHand)Enum.Parse(typeof(RobotHand), InitData.cmd3st.armNo);
                if (!String.IsNullOrEmpty(InitData.cmd3st.getPosition))
                    rc.RDGetPosition3 = Convert.ToInt32(InitData.cmd3st.getPosition);
                if (!String.IsNullOrEmpty(InitData.cmd3st.putPosition))
                    rc.RDPutPosition3 = Convert.ToInt32(InitData.cmd3st.putPosition);
                if (!String.IsNullOrEmpty(InitData.cmd3st.getSlotNo))
                    rc.RDGetSlotNo3 = Convert.ToInt32(InitData.cmd3st.getSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd3st.putSlotNo))
                    rc.RDPutSlotNo3 = Convert.ToInt32(InitData.cmd3st.putSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd3st.subcommand))
                    rc.RDSubCommand3 = Convert.ToInt32(InitData.cmd3st.subcommand);
                if (!String.IsNullOrEmpty(InitData.cmd3st.getSlotPosition))
                    rc.RDGetSlotPostion3 = Convert.ToInt32(InitData.cmd3st.getSlotPosition);
                if (!String.IsNullOrEmpty(InitData.cmd3st.putSlotPosition))
                    rc.RDPutSlotPostion3 = Convert.ToInt32(InitData.cmd3st.putSlotPosition);

                if (!String.IsNullOrEmpty(InitData.cmd4st.rcmd))
                    rc.THRCMD4 = (RobotMotion)Enum.Parse(typeof(RobotMotion), InitData.cmd4st.rcmd);
                if (!String.IsNullOrEmpty(InitData.cmd4st.armNo))
                    rc.THArmNo4 = (RobotHand)Enum.Parse(typeof(RobotHand), InitData.cmd4st.armNo);
                if (!String.IsNullOrEmpty(InitData.cmd4st.getPosition))
                    rc.THGetPosition4 = Convert.ToInt32(InitData.cmd4st.getPosition);
                if (!String.IsNullOrEmpty(InitData.cmd4st.putPosition))
                    rc.THPutPosition4 = Convert.ToInt32(InitData.cmd4st.putPosition);
                if (!String.IsNullOrEmpty(InitData.cmd4st.getSlotNo))
                    rc.THGetSlotNo4 = Convert.ToInt32(InitData.cmd4st.getSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd4st.putSlotNo))
                    rc.THPutSlotNo4 = Convert.ToInt32(InitData.cmd4st.putSlotNo);
                if (!String.IsNullOrEmpty(InitData.cmd4st.subcommand))
                    rc.THSubCommand4 = Convert.ToInt32(InitData.cmd4st.subcommand);
                if (!String.IsNullOrEmpty(InitData.cmd4st.getSlotPosition))
                    rc.THGetSlotPostion4 = Convert.ToInt32(InitData.cmd4st.getSlotPosition);
                if (!String.IsNullOrEmpty(InitData.cmd4st.putSlotPosition))
                    rc.THPutSlotPostion4 = Convert.ToInt32(InitData.cmd4st.putSlotPosition);

                if (HostInfo.Current.AllEQPInfo.Any(c => c.Units.Any(d => d.UnitID == InitData.unitid)))
                {
                    var eqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitID == InitData.unitid));
                    var unit = eqp.Units.FirstOrDefault(d => d.UnitID == InitData.unitid);
                    var sequenceno = HostInfo.Current.SequenceNo;
                    rc.SequenceNo = sequenceno;
                    eqpService.SendRobotControlCommand(unit.UnitName, "", rc);
                }

                WebSocketMessageStr.body = null;
                #endregion

                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };

            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Operation failed !",
                    returnMessageCH = "操作失败！"
                };
            }
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了RobotControlCommand下发操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion

            return WebSocketMessageStr;
        }
    }
}
