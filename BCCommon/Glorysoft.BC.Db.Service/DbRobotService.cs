using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.GlassDispath;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Db.Service
{
    public class DbRobotService : AbstractDbService, IDbRobotService
    {
        //public IList<GroupConfigure> ViewGroupConfigure(Hashtable ht)
        //{
        //    return ExecuteQueryForList<GroupConfigure>("ViewGroupConfigure", ht);
        //}
        public IList<RobotPathConfigure> ViewRobotPathConfigure(Hashtable ht)
        {
            return ExecuteQueryForList<RobotPathConfigure>("ViewRobotPathConfigure", ht);
        }
        public IList<RobotConfigure> ViewRobotConfigure(Hashtable ht)
        {
            return ExecuteQueryForList<RobotConfigure>("ViewRobotConfigure", ht);
        }
        //public IList<Linksignal> ViewRobotLinkSignalConfigure(string linename)
        //{
        //    var hashtable = new Hashtable();
        //    hashtable.Add("LineType", linename);
        //    return ExecuteQueryForList<Linksignal>("ViewRobotLinkSignalConfigure", hashtable);
        //}
        //public IList<Linksignal> UpdateViewWaitingtimeConfig(Linksignal item)
        //{
        //    return ExecuteQueryForList<Linksignal>("UpdateViewWaitingtimeConfig", item);
        //}
        //public IList<RobotLinksignal> ViewRobotLinksignalList(Hashtable ht)
        //{
        //    return ExecuteQueryForList<RobotLinksignal>("ViewRobotLinksignalList", ht);
        //}
        public IList<RobotModel> ViewRobotModelList(Hashtable ht)
        {
            return ExecuteQueryForList<RobotModel>("ViewRobotModelList", ht);
        }


        public bool InsertHisRobotCommand(HisRobotCommand HisRobotCommand)
        {
            return ExecuteInsert("InsertHisRobotCommand", HisRobotCommand);
        }

        public bool Insertbc_robot_configure(bc_robot_configure data)
        {
            return ExecuteInsert("Insertbc_robot_configure", data);
        }
        public IList<bc_robot_configure> Viewbc_robot_configure(Hashtable map)
        {
            return ExecuteQueryForList<bc_robot_configure>("Viewbc_robot_configure", map) ?? new List<bc_robot_configure>();
        }
        public bool Updatebc_robot_configure(bc_robot_configure data)
        {
            return ExecuteUpdate("Updatebc_robot_configure", data) == 1 ? true : false;
        }
        public bool Deletebc_robot_configure(Hashtable data)
        {
            return ExecuteDelete("Deletebc_robot_configure", data) == 1 ? true : false;
        }

        public bool Insertbc_robot_path_configure(bc_robot_path_configure data)
        {
            return ExecuteInsert("Insertbc_robot_path_configure", data);
        }
        public IList<bc_robot_path_configure> Viewbc_robot_path_configure(Hashtable map)
        {
            return ExecuteQueryForList<bc_robot_path_configure>("Viewbc_robot_path_configure", map) ?? new List<bc_robot_path_configure>();
        }
        public bool Updatebc_robot_path_configure(bc_robot_path_configure data)
        {
            return ExecuteUpdate("Updatebc_robot_path_configure", data) == 1 ? true : false;
        }
        public bool Deletebc_robot_path_configure(Hashtable data)
        {
            return ExecuteDelete("Deletebc_robot_path_configure", data) == 1 ? true : false;
        }

        public bool Insertbc_robot_model(bc_robot_model data)
        {
            return ExecuteInsert("Insertbc_robot_model", data);
        }
        public IList<bc_robot_model> Viewbc_robot_model(Hashtable map)
        {
            return ExecuteQueryForList<bc_robot_model>("Viewbc_robot_model", map) ?? new List<bc_robot_model>();
        }
        public bool Updatebc_robot_model(bc_robot_model data)
        {
            return ExecuteUpdate("Updatebc_robot_model", data) == 1 ? true : false;
        }
        public bool Deletebc_robot_model(Hashtable data)
        {
            return ExecuteDelete("Deletebc_robot_model", data) == 1 ? true : false;
        }
    }
}
