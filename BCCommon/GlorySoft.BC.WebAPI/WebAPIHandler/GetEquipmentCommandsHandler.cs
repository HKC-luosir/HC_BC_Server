using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetEquipmentCommandsHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getequipmentcommands",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                Hashtable hashtable = new Hashtable();
                foreach (var item in InitHistory)
                {
                    hashtable.Add(item.Key,item.Value);
                }
                //Hashtable hashtable = new Hashtable
                //    {
                //        {"line_id",lineId.ToString()}
                //    };

                var codlist = dbService.Viewbc_eqp_command_conf(hashtable);

                Dictionary<string, object> newInit = new Dictionary<string, object>();
                for (int i = 0; i < codlist.Count; i++)
                {
                    Dictionary<string, object> conf = new Dictionary<string, object>();
                    conf.Add("objectId", codlist[i].object_id);
                    conf.Add("lineId", codlist[i].line_id);
                    conf.Add("equipmentNo", codlist[i].equipment_no);
                    conf.Add("equipmentId", codlist[i].equipment_id);
                    conf.Add("subEquipmentNo", codlist[i].subequipment_no);
                    conf.Add("command", codlist[i].command_type);
                    conf.Add("machine", codlist[i].machine);
                    conf.Add("protocol", codlist[i].protocol);
                    conf.Add("commandSecsName", codlist[i].command_secsname);
                    conf.Add("commandToMap", codlist[i].command_to_map);

                    //Dictionary<string, object> conf = ToMap(codlist[i]);
                    Hashtable hashtable2 = new Hashtable
                        {
                            {"line_id",codlist[i].line_id},
                            {"command_type",codlist[i].command_type}
                        };
                    IList<bc_eqp_command_para> paralist = dbService.Viewbc_eqp_command_para(hashtable2);

                    Dictionary<string, object> list = new Dictionary<string, object>();
                    for (int j = 0; j < paralist.Count; j++)
                    {
                        Dictionary<string, object> paralistD = new Dictionary<string, object>();
                        paralistD.Add("objectId", paralist[j].object_id);
                        paralistD.Add("lineIdStr", paralist[j].line_id);
                        paralistD.Add("commandType", paralist[j].command_type);
                        paralistD.Add("parameterId", paralist[j].parameter_id);
                        paralistD.Add("parameterType", paralist[j].parameter_type);
                        paralistD.Add("parameterName", paralist[j].parameter_name);
                        paralistD.Add("required", paralist[j].required);
                        paralistD.Add("referenceValue", paralist[j].reference_value);
                        paralistD.Add("maxValue", paralist[j].max_value);
                        paralistD.Add("minValue", paralist[j].min_value);
                        paralistD.Add("itemNumber", paralist[j].item_number);
                        paralistD.Add("paraEquipmentNo", paralist[j].parameter_id);
                        paralistD.Add("clientValue", null);
                        list.Add(j.ToString(), paralistD);
                    }
                    conf.Add("parameters", list.Values.ToArray());
                    newInit.Add(i.ToString(), conf);
                }

                WebSocketMessageStr.body = newInit.Values.ToArray();

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



        /// <summary>
        /// 
        /// 将对象属性转换为key-value对
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Dictionary<String, Object> ToMap(Object o)
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new Object[] { }));
                }
            }

            return map;

        }

    }
}
