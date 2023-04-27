using System.Collections.Generic;
using Glorysoft.BC.Entity;

namespace  Glorysoft.BC.GlassDispath
{
    public interface IJobDispatch
    {
        void Initialize();
        void Start();
        void Stop();

        void CommandReturnCodeReport(int code);
       // void CommandExecuteResultReport(RobotCommandResult result);
    }
}
