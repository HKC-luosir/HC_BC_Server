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
    public class GetHistoryTableInformationHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getHistoryTableInformation",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                object tableName, pageSize, pageNo, condition;
                InitHistory.TryGetValue("tableName", out tableName);
                InitHistory.TryGetValue("pageSize", out pageSize);
                InitHistory.TryGetValue("pageNo", out pageNo);
                InitHistory.TryGetValue("pageNo", out pageNo);
                InitHistory.TryGetValue("condition", out condition);
                Dictionary<string, object> cons = condition as Dictionary<string, object>;

                Hashtable hashtable = new Hashtable();
                foreach (var item in cons)
                {
                    if (item.Key.Contains(">"))
                    {
                        hashtable.Add("start" + item.Key.Substring(0, item.Key.Length - 1), item.Value);
                    }
                    else if (item.Key.Contains("<"))
                    {
                        hashtable.Add("end" + item.Key.Substring(0, item.Key.Length - 1), item.Value);
                    }
                    else
                    {
                        hashtable.Add(item.Key, item.Value);
                    }
                }
                switch (tableName.ToString())
                {
                    case "bc_group":
                        IList<BC_Group> UserGroupList = dbService.ViewUserGroupList(hashtable);
                        var NewUserGroupList = UserGroupList.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", UserGroupList.Count);
                        InitHistory.Add("rows", NewUserGroupList);
                        break;
                    case "bc_group_authority":
                        IList<BcGroupAuthority> bC_Groups = dbService.ViewBcGroupAuthority(hashtable);
                        var userListPage = bC_Groups.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bC_Groups.Count);
                        InitHistory.Add("rows", userListPage);
                        break;
                    case "bc_robot_configure":
                        IList<BcRobotConfigure> BcRobotConfigure = dbService.ViewBcRobotConfigure(hashtable);
                        var NewBcRobotConfigure = BcRobotConfigure.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", BcRobotConfigure.Count);
                        InitHistory.Add("rows", NewBcRobotConfigure);
                        break;
                    case "bc_robot_group_configure":
                        IList<BcRobotGroupCconfigure> BcRobotGroupCconfigure = dbService.ViewBcRobotGroupCconfigure(hashtable);
                        var NewBcRobotGroupCconfigure = BcRobotGroupCconfigure.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", BcRobotGroupCconfigure.Count);
                        InitHistory.Add("rows", NewBcRobotGroupCconfigure);
                        break;
                    case "bc_robot_linksignal_configure":
                        IList<bc_robot_linksignal_configure> bc_robot_linksignal_configure = dbService.Viewbc_robot_linksignal_configure(hashtable);
                        var Newbc_robot_linksignal_configure = bc_robot_linksignal_configure.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bc_robot_linksignal_configure.Count);
                        InitHistory.Add("rows", Newbc_robot_linksignal_configure);
                        break;
                    case "bc_robot_path_configure":
                        IList<bc_robot_path_configure> bc_robot_path_configure = dbService.Viewbc_robot_path_configure_user(hashtable);
                        var Newbc_robot_path_configure = bc_robot_path_configure.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bc_robot_path_configure.Count);
                        InitHistory.Add("rows", Newbc_robot_path_configure);
                        break;
                    case "cfg_alarm": IList<cfg_alarm> cfg_alarm = dbService.Viewcfg_alarm(hashtable); var Newcfg_alarm = cfg_alarm.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_alarm.Count); InitHistory.Add("rows", Newcfg_alarm); break;

                    case "cfg_ceid": IList<cfg_ceid> cfg_ceid = dbService.Viewcfg_ceid(hashtable); var Newcfg_ceid = cfg_ceid.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_ceid.Count); InitHistory.Add("rows", Newcfg_ceid); break;
                    case "cfg_ecid": IList<cfg_ecid> cfg_ecid = dbService.Viewcfg_ecid(hashtable); var Newcfg_ecid = cfg_ecid.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_ecid.Count); InitHistory.Add("rows", Newcfg_ecid); break;
                    case "cfg_eqp": IList<cfg_eqp> cfg_eqp = dbService.Viewcfg_eqp(hashtable); var Newcfg_eqp = cfg_eqp.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_eqp.Count); InitHistory.Add("rows", Newcfg_eqp); break;
                    case "cfg_port": IList<cfg_port> cfg_port = dbService.Viewcfg_port(hashtable); var Newcfg_port = cfg_port.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_port.Count); InitHistory.Add("rows", Newcfg_port); break;
                    case "cfg_ssunit": IList<cfg_ssunit> cfg_ssunit = dbService.Viewcfg_ssunit(hashtable); var Newcfg_ssunit = cfg_ssunit.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_ssunit.Count); InitHistory.Add("rows", Newcfg_ssunit); break;
                    case "cfg_sunit": IList<cfg_sunit> cfg_sunit = dbService.Viewcfg_sunit(hashtable); var Newcfg_sunit = cfg_sunit.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_sunit.Count); InitHistory.Add("rows", Newcfg_sunit); break;
                    case "cfg_unit": IList<cfg_unit> cfg_unit = dbService.Viewcfg_unit(hashtable); var Newcfg_unit = cfg_unit.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_unit.Count); InitHistory.Add("rows", Newcfg_unit); break;
                    case "his_alarm": IList<his_alarm> his_alarm = dbService.Viewhis_alarm(hashtable); var Newhis_alarm = his_alarm.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_alarm.Count); InitHistory.Add("rows", Newhis_alarm); break;
                    case "his_carrier": IList<his_carrier> his_carrier = dbService.Viewhis_carrier(hashtable); var Newhis_carrier = his_carrier.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_carrier.Count); InitHistory.Add("rows", Newhis_carrier); break;
                    case "his_maskinfo": IList<his_maskinfo> his_maskinfo = dbService.Viewhis_maskinfo(hashtable); var Newhis_maskinfo = his_maskinfo.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_maskinfo.Count); InitHistory.Add("rows", Newhis_maskinfo); break;
                    case "his_material": IList<his_material> his_material = dbService.Viewhis_material(hashtable); var Newhis_material = his_material.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_material.Count); InitHistory.Add("rows", Newhis_material); break;
                    case "his_packingbox": IList<his_packingbox> his_packingbox = dbService.Viewhis_packingbox(hashtable); var Newhis_packingbox = his_packingbox.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_packingbox.Count); InitHistory.Add("rows", Newhis_packingbox); break;
                    case "his_recipe": IList<his_recipe> his_recipe = dbService.Viewhis_recipe(hashtable); var Newhis_recipe = his_recipe.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_recipe.Count); InitHistory.Add("rows", Newhis_recipe); break;
                    case "his_recipeppidmap": IList<his_recipeppidmap> his_recipeppidmap = dbService.Viewhis_recipeppidmap(hashtable); var Newhis_recipeppidmap = his_recipeppidmap.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_recipeppidmap.Count); InitHistory.Add("rows", Newhis_recipeppidmap); break;
                    case "his_spanel": IList<his_spanel> his_spanel = dbService.Viewhis_spanel(hashtable); var Newhis_spanel = his_spanel.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_spanel.Count); InitHistory.Add("rows", Newhis_spanel); break;
                    case "his_trayinfo": IList<his_trayinfo> his_trayinfo = dbService.Viewhis_trayinfo(hashtable); var Newhis_trayinfo = his_trayinfo.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_trayinfo.Count); InitHistory.Add("rows", Newhis_trayinfo); break;
                    case "wip_cstbox": IList<wip_cstbox> wip_cstbox = dbService.Viewwip_cstbox(hashtable); var Newwip_cstbox = wip_cstbox.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", wip_cstbox.Count); InitHistory.Add("rows", Newwip_cstbox); break;
                    case "cfg_systemconfig": IList<cfg_systemconfig> cfg_systemconfig = dbService.Viewcfg_systemconfig(hashtable); var Newcfg_systemconfig = cfg_systemconfig.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", cfg_systemconfig.Count); InitHistory.Add("rows", Newcfg_systemconfig); break;
                    case "wip_glassinfo": IList<wip_glassinfo> wip_glassinfo = dbService.Viewwip_glassinfo(hashtable); var Newwip_glassinfo = wip_glassinfo.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", wip_glassinfo.Count); InitHistory.Add("rows", Newwip_glassinfo); break;
                    case "his_user": IList<his_user> his_user = dbService.Viewhis_user(hashtable); var Newhis_user = his_user.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_user.Count); InitHistory.Add("rows", Newhis_user); break;
                    case "his_glassinfo": IList<his_glassinfo> his_glassinfo = dbService.Viewhis_glassinfo(hashtable); var Newhis_glassinfo = his_glassinfo.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize); InitHistory.Add("total", his_glassinfo.Count); InitHistory.Add("rows", Newhis_glassinfo); break;
                    case "bc_user_group":
                        IList<bc_user_group> bc_user_group = dbService.Viewbc_user_group(hashtable);
                        var Newbc_user_group = bc_user_group.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bc_user_group.Count);
                        InitHistory.Add("rows", Newbc_user_group);
                        break;
                    case "wip_alarm":
                        IList<wip_alarm> wip_alarm = dbService.Viewwip_alarm(hashtable);
                        var Newwip_alarm = wip_alarm.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", wip_alarm.Count);
                        InitHistory.Add("rows", Newwip_alarm);
                        break;
                    case "bc_eqp_command_conf":
                        IList<bc_eqp_command_conf> bc_eqp_command_conf = dbService.Viewbc_eqp_command_conf(hashtable);
                        var Newbc_eqp_command_conf = bc_eqp_command_conf.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bc_eqp_command_conf.Count);
                        InitHistory.Add("rows", Newbc_eqp_command_conf);
                        break;
                    case "bc_eqp_command_para":
                        IList<bc_eqp_command_para> bc_eqp_command_para = dbService.Viewbc_eqp_command_para(hashtable);
                        var Newbc_eqp_command_para = bc_eqp_command_para.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", bc_eqp_command_para.Count);
                        InitHistory.Add("rows", Newbc_eqp_command_para);
                        break;
                    case "his_opilog":
                        IList<his_opilog> his_opilogcount = dbService.Viewhis_opilogCount(hashtable);
                        if (pageNo != null)
                        {
                            hashtable.Add("limitpage", Convert.ToInt32(pageNo) - 1);
                        }
                        if (pageSize != null)
                        {
                            hashtable.Add("limitcount", Convert.ToInt32(pageSize));
                        }
                        var his_opilog = dbService.Viewhis_opilog(hashtable);
                        //var Newhis_opilog = his_opilog.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_opilogcount.Count);
                        InitHistory.Add("rows", his_opilog);
                        break;
                    case "his_eqp":
                        IList<his_eqp> his_eqp = dbService.Viewhis_eqp(hashtable);
                        var Newhis_eqp = his_eqp.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_eqp.Count);
                        InitHistory.Add("rows", Newhis_eqp);
                        break;
                    case "his_sunit":
                        IList<his_sunit> his_sunit = dbService.Viewhis_sunit(hashtable);
                        var Newhis_sunit = his_sunit.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_sunit.Count);
                        InitHistory.Add("rows", Newhis_sunit);
                        break;
                    case "his_port":
                        IList<his_port> his_port = dbService.Viewhis_port(hashtable);
                        var Newhis_port = his_port.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_port.Count);
                        InitHistory.Add("rows", Newhis_port);
                        break;
                    case "his_vcr":
                        IList<his_vcr> his_vcr = dbService.Viewhis_vcr(hashtable);
                        var Newhis_vcr = his_vcr.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_vcr.Count);
                        InitHistory.Add("rows", Newhis_vcr);
                        break;
                    case "his_robotcommand":
                        IList<his_robotcommand> his_robotcommand = dbService.Viewhis_robotcommand(hashtable);
                        var Newhis_robotcommand = his_robotcommand.Skip(((int)pageNo - 1) * (int)pageSize).Take((int)pageSize);
                        InitHistory.Add("total", his_robotcommand.Count);
                        InitHistory.Add("rows", Newhis_robotcommand);
                        break;
                    default:
                        break;
                }

                InitHistory.Add("order", "asc");

                WebSocketMessageStr.body = InitHistory;

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
