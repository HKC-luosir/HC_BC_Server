
using Glorysoft.Auto.Contract;

using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.WebAPI.WebAPIHandler;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.ServiceModel.Channels;

namespace Glorysoft.BC.WebAPI.WebAPIController
{
    public class WebAPIController : ApiController
    {
        protected readonly log4net.ILog Logger = LogHelper.WebAPILog;


        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly ITibcoRVService rvService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        //[System.Web.Http.HttpGet]
        //public async Task<object> EQPInfoRequest([FromBody]string eqpid)
        //{
        //    return dbService.ViewEQP(eqpid);
        //}
        // [System.Web.Http.HttpPost]
        //[System.Web.Http.HttpPost]
        //public async Task<string> WebAPI([FromBody] WebSocketMessage WebSocketMessage)
        //{
        //    //return dbService.ViewEQP(eqpid);
        //    try
        //    {
        //        //Logger.Debug(message);
        //        string result = "";
        //        //if (ClientOnMessage != null)
        //        //    ClientOnMessage(socket, message);
        //        JavaScriptSerializer js = new JavaScriptSerializer();
        //        //var type = GetJsonType(message);
        //        var type = WebSocketMessage.header.messageName;
        //        var message= WebSocketMessage.JsonSerializer();
        //        //var begin = (message.IndexOf("\"body\"") + 7);
        //        //var end = (message.IndexOf(",\"result\":") - begin);
        //        var begin = (message.IndexOf("\"body\"") + 8);
        //        var end = (message.IndexOf(",\"result\":") - begin - 1);
        //        var body = message.Substring(begin, end);
        //        //BaseClass baseClass = js.Deserialize<BaseClass>(message);                
        //        switch (type)
        //        {
        //            case "EQPInfoRequest":
        //                EQPInfoRequest EQPInfoRequest = js.Deserialize<EQPInfoRequest>(body);
        //                EQPInfoRequestHandler EQPInfoRequestHandler = new EQPInfoRequestHandler();
        //                result= EQPInfoRequestHandler.Execute(EQPInfoRequest);
        //                break;
        //        }
        //        return result;
        //    }
        //    catch(Exception ex)
        //    {

        //        return "";
        //    }

        //}

