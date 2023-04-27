using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.Auto.Contract;
using System.Collections;
using System.ServiceModel;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    public interface IDbUserService : IAutoRegister
    {

        IList<bc_sys_setting> Viewbc_sys_setting(Hashtable hashtable);
        bool Updatebc_sys_setting(Hashtable hashtable);
        IList<BC_Group> ViewUserGroupList(Hashtable hashtable);
        bool InsertUserGroup(BC_Group BC_Group);
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
        IList<his_opilog> Viewhis_opilog(Hashtable hashtable);
        IList<his_opilog> Viewhis_opilogCount(Hashtable hashtable);
        bool Inserthis_opilog(Hashtable hashtable);
        IList<cfg_alarmspec> Viewcfg_alarmspec(Hashtable hashtable);
        bool Updatecfg_alarmspec(Hashtable hashtable);
        IList<his_unit> Viewhis_unit(Hashtable hashtable);
        IList<his_unit> Viewhis_unitCount(Hashtable hashtable);
        IList<his_cassette> Viewhis_cassette(Hashtable hashtable);
        IList<his_cassette> Viewhis_cassetteCount(Hashtable hashtable);
        IList<GlassInfoEvent> ViewGlassInfoEvent();
        IList<cfg_glassexistenceposition> Viewcfg_glassexistenceposition(Hashtable hashtable);
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
    }
}
