using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using System.Collections;
using System.ServiceModel;
using Glorysoft.BC.GlassDispath;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Db.Contract
{
    [ServiceContract]
    public interface IDbRobotService : IAutoRegister
    {
        //[OperationContract]
        //IList<GroupConfigure> ViewGroupConfigure(Hashtable ht);
        [OperationContract]
        IList<RobotPathConfigure> ViewRobotPathConfigure(Hashtable ht);
        [OperationContract]
        IList<RobotConfigure> ViewRobotConfigure(Hashtable ht);
        //[OperationContract]
        //IList<Linksignal> ViewRobotLinkSignalConfigure(string linename);
        //[OperationContract]
        //IList<Linksignal> UpdateViewWaitingtimeConfig(Linksignal item);
        //[OperationContract]
        //IList<RobotLinksignal> ViewRobotLinksignalList(Hashtable ht);
        [OperationContract]
        IList<RobotModel> ViewRobotModelList(Hashtable ht);
        [OperationContract]
        bool InsertHisRobotCommand(HisRobotCommand HisRobotCommand);

        bool Insertbc_robot_configure(bc_robot_configure data);
        IList<bc_robot_configure> Viewbc_robot_configure(Hashtable Hashtable);
        bool Updatebc_robot_configure(bc_robot_configure data);
        bool Deletebc_robot_configure(Hashtable Hashtable);

        bool Insertbc_robot_path_configure(bc_robot_path_configure data);
        IList<bc_robot_path_configure> Viewbc_robot_path_configure(Hashtable Hashtable);
        bool Updatebc_robot_path_configure(bc_robot_path_configure data);
        bool Deletebc_robot_path_configure(Hashtable Hashtable);

        bool Insertbc_robot_model(bc_robot_model data);
        IList<bc_robot_model> Viewbc_robot_model(Hashtable Hashtable);
        bool Updatebc_robot_model(bc_robot_model data);
        bool Deletebc_robot_model(Hashtable Hashtable);
    }
}
