using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using log4net;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetClassInfoInsert : AbstractWebAPIMessageHandlercs
    {
        protected ILog BCLog = LogHelper.BCLog;
        public WebSocketMessage Execute(string userName, CasInfo Init)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getClassInfoInsert",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                var Eqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitID == Init.unitid));
                var Unit = Eqp.Units.FirstOrDefault(f => f.UnitID == Init.unitid);
                if (Unit != null)
                {
                    List<GlassInfo> glassList = new List<GlassInfo>();
                    PortInfo port = HostInfo.Current.PortList.FirstOrDefault(o => o.UnitID == Unit.UnitID && o.PortID == Init.portid);

                    string lotid = "";
                    string prodid = "";
                    string operid = "";
                    string ppid = "";
                    #region 需求8 3.Offline判定Box/CST下账 liuyusen 20221115
                    bool needcheck = true;
                    string[] containEQP = new string[] { "DOM", "OLB" };
                    if ((containEQP.Contains(Unit.EQPID.Substring(2, 3)) && port.PortType == "PU") || Eqp.EQPID.Contains("PRW") || Eqp.EQPID.Contains("BRW"))
                    {
                        needcheck = false;//Online 在In口已经做过check
                    }
                    #endregion
                    if (Init.allRows != null)
                    {
                        foreach (var item in Init.allRows)
                        {
                            lotid = item.LotID;
                            prodid = item.ProductID;
                            operid = item.OperationID;
                            ppid = item.ProductRecipe;

                            if (Init.type == "OFFLINE")
                            {
                                item.FunctionName = "CreateFromOPI";
                                item.CurrentUnit = Unit.UnitID;
                                item.CurrentSUnit = Unit.UnitID + "-" + port.PortID;
                                //Port信息
                                item.AbnormalCodes = item.AbnormalCodes.TrimEnd(';');
                                //根据portid获取modelposition  robot put时用于寻找target position
                                var robotmodel = Unit.RobotModelList == null ? null : Unit.RobotModelList.FirstOrDefault(c => c.PortID == Init.portid);
                                item.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                item.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                #region 需求8 3.Offline判定Box/CST下账 liuyusen 20221115
                                if (!needcheck)
                                {
                                    //BOX
                                }
                                else
                                { //jobslotexist每层是从左至右排序， 这里的item.Position
                                    var row = item.Position / 16;
                                    var col = item.Position % 16;

                                    var position = 0;
                                    if (col == 0)//没跨行
                                    {
                                        position = (row * 16) - 15;
                                    }
                                    else
                                    {
                                        position = (row * 16) + (16 - col + 1);
                                    }

                                    var slotpositionpor = position / 120;
                                    var slotposition = position % 120;
                                    var addpor = (slotpositionpor + (slotposition == 0 ? 0 : 1)) * 1000;//前后片
                                    var slotseqnum = addpor + (slotposition == 0 ? 120 : slotposition);

                                    item.SlotSequenceNo = slotseqnum;
                                    int slotPosition = slotseqnum / 1000;//前后片
                                    int slotNo = slotseqnum % 1000;
                                    item.SlotPosition = slotPosition;
                                    item.Position = slotNo;
                                }
                                #endregion
                                port.GlassInfos.Add(item);
                                glassList.Add(item);
                                //更新数据库
                                var ret = dbService.InsertGlassInfo(item);
                            }
                            else if (Init.type == "LOCAL")
                            {
                                if (port.GlassInfos.Any(c => c.GlassID == item.GlassID))
                                {
                                    glassList.Add(port.GlassInfos.FirstOrDefault(c => c.GlassID == item.GlassID));
                                }
                            }
                        }
                    }

                    //Cst信息
                    var cstinfo = port.CassetteInfo;
                    cstinfo.CassetteStatus = EnumCarrierStatus.WaitingforStartCommand;
                    cstinfo.MachineRecipeName = ppid;
                    cstinfo.LotName = lotid;
                    cstinfo.ProductSpecName = prodid;
                    cstinfo.ProcessOperationName = operid;
                    //更新数据库
                    var ret1 = dbService.UpdateCassette(cstinfo);

                    Init.glassCount = glassList.Count.ToString();


                    //下资料
                    var txid = HostInfo.Current.GetTransactionID();
                    portService.ExcuteDownloadJobFlow(Unit, cstinfo.UNITRECIPELIST, port, glassList, cstinfo.JobExistenceSlot, txid, needcheck);
                }

                WebSocketMessageStr.body = null;

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

            return WebSocketMessageStr;
        }
    }
}
