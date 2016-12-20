using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;


[DebuggerStepThrough]
public class Parameter
{
    private string p;
    private char p_2;

    public string Name { get; set; }
    public string Value { get; set; }

    public Parameter(string name, string value)
    {
        this.Name = name;
        this.Value = value;
    }


}

[DebuggerStepThrough]
public static class DbHelper
{
    public static bool IsExists(string sql, SqlConnection conn, SqlTransaction tran = null)
    {

        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);
        cmd.CommandTimeout = 1000000;
        var da = new SqlDataAdapter(cmd);

        var dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool IsExists(string sql, List<Parameter> param, SqlConnection conn, SqlTransaction tran = null)
    {

        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);
        cmd.CommandTimeout = 1000000;
        foreach (Parameter p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }

        var da = new SqlDataAdapter(cmd);

        var dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static DataTable GetDataTableFromSp(string spName, List<Parameter> param, SqlConnection conn, SqlTransaction tran = null)
    {
        var cmd = tran == null ? new SqlCommand(spName, conn) : new SqlCommand(spName, conn, tran);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 1000000;
        foreach (Parameter p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }

        var da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        var dt = new DataTable();
        da.Fill(dt);

        return dt;
    }

    public static DataTable GetDataTable(string sql, List<Parameter> param, SqlConnection conn, SqlTransaction tran = null)
    {
        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);

        foreach (Parameter p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }

        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        int fill = da.Fill(dt);
        return dt;
    }

    public static DataTable GetDataTable(string sql, SqlConnection conn, SqlTransaction tran = null)
    {
        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        int fill = da.Fill(dt);
        return dt;
    }

    public static string GetScalar(string sql, SqlConnection conn, SqlTransaction tran = null)
    {
        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();
        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            return ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            return string.Empty;
        }

    }

    public static string GetScalar(string sql, List<Parameter> param, SqlConnection conn, SqlTransaction tran = null)
    {
        var cmd = tran == null ? new SqlCommand(sql, conn) : new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        foreach (var p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }

        var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();
        da.Fill(ds);

        return ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0].ToString() : string.Empty;

    }

    public static int Execute(string sql, SqlConnection conn)
    {
        var cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        conn.Open();
        var ret = cmd.ExecuteNonQuery();
        conn.Close();
        return ret;
    }

    public static int Execute(string sql, SqlConnection conn, SqlTransaction tran)
    {
        var cmd = new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        var ret = cmd.ExecuteNonQuery();
        return ret;
    }

    public static int Execute(string sql, List<Parameter> param, SqlConnection conn)
    {
        var cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        foreach (var p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }
        var ret = cmd.ExecuteNonQuery();

        return ret;
    }

    public static int Execute(string sql, List<Parameter> param, SqlConnection conn, SqlTransaction tran)
    {
        var cmd = new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        foreach (var p in param)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }
        var ret = cmd.ExecuteNonQuery();

        return ret;
    }

    public static object ExecuteScalar(string sql, List<Parameter> parameterList, SqlConnection conn, SqlTransaction tran)
    {
        var cmd = new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        foreach (var p in parameterList)
        {
            cmd.Parameters.AddWithValue(p.Name, p.Value);
        }
        var ret = cmd.ExecuteScalar();

        return ret;
    }

    public static object ExecuteScalar(string sql, List<SqlParameter> parameterList, SqlConnection conn, SqlTransaction tran)
    {
        var cmd = new SqlCommand(sql, conn, tran);
        cmd.CommandType = CommandType.Text;
        cmd.CommandTimeout = 1000000;
        foreach (var p in parameterList)
        {
            cmd.Parameters.Add(p);
        }
        var ret = cmd.ExecuteScalar();

        return ret;
    }

    public static ArrayList RetriveInArrayList(string SQL, SqlConnection Conn)
    {
       
        SqlCommand command = new SqlCommand(SQL, Conn);
        SqlDataAdapter da = new SqlDataAdapter();        
        ArrayList li = new ArrayList();
        Conn.Open();
        SqlDataReader dr = command.ExecuteReader();
        command.CommandTimeout = 1000000;
        while (dr.Read())
        {
            li.Add(dr.Read());
        }

        Conn.Close();
        return li;
    }

   

}
