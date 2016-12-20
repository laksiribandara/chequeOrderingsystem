using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Cheque_Ordering_system.Common;
using System.Configuration;

namespace Cheque_Ordering_system
{
    public class Global1 : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AppSettings.ConnectionString = ConfigurationManager.ConnectionStrings["CHQConn"].ConnectionString;
        }

       
    }
}