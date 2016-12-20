using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Cheque_Ordering_system.Common
{
    public class FormSecurity
    {

        public static bool IsAuthUser(string Ecode, string TaskCode)
        {
            //return true;
            SqlConnection conn = new SqlConnection(AppSettings.ConnectionString);

            string sql;
            DataTable DT1, DT2;

            sql = "SELECT TaskName " +
                        "FROM WS_USER_UserGroup, WS_USER_GroupTask, WS_USER_Tasks " +
                        "WHERE WS_USER_UserGroup.GroupCode= WS_USER_GroupTask.GroupCode " +
                        "AND WS_USER_Tasks.TaskCode=WS_USER_GroupTask.TaskCode " +
                        "AND usercode='" + Ecode + "' and WS_USER_Tasks.TaskCode='" + TaskCode + "' " +
                        "And WS_USER_Tasks.ModuleCode ='" + ConfigurationManager.AppSettings["MODULE_CODE"] + "'";
            DT1 = DbHelper.GetDataTable(sql, conn);

            sql = "SELECT TASKCODE FROM WS_USER_USERTASK WHERE UserCode='" + Ecode + "' AND TaskCode='" + TaskCode + "' And ModuleCode ='" + ConfigurationManager.AppSettings["MODULE_CODE"] + "'";
            DT2 = DbHelper.GetDataTable(sql, conn);

            if (DT1.Rows.Count > 0 || DT2.Rows.Count > 0)
                return true;
            else
                return true;
        }

    }

}
