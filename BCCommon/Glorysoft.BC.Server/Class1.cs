
//using System;
//using System.Collections.Generic;
//using System.Web;
//using System.Text.RegularExpressions;
//using System.Diagnostics;
//namespace Glorysoft.BC.Server
//{
//    public class PingServices {
//        #region codes        
//        //超时时间        
//        private const int TIME_OUT = 100;
//        //包大小        
//        private const int PACKET_SIZE = 32;
//        //Ping的次数        
//        private const int TRY_TIMES = 3;
//        //取时间的正则        
//        private static Regex _reg = new Regex(@"时间=(.*?)ms", RegexOptions.Multiline | RegexOptions.IgnoreCase);
//        Process proc_Ping = new Process();       
//        ///<summary>        
//        ////// 得到速度值单位KB        
//        //////</summary>        
//        //////<param name="strCommandline">传入的命令行</param>        
//        //////<param name="packetSize">包的大小</param>        
//        //////<returns>KB</returns>        
//        private float LaunchPing(string strCommandline, int packetSize)
//        {
//            SetProcess(strCommandline);
//            proc_Ping.Start();
//            string strBuffer = proc_Ping.StandardOutput.ReadToEnd();
//            proc_Ping.Close();
//            return ParseResult(strBuffer, packetSize);
//        }       
//        ///<summary>        
//        /// 设属性        
//        ///</summary>        
//        ///<param name="strCommandline">传入的命令行</param>       
//        private void SetProcess(string strCommandline)
//        {
//            //命令行           
//            proc_Ping.StartInfo.Arguments = strCommandline;
//            //是否使用操作系统外壳来执行           
//            proc_Ping.StartInfo.UseShellExecute = false;
//            //是否在新窗口中启动            
//            proc_Ping.StartInfo.CreateNoWindow = true;
//            //exe名称默认的在System32下            
//            proc_Ping.StartInfo.FileName = "ping.exe";
//            proc_Ping.StartInfo.RedirectStandardInput = true;
//            proc_Ping.StartInfo.RedirectStandardOutput = true;
//            proc_Ping.StartInfo.RedirectStandardError = true;
//        }        
//        ///<summary>        
//        ///得到Ping的结果包括统计信息        
//        ///</summary>       
//        ///<param name="strCommandline">传入的命令行</param>        
//        ///<param name="packetSize">包的大小</param>       
//        ///<returns>KB</returns>      
//        private string LaunchPingStr(string strCommandline, int packetSize)
//        {
//            SetProcess(strCommandline);
//            proc_Ping.Start();
//            string strBuffer = proc_Ping.StandardOutput.ReadToEnd();
//            proc_Ping.Close();
//            return strBuffer;
//        }        
//        ///<summary>        
//        ///取速度值       
//        ///</summary>       
//        ///<param name="strBuffer"></param>      
//        ///<param name="packetSize"></param>        
//        ///<returns></returns>        
//        private float ParseResult(string strBuffer, int packetSize)
//        {
//            if (strBuffer.Length < 1) return 0.0F;
//            MatchCollection mc = _reg.Matches(strBuffer);
//            if (mc == null || mc.Count < 1 || mc[0].Groups == null) return 0.0F;
//            int avg;
//            if (!int.TryParse(mc[0].Groups[1].Value, out avg)) return 0.0F;
//            if (avg <= 0) return 1024.0F;
//            return (float)packetSize / avg * 1000 / 1024;
//        }     
//        #endregion 公共方法       
//        ///<summary>        
//        ///得到网速        
//        ///</summary>        
//        ///<param name="strHost">主机名或ip</param>        
//        ///<returns>kbps/s</returns>       
//        public float PingKB(string strHost)
//        {
//            return LaunchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TRY_TIMES, PACKET_SIZE, TIME_OUT), PACKET_SIZE);
//        }

//        /// <summary>
//        /// 得到Ping结果字符串   
//        /// </summary>
//        /// <param name="strHost">主机名或ip</param>
//        /// <returns></returns>
//        public string GetPingStr(string strHost)
//        {
//            return LaunchPingStr(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TRY_TIMES, PACKET_SIZE, TIME_OUT), PACKET_SIZE);
//        }
//        ///<summary>        
//        ///得到Ping结果字符串       
//        ///</summary>       
//        ///<param name="strHost">主机名或ip</param>        
//        ///<param name="PacketSize">发送测试包大小</param>        
//        ///<param name="TimeOut">超时</param>        
//        ///<param name="TryTimes">测试次数</param>       
//        ///<returns>kbps/s</returns>       
//        public string GetPingStr(string strHost, int PacketSize, int TimeOut, int TryTimes)
//        {
//            return LaunchPingStr(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TryTimes, PacketSize, TimeOut), PacketSize);
//        }
//        ///<summary>       
//        ///根据传入的参数返回Ping速度结果        
//        ///</summary>        
//        ///<param name="strHost">主机名或ip</param>       
//        ///<param name="PacketSize">发送测试包大小</param>     
//        ///<param name="TimeOut">超时</param>       
//        ///<param name="TryTimes">测试次数</param>        
//        ///<returns>kbps/s</returns>        
//        public float PingKB(string strHost, int PacketSize, int TimeOut, int TryTimes)
//        {
//            return LaunchPing(string.Format("{0} -n {1} -l {2} -w {3}", strHost, TryTimes, PacketSize, TimeOut), PacketSize);
//        }
//    }
//}