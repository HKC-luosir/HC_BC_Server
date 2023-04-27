using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{

    public interface IRobotService : IAutoRegister
    {
        void Start(string jsonfile);
        void RobotCommandFetchOutReport(string name, string commandSequenceNumber);
        void RobotArmMonitoringReport(string name, RobotCommandResult result);
        void CommandExecuteResultReport(string name, string commandSequenceNumber);
        void CommandReturnCode(string name, int code, string transactionID = "");


        // IList<RobotLinksignal> ViewRobotLinksignalList(Hashtable ht);
        IList<RobotModel> ViewRobotModelList(Hashtable ht);

        void WriteRobotDebugLog(string name, string logInfo);

        bool InsertHisRobotCommand(HisRobotCommand HisRobotCommand);
    }
}
