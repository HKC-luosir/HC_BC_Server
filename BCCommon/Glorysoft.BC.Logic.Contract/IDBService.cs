using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{

    public interface IDBService : IAutoRegister
    {
        #region DbEquipmentService
        List<EQPInfo> ViewEQP(string sLine);
        bool UpdateEQPInfo(EQPInfo EQPInfo);
        bool InsertHisEQPInfo(EQPInfo EQPInfo);
        bool InsertHisUnitResult(Unit Unit);
        IList<Unit> ViewUnitList(Hashtable map);
        bool UpdateUnitInfo(Unit Unit);
        bool UpdateSUnitInfo(SUnit SUnit);
        bool InsertHisSUnitResult(SUnit SUnit);

        bool UpdateSSUnitInfo(SSUnit SSUnit);
        User FindUser(string userID);
        IList<User> GetUserList();
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(string userID);

        IList<ECInfo> ViewECList();
        IList<EventInfo> ViewEventList(string CEED);
        IList<EventInfo> ViewAllEventList();
        int UpdateECInfo(IList<ECInfo> lst);

        int UpdateAllEventInfo(string ceed);

        int UpdateEventInfo(Hashtable map);


        IList<EQPStatusRule> ViewEQPStatusRuleList(Hashtable map);
        bool InsertEQPStatusRule(EQPStatusRule EQPStatusRule);
        int DeleteEQPStatusRule(Hashtable map);



        IList<EQPStatusGroup> ViewEQPStatusGroupList(Hashtable map);
        bool InsertEQPStatusGroup(EQPStatusGroup EQPStatusGroup);
        int DeleteEQPStatusGroup(Hashtable map);


        IList<VCR> ViewVCRList(Hashtable map);
        int UpdateVCR(VCR VCR);
        bool InsertHisVCRResult(VCR VCR);
        //Material FindOneLocation(Hashtable map);
        //bool InsertMaterial(Material material);
        //bool UpdateMaterial(Material material);
        //IList<Material> ViewMaterial(Hashtable map);
        IList<MaterialInfo> ViewMaterialInfo(MaterialInfo data);
        bool InsertMaterialHistory(MaterialInfo data);
        bool InsertMaterialInfo(MaterialInfo data);
        bool UpdateMaterialInfo(MaterialInfo data);
        bool DeleteMaterialInfo(MaterialInfo data);
        #endregion
        #region DbAlarmService
        IList<AlarmInfo> ViewAlarmList(Hashtable map);
        AlarmInfo FindOneAlarm(Hashtable map);
        bool InsertAlarmInfo(AlarmInfo alarmInfo);
        // bool ImportAlarmList(IList<AlarmInfo> lst);
        // bool UpdateAlarmEnable(AlarmInfo AlarmInfo);
        // bool UpdateAlarmInfo(Hashtable item);
        bool DeleteAlarmInfo(Hashtable alarm);
        //bool ClearAlarmList(string eqpID);
        IList<AlarmInfo> ViewAlarmHistory(Hashtable map);
        bool InsertAlarmHistory(AlarmInfo item);
        // bool DeleteAlarmHistory(DateTime dtTime);
        bool InsertWipAlarmInfo(AlarmInfo item);
        int DeleteWipAlarmInfo(Hashtable alarm);
        int DeleteWipAlarmMinInfo(Hashtable alarm);

        IList<AlarmInfo> ViewWipAlarmList(Hashtable map);
        #endregion
        #region DbPanelService
        bool InsertHisGlassInfo(GlassInfo item);
        IList<GlassInfo> GetGlassInfoList(Hashtable map);

        IList<GlassInfo> GetGlassInfoListByCstID(Hashtable map);
        IList<GlassInfo> GetGlassInfoListByAlarm(Hashtable map);
        //GlassInfo GetGlassInfo(Hashtable map);
        int InsertGlassInfo(GlassInfo item);
        bool UpdateGlassInfo(GlassInfo item);
        bool UpdateHisGlassInfo(GlassInfo item);
        bool UpdateGlassModelPosition(GlassInfo item);
        int UpdateGlassSlotSatus(Hashtable map);
        int UpdateWIPSlotSatus(GlassInfo glass);
        int UpdateGlassCVDFlag(GlassInfo glass);
        //int UpdateGlassSlotSatus(string sql);
        bool UpdateGlassInfoBeginDate(GlassInfo item);
        bool UpdateGlassInfoEndDate(GlassInfo item);
        bool UpdateGlassInfoFetchDatetime(GlassInfo item);
        //int DeleteGlassInfo(GlassInfo item);
        int DeleteGlassInfoList(Hashtable map);
        int DeleteGlassInforByDateTime();
        #region JobData 
        //IList<JobData> GetJobDataList(Hashtable map);
        //bool InsertJobData(JobData item);
        //bool UpdateJobData(JobData item);
        //int DeleteJobDataList(Hashtable map);
        #endregion
        #region  TrayInfo
        IList<TrayInfo> GetTrayInfoList(Hashtable map);

        bool InsertTrayInfo(TrayInfo item);
        bool UpdateTrayInfo(TrayInfo item);

        int DeleteTrayInfo(TrayInfo item);
        int DeleteTrayInfoList(Hashtable map);
        int DeleteTrayInfoByDateTime(string datetime);
        #endregion

        #region  MaskInfo
        IList<MaskInfo> GetMaskInfoList(Hashtable map);

        bool InsertMaskInfo(MaskInfo item);
        bool UpdateMaskInfo(MaskInfo item);

        int DeleteMaskInfo(MaskInfo item);
        int DeleteMaskInfoList(Hashtable map);
        int DeleteMaskInfoByDateTime(string datetime);
        #endregion
        #endregion



        #region DbPortService
        IList<PortInfo> ViewPortList(Hashtable Hashtable);
        bool UpdatePortInfo(PortInfo oPort);
        int UpdatePortWaitingforProcessingTime(Hashtable Hashtable);
        bool InsertHisPortInfoResult(PortInfo PortInfo);


        int InsertCassette(Cassette item);
        bool UpdateCassette(Cassette item);
        bool UpdateHisCassette(Cassette item);
        bool UpdateCassetteHasCVD(Cassette item);
        bool UpdateCassetteStartTime(Cassette item);
        bool UpdateCassetteEndTime(Cassette item);
        int DeleteCassetteList(Hashtable map);
        int DeleteCassetteByDateTime();
        IList<Cassette> GetCassetteList(Hashtable Hashtable);
        bool InsertHisCassette(Cassette item);
        //bool InsertCassetteInfo(Cassette Cassette);
        //IList<Cassette> ViewCassetteList(Hashtable Hashtable);
        //bool UpdateCassetteInfo(Cassette Cassette);

        bool Insertcfg_portgradegroup(cfg_portgradegroup data);
        IList<cfg_portgradegroup> Viewcfg_portgradegroup(Hashtable data);
        bool Updatecfg_portgradegroup(cfg_portgradegroup data);
        bool Deletecfg_portgradegroup(Hashtable data);
        #endregion

        #region Recipe
        IList<RecipeParameter> GetRecipeParameterList(RecipeParameter RecipeParameter);
        bool InsertRecipeParameter(RecipeParameter item);
        int UpdateRecipeParameter(RecipeParameter item);
        int DeleteRecipeParameter(RecipeParameter item);




        IList<PPIDAndRecipe> GetPPIDAndRecipeList(PPIDAndRecipe PPIDAndRecipe);
        bool InsertPPIDAndRecipe(PPIDAndRecipe item);
        int DeletePPIDAndRecipe(PPIDAndRecipe item);



        IList<ProcessModeMap> GetProcessModeMapList(Hashtable Hashtable);
        bool InsertProcessModeMap(ProcessModeMap item);
        int DeleteProcessModeMap(ProcessModeMap item);



        bool InsertMIXRunConfig(MIXRunConfig item);
        int DeleteMIXRunConfig(Hashtable item);
        IList<MIXRunConfig> GetMIXRunConfigList(Hashtable Hashtable);
        int UpdateMIXRunConfig(MIXRunConfig item);



        bool InsertMIXRunInputRatio(MIXRunInputRatio item);
        int DeleteMIXRunInputRatio(Hashtable item);
        IList<MIXRunInputRatio> GetMIXRunInputRatioList(Hashtable Hashtable);
        int UpdateMIXRunInputRatio(MIXRunInputRatio item);



        IList<OperationMode> GetOperationModeList(Hashtable Hashtable);
        #endregion

        #region DVData
        IList<DVData> ViewDVDataList(Hashtable map);
        bool InsertDVData(DVData DVData);

        int UpdateDVData(DVData DVData);

        int DeleteDVData(Hashtable map);



        IList<SVData> ViewSVDataList(Hashtable map);
        bool InsertSVData(SVData SVData);

        int UpdateSVData(SVData SVData);

        int DeleteSVData(Hashtable map);
        #endregion
        #region Config
        //SystemConfig ViewSystemConfig(string eqpid);
        IList<CFGS1F5> ViewCFGS1F5(Hashtable map);
        bool InsertCFGS1F5(CFGS1F5 CFGS1F5);
        int UpdateCFGS1F5(CFGS1F5 CFGS1F5);
        int DeleteCFGS1F5(CFGS1F5 CFGS1F5);

        IList<GlassExistencePosition> ViewGlassExistencePosition(Hashtable map);
        int UpdateGlassExistencePosition(GlassExistencePosition GlassExistencePosition);
        //bool InsertGlassExistencePosition(GlassExistencePosition GlassExistencePosition);
        //int UpdateGlassExistencePosition(GlassExistencePosition GlassExistencePosition);

        IList<OPILink> ViewOPILink(Hashtable map);


        IList<CFGOLDPriority> ViewCFGOLDPriority(Hashtable map);
        int UpdateCFGOLDPriority(CFGOLDPriority CFGOLDPriority);
        #endregion

        #region User
        IList<bc_sys_setting> Viewbc_sys_setting(Hashtable hashtable);
        bool Updatebc_sys_setting(Hashtable hashtable);
        IList<GlassInfo> GetUnitIdGlassInfoList(Hashtable map);
        IList<BC_Group> ViewUserGroupList(Hashtable hashtable);
        bool InsertUserGroup(BC_Group bC_Group);
        bool InsertGroupAuthority(Group_Authority Group_Authority);

        IList<Group_Authority> ViewUserGroupAuthority(string userid);

        bool DeleteUserGroup(string groupid);
        IList<Group_Authority> ViewGetGroupIdAuthority(string groupid);

        bool DeleteGroupAuthority(string groupid);
        IList<AllTable> ViewAllTableName();
        IList<TableStructure> ViewTableStructure(string tablename);
        IList<BcGroupAuthority> ViewBcGroupAuthority(Hashtable hashtable);
        IList<BcRobotConfigure> ViewBcRobotConfigure(Hashtable hashtable);
        IList<BcRobotGroupCconfigure> ViewBcRobotGroupCconfigure(Hashtable hashtable);
        IList<DisCol> ViewDisCol(Hashtable DisCol);
        IList<bc_robot_linksignal_configure> Viewbc_robot_linksignal_configure(Hashtable DisCol);
        IList<bc_robot_path_configure> Viewbc_robot_path_configure_user(Hashtable hashtable);
        IList<cfg_alarm> Viewcfg_alarm(Hashtable hashtable);
        IList<cfg_ceid> Viewcfg_ceid(Hashtable hashtable);
        IList<cfg_ecid> Viewcfg_ecid(Hashtable hashtable);
        IList<cfg_eqp> Viewcfg_eqp(Hashtable hashtable);
        IList<cfg_port> Viewcfg_port(Hashtable hashtable);
        IList<cfg_ssunit> Viewcfg_ssunit(Hashtable hashtable);
        IList<cfg_sunit> Viewcfg_sunit(Hashtable hashtable);
        IList<cfg_unit> Viewcfg_unit(Hashtable hashtable);
        IList<his_alarm> Viewhis_alarm(Hashtable hashtable);
        IList<his_alarm> Viewhis_alarmCount(Hashtable hashtable);
        IList<his_carrier> Viewhis_carrier(Hashtable hashtable);
        IList<his_maskinfo> Viewhis_maskinfo(Hashtable hashtable);
        IList<his_material> Viewhis_material(Hashtable hashtable);
        IList<his_material> Viewhis_materialCount(Hashtable hashtable);
        IList<his_packingbox> Viewhis_packingbox(Hashtable hashtable);
        IList<his_recipe> Viewhis_recipe(Hashtable hashtable);
        IList<his_recipeppidmap> Viewhis_recipeppidmap(Hashtable hashtable);
        IList<his_spanel> Viewhis_spanel(Hashtable hashtable);
        IList<his_trayinfo> Viewhis_trayinfo(Hashtable hashtable);
        IList<wip_cstbox> Viewwip_cstbox(Hashtable hashtable);
        IList<cfg_systemconfig> Viewcfg_systemconfig(Hashtable hashtable);
        IList<wip_glassinfo> Viewwip_glassinfo(Hashtable hashtable);
        IList<his_user> Viewhis_user(Hashtable hashtable);
        IList<his_glassinfo> Viewhis_glassinfo(Hashtable hashtable);
        IList<historydata> Viewhis_glassinfoCount(Hashtable hashtable);
        IList<bc_user_group> Viewbc_user_group(Hashtable hashtable);
        IList<wip_alarm> Viewwip_alarm(Hashtable hashtable);
        bool Deletecfg_eqp(string eqpid);
        bool Deletecfg_port(Hashtable portid);
        bool Deletecfg_unit(string unitid);
        bool Deletewip_glassinfo(string glsid);
        bool Deletewip_alarm(Hashtable hashtable);
        IList<bc_eqp_command_conf> Viewbc_eqp_command_conf(Hashtable hashtable);
        IList<bc_eqp_command_para> Viewbc_eqp_command_para(Hashtable hashtable);

        IList<wip_glassinfo> Viewwip_glassinfoSel(Hashtable hashtable);
        IList<cfg_eqpstatusrule> Viewcfg_eqpstatusrule(Hashtable hashtable);
        bool Deletecfg_eqpstatusrule(Hashtable hashtable);
        bool Insertcfg_eqpstatusrule(Hashtable hashtable);
        //IList<bc_robot_model> Viewbc_robot_model(Hashtable hashtable);
        bool UpdateDispatchMode(Hashtable hashtable);
        bool Deletecfg_recipeppidmap(Hashtable hashtable);
        bool Deletecfg_processmodemap(Hashtable hashtable);
        bool Insertcfg_recipeppidmap(Hashtable hashtable);
        bool Insertcfg_processmodemap(Hashtable hashtable);
        IList<cfg_operationmode> Viewcfg_operationmode(Hashtable hashtable);
        bool Deletecfg_operationmode(Hashtable hashtable);
        bool Insertcfg_operationmode(Hashtable hashtable);

        IList<wip_cassette> Viewwip_cassette(Hashtable hashtable);
        bool Deletewip_cassette(Hashtable hashtable);
        //bool Deletebc_robot_model(Hashtable hashtable);
        //bool Insertbc_robot_model(Hashtable hashtable);

        bool Updatecfg_dvdata(Hashtable hashtable);
        bool Updatecfg_svdata(Hashtable hashtable);
        bool Updatecfg_recipeparameter(Hashtable hashtable);
        IList<cfg_eqpstatusgroup> Viewcfg_eqpstatusgroup(Hashtable hashtable);
        bool Updatecfg_eqpstatusgroup(Hashtable hashtable);
        IList<cfg_mixrunconfig> Viewcfg_mixrunconfig(Hashtable hashtable);
        bool Updatecfg_mixrunconfig(Hashtable hashtable);
        IList<cfg_alarmspec> Viewcfg_alarmspec(Hashtable hashtable);
        bool Updatecfg_alarmspec(Hashtable hashtable);
        IList<his_opilog> Viewhis_opilog(Hashtable hashtable);
        IList<his_opilog> Viewhis_opilogCount(Hashtable hashtable);
        IList<his_unit> Viewhis_unit(Hashtable hashtable);
        IList<his_unit> Viewhis_unitCount(Hashtable hashtable);
        IList<his_cassette> Viewhis_cassette(Hashtable hashtable);
        IList<his_cassette> Viewhis_cassetteCount(Hashtable hashtable);
        IList<GlassInfoEvent> ViewGlassInfoEvent();
        IList<cfg_glassexistenceposition> Viewcfg_glassexistenceposition(Hashtable hashtable);
        bool Inserthis_opilog(Hashtable hashtable);
        bool Insertcfg_mixrunconfig(Hashtable hashtable);
        bool Deletecfg_mixrunconfig(int id);
        IList<ModePath> Viewbc_robot_path_configureDis();
        bool Updatecfg_recipeppidmap(Hashtable hashtable);
        bool Updatecfg_processmodemap(Hashtable hashtable);
        //bool Updatebc_robot_model(Hashtable hashtable);
        IList<his_eqp> Viewhis_eqp(Hashtable hashtable);
        IList<his_sunit> Viewhis_sunit(Hashtable hashtable);
        IList<his_port> Viewhis_port(Hashtable hashtable);
        IList<his_port> Viewhis_portCount(Hashtable hashtable);
        IList<his_vcr> Viewhis_vcr(Hashtable hashtable);
        bool Deletehis_opilog(int id);
        bool DeleteAllhis_opilog();
        IList<his_robotcommand> Viewhis_robotcommand(Hashtable hashtable);
        bool DeleteAllhis_robotcommand();

        bool UpdateIsColdRun(Hashtable has);
        #endregion

        #region UseRecipeList
        int DeleteUseRecipeList(UseRecipeList item);
        bool InsertUseRecipeList(UseRecipeList item);
        IList<UseRecipeList> GetUseRecipeList(Hashtable Hashtable);
        #endregion
        #region Test
        bool InsertTestLog(TestLog item);
        #endregion

        #region DbPalletService
        IList<his_pallet> Viewhis_palletList(Hashtable map);
        IList<his_pallet> Viewhis_palletListCount(Hashtable map);
        int Inserthis_pallet(his_pallet data);
        bool Updatehis_pallet(his_pallet data);
        #endregion

        #region DbProcessEndService
        int Insertwip_processend(wip_processend data);
        bool Insertwip_processend_glass(wip_processend_glass data);
        IList<wip_processend> Viewwip_processendList(Hashtable data);
        IList<wip_processend_glass> Viewwip_processend_glassList(Hashtable data);
        bool Updatewip_processend(wip_processend data);
        bool Updatewip_processend_glass(wip_processend_glass data);
        bool Deletewip_processend(Hashtable data);
        bool Deletewip_processend_glass(Hashtable data);
        #endregion

        #region DbEqpProfileService
        int Insertcfg_eqpprofile(cfg_eqpprofile data);
        int Insertcfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data);
        int Insertcfg_eqpprofile_item(cfg_eqpprofile_item data);
        IList<cfg_eqpprofile> Viewcfg_eqpprofile(Hashtable data);
        IList<cfg_eqpprofile_itemgroup> Viewcfg_eqpprofile_itemgroup(Hashtable data);
        IList<cfg_eqpprofile_item> Viewcfg_eqpprofile_item(Hashtable data);
        bool Updatecfg_eqpprofile(cfg_eqpprofile data);
        bool Updatecfg_eqpprofile_itemgroup(cfg_eqpprofile_itemgroup data);
        bool Updatecfg_eqpprofile_item(cfg_eqpprofile_item data);
        bool Deletecfg_eqpprofile(Hashtable data);
        bool Deletecfg_eqpprofile_itemgroup(Hashtable data);
        bool Deletecfg_eqpprofile_item(Hashtable data);
        #endregion

        #region DbRobotService
        bool Insertbc_robot_configure(bc_robot_configure data);
        IList<bc_robot_configure> Viewbc_robot_configure(Hashtable data);
        bool Updatebc_robot_configure(bc_robot_configure data);
        bool Deletebc_robot_configure(Hashtable data);
        bool Insertbc_robot_path_configure(bc_robot_path_configure data);
        IList<bc_robot_path_configure> Viewbc_robot_path_configure(Hashtable data);
        bool Updatebc_robot_path_configure(bc_robot_path_configure data);
        bool Deletebc_robot_path_configure(Hashtable data);
        bool Insertbc_robot_model(bc_robot_model data);
        IList<bc_robot_model> Viewbc_robot_model(Hashtable data);
        bool Updatebc_robot_model(bc_robot_model data);
        bool Deletebc_robot_model(Hashtable data);
        #endregion
    }
}
