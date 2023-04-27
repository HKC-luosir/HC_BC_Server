using Glorysoft.BC.WCF.Contract;
using Glorysoft.Auto.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract.WCF;
using System.Collections;
using Glorysoft.Auto.WCF;
using System.Data;

namespace Glorysoft.BC.Client
{
    public class ClientRequest
    {
        private static string WCFEndpointConfigName = ConfigurationManager.AppSettings["WCFEndpointConfigName"].ToString();                     
        private static INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);

        #region Alarm DB
        public static IList<AlarmInfo> ViewAlarmList(string eqpid)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.ViewAlarmList(eqpid);
        }
        public static bool InsertAlarmInfo(AlarmInfo alarm)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            Hashtable map = new Hashtable
            {
                {"EQPID",alarm.EQPID },
                {"EQPName",alarm.EQPName },
                {"AlarmID",alarm.AlarmID },
                {"AlarmLevel",alarm.AlarmLevel },
                {"AlarmText",alarm.AlarmText },
                {"AlarmEnable",alarm.AlarmEnable }
            };
            return proxy.InsertAlarmInfo(map);
        }
        public static bool UpdateAlarmInfo(AlarmInfo alarm)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var map = new Hashtable
            {
                { "EQPName" , alarm.EQPName },
                { "EQPID" , alarm.EQPID },
                {"AlarmID", alarm.AlarmID },
                {" AlarmLevel" , alarm.AlarmLevel },
                { "AlarmText", alarm.AlarmText ?? "" },
                {" AlarmEnable" , alarm.AlarmEnable }
            };
            return proxy.UpdateAlarmInfo(map);
        }
        public static bool DeleteAlarmInfo(AlarmInfo alarm)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            Hashtable map = new Hashtable
            {
                {"EQPID",alarm.EQPID },
                {"AlarmID",alarm.AlarmID }
            };
            return proxy.DeleteAlarmInfo(map);
        }

        public static bool ImportAlarmList(IList<AlarmInfo> lst)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.ImportAlarmList(lst);
        }

        public static bool ClearAlarmList(string eqpID)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.ClearAlarmList(eqpID);
        }

        public static IList<AlarmInfo> ViewAlarmHistory(Hashtable map)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.ViewAlarmHistory(map);
        }

        //public static IList<MaterialInfo> ViewMaterialHistory(Hashtable map)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.ViewMaterialHistory(map);
        //}

        #endregion

        #region EquipmentDB

        #endregion

        #region Recipe DB
      
        //public static void ViewPPIDAndRecipeListByLine(string lineID)
        //{
        //    //INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    //var PPIDList = proxy.ViewPPIDAndRecipeListByLine(lineID);
        //    //DataTable dt = new DataTable();
        //    //if (PPIDList.Count > 0)
        //    //{
        //    //    PPIDList.OrderBy(f => f.PPID);
        //    //    string ppid = "";
        //    //    dt.Columns.Add("LineID");
        //    //    dt.Columns.Add("PPID");
        //    //    //foreach (var item in ClientInfo.Current.OClient.EQPList.Where(o => o.Value.CheckRecipeFlag))
        //    //    //{
        //    //    //    dt.Columns.Add(item.Key);
        //    //    //}
        //    //    DataRow Current = null;
        //    //    foreach (var item in PPIDList)
        //    //    {
        //    //        if (ppid != item.PPID)
        //    //        {

        //    //            Current = dt.Rows.Add();
        //    //            Current[0] = item.LineID;
        //    //            Current[1] = item.PPID;
        //    //            int i = 0;
        //    //            foreach (var Citem in dt.Columns)
        //    //            {
        //    //                if (Citem.ToString() == item.EQPName)
        //    //                {
        //    //                    Current[i] = item.RecipeID;
        //    //                }
        //    //                i++;
        //    //            }
        //    //            ppid = item.PPID;

        //    //        }
        //    //        else
        //    //        {
        //    //            int i = 0;
        //    //            foreach (var Citem in dt.Columns)
        //    //            {
        //    //                if (Citem.ToString() == item.EQPName)
        //    //                {
        //    //                    Current[i] = item.RecipeID;
        //    //                }
        //    //                i++;
        //    //            }
        //    //        }

        //    //    }

        //    //}

        //    //ClientInfo.Current.PPIDRecipeTable = dt;
        //}
        //public static IList<Recipe> FindRecipeIDByEQPID(Recipe recipe)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.ViewRecipeHistoryByEQPID(recipe);
        //}
        //public static bool InsertPPIDAndRecipe(IList<PPIDAndRecipe> list)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.InsertPPIDAndRecipe(list);
        //}
        //public static bool DeletePPIDAndRecipeIDByPPID(string ppid)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.DeletePPIDAndRecipeIDByPPID(ppid);
        //}
        //public static IList<PPIDAndRecipe> ViewPPIDAndRecipeListByPPID(string ppid)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.ViewPPIDAndRecipeListByPPID(ppid);
        //}
        //public static bool UpdatePPIDAndRecipe(IList<PPIDAndRecipe> list)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.UpdatePPIDAndRecipe(list);
        //}
        //public static void SendToEAPPPIDAndRecipeMapChangeReport(IList<PPIDAndRecipe> list,string mode)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    proxy.SendToEAPPPIDAndRecipeMapChangeReport(list, mode);
        //}
        #endregion

        #region Port DB
        public static bool UpdateCarrierInfo(CarrierInfo oCarrier)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.UpdateCarrierInfo(oCarrier);
        }
        public static bool InsertCarrierInfo(CarrierInfo oCarrier)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.InsertCarrierInfo(oCarrier);
        }
        #endregion

        #region Glass DB
        public static IList<GlassInfo> GetPanelList(Hashtable map)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.GetPanelList(map);
        }

        #endregion

        #region EQP DB
        public static User GetUserInfo(string username)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var result = proxy.GetUserInfo(username);
            return result;
        }
        public static IList<User> GetUserList()
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var result = proxy.GetUserList();
            return result;
        }
        public static bool InsertUser(User user)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var result = proxy.InsertUser(user);
            return result;
        }
        public static bool UpdateUser(User user)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var result = proxy.UpdateUser(user);
            return result;
        }
        public static bool DeleteUser(string userID)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            var result = proxy.DeleteUser(userID);
            return result;
        }

        #endregion

        #region Client Command
        public static void UpdateControlState(string ControlState)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.UpdateControlState(ControlState);           
        }
        public static void UpdateOperationMode(int opMode)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.UpdateOperationMode(opMode);
        }
        public static void UpdateVCRMode(string mode)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.UpdateVCRMode(mode);
        }
        public static void ChangeCanOnlineCommand(bool mode)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.ChangeCanOnlineCommand(mode);
        }
        public static void SendCarrierControl(CarrierInfo oCarrier,int control)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendCarrierControl(oCarrier, control);
        }

        public static void SendWaitForStart(CarrierInfo oCarrier)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendWaitForStart(oCarrier);
        }
        public static void SendCarrierInfo(CarrierInfo oCarrier)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendCarrierInfo(oCarrier);
        }
        public static void SendMsgToEQP(string eqpName,List<string> Msg)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendMstToEQP(eqpName, Msg);
        }
        public static void SendTimeToEQP(string eqpName,string Time)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendTimeToEQP(eqpName,Time);
        }
        public static void SendTracingCommandToEQP(string eqpName,string unitName,int frequncy,int times)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendTracingCommandToEQP(eqpName, unitName, frequncy, times);
        }
        public static void SendHostMsg(string Msg)
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            proxy.SendHostMsg(Msg);
        }
        public static HostInfo GetHostInfo()
        {
            INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
            return proxy.GetHostInfo();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item">EQPID,UnitID</param>
        /// <returns></returns>
        //public static IList<Recipe> ViewRecipeHistoryByEQPID(Recipe item)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.ViewRecipeHistoryByEQPID(item);
        //}
        //public static bool UpdateOQASamplingRule(OQASamplingRule oqaSamplingRule)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    return proxy.UpdateOQASamplingRule(oqaSamplingRule);
        //}
        //public static void UpdateOQANGLotCount(int count)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    proxy.UpdateOQANGLotCount(count);
        //}
        #endregion

        //#region Robot Dispatch
        //public static void SendRobotDispatchMode(string eqpName, int mode)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    proxy.SendRobotDispatchMode(eqpName, mode);
        //}
        //public static void SendRobotCommand(RobotCommand robotCommand)
        //{
        //    INotifyService proxy = WCFClientRequest<INotifyService>.GetRequestContext(WCFEndpointConfigName);
        //    proxy.SendRobotCommand(robotCommand);
        //}
        //#endregion

    }
}
