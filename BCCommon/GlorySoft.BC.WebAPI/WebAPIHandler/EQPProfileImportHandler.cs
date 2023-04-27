using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class EQPProfileImportHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, List<EQPProfileImportObject> Data)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "EQPProfileImport",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                Hashtable ht = new Hashtable();

                if (Data != null && Data.Count > 0)
                {
                    var FileName = Data.FirstOrDefault().FileName;
                    var EQPID = FileName.Split('_')[0];
                    var tempstr = FileName.Substring(FileName.IndexOf("_V") + 1);
                    var tempstr1 = FileName.Substring(FileName.LastIndexOf("."));
                    var Version = tempstr.Replace(tempstr1, "");

                    //版本检查
                    ht = new Hashtable();
                    ht.Add("eqpid", EQPID);
                    ht.Add("profileversion", Version);
                    if (dbService.Viewcfg_eqpprofile(ht).Any())
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = "Operation fail ! same version exist!",
                            returnMessageCH = "操作失败！已存在同版本数据!"
                        };
                        return WebSocketMessageStr;
                    }

                    //检查是否是该线数据
                    var eqpinfo = HostInfo.AllEQPInfo.FirstOrDefault(c => c.EQPID == EQPID);
                    if (eqpinfo == null)
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = "Operation fail ! import eqp not equal bc line!",
                            returnMessageCH = "操作失败！导入的线别与BC线别不符!"
                        };
                        return WebSocketMessageStr;
                    }
                    //检查是否是Unit数据
                    var unitids = Data.Select(c => c.UnitID).Distinct().ToList();
                    foreach (var unitid in unitids)
                    {
                        var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitID == unitid);
                        if (unitinfo == null)
                        {
                            WebSocketMessageStr.result = new WebSocketResult()
                            {
                                returnCode = "1",
                                returnMessageEN = "Operation fail ! import unit " + unitid + " not equal bc unit!",
                                returnMessageCH = "操作失败！导入的设备" + unitid + "与BC设备不符!"
                            };
                            return WebSocketMessageStr;
                        }
                    }

                    cfg_eqpprofile profiledata = new cfg_eqpprofile();
                    profiledata.eqpid = EQPID;
                    profiledata.profilename = FileName;
                    profiledata.profileversion = Version;
                    profiledata.isenable = 0;
                    int profileid = dbService.Insertcfg_eqpprofile(profiledata);
                    if (profileid > 0)
                    {
                        int itemgroupid = 0;
                        int itemgrouporder = 1;
                        int itemorder = 1;
                        string itemType = "";
                        for (int i = 0; i < Data.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(Data[i].ItemGroupName))
                            {
                                itemType = Data[i].ItemGroupName.Substring(Data[i].ItemGroupName.IndexOf("DATA") + 4);

                                cfg_eqpprofile_itemgroup profilegroupdata = new cfg_eqpprofile_itemgroup();
                                profilegroupdata = new cfg_eqpprofile_itemgroup();
                                profilegroupdata.profileid = profileid;
                                profilegroupdata.grouptype = -1;
                                if (Data[i].ItemGroupName.ToUpper().Contains("RECIPE"))
                                {
                                    profilegroupdata.grouptype = 2;
                                }
                                if (Data[i].ItemGroupName.ToUpper().Contains("DVDATA") || Data[i].ItemGroupName.ToUpper().Contains("CVDATA"))
                                {
                                    profilegroupdata.grouptype = 3;
                                }
                                profilegroupdata.unitname = eqpinfo.Units.FirstOrDefault(c => c.UnitID == Data[i].UnitID).UnitName;
                                profilegroupdata.itemgroupname = Data[i].ItemGroupName;
                                profilegroupdata.itemgrouporder = itemgrouporder;
                                itemgroupid = dbService.Insertcfg_eqpprofile_itemgroup(profilegroupdata);
                                itemgrouporder++;
                                itemorder = 1;
                            }

                            if (!String.IsNullOrEmpty(Data[i].ItemName))
                            {
                                cfg_eqpprofile_item profileitemdata = new cfg_eqpprofile_item();
                                profileitemdata.itemgroupid = itemgroupid;
                                profileitemdata.itemname = Data[i].ItemName;
                                profileitemdata.itemoffset = "";// Data[i].Offset;
                                profileitemdata.itempoints = "";//Data[i].Points;

                                profileitemdata.itemtype = itemType;
                                switch (itemType)
                                {
                                    case "BIT":
                                        profileitemdata.itemtype = "BIT";
                                        break;
                                    case "ASCII":
                                        profileitemdata.itemtype = "A";
                                        break;
                                    case "ASCII2":
                                        profileitemdata.itemtype = "AS";
                                        break;
                                    case "INT":
                                        profileitemdata.itemtype = "I";
                                        break;
                                    case "SINT":
                                        profileitemdata.itemtype = "SI";
                                        break;
                                    case "LONG":
                                        profileitemdata.itemtype = "L";
                                        break;
                                    case "SLONG":
                                        profileitemdata.itemtype = "SL";
                                        break;
                                    case "H1":
                                        profileitemdata.itemtype = "H1";
                                        break;
                                    case "FLOAT":
                                        profileitemdata.itemtype = "SF";
                                        break;
                                    case "BIN":
                                        profileitemdata.itemtype = "BIN";
                                        break;
                                    case "BCD":
                                        profileitemdata.itemtype = "BCD";
                                        break;
                                    default:
                                        break;
                                }

                                if (!String.IsNullOrEmpty(Data[i].MESItemName))
                                    profileitemdata.mesitemname = Data[i].MESItemName;

                                float op = 1;
                                if (!String.IsNullOrEmpty(Data[i].OperationProportion))
                                {
                                    float.TryParse(Data[i].OperationProportion, out op);
                                }
                                profileitemdata.operationproportion = op;

                                if (!String.IsNullOrEmpty(Data[i].DataIndex))
                                    profileitemdata.dataindex = Data[i].DataIndex;
                                profileitemdata.itemorder = itemorder;
                                dbService.Insertcfg_eqpprofile_item(profileitemdata);
                                itemorder++;
                            }
                        }
                    }
                }

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

            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了EQPProfileImport操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            return WebSocketMessageStr;
        }
    }
}
