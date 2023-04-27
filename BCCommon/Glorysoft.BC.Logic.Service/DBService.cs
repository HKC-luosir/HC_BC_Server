using Glorysoft.Auto.Contract;

using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Service
{

    public class DBService : AbstractEventHandler, IDBService
    {
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        #region DbEquipmentService
        public List<EQPInfo> ViewEQP(string eqpid)
        {
            return dbEqp.ViewEQP(eqpid);
        }
        public bool UpdateEQPInfo(EQPInfo EQPInfo)
        {
            var res = dbEqp.UpdateEQPInfo(EQPInfo);
            if (res)
                dbEqp.InsertHisEQPInfo(EQPInfo);
            return res;
        }
        public bool InsertHisEQPInfo(EQPInfo EQPInfo)
        {
            return dbEqp.InsertHisEQPInfo(EQPInfo);
        }
        
        public bool InsertHisUnitResult(Unit Unit)
        {
            return dbEqp.InsertHisUnitResult(Unit);
        }
        public IList<Unit> ViewUnitList(Hashtable map)
        {
            return dbEqp.ViewUnitList(map);
        }
        public bool UpdateUnitInfo(Unit Unit)
        {
            var res = dbEqp.UpdateUnitInfo(Unit);
            if (res)
                dbEqp.InsertHisUnitResult(Unit);
            return res;
        }
        public bool UpdateSUnitInfo(SUnit SUnit)
        {
            var res = dbEqp.UpdateSUnitInfo(SUnit);
            if (res)
                dbEqp.InsertHisSUnitResult(SUnit);
            return res;
        }
        public bool InsertHisSUnitResult(SUnit SUnit)
        {
            return dbEqp.InsertHisSUnitResult(SUnit);
        }
        
        public bool UpdateSSUnitInfo(SSUnit SSUnit)
        {
            return dbEqp.UpdateSSUnitInfo(SSUnit);
        }
        public User FindUser(string userID)
        {
            return dbEqp.FindUser(userID);
        }
        public IList<User> GetUserList()
        {
            return dbEqp.GetUserList();
        }
        public bool InsertUser(User user)
        {
            return dbEqp.InsertUser(user);
        }
        public bool UpdateUser(User user)
        {
            return dbEqp.UpdateUser(user);
        }
        public bool DeleteUser(string userID)
        {
            return dbEqp.DeleteUser(userID);
        }

        public IList<ECInfo> ViewECList()
        {
            return dbEqp.ViewECList();
        }
        public IList<EventInfo> ViewEventList(string CEED)
        {
            return dbEqp.ViewEventList(CEED);
        }
        public IList<EventInfo> ViewAllEventList()
        {
            return dbEqp.ViewAllEventList();
        }
        public int UpdateECInfo(IList<ECInfo> lst)
        {

            return dbEqp.UpdateECInfo(lst);
        }

        public int UpdateAllEventInfo(string ceed)
        {

            return dbEqp.UpdateAllEventInfo(ceed);
        }

        public int UpdateEventInfo(Hashtable map)
        {

            return dbEqp.UpdateEventInfo(map);
        }



        public IList<EQPStatusRule> ViewEQPStatusRuleList(Hashtable map)
        {
            return dbEqp.ViewEQPStatusRuleList(map);
        }
        public bool InsertEQPStatusRule(EQPStatusRule EQPStatusRule)
        {
            return dbEqp.InsertEQPStatusRule(EQPStatusRule);
        }
        public int DeleteEQPStatusRule(Hashtable map)
        {
            return dbEqp.DeleteEQPStatusRule(map);
        }



        public IList<EQPStatusGroup> ViewEQPStatusGroupList(Hashtable map)
        {
            return dbEqp.ViewEQPStatusGroupList(map);
        }
        public bool InsertEQPStatusGroup(EQPStatusGroup EQPStatusGroup)
        {
            return dbEqp.InsertEQPStatusGroup(EQPStatusGroup);
        }
        public int DeleteEQPStatusGroup(Hashtable map)
        {
            return dbEqp.DeleteEQPStatusGroup(map);
        }


        public IList<VCR> ViewVCRList(Hashtable map)
        {
            return dbEqp.ViewVCRList(map);
        }
        public int UpdateVCR(VCR VCR)
        {
            return dbEqp.UpdateVCR(VCR);
        }
        public bool InsertHisVCRResult(VCR VCR)
        {
            return dbEqp.InsertHisVCRResult(VCR);
        }
        public IList<MaterialInfo> ViewMaterialInfo(MaterialInfo data)
        {
            return dbMaterial.ViewMaterialInfo(data);
        }
        public bool InsertMaterialHistory(MaterialInfo data)
        {
            return dbMaterial.InsertMaterialHistory(data);
        }
        public bool InsertMaterialInfo(MaterialInfo data)
        {
            var res = dbMaterial.InsertMaterialInfo(data);
            if(res)
                dbMaterial.InsertMaterialHistory(data);
            return res;
        }
        public bool UpdateMaterialInfo(MaterialInfo data)
        {
            var res = dbMaterial.UpdateMaterialInfo(data);
            if (res)
            {
                var currdata = dbMaterial.ViewMaterialInfo(data);
                if (currdata != null && currdata.Count > 0)
                    dbMaterial.InsertMaterialHistory(currdata[0]);
            }
            return res;
        }
        public bool DeleteMaterialInfo(MaterialInfo data)
        {
            return dbMaterial.DeleteMaterialInfo(data);
        }
        //public bool InsertMaterial(Material material)
        //{
        //    return dbMaterial.InsertMaterial(material);
        //}
        //public Material FindOneLocation(Hashtable map)
        //{
        //    return dbMaterial.FindOneLocation(map);
        //}
        //public bool InsertMaterial(Material material)
        //{
        //    return dbMaterial.InsertMaterial(material);
        //}
        //public bool UpdateMaterial(Material material)
        //{
        //    return dbMaterial.UpdateMaterial(material);
        //}
        //public IList<Material> ViewMaterial(Hashtable map)
        //{
        //    return dbMaterial.ViewMaterial(map);
        //}
        #endregion
        #region DbAlarmService
        public IList<AlarmInfo> ViewAlarmList(Hashtable map)
        {
            return dbAlarm.ViewAlarmList(map);
        }
        public AlarmInfo FindOneAlarm(Hashtable map)
        {
            return dbAlarm.FindOneAlarm(map);
        }
        public bool InsertAlarmInfo(AlarmInfo alarmInfo)
        {
            return dbAlarm.InsertAlarmInfo(alarmInfo);
        }
        //public bool ImportAlarmList(IList<AlarmInfo> lst)
        //{
        //    return dbAlarm.ImportAlarmList(lst);
        //}
        //public bool UpdateAlarmEnable(AlarmInfo AlarmInfo)
        //{
        //    return dbAlarm.UpdateAlarmEnable(AlarmInfo);
        //}
        //public bool UpdateAlarmInfo(Hashtable item)
        //{
        //    return dbAlarm.UpdateAlarmInfo(item);
        //}
        public bool DeleteAlarmInfo(Hashtable alarm)
        {
            return dbAlarm.DeleteAlarmInfo(alarm);
        }
        //public bool ClearAlarmList(string eqpID)
        //{
        //    return dbAlarm.ClearAlarmList(eqpID);
        //}
        public IList<AlarmInfo> ViewAlarmHistory(Hashtable map)
        {
            return dbAlarm.ViewAlarmHistory(map);
        }
        public bool InsertAlarmHistory(AlarmInfo item)
        {
            return dbAlarm.InsertAlarmHistory(item);
        }
        //public bool DeleteAlarmHistory(DateTime dtTime)
        //{
        //    return dbAlarm.DeleteAlarmHistory(dtTime);
        //}
        public bool InsertWipAlarmInfo(AlarmInfo item)
        {
            return dbAlarm.InsertWipAlarmInfo(item);
        }
        public int DeleteWipAlarmInfo(Hashtable map)
        {
            return dbAlarm.DeleteWipAlarmInfo(map);
        }
        public int DeleteWipAlarmMinInfo(Hashtable map)
        {
            return dbAlarm.DeleteWipAlarmMinInfo(map);
        }
        
        public IList<AlarmInfo> ViewWipAlarmList(Hashtable map)
        {
            return dbAlarm.ViewWipAlarmList(map);
        }
        #endregion
        #region DbPanelService
        public bool InsertHisGlassInfo(GlassInfo item)
        {
            return dbPanel.InsertHisGlassInfo(item);
        }
        public IList<GlassInfo> GetGlassInfoList(Hashtable map)
        {
            return dbPanel.GetGlassInfoList(map);
        }
        public IList<GlassInfo> GetGlassInfoListByCstID(Hashtable map)
        {
            return dbPanel.GetGlassInfoListByCstID(map);
        }
        public IList<GlassInfo> GetGlassInfoListByAlarm(Hashtable map)
        {
            return dbPanel.GetGlassInfoListByAlarm(map);
        }
        //public GlassInfo GetGlassInfo(Hashtable map)
        //{
        //    return dbPanel.GetGlassInfo(map);
        //}
        public int InsertGlassInfo(GlassInfo item)
        {
            var res = dbPanel.InsertGlassInfo(item);
            if (Convert.ToInt32(res) > 0)
            {
                item.ID = Convert.ToInt32(res);
                dbPanel.InsertHisGlassInfo(item);
            }
            return Convert.ToInt32(res);
        }
        public bool UpdateGlassInfo(GlassInfo item)
        {
            var res = dbPanel.UpdateGlassInfo(item);
            if (res)
            {
                if (!String.IsNullOrEmpty(item.FunctionName))
                {
                    dbPanel.InsertHisGlassInfo(item);
                }
            }
            return res;
        }
        public bool UpdateHisGlassInfo(GlassInfo item)
        {
            var res = dbPanel.UpdateHisGlassInfo(item);
            return res;
        }
        public bool UpdateGlassModelPosition(GlassInfo item)
        {
            return dbPanel.UpdateGlassModelPosition(item);
        }
        public int UpdateGlassSlotSatus(Hashtable map)
        {
            return dbPanel.UpdateGlassSlotSatus(map);
        }
        public int UpdateWIPSlotSatus(GlassInfo glass)
        {
            Hashtable map = new Hashtable();
            map.Add("CassetteSequenceNo", glass.CassetteSequenceNo);
            map.Add("SlotSequenceNo", glass.SlotSequenceNo);
            map.Add("SlotSatus", glass.SlotSatus);
            return dbPanel.UpdateWIPSlotSatus(map);
        }
        public int UpdateGlassCVDFlag(GlassInfo glass)
        {
            Hashtable map = new Hashtable();
            map.Add("CassetteSequenceNo", glass.CassetteSequenceNo);
            map.Add("SlotSequenceNo", glass.SlotSequenceNo);
            map.Add("CVDFlag", glass.CVDFlag);
            return dbPanel.UpdateGlassCVDFlag(map);
        }
        //public int UpdateGlassSlotSatus(string sql)
        //{
        //    return dbPanel.UpdateGlassSlotSatus(sql);
        //}


        public bool UpdateGlassInfoBeginDate(GlassInfo item)
        {
            return dbPanel.UpdateGlassInfoBeginDate(item);
        }
        public bool UpdateGlassInfoEndDate(GlassInfo item)
        {
            return dbPanel.UpdateGlassInfoEndDate(item);
        }
        public bool UpdateGlassInfoFetchDatetime(GlassInfo item)
        {
            return dbPanel.UpdateGlassInfoFetchDatetime(item);
        }
        //public int DeleteGlassInfo(GlassInfo item)
        //{
        //    return dbPanel.DeleteGlassInfo(item);
        //}
        public int DeleteGlassInfoList(Hashtable map)
        {
            return dbPanel.DeleteGlassInfoList(map);
        }
        public int DeleteGlassInforByDateTime()
        {
            return dbPanel.DeleteGlassInforByDateTime();
        }
        #region JobData 
        //public IList<JobData> GetJobDataList(Hashtable map)
        //{
        //    return dbPanel.GetJobDataList(map);
        //}
        //public bool InsertJobData(JobData item)
        //{
        //    return dbPanel.InsertJobData(item);
        //}
        //public bool UpdateJobData(JobData item)
        //{
        //    return dbPanel.UpdateJobData(item);
        //}
        //public int DeleteJobDataList(Hashtable map)
        //{
        //    return dbPanel.DeleteJobDataList(map);
        //}
        #endregion
        #region  TrayInfo
        public IList<TrayInfo> GetTrayInfoList(Hashtable map)
        {
            return dbPanel.GetTrayInfoList(map);
        }

        public bool InsertTrayInfo(TrayInfo item)
        {
            return dbPanel.InsertTrayInfo(item);
        }
        public bool UpdateTrayInfo(TrayInfo item)
        {
            return dbPanel.UpdateTrayInfo(item);
        }

        public int DeleteTrayInfo(TrayInfo item)
        {
            return dbPanel.DeleteTrayInfo(item);
        }
        public int DeleteTrayInfoList(Hashtable map)
        {
            return dbPanel.DeleteTrayInfoList(map);
        }
        public int DeleteTrayInfoByDateTime(string datetime)
        {
            return dbPanel.DeleteTrayInfoByDateTime(datetime);
        }
        #endregion

        #region  MaskInfo
        public IList<MaskInfo> GetMaskInfoList(Hashtable map)
        {
            return dbPanel.GetMaskInfoList(map);
        }

        public bool InsertMaskInfo(MaskInfo item)
        {
            return dbPanel.InsertMaskInfo(item);
        }
        public bool UpdateMaskInfo(MaskInfo item)
        {
            return dbPanel.UpdateMaskInfo(item);
        }

        public int DeleteMaskInfo(MaskInfo item)
        {
            return dbPanel.DeleteMaskInfo(item);
        }
        public int DeleteMaskInfoList(Hashtable map)
        {
            return dbPanel.DeleteMaskInfoList(map);
        }
        public int DeleteMaskInfoByDateTime(string datetime)
        {
            return dbPanel.DeleteMaskInfoByDateTime(datetime);
        }
        #endregion
        //public IList<SPanelInfo> GetSPanelList(Hashtable map)
        //{
        //    return dbPanel.GetSPanelList(map);
        //}
        //public bool InsertSPanelInfo(SPanelInfo item)
        //{
        //    return dbPanel.InsertSPanelInfo(item);
        //}
        //public bool UpdateSPanelInfo(SPanelInfo item)
        //{
        //    return dbPanel.UpdateSPanelInfo(item);
        //}
        //public int DeleteSPanelInfo(SPanelInfo item)
        //{
        //    return dbPanel.DeleteSPanelInfo(item);
        //}
        #endregion
        #region DbPortService
        public IList<PortInfo> ViewPortList(Hashtable Hashtable)
        {
            return dbPort.ViewPortList(Hashtable);
        }
        public bool UpdatePortInfo(PortInfo oPort)
        {
            var res = dbPort.UpdatePortInfo(oPort);
            if (res)
                dbPort.InsertHisPortInfoResult(oPort);
            return res;
        }
        public int UpdatePortWaitingforProcessingTime(Hashtable Hashtable)
        {
            return dbPort.UpdatePortWaitingforProcessingTime(Hashtable);
        }
        public bool InsertHisPortInfoResult(PortInfo PortInfo)
        {
            return dbPort.InsertHisPortInfoResult(PortInfo);
        }
        

        public int InsertCassette(Cassette item)
        {
            var res = dbPort.InsertCassette(item);
            if (Convert.ToInt32(res) > 0)
            {
                item.ID = Convert.ToInt32(res);
                dbPort.InsertHisCassette(item);
            }
            return Convert.ToInt32(res);
        }
        public bool UpdateCassette(Cassette item)
        {
            var res = dbPort.UpdateCassette(item);
            if (res)
                dbPort.UpdateHisCassette(item);
            return res;
        }
        public bool UpdateHisCassette(Cassette item)
        {
            return dbPort.UpdateHisCassette(item);
        }
        public bool UpdateCassetteHasCVD(Cassette item)
        {
            return dbPort.UpdateCassetteHasCVD(item);
        }
        public bool UpdateCassetteStartTime(Cassette item)
        {
            return dbPort.UpdateCassetteStartTime(item);
        }
        public bool UpdateCassetteEndTime(Cassette item)
        {
            return dbPort.UpdateCassetteEndTime(item);
        }
        public int DeleteCassetteList(Hashtable map)
        {
            return dbPort.DeleteCassetteList(map);
        }
        public int DeleteCassetteByDateTime()
        {
            return dbPort.DeleteCassetteByDateTime();
        }
        public IList<Cassette> GetCassetteList(Hashtable Hashtable)
        {
            return dbPort.GetCassetteList(Hashtable);
        }
        public bool InsertHisCassette(Cassette item)
        {
            return dbPort.InsertHisCassette(item);
        }

        //public bool InsertCassetteInfo(Cassette Cassette)
        //{
        //    return dbPort.InsertCassetteInfo(Cassette);
        //}
        //public IList<Cassette> ViewCassetteList(Hashtable Hashtable)
        //{
        //    return dbPort.ViewCassetteList(Hashtable);
        //}
        //public bool UpdateCassetteInfo(Cassette Cassette)
        //{
        //    return dbPort.UpdateCassetteInfo(Cassette);
        //}

        public bool Insertcfg_portgradegroup(cfg_portgradegroup data)
        {
            return dbPort.Insertcfg_portgradegroup(data);
        }
        public IList<cfg_portgradegroup> Viewcfg_portgradegroup(Hashtable data)
        {
            return dbPort.Viewcfg_portgradegroup(data);
        }
        public bool Updatecfg_portgradegroup(cfg_portgradegroup data)
        {
            return dbPort.Updatecfg_portgradegroup(data);
        }
        public bool Deletecfg_portgradegroup(Hashtable data)
        {
            return dbPort.Deletecfg_portgradegroup(data);
        }
        #endregion


        #region Recipe
        //public bool InsertRecipe(Recipe item)
        //{
        //    return dbRecipe.InsertRecipe(item);
        //}
        //public bool UpdateRecipe(Recipe item)
        //{
        //    return dbRecipe.UpdateRecipe(item);
        //}
        //public bool DeleteRecipe(Recipe item)
        //{
        //    return dbRecipe.DeleteRecipe(item);
        //}
        //public IList<Recipe> GetRecipeList(Recipe recipe)
        //{
        //    return dbRecipe.GetRecipeList(recipe);
        //}
        public IList<RecipeParameter> GetRecipeParameterList(RecipeParameter RecipeParameter)
        {
            return dbRecipe.GetRecipeParameterList(RecipeParameter);
        }
        public bool InsertRecipeParameter(RecipeParameter item)
        {
            return dbRecipe.InsertRecipeParameter(item);
        }
        public int UpdateRecipeParameter(RecipeParameter item)
        {
            return dbRecipe.UpdateRecipeParameter(item);
        }
        public int DeleteRecipeParameter(RecipeParameter item)
        {
            return dbRecipe.DeleteRecipeParameter(item);
        }




        public IList<PPIDAndRecipe> GetPPIDAndRecipeList(PPIDAndRecipe PPIDAndRecipe)
        {
            return dbRecipe.GetPPIDAndRecipeList(PPIDAndRecipe);
        }
        public bool InsertPPIDAndRecipe(PPIDAndRecipe item)
        {
            return dbRecipe.InsertPPIDAndRecipe(item);
        }
        public int DeletePPIDAndRecipe(PPIDAndRecipe item)
        {
            return dbRecipe.DeletePPIDAndRecipe(item);
        }

        public IList<ProcessModeMap> GetProcessModeMapList(Hashtable Hashtable)
        {
            return dbRecipe.GetProcessModeMapList(Hashtable);
        }
        public bool InsertProcessModeMap(ProcessModeMap item)
        {
            return dbRecipe.InsertProcessModeMap(item);
        }
        public int DeleteProcessModeMap(ProcessModeMap item)
        {
            return dbRecipe.DeleteProcessModeMap(item);
        }




        public bool InsertMIXRunConfig(MIXRunConfig item)
        {
            return dbRecipe.InsertMIXRunConfig(item);
        }
        public int DeleteMIXRunConfig(Hashtable item)
        {
            return dbRecipe.DeleteMIXRunConfig(item);
        }
        public IList<MIXRunConfig> GetMIXRunConfigList(Hashtable Hashtable)
        {
            return dbRecipe.GetMIXRunConfigList(Hashtable);
        }
        public int UpdateMIXRunConfig(MIXRunConfig item)
        {
            return dbRecipe.UpdateMIXRunConfig(item);
        }


        public bool InsertMIXRunInputRatio(MIXRunInputRatio item)
        {
            return dbRecipe.InsertMIXRunInputRatio(item);
        }
        public int DeleteMIXRunInputRatio(Hashtable item)
        {
            return dbRecipe.DeleteMIXRunInputRatio(item);
        }
        public IList<MIXRunInputRatio> GetMIXRunInputRatioList(Hashtable Hashtable)
        {
            return dbRecipe.GetMIXRunInputRatioList(Hashtable);
        }
        public int UpdateMIXRunInputRatio(MIXRunInputRatio item)
        {
            return dbRecipe.UpdateMIXRunInputRatio(item);
        }


        public IList<OperationMode> GetOperationModeList(Hashtable Hashtable)
        {
            return dbRecipe.GetOperationModeList(Hashtable);
        }
        #endregion
        #region DVData
        public IList<DVData> ViewDVDataList(Hashtable map)
        {
            return dbDVData.ViewDVDataList(map);
        }
        public bool InsertDVData(DVData DVData)
        {
            return dbDVData.InsertDVData(DVData);
        }
        public int UpdateDVData(DVData DVData)
        {
            return dbDVData.UpdateDVData(DVData);
        }
        public int DeleteDVData(Hashtable map)
        {
            return dbDVData.DeleteDVData(map);
        }

        public IList<SVData> ViewSVDataList(Hashtable map)
        {
            return dbDVData.ViewSVDataList(map);
        }
        public bool InsertSVData(SVData SVData)
        {
            return dbDVData.InsertSVData(SVData);
        }
        public int UpdateSVData(SVData SVData)
        {
            return dbDVData.UpdateSVData(SVData);
        }
        public int DeleteSVData(Hashtable map)
        {
            return dbDVData.DeleteSVData(map);
        }
        #endregion
        #region Config
        //public SystemConfig ViewSystemConfig(string eqpid)
        //{
        //    return dbConfig.ViewSystemConfig(eqpid);
        //}

        public IList<CFGS1F5> ViewCFGS1F5(Hashtable map)
        {
            return dbConfig.ViewCFGS1F5(map);
        }
        public bool InsertCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return dbConfig.InsertCFGS1F5(CFGS1F5);
        }
        public int UpdateCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return dbConfig.UpdateCFGS1F5(CFGS1F5);
        }

        public int DeleteCFGS1F5(CFGS1F5 CFGS1F5)
        {
            return dbConfig.DeleteCFGS1F5(CFGS1F5);
        }
        public IList<GlassExistencePosition> ViewGlassExistencePosition(Hashtable map)
        {
            return dbConfig.ViewGlassExistencePosition(map);
        }
        public int UpdateGlassExistencePosition(GlassExistencePosition GlassExistencePosition)
        {
            return dbConfig.UpdateGlassExistencePosition(GlassExistencePosition);
        }

        public IList<OPILink> ViewOPILink(Hashtable map)
        {
            return dbConfig.ViewOPILink(map);
        }

        public IList<CFGOLDPriority> ViewCFGOLDPriority(Hashtable map)
        {
            return dbConfig.ViewCFGOLDPriority(map);
        }
        public int UpdateCFGOLDPriority(CFGOLDPriority CFGOLDPriority)
        {
            return dbConfig.UpdateCFGOLDPriority(CFGOLDPriority);
        }
        #endregion

        #region User
        public IList<bc_sys_setting> Viewbc_sys_setting(Hashtable hashtable)
        {
            return dbUser.Viewbc_sys_setting(hashtable);
        }
        public bool Updatebc_sys_setting(Hashtable hashtable)
        {
            return dbUser.Updatebc_sys_setting(hashtable);
        }
        public IList<GlassInfo> GetUnitIdGlassInfoList(Hashtable map)
        {
            return dbPanel.GetUnitIdGlassInfoList(map);
        }
        public IList<BC_Group> ViewUserGroupList(Hashtable hashtable)
        {
            return dbUser.ViewUserGroupList(hashtable);
        }

        public bool InsertUserGroup(BC_Group BC_Group)
        {
            return dbUser.InsertUserGroup(BC_Group);
        }
        public bool InsertGroupAuthority(Group_Authority Group_Authority)
        {
            return dbUser.InsertGroupAuthority(Group_Authority);
        }

        public IList<Group_Authority> ViewUserGroupAuthority(string userid)
        {
            return dbUser.ViewUserGroupAuthority(userid);
        }

        public bool DeleteUserGroup(string groupid)
        {
            return dbUser.DeleteUserGroup(groupid);
        }
        public IList<Group_Authority> ViewGetGroupIdAuthority(string groupid)
        {
            return dbUser.ViewGetGroupIdAuthority(groupid);
        }

        public bool DeleteGroupAuthority(string groupid)
        {
            return dbUser.DeleteGroupAuthority(groupid);
        }

        public IList<AllTable> ViewAllTableName()
        {
            return dbUser.ViewAllTableName();
        }
        public IList<TableStructure> ViewTableStructure(string tablename)
        {
            return dbUser.ViewTableStructure(tablename);
        }
        public IList<BcGroupAuthority> ViewBcGroupAuthority(Hashtable hashtable)
        {
            return dbUser.ViewBcGroupAuthority(hashtable);
        }
        public IList<BcRobotConfigure> ViewBcRobotConfigure(Hashtable hashtable)
        {
            return dbUser.ViewBcRobotConfigure(hashtable);
        }
        public IList<BcRobotGroupCconfigure> ViewBcRobotGroupCconfigure(Hashtable hashtable)
        {
            return dbUser.ViewBcRobotGroupCconfigure(hashtable);
        }
        public IList<DisCol> ViewDisCol(Hashtable DisCol)
        {
            return dbUser.ViewDisCol(DisCol);
        }
        public IList<bc_robot_linksignal_configure> Viewbc_robot_linksignal_configure(Hashtable hashtable)
        {
            return dbUser.Viewbc_robot_linksignal_configure(hashtable);
        }
        public IList<bc_robot_path_configure> Viewbc_robot_path_configure_user(Hashtable hashtable)
        {
            return dbUser.Viewbc_robot_path_configure_user(hashtable);
        }

        public IList<cfg_alarm> Viewcfg_alarm(Hashtable hashtable)
        {
            return dbUser.Viewcfg_alarm(hashtable);
        }
        public IList<cfg_ceid> Viewcfg_ceid(Hashtable hashtable) { return dbUser.Viewcfg_ceid(hashtable); }
        public IList<cfg_ecid> Viewcfg_ecid(Hashtable hashtable) { return dbUser.Viewcfg_ecid(hashtable); }
        public IList<cfg_eqp> Viewcfg_eqp(Hashtable hashtable) { return dbUser.Viewcfg_eqp(hashtable); }
        public IList<cfg_port> Viewcfg_port(Hashtable hashtable) { return dbUser.Viewcfg_port(hashtable); }
        public IList<cfg_ssunit> Viewcfg_ssunit(Hashtable hashtable) { return dbUser.Viewcfg_ssunit(hashtable); }
        public IList<cfg_sunit> Viewcfg_sunit(Hashtable hashtable) { return dbUser.Viewcfg_sunit(hashtable); }
        public IList<cfg_unit> Viewcfg_unit(Hashtable hashtable) { return dbUser.Viewcfg_unit(hashtable); }
        public IList<his_alarm> Viewhis_alarm(Hashtable hashtable) { return dbUser.Viewhis_alarm(hashtable); }
        public IList<his_alarm> Viewhis_alarmCount(Hashtable hashtable) { return dbUser.Viewhis_alarmCount(hashtable); }
        public IList<his_carrier> Viewhis_carrier(Hashtable hashtable) { return dbUser.Viewhis_carrier(hashtable); }
        public IList<his_maskinfo> Viewhis_maskinfo(Hashtable hashtable) { return dbUser.Viewhis_maskinfo(hashtable); }
        public IList<his_material> Viewhis_material(Hashtable hashtable) { return dbUser.Viewhis_material(hashtable); }
        public IList<his_material> Viewhis_materialCount(Hashtable hashtable) { return dbUser.Viewhis_materialCount(hashtable); }
        public IList<his_packingbox> Viewhis_packingbox(Hashtable hashtable) { return dbUser.Viewhis_packingbox(hashtable); }
        public IList<his_recipe> Viewhis_recipe(Hashtable hashtable) { return dbUser.Viewhis_recipe(hashtable); }
        public IList<his_recipeppidmap> Viewhis_recipeppidmap(Hashtable hashtable) { return dbUser.Viewhis_recipeppidmap(hashtable); }
        public IList<his_spanel> Viewhis_spanel(Hashtable hashtable) { return dbUser.Viewhis_spanel(hashtable); }
        public IList<his_trayinfo> Viewhis_trayinfo(Hashtable hashtable) { return dbUser.Viewhis_trayinfo(hashtable); }
        public IList<wip_cstbox> Viewwip_cstbox(Hashtable hashtable) { return dbUser.Viewwip_cstbox(hashtable); }
        public IList<cfg_systemconfig> Viewcfg_systemconfig(Hashtable hashtable) { return dbUser.Viewcfg_systemconfig(hashtable); }
        public IList<wip_glassinfo> Viewwip_glassinfo(Hashtable hashtable) { return dbUser.Viewwip_glassinfo(hashtable); }
        public IList<his_user> Viewhis_user(Hashtable hashtable) { return dbUser.Viewhis_user(hashtable); }
        public IList<his_glassinfo> Viewhis_glassinfo(Hashtable hashtable) { return dbUser.Viewhis_glassinfo(hashtable); }
        public IList<historydata> Viewhis_glassinfoCount(Hashtable hashtable) { return dbUser.Viewhis_glassinfoCount(hashtable); }
        public IList<bc_user_group> Viewbc_user_group(Hashtable hashtable) { return dbUser.Viewbc_user_group(hashtable); }
        public IList<wip_alarm> Viewwip_alarm(Hashtable hashtable) { return dbUser.Viewwip_alarm(hashtable); }
        public bool Deletecfg_eqp(string eqpid)
        {
            return dbUser.Deletecfg_eqp(eqpid);
        }
        public bool Deletecfg_port(Hashtable portid)
        {
            return dbUser.Deletecfg_port(portid);
        }
        public bool Deletecfg_unit(string unitid)
        {
            return dbUser.Deletecfg_unit(unitid);
        }
        public bool Deletewip_glassinfo(string glsid)
        {
            return dbUser.Deletewip_glassinfo(glsid);
        }
        public bool Deletewip_alarm(Hashtable hashtable)
        {
            return dbUser.Deletewip_alarm(hashtable);
        }
        public IList<bc_eqp_command_conf> Viewbc_eqp_command_conf(Hashtable hashtable)
        {
            return dbUser.Viewbc_eqp_command_conf(hashtable);
        }
        public IList<bc_eqp_command_para> Viewbc_eqp_command_para(Hashtable hashtable)
        {
            return dbUser.Viewbc_eqp_command_para(hashtable);
        }

        public IList<wip_glassinfo> Viewwip_glassinfoSel(Hashtable hashtable)
        {
            return dbUser.Viewwip_glassinfoSel(hashtable);
        }
        public IList<cfg_eqpstatusrule> Viewcfg_eqpstatusrule(Hashtable hashtable) { return dbUser.Viewcfg_eqpstatusrule(hashtable); }
        public bool Deletecfg_eqpstatusrule(Hashtable hashtable)
        {
            return dbUser.Deletecfg_eqpstatusrule(hashtable);
        }
        public bool Insertcfg_eqpstatusrule(Hashtable hashtable)
        {
            return dbUser.Insertcfg_eqpstatusrule(hashtable);
        }
        //public IList<bc_robot_model> Viewbc_robot_model(Hashtable hashtable) { return dbUser.Viewbc_robot_model(hashtable); }
        public bool UpdateDispatchMode(Hashtable hashtable) { return dbUser.UpdateDispatchMode(hashtable); }
        public bool Deletecfg_recipeppidmap(Hashtable hashtable)
        {
            return dbUser.Deletecfg_recipeppidmap(hashtable);
        }
        public bool Deletecfg_processmodemap(Hashtable hashtable)
        {
            return dbUser.Deletecfg_processmodemap(hashtable);
        }
        public bool Insertcfg_recipeppidmap(Hashtable hashtable)
        {
            return dbUser.Insertcfg_recipeppidmap(hashtable);
        }
        public bool Insertcfg_processmodemap(Hashtable hashtable)
        {
            return dbUser.Insertcfg_processmodemap(hashtable);
        }
        public IList<cfg_operationmode> Viewcfg_operationmode(Hashtable hashtable) { return dbUser.Viewcfg_operationmode(hashtable); }
        public bool Deletecfg_operationmode(Hashtable hashtable)
        {
            return dbUser.Deletecfg_operationmode(hashtable);
        }
        public bool Insertcfg_operationmode(Hashtable hashtable)
        {
            return dbUser.Insertcfg_operationmode(hashtable);
        }

        public IList<wip_cassette> Viewwip_cassette(Hashtable hashtable)
        {
            return dbUser.Viewwip_cassette(hashtable);
        }
        public bool Deletewip_cassette(Hashtable hashtable)
        {
            return dbUser.Deletewip_cassette(hashtable);
        }
        //public bool Deletebc_robot_model(Hashtable hashtable)
        //{
        //    return dbUser.Deletebc_robot_model(hashtable);
        //}
        //public bool Insertbc_robot_model(Hashtable hashtable)
        //{
        //    return dbUser.Insertbc_robot_model(hashtable);
        //}

        public bool Updatecfg_dvdata(Hashtable hashtable)
        {
            return dbUser.Updatecfg_dvdata(hashtable);
        }
        public bool Updatecfg_svdata(Hashtable hashtable)
        {
            return dbUser.Updatecfg_svdata(hashtable);
        }
        public bool Updatecfg_recipeparameter(Hashtable hashtable)
        {
            return dbUser.Updatecfg_recipeparameter(hashtable);
        }
        public IList<cfg_eqpstatusgroup> Viewcfg_eqpstatusgroup(Hashtable hashtable)
        {
            return dbUser.Viewcfg_eqpstatusgroup(hashtable);
        }
        public bool Updatecfg_eqpstatusgroup(Hashtable hashtable)
        {
            return dbUser.Updatecfg_eqpstatusgroup(hashtable);
        }
        public IList<cfg_mixrunconfig> Viewcfg_mixrunconfig(Hashtable hashtable)
        {
            return dbUser.Viewcfg_mixrunconfig(hashtable);
        }
        public bool Updatecfg_mixrunconfig(Hashtable hashtable)
        {
            return dbUser.Updatecfg_mixrunconfig(hashtable);
        }
        public IList<cfg_alarmspec> Viewcfg_alarmspec(Hashtable hashtable)
        {
            return dbUser.Viewcfg_alarmspec(hashtable);
        }
        public bool Updatecfg_alarmspec(Hashtable hashtable)
        {
            return dbUser.Updatecfg_alarmspec(hashtable);
        }
        public IList<his_opilog> Viewhis_opilog(Hashtable hashtable)
        {
            return dbUser.Viewhis_opilog(hashtable);
        }
        public IList<his_opilog> Viewhis_opilogCount(Hashtable hashtable)
        {
            return dbUser.Viewhis_opilogCount(hashtable);
        }
        public IList<his_unit> Viewhis_unit(Hashtable hashtable)
        {
            return dbUser.Viewhis_unit(hashtable);
        }
        public IList<his_unit> Viewhis_unitCount(Hashtable hashtable)
        {
            return dbUser.Viewhis_unitCount(hashtable);
        }
        public IList<his_cassette> Viewhis_cassette(Hashtable hashtable)
        {
            return dbUser.Viewhis_cassette(hashtable);
        }
        public IList<his_cassette> Viewhis_cassetteCount(Hashtable hashtable)
        {
            return dbUser.Viewhis_cassetteCount(hashtable);
        }
        public IList<GlassInfoEvent> ViewGlassInfoEvent()
        {
            return dbUser.ViewGlassInfoEvent();
        }
        public IList<cfg_glassexistenceposition> Viewcfg_glassexistenceposition(Hashtable hashtable)
        {
            return dbUser.Viewcfg_glassexistenceposition(hashtable);
        }
        public bool Inserthis_opilog(Hashtable hashtable)
        {
            return dbUser.Inserthis_opilog(hashtable);
        }
        public bool Insertcfg_mixrunconfig(Hashtable hashtable)
        {
            return dbUser.Insertcfg_mixrunconfig(hashtable);
        }
        public bool Deletecfg_mixrunconfig(int id)
        {
            return dbUser.Deletecfg_mixrunconfig(id);
        }
        public IList<ModePath> Viewbc_robot_path_configureDis()
        {
            return dbUser.Viewbc_robot_path_configureDis();
        }
        public bool Updatecfg_recipeppidmap(Hashtable hashtable)
        {
            return dbUser.Updatecfg_recipeppidmap(hashtable);
        }
        public bool Updatecfg_processmodemap(Hashtable hashtable)
        {
            return dbUser.Updatecfg_processmodemap(hashtable);
        }
        //public bool Updatebc_robot_model(Hashtable hashtable)
        //{
        //    return dbUser.Updatebc_robot_model(hashtable);
        //}
        public IList<his_eqp> Viewhis_eqp(Hashtable hashtable)
        {
            return dbUser.Viewhis_eqp(hashtable);
        }
        public IList<his_sunit> Viewhis_sunit(Hashtable hashtable) { return dbUser.Viewhis_sunit(hashtable); }
        public IList<his_port> Viewhis_port(Hashtable hashtable) { return dbUser.Viewhis_port(hashtable); }
        public IList<his_port> Viewhis_portCount(Hashtable hashtable) { return dbUser.Viewhis_portCount(hashtable); }
        public IList<his_vcr> Viewhis_vcr(Hashtable hashtable) { return dbUser.Viewhis_vcr(hashtable); }
        public bool Deletehis_opilog(int id)
        {
            return dbUser.Deletehis_opilog(id);
        }
        public bool DeleteAllhis_opilog()
        {
            return dbUser.DeleteAllhis_opilog();
        }
        public IList<his_robotcommand> Viewhis_robotcommand(Hashtable hashtable) { return dbUser.Viewhis_robotcommand(hashtable); }
        
        public bool DeleteAllhis_robotcommand()
        {
            return dbUser.DeleteAllhis_robotcommand();
        }
        public bool UpdateIsColdRun(Hashtable has)
        {
            return dbUser.UpdateIsColdRun(has);
        }
        #endregion
        #region Test
        public bool InsertTestLog(TestLog item)
        {
            return dbtest.InsertTestLog(item);
        }
        #endregion
        #region UseRecipeList
        public IList<UseRecipeList> GetUseRecipeList(Hashtable Hashtable)
        {
            return dbRecipe.GetUseRecipeList(Hashtable);
        }

        public bool InsertUseRecipeList(UseRecipeList item)
        {
            return dbRecipe.InsertUseRecipeList(item);
        }

        public int DeleteUseRecipeList(UseRecipeList item)
        {
            return dbRecipe.DeleteUseRecipeList(item);
        }
        #endregion

        #region DbPalletService
        public IList<his_pallet> Viewhis_palletList(Hashtable map)
        {
            return dbPallet.Viewhis_palletList(map);
        }
        public IList<his_pallet> Viewhis_palletListCount(Hashtable map)
        {
            return dbPallet.Viewhis_palletListCount(map);
        }
        public int Inserthis_pallet(his_pallet data)
        {
            int id = 0;
            var res = dbPallet.Inserthis_pallet(data);
            int.TryParse(res.ToString(), out id);
            return id;
        }
        public bool Updatehis_pallet(his_pallet data)
        {
            var res = dbPallet.Updatehis_pallet(data);
            return res;
        }
        #endregion

        #region DbProcessEndService
        public int Insertwip_processend(wip_processend data)
        {
            int id = 0;
            var res = dbProcessEnd.Insertwip_processend(data);
            int.TryParse(res.ToString(), out id);
            return id;
        }
        public bool Insertwip_processend_glass(wip_processend_glass data)
        {
            return dbProcessEnd.Insertwip_processend_glass(data);
        }
        public IList<wip_processend> Viewwip_processendList(Hashtable data)
        {
            return dbProcessEnd.Viewwip_processendList(data);
        }
        public IList<wip_processend_glass> Viewwip_processend_glassList(Hashtable data)
        {
            return dbProcessEnd.Viewwip_processend_glassList(data);
        }
        public bool Updatewip_processend(wip_processend data)
        {
            return dbProcessEnd.Updatewip_processend(data);
        }
        public bool Updatewip_processend_glass(wip_processend_glass data)
        {
            return dbProcessEnd.Updatewip_processend_glass(data);
        }
        public bool Deletewip_processend(Hashtable data)
        {
            return dbProcessEnd.Deletewip_processend(data);
        }
        public bool Deletewip_processend_glass(Hashtable data)
        {
            return dbProcessEnd.Deletewip_processend_glass(data);
        }
        #endregion

        #region DbEqpProfileService
        public int Insertcfg_eqpprofile(cfg_eqpprofile data)
        {
            int id = 0;
            var res = dbEqpProfile.Insertcfg_eqpprofile(data);
            int.TryParse(res.ToString(), out id);
            return id;
        }
        public int Insertcfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data)
        {
            int id = 0;
            var res = dbEqpProfile.Insertcfg_eqpprofile_itemgroup(data);
            int.TryParse(res.ToString(), out id);
            return id;
        }
        public int Insertcfg_eqpprofile_item(cfg_eqpprofile_item data)
        {
            int id = 0;
            var res = dbEqpProfile.Insertcfg_eqpprofile_item(data);
            int.TryParse(res.ToString(), out id);
            return id;
        }
        public IList<cfg_eqpprofile> Viewcfg_eqpprofile(Hashtable data)
        {
            return dbEqpProfile.Viewcfg_eqpprofile(data);
        }
        public IList<cfg_eqpprofile_itemgroup> Viewcfg_eqpprofile_itemgroup(Hashtable data)
        {
            return dbEqpProfile.Viewcfg_eqpprofile_itemgroup(data);
        }
        public IList<cfg_eqpprofile_item> Viewcfg_eqpprofile_item(Hashtable data)
        {
            return dbEqpProfile.Viewcfg_eqpprofile_item(data);
        }
        public bool Updatecfg_eqpprofile(cfg_eqpprofile data)
        {
            return dbEqpProfile.Updatecfg_eqpprofile(data);
        }
        public bool Updatecfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data)
        {
            return dbEqpProfile.Updatecfg_eqpprofile_itemgroup(data);
        }
        public bool Updatecfg_eqpprofile_item(cfg_eqpprofile_item data)
        {
            return dbEqpProfile.Updatecfg_eqpprofile_item(data);
        }
        public bool Deletecfg_eqpprofile(Hashtable data)
        {
            return dbEqpProfile.Deletecfg_eqpprofile(data);
        }
        public bool Deletecfg_eqpprofile_itemgroup(Hashtable data)
        {
            return dbEqpProfile.Deletecfg_eqpprofile_itemgroup(data);
        }
        public bool Deletecfg_eqpprofile_item(Hashtable data)
        {
            return dbEqpProfile.Deletecfg_eqpprofile_item(data);
        }
        #endregion

        #region DbRobotService
        public bool Insertbc_robot_configure(bc_robot_configure data)
        {
            return dbRobot.Insertbc_robot_configure(data);
        }
        public IList<bc_robot_configure> Viewbc_robot_configure(Hashtable data)
        {
            return dbRobot.Viewbc_robot_configure(data);
        }
        public bool Updatebc_robot_configure(bc_robot_configure data)
        {
            return dbRobot.Updatebc_robot_configure(data);
        }
        public bool Deletebc_robot_configure(Hashtable data)
        {
            return dbRobot.Deletebc_robot_configure(data);
        }

        public bool Insertbc_robot_path_configure(bc_robot_path_configure data)
        {
            return dbRobot.Insertbc_robot_path_configure(data);
        }
        public IList<bc_robot_path_configure> Viewbc_robot_path_configure(Hashtable data)
        {
            return dbRobot.Viewbc_robot_path_configure(data);
        }
        public bool Updatebc_robot_path_configure(bc_robot_path_configure data)
        {
            return dbRobot.Updatebc_robot_path_configure(data);
        }
        public bool Deletebc_robot_path_configure(Hashtable data)
        {
            return dbRobot.Deletebc_robot_path_configure(data);
        }

        public bool Insertbc_robot_model(bc_robot_model data)
        {
            return dbRobot.Insertbc_robot_model(data);
        }
        public IList<bc_robot_model> Viewbc_robot_model(Hashtable data)
        {
            return dbRobot.Viewbc_robot_model(data);
        }
        public bool Updatebc_robot_model(bc_robot_model data)
        {
            return dbRobot.Updatebc_robot_model(data);
        }
        public bool Deletebc_robot_model(Hashtable data)
        {
            return dbRobot.Deletebc_robot_model(data);
        }
        #endregion
    }
}
