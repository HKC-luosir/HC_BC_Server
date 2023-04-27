using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity;
namespace Glorysoft.BC.Db.Service
{
    public class DbUserService : AbstractDbService, IDbUserService
    {
        public IList<bc_sys_setting> Viewbc_sys_setting(Hashtable hashtable)
        {
            return ExecuteQueryForList<bc_sys_setting>("Viewbc_sys_setting", hashtable);
        }
        public bool Updatebc_sys_setting(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatebc_sys_setting", hashtable) == 1 ? true : false;
        }
        public IList<BC_Group> ViewUserGroupList(Hashtable hashtable)
        {
            return ExecuteQueryForList<BC_Group>("ViewUserGroupList", hashtable);
        }

        public bool InsertUserGroup(BC_Group BC_Group)
        {
            return ExecuteInsert("InsertUserGroup", BC_Group);
        }

        public bool InsertGroupAuthority(Group_Authority Group_Authority)
        {
            return ExecuteInsert("InsertGroupAuthority", Group_Authority);
        }
        public IList<Group_Authority> ViewUserGroupAuthority(string userid)
        {
            return ExecuteQueryForList<Group_Authority>("ViewUserGroupAuthority", userid);
        }
        public bool DeleteUserGroup(string groupid)
        {
            return ExecuteDelete("DeleteUserGroup", groupid) == 1 ? true : false;
        }
        public IList<Group_Authority> ViewGetGroupIdAuthority(string groupid)
        {
            return ExecuteQueryForList<Group_Authority>("ViewGetGroupIdAuthority", groupid);
        }

        public bool DeleteGroupAuthority(string groupid)
        {
            return ExecuteDelete("DeleteGroupAuthority", groupid) == 1 ? true : false;
        }
        public IList<AllTable> ViewAllTableName()
        {
            return ExecuteQueryForList<AllTable>("ViewAllTableName", null);
        }
        public IList<TableStructure> ViewTableStructure(string tablename)
        {
            return ExecuteQueryForList<TableStructure>("ViewTableStructure", tablename);
        }
        public IList<BcGroupAuthority> ViewBcGroupAuthority(Hashtable hashtable)
        {
            return ExecuteQueryForList<BcGroupAuthority>("ViewBcGroupAuthority", hashtable);
        }
        public IList<BcRobotConfigure> ViewBcRobotConfigure(Hashtable hashtable)
        {
            return ExecuteQueryForList<BcRobotConfigure>("ViewBcRobotConfigure", hashtable);
        }
        public IList<BcRobotGroupCconfigure> ViewBcRobotGroupCconfigure(Hashtable hashtable)
        {
            return ExecuteQueryForList<BcRobotGroupCconfigure>("ViewBcRobotGroupCconfigure", hashtable);
        }
        public IList<DisCol> ViewDisCol(Hashtable hashtable)
        {
            return ExecuteQueryForList<DisCol>("ViewDisCol", hashtable);
        }
        public IList<bc_robot_linksignal_configure> Viewbc_robot_linksignal_configure(Hashtable hashtable)
        {
            return ExecuteQueryForList<bc_robot_linksignal_configure>("Viewbc_robot_linksignal_configure", hashtable);
        }

        public IList<bc_robot_path_configure> Viewbc_robot_path_configure_user(Hashtable hashtable)
        {
            return ExecuteQueryForList<bc_robot_path_configure>("Viewbc_robot_path_configure_user", hashtable);
        }

