using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cheque_Ordering_system.Common;

namespace Cheque_Ordering_system.Web
{
    public partial class Booktype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!FormSecurity.IsAuthUser(Session["EmpCode"].ToString(), "CHQAD"))
                {
                    Response.Redirect("NotAuthorized.aspx", false);    
                }
            }
        }
    }
}