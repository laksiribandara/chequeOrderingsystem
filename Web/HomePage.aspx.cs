using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cheque_Ordering_system.Web
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //--------Used SP
            //-----> spGetEmpCurrentTimeDetails, spGetEmpCurrentTimeDetailsBrDept, spGetEmpBrancheDeptAttenDetails

            //--------2016/04/25 Actual Timein maybe less than Recored Timein (requested by Nuwan Weragala) ---> Isuru
            //--------2016/04/25 The date Actual-Time-In Date can enter follwoing Date ---> Isuru
            string a = string.Empty();
        }
    }
}