        public IList<cfg_alarm> Viewcfg_alarm(Hashtable hashtable)
        {
            return ExecuteQueryForList<cfg_alarm>("Viewcfg_alarm", hashtable);
        }
        public IList<cfg_ceid> Viewcfg_ceid(Hashtable hashtable) { return ExecuteQueryForList<cfg_ceid>("Viewcfg_ceid", hashtable); }
        public IList<cfg_ecid> Viewcfg_ecid(Hashtable hashtable) { return ExecuteQueryForList<cfg_ecid>("Viewcfg_ecid", hashtable); }
        public IList<cfg_eqp> Viewcfg_eqp(Hashtable hashtable) { return ExecuteQueryForList<cfg_eqp>("Viewcfg_eqp", hashtable); }
        public IList<cfg_port> Viewcfg_port(Hashtable hashtable) { return ExecuteQueryForList<cfg_port>("Viewcfg_port", hashtable); }
        public IList<cfg_ssunit> Viewcfg_ssunit(Hashtable hashtable) { return ExecuteQueryForList<cfg_ssunit>("Viewcfg_ssunit", hashtable); }
        public IList<cfg_sunit> Viewcfg_sunit(Hashtable hashtable) { return ExecuteQueryForList<cfg_sunit>("Viewcfg_sunit", hashtable); }
        public IList<cfg_unit> Viewcfg_unit(Hashtable hashtable) { return ExecuteQueryForList<cfg_unit>("Viewcfg_unit", hashtable); }
        public IList<his_alarm> Viewhis_alarm(Hashtable hashtable) { return ExecuteQueryForList<his_alarm>("Viewhis_alarm", hashtable); }
        public IList<his_alarm> Viewhis_alarmCount(Hashtable hashtable) { return ExecuteQueryForList<his_alarm>("Viewhis_alarmCount", hashtable); }
        public IList<his_carrier> Viewhis_carrier(Hashtable hashtable) { return ExecuteQueryForList<his_carrier>("Viewhis_carrier", hashtable); }
        public IList<his_maskinfo> Viewhis_maskinfo(Hashtable hashtable) { return ExecuteQueryForList<his_maskinfo>("Viewhis_maskinfo", hashtable); }
        public IList<his_material> Viewhis_material(Hashtable hashtable) { return ExecuteQueryForList<his_material>("Viewhis_material", hashtable); }
        public IList<his_material> Viewhis_materialCount(Hashtable hashtable) { return ExecuteQueryForList<his_material>("Viewhis_materialCount", hashtable); }
        public IList<his_packingbox> Viewhis_packingbox(Hashtable hashtable) { return ExecuteQueryForList<his_packingbox>("Viewhis_packingbox", hashtable); }
        public IList<his_recipe> Viewhis_recipe(Hashtable hashtable) { return ExecuteQueryForList<his_recipe>("Viewhis_recipe", hashtable); }
        public IList<his_recipeppidmap> Viewhis_recipeppidmap(Hashtable hashtable) { return ExecuteQueryForList<his_recipeppidmap>("Viewhis_recipeppidmap", hashtable); }
        public IList<his_spanel> Viewhis_spanel(Hashtable hashtable) { return ExecuteQueryForList<his_spanel>("Viewhis_spanel", hashtable); }
        public IList<his_trayinfo> Viewhis_trayinfo(Hashtable hashtable) { return ExecuteQueryForList<his_trayinfo>("Viewhis_trayinfo", hashtable); }
        public IList<wip_cstbox> Viewwip_cstbox(Hashtable hashtable) { return ExecuteQueryForList<wip_cstbox>("Viewwip_cstbox", hashtable); }
        public IList<cfg_systemconfig> Viewcfg_systemconfig(Hashtable hashtable) { return ExecuteQueryForList<cfg_systemconfig>("Viewcfg_systemconfig", hashtable); }
        public IList<wip_glassinfo> Viewwip_glassinfo(Hashtable hashtable) { return ExecuteQueryForList<wip_glassinfo>("Viewwip_glassinfo", hashtable); }
        public IList<his_user> Viewhis_user(Hashtable hashtable) { return ExecuteQueryForList<his_user>("Viewhis_user", hashtable); }
        public IList<his_glassinfo> Viewhis_glassinfo(Hashtable hashtable) { return ExecuteQueryForList<his_glassinfo>("Viewhis_glassinfo", hashtable); }
        public IList<historydata> Viewhis_glassinfoCount(Hashtable hashtable) { return ExecuteQueryForList<historydata>("Viewhis_glassinfoCount", hashtable); }
        public IList<bc_user_group> Viewbc_user_group(Hashtable hashtable) { return ExecuteQueryForList<bc_user_group>("Viewbc_user_group", hashtable); }
        public IList<wip_alarm> Viewwip_alarm(Hashtable hashtable) { return ExecuteQueryForList<wip_alarm>("Viewwip_alarm", hashtable); }
        public bool Deletecfg_eqp(string eqpid)
        {
            return ExecuteDelete("Deletecfg_eqp", eqpid) == 1 ? true : false;
        }
        public bool Deletecfg_port(Hashtable portid)
        {
            return ExecuteDelete("Deletecfg_port", portid) == 1 ? true : false;
        }

        public bool Deletecfg_unit(string unitid)
        {
            return ExecuteDelete("Deletecfg_unit", unitid) == 1 ? true : false;
        }

        public bool Deletewip_glassinfo(string glsid)
        {
            return ExecuteDelete("Deletewip_glassinfo", glsid) == 1 ? true : false;
        }

