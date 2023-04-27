using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Glorysoft.EIPDriver;
using System.Xml;
using System.Text;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class EQPProfileControlHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = type,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                Hashtable ht = new Hashtable();

                object ProfileName, ProfileVersion, ProfileID, EQPID, UnitName, ItemGroupID, ItemGroupName, ItemID, ItemName, MESItemName, GroupType, pageNum, pageSize;
                InitData.TryGetValue("pageNum", out pageNum);
                InitData.TryGetValue("pageSize", out pageSize);
                InitData.TryGetValue("eqpid", out EQPID);
                InitData.TryGetValue("profilename", out ProfileName);
                InitData.TryGetValue("profileversion", out ProfileVersion);
                InitData.TryGetValue("ProfileID", out ProfileID);
                InitData.TryGetValue("itemgroupid", out ItemGroupID);
                InitData.TryGetValue("itemgroupname", out ItemGroupName);
                InitData.TryGetValue("itemname", out ItemName);
                InitData.TryGetValue("mesitemname", out MESItemName);
                InitData.TryGetValue("itemid", out ItemID);
                InitData.TryGetValue("unitname", out UnitName);
                InitData.TryGetValue("grouptype", out GroupType);

                switch (type)
                {
                    case "EQPProfileControlAvail":
                        {
                            ht = new Hashtable();
                            ht.Add("id", Convert.ToInt32(ProfileID));
                            var data = dbService.Viewcfg_eqpprofile(ht).FirstOrDefault();
                            if (data != null)
                            {
                                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == data.eqpid);
                                if (eqpinfo == null)
                                {
                                    WebSocketMessageStr.result = new WebSocketResult()
                                    {
                                        returnCode = "1",
                                        returnMessageEN = "Operation failed ! EQPID not found !",
                                        returnMessageCH = "操作失败！EQPID不存在!"
                                    };
                                    goto Res;
                                }

                                //失效旧的
                                ht = new Hashtable();
                                ht.Add("eqpid", data.eqpid);
                                ht.Add("isenable", 1);
                                var data_old = dbService.Viewcfg_eqpprofile(ht).FirstOrDefault(c => c.id != data.id);
                                if (data_old != null)
                                {
                                    data_old.isenable = 0;
                                    dbService.Updatecfg_eqpprofile(data_old);
                                }

                                #region 修改本地缓存
                                ht = new Hashtable();
                                ht.Add("profileid", Convert.ToInt32(ProfileID));
                                var data_itemgroup = dbService.Viewcfg_eqpprofile_itemgroup(ht).ToList();
                                {
                                    if (data_itemgroup != null && data_itemgroup.Count > 0)
                                    {
                                        List<string> units = data_itemgroup.Select(c => c.unitname).Distinct().ToList();
                                        foreach (var unitname in units)//按设备
                                        {
                                            var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitName == unitname);
                                            if (unitinfo == null)
                                            {
                                                WebSocketMessageStr.result = new WebSocketResult()
                                                {
                                                    returnCode = "1",
                                                    returnMessageEN = String.Format("Operation failed ! UNITID:{0} not found !", unitname),
                                                    returnMessageCH = String.Format("操作失败！UNITID:{0}不存在!", unitname)
                                                };
                                                goto Res;
                                            }
                                            List<DVData> eqpDVDataList = new List<DVData>();
                                            List<SVData> eqpSVDataList = new List<SVData>();
                                            List<RecipeParameter> eqpRecipeParameterList = new List<RecipeParameter>();

                                            List<int> grouptypes = data_itemgroup.Where(c => c.unitname == unitname).Select(c => c.grouptype).Distinct().ToList();
                                            foreach (var grouptype in grouptypes)//按类型
                                            {
                                                ItemGroupXml itemgroupXmls = new ItemGroupXml();

                                                List<Block> newitemgroups = new List<Block>();
                                                var itemgroup = data_itemgroup.Where(c => c.unitname == unitname && c.grouptype == grouptype).OrderBy(c => c.itemgrouporder).ToList();
                                                for (int i = 0; i < itemgroup.Count; i++)
                                                {

                                                    Block newitemgroup = new Block();
                                                    newitemgroup.EQPName = unitname;
                                                    newitemgroup.Name = unitname + "_" + itemgroup[i].itemgroupname;

                                                    GItemGroupMapMrg itemgroupXml = new GItemGroupMapMrg();
                                                    itemgroupXml.Name = newitemgroup.Name;

                                                    ht = new Hashtable();
                                                    ht.Add("itemgroupid", itemgroup[i].id);
                                                    var data_item = dbService.Viewcfg_eqpprofile_item(ht).ToList();
                                                    if (data_item != null && data_item.Count > 0)
                                                    {
                                                        var item = data_item.OrderBy(c => c.itemorder).ToList();
                                                        for (int j = 0; j < item.Count; j++)
                                                        {
                                                            Item newitem = new Item();
                                                            newitem.Name = item[j].itemname;
                                                            newitem.EventID = item[j].itemname;
                                                            //newitem.Offset = item[j].itemoffset;
                                                            //newitem.Points = item[j].itempoints;
                                                            newitem.ParseItemType(item[j].itemtype);
                                                            newitemgroup.AddItem(newitem);

                                                            GItemMrg itemMrg = new GItemMrg();
                                                            itemMrg.Name = newitem.Name;
                                                            //itemMrg.Offset = newitem.Offset;
                                                            //itemMrg.Points = newitem.Points;
                                                            itemMrg.Type = item[j].itemtype;
                                                            itemgroupXml.ItemMrg.Add(itemMrg);

                                                            if (itemgroup[i].itemgroupname.ToUpper().Contains("DVDATA"))
                                                            {
                                                                if (!String.IsNullOrEmpty(item[j].dataindex))
                                                                {
                                                                    DVData eqpDVData = new DVData();
                                                                    eqpDVData.EQPID = data.eqpid;
                                                                    eqpDVData.UNITID = unitinfo.UnitID;
                                                                    eqpDVData.Index = Convert.ToInt32(item[j].dataindex);
                                                                    eqpDVData.DVName = item[j].mesitemname;
                                                                    eqpDVData.OperationProportion = item[j].operationproportion;
                                                                    if (itemgroup[i].itemgroupname.ToUpper().Contains("INT"))
                                                                    {
                                                                        eqpDVData.ItemName = "INT";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("ASCII"))
                                                                    {
                                                                        eqpDVData.ItemName = "ASCII";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("SI"))
                                                                    {
                                                                        eqpDVData.ItemName = "SI";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("FLOAT"))
                                                                    {
                                                                        eqpDVData.ItemName = "FLOAT";
                                                                    }
                                                                    eqpDVDataList.Add(eqpDVData);
                                                                }
                                                            }
                                                            else if (itemgroup[i].itemgroupname.ToUpper().Contains("CVDATA"))
                                                            {
                                                                if (!String.IsNullOrEmpty(item[j].dataindex))
                                                                {
                                                                    SVData eqpSVData = new SVData();
                                                                    eqpSVData.EQPID = data.eqpid;
                                                                    eqpSVData.UNITID = unitinfo.UnitID;
                                                                    eqpSVData.Index = Convert.ToInt32(item[j].dataindex);
                                                                    eqpSVData.SVName = item[j].mesitemname;
                                                                    eqpSVData.OperationProportion = item[j].operationproportion;
                                                                    if (itemgroup[i].itemgroupname.ToUpper().Contains("INT"))
                                                                    {
                                                                        eqpSVData.ItemName = "INT";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("ASCII"))
                                                                    {
                                                                        eqpSVData.ItemName = "ASCII";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("SI"))
                                                                    {
                                                                        eqpSVData.ItemName = "SI";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("FLOAT"))
                                                                    {
                                                                        eqpSVData.ItemName = "FLOAT";
                                                                    }
                                                                    eqpSVDataList.Add(eqpSVData);
                                                                }
                                                            }
                                                            else if (itemgroup[i].itemgroupname.ToUpper().Contains("RECIPE"))
                                                            {
                                                                if (!String.IsNullOrEmpty(item[j].dataindex))
                                                                {
                                                                    RecipeParameter eqpRecipeParameter = new RecipeParameter();
                                                                    eqpRecipeParameter.EQPID = data.eqpid;
                                                                    eqpRecipeParameter.UnitID = unitinfo.UnitID;
                                                                    eqpRecipeParameter.Index = Convert.ToInt32(item[j].dataindex);
                                                                    eqpRecipeParameter.RecipeParameterName = item[j].mesitemname;
                                                                    eqpRecipeParameter.OperationProportion = item[j].operationproportion;
                                                                    if (itemgroup[i].itemgroupname.ToUpper().Contains("INT"))
                                                                    {
                                                                        eqpRecipeParameter.ItemName = "INT";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("ASCII"))
                                                                    {
                                                                        eqpRecipeParameter.ItemName = "ASCII";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("SI"))
                                                                    {
                                                                        eqpRecipeParameter.ItemName = "SI";
                                                                    }
                                                                    else if (itemgroup[i].itemgroupname.ToUpper().Contains("FLOAT"))
                                                                    {
                                                                        eqpRecipeParameter.ItemName = "FLOAT";
                                                                    }
                                                                    eqpRecipeParameterList.Add(eqpRecipeParameter);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    newitemgroups.Add(newitemgroup);
                                                    itemgroupXmls.GroupMap.Add(itemgroupXml);
                                                }

                                                ////更新driver缓存
                                                ////connname在配置连接文件中默认用设备名称
                                                //PLCContexts.Current.UpdateItemGroupConfig(unitname, newitemgroups);

                                                ////更新xml文件
                                                //String xmlName = "";
                                                //String xmlTypeName = "";
                                                //switch (grouptype)
                                                //{
                                                //    case 1:
                                                //        xmlTypeName = "JobData";
                                                //        break;
                                                //    case 2:
                                                //        xmlTypeName = "Recipe";
                                                //        break;
                                                //    case 3:
                                                //        xmlTypeName = "ProcessData";
                                                //        break;
                                                //    default:
                                                //        break;
                                                //}
                                                //xmlName = unitname + "_EQPProfile_" + xmlTypeName + ".xml";

                                                //if (PLCContexts.Current.plcContexts.ContainsKey(unitname))
                                                //{
                                                //    var MapFiles = PLCContexts.Current.plcContexts[unitname].socketInfo.PlcMapFile.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                                //    if (MapFiles.Any(c => c.Contains(xmlName)))
                                                //    {
                                                //        var MapFileName = MapFiles.FirstOrDefault(c => c.Contains(xmlName));
                                                //        var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MapFileName);
                                                //        var MapFileBackupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MapFileName.Substring(0, MapFileName.LastIndexOf("\\")), DateTime.Now.ToString("yyyy-MM-dd"));
                                                //        var filebackupname = unitname + "_EQPProfile_" + xmlTypeName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                                                //        if (!Directory.Exists(MapFileBackupPath))
                                                //            Directory.CreateDirectory(MapFileBackupPath);
                                                //        //备份旧文件
                                                //        File.Copy(filename, Path.Combine(MapFileBackupPath, filebackupname));
                                                //        //替换新文件
                                                //        Serialize<ItemGroupXml>(itemgroupXmls, filename);
                                                //    }
                                                //}
                                            }

                                            //更新BC缓存
                                            if (eqpDVDataList.Count > 0)
                                            {
                                                ht = new Hashtable();
                                                ht.Add("EQPID", data.eqpid);
                                                ht.Add("UNITID", unitinfo.UnitID);
                                                dbService.DeleteDVData(ht);

                                                foreach (var eqpDVData in eqpDVDataList)
                                                {
                                                    dbService.InsertDVData(eqpDVData);
                                                }

                                                if (HostInfo.Current.DVDataList.ContainsKey(unitinfo.UnitID))
                                                {
                                                    HostInfo.Current.DVDataList[unitinfo.UnitID] = eqpDVDataList;
                                                }
                                                else
                                                {
                                                    HostInfo.Current.DVDataList.TryAdd(unitinfo.UnitID, eqpDVDataList);
                                                }
                                            }
                                            if (eqpSVDataList.Count > 0)
                                            {
                                                ht = new Hashtable();
                                                ht.Add("EQPID", data.eqpid);
                                                ht.Add("UNITID", unitinfo.UnitID);
                                                dbService.DeleteSVData(ht);

                                                foreach (var eqpSVData in eqpSVDataList)
                                                {
                                                    dbService.InsertSVData(eqpSVData);
                                                }

                                                if (HostInfo.Current.SVDataList.ContainsKey(unitinfo.UnitID))
                                                {
                                                    HostInfo.Current.SVDataList[unitinfo.UnitID] = eqpSVDataList;
                                                }
                                                else
                                                {
                                                    HostInfo.Current.SVDataList.TryAdd(unitinfo.UnitID, eqpSVDataList);
                                                }
                                            }
                                            if (eqpRecipeParameterList.Count > 0)
                                            {
                                                RecipeParameter delrcp = new RecipeParameter();
                                                delrcp.EQPID = data.eqpid;
                                                delrcp.UnitID = unitinfo.UnitID;
                                                dbService.DeleteRecipeParameter(delrcp);

                                                foreach (var eqpRecipeParameter in eqpRecipeParameterList)
                                                {
                                                    dbService.InsertRecipeParameter(eqpRecipeParameter);
                                                }

                                                if (HostInfo.Current.RecipeParameterList.ContainsKey(unitinfo.UnitID))
                                                {
                                                    HostInfo.Current.RecipeParameterList[unitinfo.UnitID] = eqpRecipeParameterList;
                                                }
                                                else
                                                {
                                                    HostInfo.Current.RecipeParameterList.TryAdd(unitinfo.UnitID, eqpRecipeParameterList);
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                //生效新的
                                data.isenable = 1;
                                dbService.Updatecfg_eqpprofile(data);
                            }
                        }
                        break;
                    case "SelectEQPProfileList":
                        {
                            ht = new Hashtable();
                            ht.Add("eqpid", EQPID.ToString());
                            ht.Add("profilename", ProfileName.ToString());
                            ht.Add("profileversion", ProfileVersion.ToString());
                            IList<cfg_eqpprofile> list = dbService.Viewcfg_eqpprofile(ht);
                            var newdata = list.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                            InitData.Add("total", list.Count);
                            InitData.Add("rows", newdata);
                            WebSocketMessageStr.body = InitData;
                        }
                        break;
                    case "SelectEQPProfileItemGroupList":
                        {
                            ht = new Hashtable();
                            ht.Add("profileid", Convert.ToInt32(ProfileID));
                            if (!String.IsNullOrEmpty(GroupType.ToString()))
                                ht.Add("grouptype", Convert.ToInt32(GroupType));
                            ht.Add("unitname", UnitName.ToString());
                            ht.Add("itemgroupname", ItemGroupName.ToString());
                            IList<cfg_eqpprofile_itemgroup> list = dbService.Viewcfg_eqpprofile_itemgroup(ht);
                            var newdata = list.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                            InitData.Add("total", list.Count);
                            InitData.Add("rows", newdata);
                            WebSocketMessageStr.body = InitData;
                        }
                        break;
                    case "SelectEQPProfileItemList":
                        {
                            ht = new Hashtable();
                            ht.Add("itemgroupid", Convert.ToInt32(ItemGroupID));
                            ht.Add("itemname", ItemName.ToString());
                            ht.Add("mesitemname", MESItemName.ToString());
                            IList<cfg_eqpprofile_item> list = dbService.Viewcfg_eqpprofile_item(ht);
                            var newdata = list.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                            InitData.Add("total", list.Count);
                            InitData.Add("rows", newdata);
                            WebSocketMessageStr.body = InitData;
                        }
                        break;
                    case "EQPProfileDelete":
                        {
                            ht = new Hashtable();
                            ht.Add("id", Convert.ToInt32(ProfileID));
                            dbService.Deletecfg_eqpprofile(ht);

                            ht = new Hashtable();
                            ht.Add("profileid", Convert.ToInt32(ProfileID));
                            IList<cfg_eqpprofile_itemgroup> list = dbService.Viewcfg_eqpprofile_itemgroup(ht);
                            var itemgroupids = list.Select(c => c.id);
                            foreach (var itemgroupid in itemgroupids)
                            {
                                ht = new Hashtable();
                                ht.Add("id", Convert.ToInt32(itemgroupid));
                                dbService.Deletecfg_eqpprofile_itemgroup(ht);
                                ht = new Hashtable();
                                ht.Add("itemgroupid", Convert.ToInt32(itemgroupid));
                                dbService.Deletecfg_eqpprofile_item(ht);
                            }
                        }
                        break;
                    case "EQPProfileItemGroupDelete":
                        {
                            ht = new Hashtable();
                            ht.Add("id", Convert.ToInt32(ItemGroupID));
                            dbService.Deletecfg_eqpprofile_itemgroup(ht);
                            ht = new Hashtable();
                            ht.Add("itemgroupid", Convert.ToInt32(ItemGroupID));
                            dbService.Deletecfg_eqpprofile_item(ht);
                        }
                        break;
                    case "EQPProfileItemDelete":
                        {
                            ht = new Hashtable();
                            ht.Add("id", Convert.ToInt32(ItemID));
                            dbService.Deletecfg_eqpprofile_item(ht);
                        }
                        break;
                    default:
                        break;
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

            Res:
            #region OPI操作记录
            if (!type.Contains("Select"))
            {
                Hashtable opiHis = new Hashtable();
                opiHis.Add("userid", userName);
                opiHis.Add("operating", "进行了" + type + "操作！");
                opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
                opiHis.Add("clientip", clientip);
                dbService.Inserthis_opilog(opiHis);
            }
            #endregion
            return WebSocketMessageStr;
        }

        private bool Serialize<T>(T obj, string filePath)
        {
            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "http://www.w3.org/2001/XMLSchema-instance");
                ns.Add("", "http://www.w3.org/2001/XMLSchema");
                ns.Add(string.Empty, string.Empty);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmltw = new XmlTextWriter(filePath, Encoding.UTF8);
                xmltw.Formatting = Formatting.Indented;
                xs.Serialize(xmltw, obj, ns);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
