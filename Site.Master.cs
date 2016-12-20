using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cheque_Ordering_system.DA;
using Cheque_Ordering_system.Common;
using System.Text;
using System.Data;
using System.Configuration;

namespace Cheque_Ordering_system
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        EmployeeDA _EmpDal = new EmployeeDA();

        #region Public Variables


        private String CompanyCode
        {
            get { return Session["CoCode"].ToString(); }
            set { Session["CoCode"] = value; }
        }

        private String EmployeeCode
        {
            get { return Session["EmpCode"].ToString(); }
            set { Session["EmpCode"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DisplayUserInfo();
                VisibleLink();
                //  VisibleTab();
            }
        }

        private void DisplayUserInfo()
        {
            if (Session["EmpCode"] != null && Session["CoCode"] != null)
            {
                var dt = _EmpDal.GetUser(CompanyCode, EmployeeCode);

                StringBuilder UserInfoString = new StringBuilder();
                StringBuilder UserBranch = new StringBuilder();

                if (dt == null)
                {
                    UserInfoString.Append(EmployeeCode + " No User Info");
                    lblUserInfo.Text = UserInfoString.ToString();
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    UserInfoString.Append(dr["EmpName"]);
                    UserBranch.Append(dr["BrchCode"]);
                }
                else
                {
                    UserInfoString.Append(EmployeeCode + " No User Info");
                }

                lblUserInfo.Text = UserInfoString.ToString().ToUpper();
                lblLoggedUserStaffNo.Text = EmployeeCode.ToUpper();

                Session["BrCode"] = UserBranch.ToString();

            }
        }


        private void VisibleLink()
        {
            if (FormSecurity.IsAuthUser(Session["EmpCode"].ToString(), "CHQAD"))
            {
                //lbAdmin.Visible = true;
                //lbAssigningTimeMachines.Visible = true;
                lbbookcol.Visible = true;
                lbbooktype.Visible = true;

            }
        }

        private void VisibleTab()
        {
            if (!FormSecurity.IsAuthUser(Session["EmpCode"].ToString(), "CHQAD"))
            {
                liAdmin.Visible = false;                

            }
        }

        protected void libLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect(ConfigurationManager.AppSettings["LOGIN_URL"], false);
        }

        protected void libHome_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["HOME_URL"], false);
        }


    }
}