        //[System.Web.Http.HttpPost]
        //public async Task<string> WebAPI([FromBody] string message)
        //{            
        //    try
        //    {                
        //        string result = "";
        //        JavaScriptSerializer js = new JavaScriptSerializer();
        //        var type = GetJsonType(message);
        //        var begin = (message.IndexOf("\"body\"") + 7);
        //        var end = (message.IndexOf(",\"result\":") - begin);                
        //        var body = message.Substring(begin, end);                        
        //        switch (type)
        //        {
        //            case "EQPInfoRequest":
        //                EQPInfoRequest EQPInfoRequest = js.Deserialize<EQPInfoRequest>(body);
        //                EQPInfoRequestHandler EQPInfoRequestHandler = new EQPInfoRequestHandler();
        //                result = EQPInfoRequestHandler.Execute(EQPInfoRequest);
        //                break;
        //            case "UnitInfoRequest":
        //                UnitInfoRequest UnitInfoRequest = js.Deserialize<UnitInfoRequest>(body);
        //                UnitInfoRequestHandler UnitInfoRequestHandler = new UnitInfoRequestHandler();
        //                result= UnitInfoRequestHandler.Execute(UnitInfoRequest);
        //                break;

        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        [System.Web.Http.HttpPost]
        public async Task<WebSocketMessage> WebAPI([FromBody] WebAPIMessage WebAPIMessage)
        {
            WebSocketMessage result = new WebSocketMessage();
            try
            {
                var clientreq = (RemoteEndpointMessageProperty)Request.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"];
                var clientip = clientreq.Address;
                Dictionary<string, object> data;
                JavaScriptSerializer js = new JavaScriptSerializer();
                var type = WebAPIMessage.header.messageName;
                var userName = WebAPIMessage.header.userName;
                //var begin = (message.IndexOf("\"body\"") + 7);
                //var end = (message.IndexOf(",\"result\":") - begin);
                //var body = message.Substring(begin, end);
                string bodyStr = Convert.ToString(WebAPIMessage.body);
                string commonStr = Convert.ToString(WebAPIMessage.common);

                string RequestJson = WebAPIMessage.JsonSerializer();
                
                RequestJson = RequestJson.Replace(MidStrEx_New(RequestJson, "\"body\":", ",\"common\":"),bodyStr);
                Logger.Info("页面请求Json：\r\n" + RequestJson);
                //dynamic cstInfo = JsonConvert.DeserializeObject<dynamic>(bodyStr);
                switch (type)
                {
                    #region 登录以及主页请求
                    case "EQPInfoRequest":
                        EQPInfoRequest EQPInfoRequest = js.Deserialize<EQPInfoRequest>(bodyStr);
                        EQPInfoRequestHandler EQPInfoRequestHandler = new EQPInfoRequestHandler();
                        result = EQPInfoRequestHandler.Execute(EQPInfoRequest);
                        break;
                    case "GetAlarmByLineIdRequest":
                        wip_alarm wip_alarm = js.Deserialize<wip_alarm>(bodyStr);
                        GetAlarmInfoBylineId GetAlarmInfoBylineId = new GetAlarmInfoBylineId();
                        result = GetAlarmInfoBylineId.Execute(wip_alarm);
                        break;
                    case "UnitInfoRequest":
                        UnitInfoRequest UnitInfoRequest = js.Deserialize<UnitInfoRequest>(bodyStr);
                        UnitInfoRequestHandler UnitInfoRequestHandler = new UnitInfoRequestHandler();
                        result = UnitInfoRequestHandler.Execute(UnitInfoRequest, userName);
                        break;
                    case "AllDataUpdateRequest":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        AllDataUpdateRequestHandler AllDataUpdateRequestHandler = new AllDataUpdateRequestHandler();
                        result = AllDataUpdateRequestHandler.Execute(data);
                        break;
                    case "GetJobByEquipmentRequest":
                        Dictionary<string, object> glassInfo = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetJobByEquipmentRequestHandler GetJobByEquipmentRequestHandler = new GetJobByEquipmentRequestHandler();
                        result = GetJobByEquipmentRequestHandler.Execute(glassInfo);
                        break;
                    case "EquipemntInformationRequest":
                        Line Line = js.Deserialize<Line>(bodyStr);
                        EquipemntInformationRequestHandler EquipemntInformationRequestHandler = new EquipemntInformationRequestHandler();
                        result = EquipemntInformationRequestHandler.Execute(Line);
                        break;
                    case "PortInformationRequest":
                        Ports Ports = js.Deserialize<Ports>(bodyStr);
                        PortInformationRequestHandler PortInformationRequestHandler = new PortInformationRequestHandler();
                        result = PortInformationRequestHandler.Execute(Ports);
                        break;
                    case "UnitInformationRequest":
                        Units Units = js.Deserialize<Units>(bodyStr);
                        UnitInformationRequestHandler UnitInformationRequestHandler = new UnitInformationRequestHandler();
                        result = UnitInformationRequestHandler.Execute(Units);
                        break;
                    case "getLineMaskInfo":
                        GetLineMaskInfoHandler GetLineMaskInfoHandler = new GetLineMaskInfoHandler();
                        result = GetLineMaskInfoHandler.Execute();
                        break;
                    case "getUserGroupNameRequest":
                        Dictionary<string, object> UserGroup = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetUserGroupNameRequestHandler GetUserGroupNameRequestHandler = new GetUserGroupNameRequestHandler();
                        result = GetUserGroupNameRequestHandler.Execute(userName, UserGroup);
                        break;
                    case "getLinkSignalItems":
                        GetLinkSignalItems GetLinkSignalItems = new GetLinkSignalItems();
                        result = GetLinkSignalItems.Execute(userName, type);
                        break;

                    #endregion
                    #region  User信息接口
                    case "UserLoginReqeust":
                        bcUser bcUser = js.Deserialize<bcUser>(bodyStr);
                        UserLoginReqeustHandler UserLoginReqeustHandler = new UserLoginReqeustHandler();
                        result = UserLoginReqeustHandler.Execute(bcUser, userName, clientip, type);
                        break;
                    case "updateuserinfo":
                        bcUser = js.Deserialize<bcUser>(bodyStr);
                        UpdateUserInfo UpdateUserInfo = new UpdateUserInfo();
                        result = UpdateUserInfo.Execute(bcUser, userName, clientip, type);
                        break;
                    case "bcuserinfo":
                        UserInfoRequest UserInfoRequest = js.Deserialize<UserInfoRequest>(bodyStr);
                        BcUserInfo BcUserInfo = new BcUserInfo();
                        result = BcUserInfo.Execute(UserInfoRequest);
                        break;
                    case "SaveUser":
                        bcUser = js.Deserialize<bcUser>(bodyStr);
                        SaveUser SaveUser = new SaveUser();
                        result = SaveUser.Execute(bcUser, userName, clientip, type);
                        break;
                    case "deleteusers":
                        bcUser = js.Deserialize<bcUser>(bodyStr);
                        DeleteUsers DeleteUsers = new DeleteUsers();
                        result = DeleteUsers.Execute(bcUser, userName, clientip, type);
                        break;
                    case "importExcelUserList":
                        List<User> users = js.Deserialize<List<User>>(bodyStr);
                        importExcelUserListHandler importExcelUserListHandler = new importExcelUserListHandler();
                        result = importExcelUserListHandler.Execute(userName, clientip, users);
                        break;
                    case "getUsergroupList":
                        UserInfoRequest = js.Deserialize<UserInfoRequest>(bodyStr);
                        GetUsergroupListHandler GetUsergroupListHandler = new GetUsergroupListHandler();
                        result = GetUsergroupListHandler.Execute(UserInfoRequest);
                        break;
                    case "saveUserGroup":
                        GroupAuthority GroupAuthority = js.Deserialize<GroupAuthority>(bodyStr);
                        SaveUserGroupHandler SaveUserGroupHandler = new SaveUserGroupHandler();
                        result = SaveUserGroupHandler.Execute(userName, clientip, GroupAuthority, type);
                        break;
                    case "deleteusergroups":
                        BC_Group BC_Group = js.Deserialize<BC_Group>(bodyStr);
                        DeleteUserGroupsHandler DeleteUserGroupsHandler = new DeleteUserGroupsHandler();
                        result = DeleteUserGroupsHandler.Execute(userName, clientip, BC_Group, type);
                        break;
                    case "getUserGroupInfo":
                        BC_Group = js.Deserialize<BC_Group>(bodyStr);
                        GetUserGroupInfoHandler GetUserGroupInfoHandler = new GetUserGroupInfoHandler();
                        result = GetUserGroupInfoHandler.Execute(userName, BC_Group);
                        break;
                    #endregion
                    #region HistoryManagement
                    case "getInitHistory":
                        GetInitHistoryHandler GetInitHistoryHandler = new GetInitHistoryHandler();
                        result = GetInitHistoryHandler.Execute(userName);
                        break;
                    case "getHistorySelectFiles":
                        GetHistorySelectFilesHandler GetHistorySelectFilesHandler = new GetHistorySelectFilesHandler();
                        result = GetHistorySelectFilesHandler.Execute(userName, commonStr);
                        break;
                    case "saveHistoryTable":
                        SaveHistoryTableHandler SaveHistoryTableHandler = new SaveHistoryTableHandler();
                        result = SaveHistoryTableHandler.Execute(userName, bodyStr, commonStr);
                        break;
                    case "getHistoryDelete":
                        GetHistoryDeleteHandler GetHistoryDeleteHandler = new GetHistoryDeleteHandler();
                        result = GetHistoryDeleteHandler.Execute(userName, commonStr);
                        break;
                    case "getHistoryTableInformation":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetHistoryTableInformationHandler GetHistoryTableInformationHandler = new GetHistoryTableInformationHandler();
                        result = GetHistoryTableInformationHandler.Execute(userName, data);
                        break;
                    case "getHistoryBySeachCondition":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetHistoryBySeachConditionHandler GetHistoryBySeachConditionHandler = new GetHistoryBySeachConditionHandler();
                        result = GetHistoryBySeachConditionHandler.Execute(userName, data);
                        break;
                    case "getUnitHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetUnitHisData GetUnitHisData = new GetUnitHisData();
                        result = GetUnitHisData.Execute(userName, data);
                        break;
                    case "getPortHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetPortHisData GetPortHisData = new GetPortHisData();
                        result = GetPortHisData.Execute(userName, data);
                        break;
                    case "getCassetteHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCassetteHisData GetCassetteHisData = new GetCassetteHisData();
                        result = GetCassetteHisData.Execute(userName, data);
                        break;
                    case "getGlassInfoHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetGlassInfoHisData GetGlassInfoHisData = new GetGlassInfoHisData();
                        result = GetGlassInfoHisData.Execute(userName, data);
                        break;
                    case "getMaterialHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetMaterialHisData GetMaterialHisData = new GetMaterialHisData();
                        result = GetMaterialHisData.Execute(userName, data);
                        break;
                    case "getPalletHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetPalletHisData GetPalletHisData = new GetPalletHisData();
                        result = GetPalletHisData.Execute(userName, data);
                        break;
                    case "getGlassEventList":
                        GetGlassEventList GetGlassEventList = new GetGlassEventList();
                        result = GetGlassEventList.Execute(userName);
                        break;
                    case "getAlarmHisData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetAlarmHisData GetAlarmHisData = new GetAlarmHisData();
                        result = GetAlarmHisData.Execute(userName, data);
                        break;
                    case "GETModePathDis":
                        GETModePathDis gETModePathDis = new GETModePathDis();
                        result = gETModePathDis.Execute(userName, type);
                        break;
                    case "GetOPILogAllDelete":
                        GetOPILogAllDelete GetOPILogAllDelete = new GetOPILogAllDelete();
                        result = GetOPILogAllDelete.Execute(userName, clientip);
                        break;
                    case "GetRobotCommandHisAllDelete":
                        GetRobotCommandHisAllDelete GetRobotCommandHisAllDelete = new GetRobotCommandHisAllDelete();
                        result = GetRobotCommandHisAllDelete.Execute(userName, clientip);
                        break;
                    #endregion
                    #region WIP
                    case "getEqpData":
                        Dictionary<string, object> eqpdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetEqpDataHandler GetEqpDataHandler = new GetEqpDataHandler();
                        result = GetEqpDataHandler.Execute(userName, eqpdata);
                        break;
                    case "delEqpData":
                        IList<cfg_eqp> deldata = js.Deserialize<IList<cfg_eqp>>(bodyStr);
                        DelEqpDataHandler DelEqpDataHandler = new DelEqpDataHandler();
                        result = DelEqpDataHandler.Execute(userName, deldata);
                        break;
                    case "getPortData":
                        Dictionary<string, object> portdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetPortDataHandler GetPortDataHandler = new GetPortDataHandler();
                        result = GetPortDataHandler.Execute(userName, portdata);
                        break;
                    case "getPortControlData":
                        Dictionary<string, object> portcontroldata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetPortControlDataHandler getPortControlDataHandler = new GetPortControlDataHandler();
                        result = getPortControlDataHandler.Execute(userName, portcontroldata);
                        break;
                    case "delPortData":
                        IList<cfg_port> delportdata = js.Deserialize<IList<cfg_port>>(bodyStr);
                        DelPortDataHandler DelPortDataHandler = new DelPortDataHandler();
                        result = DelPortDataHandler.Execute(userName, clientip, delportdata, type);
                        break;
                    case "getUnitData":
                        Dictionary<string, object> unitdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetUnitDataHandler GetUnitDataHandler = new GetUnitDataHandler();
                        result = GetUnitDataHandler.Execute(userName, unitdata);
                        break;
                    case "delUnitData":
                        IList<cfg_unit> delunitdata = js.Deserialize<IList<cfg_unit>>(bodyStr);
                        DelUnitDataHandler DelunitDataHandler = new DelUnitDataHandler();
                        result = DelunitDataHandler.Execute(userName, delunitdata);
                        break;
                    case "getGlassData":
                        Dictionary<string, object> glassdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetGlassDataHandler GetGlassDataHandler = new GetGlassDataHandler();
                        result = GetGlassDataHandler.Execute(userName, glassdata);
                        break;
                    case "getMaterialData":
                        Dictionary<string, object> materialdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetMaterialDataHandler GetMaterialDataHandler = new GetMaterialDataHandler();
                        result = GetMaterialDataHandler.Execute(userName, materialdata);
                        break;
                    case "delGlassData":
                        IList<wip_glassinfo> delglassdata = js.Deserialize<IList<wip_glassinfo>>(bodyStr);
                        DelGlassDataHandler DelglassDataHandler = new DelGlassDataHandler(); 
                        result = DelglassDataHandler.Execute(userName, clientip, delglassdata, type);
                        break;
                    case "recGlassData":
                        IList<wip_glassinfo> recglassdata = js.Deserialize<IList<wip_glassinfo>>(bodyStr);
                        RecoveryGlassDataHandler RecglassDataHandler = new RecoveryGlassDataHandler();
                        result = RecglassDataHandler.Execute(userName, clientip, recglassdata, type);
                        break;
                    case "getAlarmData":
                        Dictionary<string, object> alarmdata = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetAlarmDataHandler GetAlarmDataHandler = new GetAlarmDataHandler();
                        result = GetAlarmDataHandler.Execute(userName, alarmdata);
                        break;
                    case "delAlarmData":
                        IList<wip_alarm> delAlarmdata = js.Deserialize<IList<wip_alarm>>(bodyStr);
                        DelAlarmDataHandler DelAlarmDataHandler = new DelAlarmDataHandler();
                        result = DelAlarmDataHandler.Execute(userName, clientip, delAlarmdata, type);
                        break;
                    case "GetOperationModeData":
                        Dictionary<string, object> OperationModeData = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetOperationModeData GetOperationModeData = new GetOperationModeData();
                        result = GetOperationModeData.Execute(userName, OperationModeData);
                        break;
                    case "AddOperationModeData":
                        cfg_operationmode OperationModeData2 = js.Deserialize<cfg_operationmode>(bodyStr);
                        AddOperationModeData AddOperationModeData = new AddOperationModeData();
                        result = AddOperationModeData.Execute(userName, clientip, OperationModeData2, type);
                        break;
                    case "DelOperationModeData":
                        cfg_operationmode OperationModeData3 = js.Deserialize<cfg_operationmode>(bodyStr);
                        DelOperationModeData DelOperationModeData = new DelOperationModeData();
                        result = DelOperationModeData.Execute(userName, clientip, OperationModeData3, type);
                        break;
                    case "GetAllOperationModeData":
                        GetAllOperationModeData GetAllOperationModeData = new GetAllOperationModeData();
                        result = GetAllOperationModeData.Execute(userName, bodyStr);
                        break;
                    case "getCassetteData":
                        Dictionary<string, object> CassetteData = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCassetteData GetCassetteData = new GetCassetteData();
                        result = GetCassetteData.Execute(userName, CassetteData);
                        break;
                    case "delCassetteData":
                        IList<wip_cassette> delCassetteData = js.Deserialize<IList<wip_cassette>>(bodyStr);
                        DelCassetteData DelCassetteData = new DelCassetteData();
                        result = DelCassetteData.Execute(userName, clientip, delCassetteData);
                        break;
                    case "getRobotModelData":
                        Dictionary<string, object> robotModelData = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetRobotModelData GetRobotModelData = new GetRobotModelData();
                        result = GetRobotModelData.Execute(userName, robotModelData);
                        break;
                    case "delRobotModelData":
                        bc_robot_model delRobotModelData = js.Deserialize<bc_robot_model>(bodyStr);
                        DelRobotModelData DelRobotModelData = new DelRobotModelData();
                        result = DelRobotModelData.Execute(userName, clientip, delRobotModelData);
                        break;
                    case "addRobotModelData":
                        bc_robot_model addRobotModelData = js.Deserialize<bc_robot_model>(bodyStr);
                        AddRobotModelData AddRobotModelData = new AddRobotModelData();
                        result = AddRobotModelData.Execute(userName, clientip, addRobotModelData);
                        break;
                    case "updateRobotModelData":
                        bc_robot_model addRobotModelData2 = js.Deserialize<bc_robot_model>(bodyStr);
                        updateRobotModelData updateRobotModelData = new updateRobotModelData();
                        result = updateRobotModelData.Execute(userName, clientip, addRobotModelData2);
                        break;
                    case "getProcessEndData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetProcessEndData GetProcessEndData = new GetProcessEndData();
                        result = GetProcessEndData.Execute(userName, data);
                        break;
                    case "getProcessEndGlassData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetProcessEndGlassData GetProcessEndGlassData = new GetProcessEndGlassData();
                        result = GetProcessEndGlassData.Execute(userName, data);
                        break;
                    case "saveProcessEndGlassData":
                        ProcessEndGlassInfo processEndGlassInfo = js.Deserialize<ProcessEndGlassInfo>(bodyStr);
                        SaveProcessEndGlassData SaveProcessEndGlassData = new SaveProcessEndGlassData();
                        result = SaveProcessEndGlassData.Execute(userName, clientip, processEndGlassInfo);
                        break;
                    case "deleteProcessEndGlassData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        DeleteProcessEndGlassData DeleteProcessEndGlassData = new DeleteProcessEndGlassData();
                        result = DeleteProcessEndGlassData.Execute(userName, clientip, data);
                        break;
                    case "updateProcessEndGlassData":
                        wip_processend_glass updatewipprocessendglass = js.Deserialize<wip_processend_glass>(bodyStr);
                        UpdateProcessEndGlassData UpdateProcessEndGlassData = new UpdateProcessEndGlassData();
                        result = UpdateProcessEndGlassData.Execute(userName, clientip, updatewipprocessendglass);
                        break;
                    case "updateProcessEndData":
                        wip_processend updatewipprocessend = js.Deserialize<wip_processend>(bodyStr);
                        UpdateProcessEndData UpdateProcessEndData = new UpdateProcessEndData();
                        result = UpdateProcessEndData.Execute(userName, clientip, updatewipprocessend);
                        break;
                    case "deleteProcessEndData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        DeleteProcessEndData DeleteProcessEndData = new DeleteProcessEndData();
                        result = DeleteProcessEndData.Execute(userName, clientip, data);
                        break;
                    case "sendMESProcessEndData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SendMESProcessEndData SendMESProcessEndData = new SendMESProcessEndData();
                        result = SendMESProcessEndData.Execute(userName, clientip, data);
                        break;
                    case "sendMESProcessEndGlassData":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SendMESProcessEndGlassData SendMESProcessEndGlassData = new SendMESProcessEndGlassData();
                        result = SendMESProcessEndGlassData.Execute(userName, clientip, data);
                        break;
                    #endregion
                    #region Commands
                    case "PortTypeChangeCommand":
                    case "PortTransferModeChangeCommand":
                    case "PortEnableModeChangeCommand":
                    case "PortTypeAutoChangeModeCommand":
                    case "PortCassetteTypeChangeCommand":
                    case "PortModeChangeCommand":
                    case "PortPauseModeChangeCommand":
                    case "PortGradeChangeCommand":
                    case "PortQTimeChangeCommand":
                    case "PortControlCommand":
                        PortInfo PortInfo = js.Deserialize<PortInfo>(bodyStr);
                        PortCommandHandler PortCommand = new PortCommandHandler();
                        result = PortCommand.Execute(userName, clientip, type, PortInfo);
                        break;
                    case "LineOperationModeCommand":
                        Dictionary<string, object> OperationModeCut = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        LineOperationModeCommandHandler LineOperationModeCommand = new LineOperationModeCommandHandler();
                        result = LineOperationModeCommand.Execute(userName, clientip, OperationModeCut);
                        break;
                    case "MESControlModeCommand":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        MESControlModeCommandHandler MESControlModeCommand = new MESControlModeCommandHandler();
                        result = MESControlModeCommand.Execute(userName, clientip, data);
                        break;
                    case "DateTimeCommand":
                    case "CIMModeChangeCommand":
                    case "IsProcessEndFlagChange":
                    case "IsJobDataRequestFlagChange":
                    case "CVReportTimeChangeCommand":
                    case "RecipeParameterRequestCommand":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        EQPControlCommandHandler EQPControlCommand = new EQPControlCommandHandler();
                        result = EQPControlCommand.Execute(userName, clientip, type, data);
                        break;
                    //case "CIMMessageSetCommand":
                    //case "CIMMessageClearCommand":
                    case "CIMMessageCommand":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        CIMMessageCommandHandler CIMMessageCommand = new CIMMessageCommandHandler();
                        result = CIMMessageCommand.Execute(userName, clientip, data);
                        break;
                    case "SamplingDownloadCommand":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SamplingDownloadCommandHandler SamplingDownloadCommand = new SamplingDownloadCommandHandler();
                        result = SamplingDownloadCommand.Execute(userName, clientip, data);
                        break;
                    case "DVSamplingFlagCommand":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        DVSamplingFlagCommandHandler DVSamplingFlagCommand = new DVSamplingFlagCommandHandler();
                        result = DVSamplingFlagCommand.Execute(userName, clientip, data);
                        break;
                    case "getequipmentcommands":
                        Dictionary<string, object> commands = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetEquipmentCommandsHandler GetEquipmentCommandsHandler = new GetEquipmentCommandsHandler();
                        result = GetEquipmentCommandsHandler.Execute(userName, commands);
                        break;
                    case "GetServerLinesMessageRequest":
                        Dictionary<string, object> serverLine = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetServerLinesMessageRequestHandler GetServerLinesMessageRequestHandler = new GetServerLinesMessageRequestHandler();
                        result = GetServerLinesMessageRequestHandler.Execute(userName, serverLine);
                        break;
                    case "cmdRobotControl":
                        commandForm commandForm = js.Deserialize<commandForm>(bodyStr);
                        cmdRobotControlHandler cmdRobotControlHandler = new cmdRobotControlHandler();
                        result = cmdRobotControlHandler.Execute(userName, clientip, commandForm);
                        break;
                    case "getrobotModelList":
                        Dictionary<string, object> eqpid = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetrobotModelListHandler GetrobotModelListHandler = new GetrobotModelListHandler();
                        result = GetrobotModelListHandler.Execute(userName, eqpid);
                        break;
                    case "getrobotDataList":
                        GetPortInformationListHandel GetPortInformationListHandel = new GetPortInformationListHandel();
                        result = GetPortInformationListHandel.Execute(userName, bodyStr);
                        break;
                    case "getDispatchModeCut":
                        Dictionary<string, object> DispatchMode = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetDispatchModeCut GetDispatchModeCut = new GetDispatchModeCut();
                        result = GetDispatchModeCut.Execute(userName, clientip, DispatchMode);
                        break;
                    case "getCassetteCommand":
                        Dictionary<string, object> cassetteCommandData = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCassetteCommandHandler GetCassetteCommandHandler = new GetCassetteCommandHandler();
                        result = GetCassetteCommandHandler.Execute(userName, clientip, cassetteCommandData);
                        break;
                    case "getClassInfoInsert":
                        CasInfo GlassInfo = js.Deserialize<CasInfo>(bodyStr);
                        GetClassInfoInsert GetClassInfoInsert = new GetClassInfoInsert();
                        result = GetClassInfoInsert.Execute(userName, GlassInfo);
                        break;
                    case "setColdRunCount":
                        SetColdRunCount SetColdRunCount = new SetColdRunCount();
                        result = SetColdRunCount.Execute(userName, clientip, Convert.ToInt32(bodyStr));
                        break;
                    case "setLocalRecive":
                        SetLocalRecive SetLocalRecive = new SetLocalRecive();
                        result = SetLocalRecive.Execute(userName, bodyStr);
                        break;
                    case "getMesRuleList":
                        GetMesRuleList GetMesRuleList = new GetMesRuleList();
                        result = GetMesRuleList.Execute(userName);
                        break;
                    case "getEqRuleList":
                        GetEqRuleList GetEqRuleList = new GetEqRuleList();
                        result = GetEqRuleList.Execute(userName);
                        break;
                    case "IsColdRunChangeRequest":
                        Dictionary<string, string> isColdRunChange = js.Deserialize<Dictionary<string, string>>(bodyStr);
                        IsColdRunChangeRequest IsColdRunChangeRequest = new IsColdRunChangeRequest();
                        result = IsColdRunChangeRequest.Execute(userName, clientip, isColdRunChange);
                        break;
                    #endregion
                    #region Setting
                    case "SelectAlarmConfigRequest":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SelectAlarmConfigRequest selectAlarmConfigRequest = new SelectAlarmConfigRequest();
                        result = selectAlarmConfigRequest.Execute(userName, data);
                        break;
                    case "AddAlarmConfigSpecRequest":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        AddAlarmConfigSpecRequest AddAlarmConfigSpecRequest = new AddAlarmConfigSpecRequest();
                        result = AddAlarmConfigSpecRequest.Execute(userName, clientip, data);
                        break;
                    case "UpdateAlarmConfigSpecRequest":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        UpdateAlarmConfigSpecRequest UpdateAlarmConfigSpecRequest = new UpdateAlarmConfigSpecRequest();
                        result = UpdateAlarmConfigSpecRequest.Execute(userName, clientip, data);
                        break;
                    case "DeleteAlarmConfigSpecRequest":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        DeleteAlarmConfigSpecRequest DeleteAlarmConfigSpecRequest = new DeleteAlarmConfigSpecRequest();
                        result = DeleteAlarmConfigSpecRequest.Execute(userName, clientip, data);
                        break;
                    case "importExcelAlarmList":
                        List<AlarmInfo> alarms = js.Deserialize<List<AlarmInfo>>(bodyStr);
                        importExcelAlarmListHandler importExcelAlarmListHandler = new importExcelAlarmListHandler();
                        result = importExcelAlarmListHandler.Execute(userName, clientip, alarms);
                        break;
                    case "saveSystemSettingRequest":
                        List<bc_sys_setting> bcsyssettingdata = js.Deserialize<List<bc_sys_setting>>(bodyStr);
                        SaveSystemSettingRequest saveSystemSettingRequest = new SaveSystemSettingRequest();
                        result = saveSystemSettingRequest.Execute(userName, clientip, bcsyssettingdata);
                        break;
                    case "getSystemSettingList":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SelectSystemSettingRequest selectSystemSettingRequest = new SelectSystemSettingRequest();
                        result = selectSystemSettingRequest.Execute(userName, data);
                        break;
                    case "SelectLineStatusSpecRequest":
                        Dictionary<string, object> linstatus = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        SelectLineStatusSpecRequest SelectLineStatusSpecRequest = new SelectLineStatusSpecRequest();
                        result = SelectLineStatusSpecRequest.Execute(userName, linstatus);
                        break;
                    case "AddLineStatusSpecRequest":
                        cfg_eqpstatusrule cfg_eqpstatusrule = js.Deserialize<cfg_eqpstatusrule>(bodyStr);
                        AddLineStatusSpecRequest AddLineStatusSpecRequest = new AddLineStatusSpecRequest();
                        result = AddLineStatusSpecRequest.Execute(userName, clientip, cfg_eqpstatusrule);
                        break;
                    case "DeleteLineStatusSpecRequest":
                        cfg_eqpstatusrule = js.Deserialize<cfg_eqpstatusrule>(bodyStr);
                        DeleteLineStatusSpecRequest DeleteLineStatusSpecRequest = new DeleteLineStatusSpecRequest();
                        result = DeleteLineStatusSpecRequest.Execute(userName, clientip, cfg_eqpstatusrule);
                        break;
                    case "SelectPortGradeGroupList":
                    case "PortGradeGroupDelete":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        PortGradeGroupControlHandler portGradeGroupControlHandler = new PortGradeGroupControlHandler();
                        result = portGradeGroupControlHandler.Execute(userName, clientip, type, data);
                        break;
                    case "PortGradeGroupAdd":
                    case "PortGradeGroupUpdate":
                        cfg_portgradegroup cfg_portgradegroupdata = js.Deserialize<cfg_portgradegroup>(bodyStr);
                        PortGradeGroupUpdateHandler portGradeGroupUpdateHandler = new PortGradeGroupUpdateHandler();
                        result = portGradeGroupUpdateHandler.Execute(userName, clientip, type, cfg_portgradegroupdata);
                        break;
                    #endregion
                    #region Recipe
                    case "CurrentRecipeIdCheckFlagChange":
                    //case "RecipeParamCheckFlagChange":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        RecipeControlHandler RecipeControl = new RecipeControlHandler();
                        result = RecipeControl.Execute(userName, clientip, type, data);
                        break;
                    case "SelectRecipeInfoRequest":
                        Dictionary<string, object> SelectRecipeInfo = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetRecipListHandler GetRecipListHandler = new GetRecipListHandler();
                        result = GetRecipListHandler.Execute(userName, SelectRecipeInfo);
                        break;
                    case "DeleteRecipeInfoRequest":
                        Dictionary<string, object> deleteRecipeInfo = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        DeleteRecipeInfoRequest DeleteRecipeInfoRequest = new DeleteRecipeInfoRequest();
                        result = DeleteRecipeInfoRequest.Execute(userName, clientip, deleteRecipeInfo);
                        break;
                    case "getUnitList":
                        Dictionary<string, object> getUnitList = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetUnitList GetUnitList = new GetUnitList();
                        result = GetUnitList.Execute(userName, getUnitList);
                        break;
                    case "AddRecipeInfoRequest":
                        AddRecipeInfo addRecipeInfo = js.Deserialize<AddRecipeInfo>(bodyStr);
                        AddRecipeInfoRequest AddRecipeInfoRequest = new AddRecipeInfoRequest();
                        result = AddRecipeInfoRequest.Execute(userName, clientip, addRecipeInfo);
                        break;
                    case "UpdateRecipeInfoRequest":
                        AddRecipeInfo addRecipeInfo2 = js.Deserialize<AddRecipeInfo>(bodyStr);
                        UpdateRecipeInfoRequest UpdateRecipeInfoRequest = new UpdateRecipeInfoRequest();
                        result = UpdateRecipeInfoRequest.Execute(userName, clientip, addRecipeInfo2, type);
                        break;
                    case "GetPPIDRecipeInfoRequest":
                        Dictionary<string, object> PPIDRecipeInfo = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetPPIDRecipeInfoRequest GetPPIDRecipeInfoRequest = new GetPPIDRecipeInfoRequest();
                        result = GetPPIDRecipeInfoRequest.Execute(userName, PPIDRecipeInfo);
                        break;
                    case "GetCfgEqpStatusGroupList":
                        GetCfgEqpStatusGroupList GetCfgEqpStatusGroupList = new GetCfgEqpStatusGroupList();
                        result = GetCfgEqpStatusGroupList.Execute(userName, bodyStr);
                        break;
                    case "GetCfgEqpStatusGroupUpdate":
                        Dictionary<string, object> getCfgEqpStatusGroupUpdate = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCfgEqpStatusGroupUpdate GetCfgEqpStatusGroupUpdate = new GetCfgEqpStatusGroupUpdate();
                        result = GetCfgEqpStatusGroupUpdate.Execute(userName, clientip, getCfgEqpStatusGroupUpdate);
                        break;
                    case "GetCfgMixRunConfigList":
                        GetCfgMixRunConfigList GetCfgMixRunConfigList = new GetCfgMixRunConfigList();
                        result = GetCfgMixRunConfigList.Execute(userName, bodyStr);
                        break;
                    case "GetCfgMixRunConfigUpdate":
                    case "GetCfgMixRunConfigAdd":
                        Dictionary<string, object> getCfgMixRunConfigUpdate = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCfgMixRunConfigUpdate GetCfgMixRunConfigUpdate = new GetCfgMixRunConfigUpdate();
                        result = GetCfgMixRunConfigUpdate.Execute(userName, clientip, type, getCfgMixRunConfigUpdate);
                        break;
                    case "GetCfgMixRunConfigDelete":
                        GetCfgMixRunConfigDelete GetCfgMixRunConfigDelete = new GetCfgMixRunConfigDelete();
                        result = GetCfgMixRunConfigDelete.Execute(userName, clientip, type, bodyStr);
                        break;
                    case "GetMIXRunInputRatioList":
                        MIXRunInputRatio CfgMixRunInputRatio1 = js.Deserialize<MIXRunInputRatio>(bodyStr);
                        GetMIXRunInputRatioListHandler GetMIXRunInputRatioList = new GetMIXRunInputRatioListHandler();
                        result = GetMIXRunInputRatioList.Execute(userName, clientip, type, CfgMixRunInputRatio1);
                        break;
                    case "GetCfgMixRunInputRatioUpdate":
                    case "GetCfgMixRunInputRatioAdd":
                    case "GetCfgMixRunInputRatioDelete":
                        MIXRunInputRatio CfgMixRunInputRatio = js.Deserialize<MIXRunInputRatio>(bodyStr);
                        GetCfgMixRunInputRatioUpdateHandler GetCfgMixRunInputRatioUpdate = new GetCfgMixRunInputRatioUpdateHandler();
                        result = GetCfgMixRunInputRatioUpdate.Execute(userName, clientip, type, CfgMixRunInputRatio);
                        break;
                    #endregion
                    #region ProcessData
                    case "getCfg_dvdata":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCfg_dvdata GetCfg_dvdata = new GetCfg_dvdata();
                        result = GetCfg_dvdata.Execute(userName, data);
                        break;
                    case "updateCfg_dvdata":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        UpdateCfgDvData UpdateCfgDvData = new UpdateCfgDvData();
                        result = UpdateCfgDvData.Execute(userName, clientip, data);
                        break;
                    case "getcfg_svdata":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetcfgSvData GetcfgSvData = new GetcfgSvData();
                        result = GetcfgSvData.Execute(userName, data);
                        break;
                    case "updatecfg_svdata":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        UpdateCfgSvData UpdateCfgSvData = new UpdateCfgSvData();
                        result = UpdateCfgSvData.Execute(userName, clientip, data);
                        break;
                    case "getcfg_recipeparameter":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCfgRecipeParameter GetCfgRecipeParameter = new GetCfgRecipeParameter();
                        result = GetCfgRecipeParameter.Execute(userName, data);
                        break;
                    case "updatecfg_recipeparameter":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        UpdateCfgRecipeParameter UpdateCfgRecipeParameter = new UpdateCfgRecipeParameter();
                        result = UpdateCfgRecipeParameter.Execute(userName, clientip, data);
                        break;
                    case "getcfg_alarmspec":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        GetCfgAlarmSpec GetCfgAlarmSpec = new GetCfgAlarmSpec();
                        result = GetCfgAlarmSpec.Execute(userName, data);
                        break;
                    case "updatecfg_alarmspec":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        UpdateCfgAlarmSpec UpdateCfgAlarmSpec = new UpdateCfgAlarmSpec();
                        result = UpdateCfgAlarmSpec.Execute(userName, clientip, data);
                        break;
                    #endregion
                    #region Test
                    case "TestDataList": //Test页面数据展示
                        TestDataList testDataList = new TestDataList();
                        result = testDataList.Execute(userName, type);
                        break;
                    case "TestDataDelete":  //Test页面数据删除
                        his_opilog his_opilog = js.Deserialize<his_opilog>(bodyStr);
                        TestDataDelete testDataDelete = new TestDataDelete();
                        result = testDataDelete.Execute(userName, clientip, type, his_opilog);
                        break;
                    #endregion
                    #region EQPProfile
                    case "SelectEQPProfileList":
                    case "SelectEQPProfileItemGroupList":
                    case "SelectEQPProfileItemList":
                    case "EQPProfileControlAvail":
                    case "EQPProfileDelete":
                    case "EQPProfileItemGroupDelete":
                    case "EQPProfileItemDelete":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        EQPProfileControlHandler eQPProfileControlHandler = new EQPProfileControlHandler();
                        result = eQPProfileControlHandler.Execute(userName, clientip, type, data);
                        break;
                    case "EQPProfileImport":
                        List<EQPProfileImportObject> eQPProfileImportObject = js.Deserialize<List<EQPProfileImportObject>>(bodyStr);
                        EQPProfileImportHandler eQPProfileImportHandler = new EQPProfileImportHandler();
                        result = eQPProfileImportHandler.Execute(userName, clientip, eQPProfileImportObject);
                        break;
                    case "EQPProfileItemGroupAdd":
                    case "EQPProfileItemGroupUpdate":
                        cfg_eqpprofile_itemgroup itemgroupdata = js.Deserialize<cfg_eqpprofile_itemgroup>(bodyStr);
                        EQPProfileItemGroupUpdateHandler eQPProfileItemGroupUpdateHandler = new EQPProfileItemGroupUpdateHandler();
                        result = eQPProfileItemGroupUpdateHandler.Execute(userName, clientip, type, itemgroupdata);
                        break;
                    case "EQPProfileItemAdd":
                    case "EQPProfileItemUpdate":
                        cfg_eqpprofile_item itemdata = js.Deserialize<cfg_eqpprofile_item>(bodyStr);
                        EQPProfileItemUpdateHandler eQPProfileItemUpdateHandler = new EQPProfileItemUpdateHandler();
                        result = eQPProfileItemUpdateHandler.Execute(userName, clientip, type, itemdata);
                        break;
                    #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
                    case "BCTerminate":
                        //断开EIP连接
                        IEQPService eqpService = CommonContexts.ResolveInstance<IEQPService>();
                        eqpService.Terminate();
                        break;
                    #endregion
                    #region 获取BC当前信息 liuyusen 20221205 
                    case "GetBCCurrentInfo":
                        GetBCCurrentInfoHandler getBCCurrentInfoHandler = new GetBCCurrentInfoHandler();
                        result = getBCCurrentInfoHandler.Execute(userName);
                        break;
                    #endregion
                    #endregion
                    #region Robot
                    case "SelectRobotConfigureList":
                    case "RobotConfigureDelete":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        RobotConfigureControlHandler robotConfigureControlHandler = new RobotConfigureControlHandler();
                        result = robotConfigureControlHandler.Execute(userName, clientip, type, data);
                        break;
                    case "RobotConfigureAdd":
                    case "RobotConfigureUpdate":
                        bc_robot_configure bc_robot_configuredata = js.Deserialize<bc_robot_configure>(bodyStr);
                        RobotConfigureUpdateHandler robotConfigureUpdateHandler = new RobotConfigureUpdateHandler();
                        result = robotConfigureUpdateHandler.Execute(userName, clientip, type, bc_robot_configuredata);
                        break;
                    case "SelectRobotPathConfigureList":
                    case "RobotPathConfigureDelete":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        RobotPathConfigureControlHandler robotPathConfigureControlHandler = new RobotPathConfigureControlHandler();
                        result = robotPathConfigureControlHandler.Execute(userName, clientip, type, data);
                        break;
                    case "RobotPathConfigureAdd":
                    case "RobotPathConfigureUpdate":
                        bc_robot_path_configure bc_robot_path_configuredata = js.Deserialize<bc_robot_path_configure>(bodyStr);
                        RobotPathConfigureUpdateHandler robotPathConfigureUpdateHandler = new RobotPathConfigureUpdateHandler();
                        result = robotPathConfigureUpdateHandler.Execute(userName, clientip, type, bc_robot_path_configuredata);
                        break;
                    case "SelectRobotModelList":
                    case "RobotModelDelete":
                        data = js.Deserialize<Dictionary<string, object>>(bodyStr);
                        RobotModelControlHandler robotModelControlHandler = new RobotModelControlHandler();
                        result = robotModelControlHandler.Execute(userName, clientip, type, data);
                        break;
                    case "RobotModelAdd":
                    case "RobotModelUpdate":
                        bc_robot_model bc_robot_modeldata = js.Deserialize<bc_robot_model>(bodyStr);
                        RobotModelUpdateHandler robotModelUpdateHandler = new RobotModelUpdateHandler();
                        result = robotModelUpdateHandler.Execute(userName, clientip, type, bc_robot_modeldata);
                        break;
                    #endregion
                    //case "Pht600SetPort":
                    //    Dictionary<string, object> Pht600SetInfo = js.Deserialize<Dictionary<string, object>>(bodyStr);
                    //    Pht600SetPortHandler Pht600SetPortHandler = new Pht600SetPortHandler();
                    //    result = Pht600SetPortHandler.Execute(Pht600SetInfo);
                    //    break;
                    case "GetPortIDList":
                        GetPortIDListHandler GetPortIDListHandler = new GetPortIDListHandler();
                        result = GetPortIDListHandler.Execute();
                        break;
                }
                Logger.Info("返回：" + result.JsonSerializer());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                return result;
            }
        }

        public static string MidStrEx_New(string sourse, string startstr, string endstr)
        {
            Regex rg = new Regex("(?<=(" + startstr + "))[.\\s\\S]*?(?=(" + endstr + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(sourse).Value;
        }

        public string GetJsonType(string json)
        {
            try
            {

                WebSocketMessage rt = JsonConvert.DeserializeObject<WebSocketMessage>(json);
                return rt.header.messageName;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return "";
            }


        }
    }
}