        public bool Deletewip_alarm(Hashtable hashtable)
        {
            return ExecuteDelete("Deletewip_alarm", hashtable) == 1 ? true : false;
        }
        public IList<bc_eqp_command_conf> Viewbc_eqp_command_conf(Hashtable hashtable)
        {
            return ExecuteQueryForList<bc_eqp_command_conf>("Viewbc_eqp_command_conf", hashtable);
        }
        public IList<bc_eqp_command_para> Viewbc_eqp_command_para(Hashtable hashtable)
        {
            return ExecuteQueryForList<bc_eqp_command_para>("Viewbc_eqp_command_para", hashtable);
        }
        public IList<wip_glassinfo> Viewwip_glassinfoSel(Hashtable hashtable)
        {
            return ExecuteQueryForList<wip_glassinfo>("Viewwip_glassinfoSel", hashtable);
        }
        public IList<cfg_eqpstatusrule> Viewcfg_eqpstatusrule(Hashtable hashtable) { return ExecuteQueryForList<cfg_eqpstatusrule>("Viewcfg_eqpstatusrule", hashtable); }
        public bool Deletecfg_eqpstatusrule(Hashtable hashtable)
        {
            return ExecuteDelete("Deletecfg_eqpstatusrule", hashtable) == 1 ? true : false;
        }
        public bool Insertcfg_eqpstatusrule(Hashtable hashtable)
        {
            return ExecuteInsert("Insertcfg_eqpstatusrule", hashtable);
        }
        //public IList<bc_robot_model> Viewbc_robot_model(Hashtable hashtable) { return ExecuteQueryForList<bc_robot_model>("Viewbc_robot_model", hashtable); }
        public bool UpdateDispatchMode(Hashtable hashtable)
        {
            return ExecuteUpdate("UpdateDispatchMode", hashtable) == 1 ? true : false;
        }
        public bool Deletecfg_recipeppidmap(Hashtable hashtable)
        {
            return ExecuteDelete("Deletecfg_recipeppidmap", hashtable) == 1 ? true : false;
        }
        public bool Deletecfg_processmodemap(Hashtable hashtable)
        {
            return ExecuteDelete("Deletecfg_processmodemap", hashtable) == 1 ? true : false;
        }
        public bool Insertcfg_recipeppidmap(Hashtable hashtable)
        {
            return ExecuteInsert("Insertcfg_recipeppidmap", hashtable);
        }
        public bool Insertcfg_processmodemap(Hashtable hashtable)
        {
            return ExecuteInsert("Insertcfg_processmodemap", hashtable);
        }
        public IList<cfg_operationmode> Viewcfg_operationmode(Hashtable hashtable) { return ExecuteQueryForList<cfg_operationmode>("Viewcfg_operationmode", hashtable); }
        public bool Deletecfg_operationmode(Hashtable hashtable)
        {
            return ExecuteDelete("Deletecfg_operationmode", hashtable) == 1 ? true : false;
        }
        public bool Insertcfg_operationmode(Hashtable hashtable)
        {
            return ExecuteInsert("Insertcfg_operationmode", hashtable);
        }

        public IList<wip_cassette> Viewwip_cassette(Hashtable hashtable)
        {
            return ExecuteQueryForList<wip_cassette>("Viewwip_cassette", hashtable);
        }
        public bool Deletewip_cassette(Hashtable hashtable)
        {
            return ExecuteDelete("Deletewip_cassette", hashtable) == 1 ? true : false;
        }
        //public bool Deletebc_robot_model(Hashtable hashtable)
        //{
        //    return ExecuteDelete("Deletebc_robot_model", hashtable) == 1 ? true : false;
        //}
        //public bool Insertbc_robot_model(Hashtable hashtable)
        //{
        //    return ExecuteInsert("Insertbc_robot_model", hashtable);
        //}
        public bool Updatecfg_dvdata(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_dvdata", hashtable) == 1 ? true : false;
        }
        public bool Updatecfg_svdata(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_svdata", hashtable) == 1 ? true : false;
        }
        public bool Updatecfg_recipeparameter(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_recipeparameter", hashtable) == 1 ? true : false;
        }
        public IList<cfg_eqpstatusgroup> Viewcfg_eqpstatusgroup(Hashtable hashtable)
        {
            return ExecuteQueryForList<cfg_eqpstatusgroup>("Viewcfg_eqpstatusgroup", hashtable);
        }
        public bool Updatecfg_eqpstatusgroup(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_eqpstatusgroup", hashtable) == 1 ? true : false;
        }

