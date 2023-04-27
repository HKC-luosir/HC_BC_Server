using Glorysoft.BC.Entity;
using System;
using System.Configuration;
using System.Threading;
using System.Windows;
using System.Web.Http.SelfHost;
using System.Web.Http.Cors;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Dispatcher;
using GlorySoft.BC.WebAPI;

namespace Glorysoft.BC.Server
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private Mutex mutx;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool bNewCreate;
            mutx = new Mutex(true, "BC", out bNewCreate);
            LogHelper.BCLog.DebugFormat("BC Begion Time:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            if (!bNewCreate)
            {
                MessageBox.Show("BC已启动!!", "Infor", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }
            base.OnStartup(e);
            //this.Startup += new StartupEventHandler(WebAPIStartup);
            WebAPIStartup();
        }
        private void WebAPIStartup()
        {
            try
            {
                HttpSelfHostConfiguration _config = null;
                HttpSelfHostServer _serverhost = null;
                _config = new HttpSelfHostConfiguration(ConfigurationManager.AppSettings["WebAPILocalAddress"]);
                _config.EnableCors(new EnableCorsAttribute("*", "*", "*"));//跨域允许设置
                _config.Services.Replace(typeof(IAssembliesResolver), new AssembliesResolver());
                _config.MaxReceivedMessageSize = 2147483647;
                _config.MaxBufferSize = 2147483647;
                _config.Routes.MapHttpRoute(
                 "API Default",
                 "api/{controller}/{action}/{username}",
                 new { username = RouteParameter.Optional }
                 );
                // 干掉XML序列化器
                _config.Formatters.Remove(_config.Formatters.XmlFormatter);

                // 解决json序列化时的循环引用问题
                _config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                // 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
                _config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
                //_config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();


                //start 
                _serverhost = new HttpSelfHostServer(_config);
                _serverhost.OpenAsync().Wait();
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
    }
}
