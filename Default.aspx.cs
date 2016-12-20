using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cheque_Ordering_system.BL;

namespace Cheque_Ordering_system
{
    public partial class _Default : System.Web.UI.Page
    {
        private string CompanyCode
        {
            get { return Session["CoCode"].ToString(); }
            set { Session["CoCode"] = value; }
        }

        private string EmployeeCode
        {
            get { return Session["EmpCode"].ToString(); }
            set { Session["EmpCode"] = value; }
        }
        EmployeeBL _EmployeeBL = new EmployeeBL();       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["hdnUserCode"] != null)
            {
                EmployeeCode = Request.Form["hdnUserCode"].ToString();
            }
            else
            {
                EmployeeCode = User.Identity.Name;
            }
            Session["EmpCode"] = EmployeeCode;


            CompanyCode = _EmployeeBL.GetCoCode(EmployeeCode);
            if (CompanyCode != null)
            {
                Response.Redirect("~/Web/HomePage.aspx");
                //Response.Redirect("~/Web/AssigningEmailAddresses.aspx");
            }
            else
            {
                Response.Redirect("~/Web/NotAuthorized.aspx", false);
            }

        }
    }
}
