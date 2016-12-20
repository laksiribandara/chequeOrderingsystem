//--------------------------------------------------------------------------------------------------------------//
//------------------------------------------- Development Login Page -------------------------------------------//
//--------------------------------------------------------------------------------------------------------------//

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{

    //DB Connection String-------------------------------------------------------------------------------

    //From AppSettings
    //static string conString = ConfigurationManager.AppSettings["ConnectionString"]; 

    //From ConnectionStrings
    static string conString = ConfigurationManager.ConnectionStrings["CHQConn"].ConnectionString;

    //---------------------------------------------------------------------------------------------------

    SqlConnection connection = new SqlConnection(conString);
    private SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
    DataTable dTable = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtusn.Focus();
        txt.Text = User.Identity.Name;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string companyCode = string.Empty;
        string userName = string.Empty;
        string passWord = string.Empty;
        string correctPassWord = string.Empty;
        string empCode = string.Empty;

        Session.Remove("CoCode");
        Session.Remove("EmpCode");

        userName = txtusn.Text.Trim();
        passWord = "a";

        if (userName != "")
        {
            companyCode = GetCompanyCode(userName);
            //   correctPassWord =GetPassWord(companyCode, userName);

            string pswd = Encryptor.MD5Hash(passWord);
            //a = 0cc175b9c0f1b6a831c399e269772661

            correctPassWord = "0cc175b9c0f1b6a831c399e269772661";

            if (pswd == correctPassWord)
            {
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, "");
                string cookiestr = FormsAuthentication.Encrypt(tkt);
                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);
                Session["EmpCode"] = txtusn.Text.Trim();
                Session["CoCode"] = companyCode;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                //Incorrect Password
                Session["EmpCode"] = txtusn.Text.Trim();
                Session["CoCode"] = companyCode;
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            //No user name
        }
    }

    private string GetPassWord(string companyCode, string userName)
    {
        string correctPassword = null;

        try
        {
            string sql = "";
            sql = " SELECT PSWRD ";
            sql += "FROM  TESTUSERS ";
            sql += "WHERE ECODE = '" + userName + "' ";

            dTable = new DataTable();
            dTable = RetrieveInDataTable(sql);
            if (dTable.Rows.Count > 0 && dTable != null)
            {
                correctPassword = dTable.Rows[0]["PSWRD"].ToString().Trim();
            }

            return correctPassword;
        }
        catch (Exception ex)
        {

            //Log error if possible

            String clientIP = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ?
                                HttpContext.Current.Request.UserHostAddress :
                                HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            ErrLogger.WriteError(ex.Message, clientIP + "/" + userName);

            return correctPassword;
        }
    }

    private string GetCompanyCode(string empCode)
    {
        string coCode = null;
        string sql = "";
        sql = "SELECT COCODE FROM HRDDAT WHERE EDACT='A'  ";
        sql += "AND EDRFC NOT IN ('RS','DA') AND COCODE='SSB'  ";
        sql += "AND EDCODE='" + empCode + "'  ";

        dTable = new DataTable();
        dTable = RetrieveInDataTable(sql);
        if (dTable != null)
        {
            if (dTable.Rows.Count > 0)
            {
                coCode = dTable.Rows[0]["COCODE"].ToString().Trim();
            }
            else if (dTable.Rows.Count == 0)
            {
                sql = " SELECT COCODE FROM HRDDAT WHERE EDACT='A'  ";
                sql += " AND EDRFC NOT IN ('RS','DA') AND COCODE='SBL'   ";
                sql += "AND EDCODE='" + empCode + "'  ";
                dTable = new DataTable();
                dTable = RetrieveInDataTable(sql);
                if (dTable.Rows.Count > 0)
                {
                    coCode = dTable.Rows[0]["COCODE"].ToString().Trim();
                }
            }
        }
        return coCode;
    }

    public DataTable RetrieveInDataTable(string sql)
    {
        try
        {
            dTable = new DataTable();
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = CommandType.Text;
            sqlDataAdapter.SelectCommand = command;
            sqlDataAdapter.Fill(dTable);
            connection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally { connection.Close(); }
        return dTable;
    }
}

public static class Encryptor
{
    public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        //compute hash from the bytes of text
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //get hash result after compute it
        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            //change it into 2 hexadecimal digits
            //for each byte
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }
}
