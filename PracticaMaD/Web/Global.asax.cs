using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.HTTP.Util.IoC;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// The language and country information is loaded when applicattion
        /// starts. It will be used in the combo boxes.
        /// </summary>
        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager IoCManager = new IoCManagerNinject();
            IoCManager.Configure();

            Application["managerIoC"] = IoCManager;
            LogManager.RecordMessage("NInject kernel container configured and started", MessageType.Info);

            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionManager.TouchSession(Context);
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
            ((IKernel)Application["kernelIoC"]).Dispose();

            LogManager.RecordMessage("NInject kernel container disposed", MessageType.Info);
        }
    }
}