        public IList<cfg_mixrunconfig> Viewcfg_mixrunconfig(Hashtable hashtable)
        {
            return ExecuteQueryForList<cfg_mixrunconfig>("Viewcfg_mixrunconfig", hashtable);
        }
        public bool Updatecfg_mixrunconfig(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_mixrunconfig", hashtable) == 1 ? true : false;
        }

        public IList<his_opilog> Viewhis_opilog(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_opilog>("Viewhis_opilog", hashtable);
        }
        public IList<his_opilog> Viewhis_opilogCount(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_opilog>("Viewhis_opilogCount", hashtable);
        }
        public bool Inserthis_opilog(Hashtable hashtable)
        {
            return ExecuteInsert("Inserthis_opilog", hashtable);
        }
        public IList<cfg_alarmspec> Viewcfg_alarmspec(Hashtable hashtable)
        {
            return ExecuteQueryForList<cfg_alarmspec>("Viewcfg_alarmspec", hashtable);
        }
        public bool Updatecfg_alarmspec(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_alarmspec", hashtable) == 1 ? true : false;
        }

        public IList<his_unit> Viewhis_unit(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_unit>("Viewhis_unit", hashtable);
        }

        public IList<his_unit> Viewhis_unitCount(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_unit>("Viewhis_unitCount", hashtable);
        }

        public IList<his_cassette> Viewhis_cassette(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_cassette>("Viewhis_cassette", hashtable);
        }

        public IList<his_cassette> Viewhis_cassetteCount(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_cassette>("Viewhis_cassetteCount", hashtable);
        }
        public IList<GlassInfoEvent> ViewGlassInfoEvent()
        {
            return ExecuteQueryForList<GlassInfoEvent>("ViewGlassInfoEvent", null);
        }
        public IList<cfg_glassexistenceposition> Viewcfg_glassexistenceposition(Hashtable hashtable)
        {
            return ExecuteQueryForList<cfg_glassexistenceposition>("Viewcfg_glassexistenceposition", hashtable);
        }
        public bool Insertcfg_mixrunconfig(Hashtable hashtable)
        {
            return ExecuteInsert("Insertcfg_mixrunconfig", hashtable);
        }
        public bool Deletecfg_mixrunconfig(int id)
        {
            return ExecuteDelete("Deletecfg_mixrunconfig", id) == 1 ? true : false;
        }
        public IList<ModePath> Viewbc_robot_path_configureDis()
        {
            return ExecuteQueryForList<ModePath>("Viewbc_robot_path_configureDis", null);
        }

        public bool Updatecfg_recipeppidmap(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_recipeppidmap", hashtable) == 1 ? true : false;
        }
        public bool Updatecfg_processmodemap(Hashtable hashtable)
        {
            return ExecuteUpdate("Updatecfg_processmodemap", hashtable) == 1 ? true : false;
        }
        //public bool Updatebc_robot_model(Hashtable hashtable)
        //{
        //    return ExecuteUpdate("Updatebc_robot_model", hashtable) == 1 ? true : false;
        //}

        public IList<his_eqp> Viewhis_eqp(Hashtable hashtable)
        {
            return ExecuteQueryForList<his_eqp>("Viewhis_eqp", hashtable);
        }
        public IList<his_sunit> Viewhis_sunit(Hashtable hashtable) { return ExecuteQueryForList<his_sunit>("Viewhis_sunit", hashtable); }
        public IList<his_port> Viewhis_port(Hashtable hashtable) { return ExecuteQueryForList<his_port>("Viewhis_port", hashtable); }
        public IList<his_port> Viewhis_portCount(Hashtable hashtable) { return ExecuteQueryForList<his_port>("Viewhis_portCount", hashtable); }
        public IList<his_vcr> Viewhis_vcr(Hashtable hashtable) { return ExecuteQueryForList<his_vcr>("Viewhis_vcr", hashtable); }
        public bool Deletehis_opilog(int id)
        {
            return ExecuteDelete("Deletehis_opilog", id) == 1 ? true : false;
        }
        public bool DeleteAllhis_opilog()
        {
            return ExecuteDelete("DeleteAllhis_opilog", null) == 1 ? true : false;
        }
        public IList<his_robotcommand> Viewhis_robotcommand(Hashtable hashtable) { return ExecuteQueryForList<his_robotcommand>("Viewhis_robotcommand", hashtable); }
        public bool DeleteAllhis_robotcommand()
        {
            return ExecuteDelete("DeleteAllhis_robotcommand", null) == 1 ? true : false;
        }

        public bool UpdateIsColdRun(Hashtable has)
        {
            return ExecuteUpdate("UpdateIsColdRun", has) == 1 ? true : false;
        }



    }
}
