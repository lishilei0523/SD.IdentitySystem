using System;
using System.Web;
using ShSoft.Framework2016.Infrastructure.Global;

namespace SD.UAC.AppService
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            InitDatabase.Register();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}