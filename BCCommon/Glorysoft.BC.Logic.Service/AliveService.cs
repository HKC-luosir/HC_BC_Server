using Glorysoft.BC.Logic.Contract;
using System;
using System.Threading;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.Auto.Contract;

namespace Glorysoft.BC.Logic.Service
{

    public class AliveService : AbstractEventHandler, IAliveService
    {
        //private Thread thredPLCMsg;
       // private Thread thredPLCAlive;
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        protected static readonly IReadLinkInfo readLinkCmd = CommonContexts.ResolveInstance<IReadLinkInfo>();
        public void MonitorPLCAlive()
        {


            //Thread thredPLCAlive = new Thread(PLCAlive);
            //thredPLCAlive.Start();
            //readLinkCmd.Start();
            //Thread thredMESAlive = new Thread(MESAlive);
            //thredMESAlive.Start();
        }
        private void PLCAlive()
        {
            while (true)
            {
                try
                {
                    foreach (var item in HostInfo.Current.EQPInfo.Units)
                    {
                        if(item.CommandType== Consts.CommandType.PLC.GetHashCode())
                        {
                            if (DateTime.Now > item.AliveUpdate.AddSeconds(9))
                            {
                                item.IsConnect = Consts.IsConnect.Down.ToString();//false;
                                                                                  //item.Value.IsConnect = false;
                            }
                            else
                            {
                                if (item.IsConnect == Consts.IsConnect.Down.ToString())
                                {
                                    item.IsConnect = Consts.IsConnect.Alive.ToString();//true;
                                    if (HostInfo.Current.SystemConfig.PLCDateTimeEnable)
                                    {
                                        SendEQPDateTime(item.UnitName);
                                    }
                                }
                            }
                        }
                        
                        //Send To Client 更新设备连接状态
                      //  ClientService.UpdateEQPConnectStatus(item.Value);
                    }
                    //暂停300毫秒后继续执行
                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.Debug(ex.Message);
                }
            }
        }

      
        private void SendEQPDateTime(string EQPName)
        {
            try
            {
                //string datetime = DateTime.Now.ToString(DateFormat.NoSpace);//DateTime.Now.ToString("yyyyMMddHHmmss");
                //eqpCmd.DateTimeSetCommand(EQPName, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), datetime);
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
                //发送datetime到plc
        }


        //private void MESAlive()
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            if (DateTime.Now > HostInfo.MesAliveUpdate.AddSeconds(9))
        //            {
        //                HostInfo.IsMESConnect = false;
        //            }
        //            else
        //            {
        //                HostInfo.IsMESConnect = true;
        //            }
        //            //暂停300毫秒后继续执行
        //            Thread.Sleep(300);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.BCLog.Debug(ex.Message);
        //        }
        //    }
        //}
    }
}
