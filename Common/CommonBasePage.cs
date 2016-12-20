using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Contexts;

public class CommonBasePage : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Context.Session != null)
        {
            if (Session.IsNewSession)
            {
                string szCookieHeader = Request.Headers["Cookie"];
                if ((szCookieHeader != null) && (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                {
                    ErrLogger.WriteError("Session Expired","0");
                    Response.Redirect("~/Web/Error.aspx", false);
                }
            }
        }
    }
}
