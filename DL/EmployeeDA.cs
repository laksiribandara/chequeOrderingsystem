using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Cheque_Ordering_system.Common;
using Cheque_Ordering_system.Models;
namespace Cheque_Ordering_system.DA
{
    public class EmployeeDA
    {

        readonly SqlConnection conn = new SqlConnection(AppSettings.ConnectionString);

        public EmpDetailsDOM GetEmployeeDetails(string empCode, string CompanyCode)
        {
            DataTable dTable;

            //StringBuilder sqlBuilder = new StringBuilder();
            EmpDetailsDOM oEmpDetail = new EmpDetailsDOM();

            try
            {
                string sSql;

                sSql = "Select S.ESNAME AS EmpName, S.ESKNAM as KnownName, S.estitl AS Title, S.ESDOJ AS DOJ,S.ESDOB AS DOB,S.ESEMPTYPE As EmpType,  " +
                        "D.EDBRCH AS BranchCode, B.BRLDES AS BranchName, D.EDDEPT AS DeptCode,D.EDINCH AS Position,D.EDRATE AS Salary, DP.DPLDES AS DeptName,  " +
                        "ISNULL((SELECT MAX(EDEFDT) AS EDDate FROM HRDDAT WHERE (EDCODE = S.ESCODE) AND (EDRFC = 'PM')),'') AS LPDATE,  " +
                        "D.EDOCCG AS GradeCode,  G.OGLDES As GradeName, D.EDJTIT AS JobCode, JTLDES As JobTitle,  " +
                        "D.EDNTLE AS DesgCode,  NTLDES As Designation, D.EDSHFT AS EDSHFT,D.EDINCH AS InchargeCode " +
                        "From HRSDAT S Inner Join HRDDAT D " +
                        "On S.ESCODE = D.EDCODE And S.COCODE = D.COCODE And D.EDACT = 'A' " +
                        "Inner Join HRBRCH B On D.EDBRCH = B.BRCODE And D.COCODE = B.COCODE " +
                        "Inner Join HRDEPT DP On D.EDDEPT = DP.DPCODE And D.COCODE = DP.COCODE " +
                        "Inner Join HROCCG G On D.EDOCCG = G.OGCODE And  D.COCODE = G.COCODE " +
                        "Inner Join HRJTIT J On D.EDJTIT = J.JTCODE And D.COCODE = J.COCODE " +
                        "Inner Join HRNTLE DS On D.EDNTLE = DS.NTCODE And D.COCODE = DS.COCODE " +
                        "AND S.ESCODE = '" + empCode + "' And S.COCODE = '" + CompanyCode + "' " +
                        "ORDER BY convert(int,D.edcode) ";

                dTable = DbHelper.GetDataTable(sSql, conn);


                if (dTable.Rows.Count > 0 && dTable != null)
                {
                    //AppraiseeDetail.EmpCode = dTable.Rows[0]["EmpCode"].ToString().Trim();
                    oEmpDetail.EmpCode = empCode;
                    oEmpDetail.EmpName = dTable.Rows[0]["EmpName"].ToString().Trim();
                    oEmpDetail.EmpTitle = dTable.Rows[0]["Title"].ToString().Trim();
                    oEmpDetail.KnownName = dTable.Rows[0]["KnownName"].ToString().Trim();
                    oEmpDetail.BranchCode = dTable.Rows[0]["BranchCode"].ToString().Trim();
                    oEmpDetail.BranchName = dTable.Rows[0]["BranchName"].ToString().Trim();
                    oEmpDetail.DeptCode = dTable.Rows[0]["DeptCode"].ToString().Trim();
                    oEmpDetail.DeptName = dTable.Rows[0]["DeptName"].ToString().Trim();
                    oEmpDetail.GradeCode = dTable.Rows[0]["GradeCode"].ToString().Trim();
                    oEmpDetail.GradeName = dTable.Rows[0]["GradeName"].ToString().Trim();
                    oEmpDetail.JobCode = dTable.Rows[0]["JobCode"].ToString().Trim();
                    oEmpDetail.JobTitle = dTable.Rows[0]["JobTitle"].ToString().Trim();
                    oEmpDetail.LPDATE = Convert.ToDateTime(dTable.Rows[0]["LPDATE"]);
                    oEmpDetail.EmpType = dTable.Rows[0]["EmpType"].ToString().Trim();
                    oEmpDetail.CurrentPosition = dTable.Rows[0]["Position"].ToString().Trim();
                    oEmpDetail.ShiftCode = dTable.Rows[0]["EDSHFT"].ToString().Trim();
                    oEmpDetail.InchargeCode = dTable.Rows[0]["InchargeCode"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return oEmpDetail;
        }

        public List<EmpDetailsDOM> GetAllEmployee()
        {
            {

                string sql = "";

                sql = " SELECT   S.ESCODE + ' : ' + S.ESNAME AS Employee ,S.ESCODE AS StaffNo" +
                      " FROM  HRDDAT AS D INNER JOIN HRSDAT AS S " +
                      " ON D.EDCODE = S.ESCODE AND D.COCODE = S.COCODE " +
                      " WHERE D.edact = 'A' AND (D.edrfc NOT IN ('RS','DA') AND D.COCODE = 'SBL') " +                     
                      " AND D.EDCODE = S.ESCODE AND D.COCODE = S.COCODE ORDER BY convert(int,D.edcode) ";
                DataTable dt = DbHelper.GetDataTable(sql, conn);

                List<EmpDetailsDOM> EmployeeDetais = new List<EmpDetailsDOM>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmpDetailsDOM rowEmpDetails = new EmpDetailsDOM();
                    rowEmpDetails.EmpName = dt.Rows[i]["Employee"].ToString();
                    rowEmpDetails.EmpCode = dt.Rows[i]["StaffNo"].ToString();
                    EmployeeDetais.Add(rowEmpDetails);
                }

                return EmployeeDetais;
            }

        }

        public List<EmpDetailsDOM> GetRelSuperviors(string BranchCode, string DeptCode)
        {
            string DepSupervisorgrade = "6"; //// Department Supervisors Grade 7 or higer..

            string sql = "";

            sql = " SELECT  S.ESCODE + ' : ' + S.ESNAME AS Employee, S.ESCODE AS EmpCode, S.COCODE AS CompCode,";
            sql += " D.EDBRCH + ' : ' +HRBRCH.BRLDES AS Branch, D.EDDEPT+ ' : ' + HRDEPT.DPLDES  AS Department, D.EDOCCG As Grade,D.EDJTIT As JobTitle, D.EDBRCH,D.EDDEPT ";
            sql += " FROM  HRDDAT AS D INNER JOIN  ";
            sql += " HRSDAT AS S ON D.EDCODE = S.ESCODE AND D.COCODE = S.COCODE INNER JOIN  ";
            sql += " HRDEPT ON D.COCODE = HRDEPT.COCODE AND D.EDDEPT = HRDEPT.DPCODE INNER JOIN ";
            sql += " HRBRCH ON D.COCODE = HRBRCH.COCODE AND D.EDBRCH = HRBRCH.BRCODE  ";
            sql += " WHERE D.EDRFC NOT IN ('RS','DA') AND D.edact = 'A'  ";


            //If a Branch Employee
            if (BranchCode != "SDH")
            {
                sql += " AND D.EDBRCH = '" + BranchCode + "' ";
                sql += " AND D.EDINCH IN ('BMG','ABM') ";
            }
            //If a Department Employee
            else
            {
                sql += " AND D.EDDEPT = '" + DeptCode + "' ";
                sql += " AND Convert(int,D.EDOCCG) > " + DepSupervisorgrade + " ";
            }
            sql += "AND (D.edrfc NOT IN ('RS','DA') AND D.COCODE = 'SBL')  ";
        
            sql += "AND D.EDCODE = S.ESCODE AND D.COCODE = S.COCODE  ";
            sql += " ORDER BY convert(int,D.edcode) ";

            try
            {
                DataTable dt = DbHelper.GetDataTable(sql, conn);

                List<EmpDetailsDOM> Emplist = new List<EmpDetailsDOM>();

                foreach (DataRow dr in dt.Rows)
                {

                    EmpDetailsDOM rowEmpList = new EmpDetailsDOM();
                    rowEmpList.EmpName = dr["Employee"].ToString();
                    rowEmpList.EmpCode = dr["EmpCode"].ToString();
                    Emplist.Add(rowEmpList);

                }

                return Emplist;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetUser(string CompanyCode, string StaffNo)
        {

            string sql;

            sql = "SELECT " +
                "HRSDAT.ESCODE as StaffNo, " +
                "HRSDAT.ESNAME AS EmpName, " +
                //"HRSDAT.ESFNAM AS EmpName, " +
                "HRSDAT.ESDOB as  DOB, " +
                "HRSDAT.ESTITL AS Title, " +
                "HRSDAT.ESSEX AS Sex, " +
                "LEFT(Convert(varchar,ISNULL(HRSDAT.ESDOJ,''),113),11)  AS DOJ, " +
                // "HRSDAT.ESDOJ AS DOJ, " +
                "HISTORY.EDBRCH AS BrchCode, " +
                "ISNULL((SELECT Top 1 BRLDES FROM HRBRCH WHERE HISTORY.EDBRCH = HRBRCH.BRCODE AND HISTORY.COCODE = HRBRCH.COCODE),HISTORY.EDBRCH) AS BrchName, " +
                "HISTORY.EDDEPT AS DeptCode,  " +
                "ISNULL((SELECT Top 1 DPLDES FROM HRDEPT WHERE HISTORY.COCODE = HRDEPT.COCODE AND HISTORY.EDDEPT = HRDEPT.DPCODE),HISTORY.EDDEPT) AS DeptName, " +
                "ISNULL((SELECT MAX(EDEFDT) AS EDDate FROM HISTORY WHERE (EDCODE = '" + StaffNo + "') AND (EDRFC = 'PM')),'') AS LPDATE,  " +
                "HISTORY.EDNTLE AS GradeCode,  " +
                "ISNULL((SELECT Top 1 OGLDES FROM HROCCG WHERE HISTORY.EDNTLE = HROCCG.OGCODE AND HISTORY.COCODE = HROCCG.COCODE),HISTORY.EDNTLE) AS GradeName, " +
                "HISTORY.EDJTIT AS JTCODE,  " +
                "ISNULL((SELECT Top 1 JTLDES FROM HRJTIT WHERE HISTORY.EDJTIT = HRJTIT.JTCODE AND HISTORY.COCODE=HRJTIT.COCODE),HISTORY.EDJTIT) AS JobTitle, " +
                "HISTORY.EDNTLE AS DESGCode,  " +
                "ISNULL((SELECT Top 1 NTLDES from  HRNTLE WHERE HISTORY.COCODE = HRNTLE.COCODE AND HISTORY.EDNTLE = HRNTLE.NTCODE),HISTORY.EDNTLE ) AS DESG  " +
                "FROM  HRSDAT   " +
                "INNER JOIN HISTORY ON HRSDAT.ESCODE = HISTORY.EDCODE AND HISTORY.COCODE=HRSDAT.COCODE  " +
                "WHERE (HRSDAT.ESCODE = '" + StaffNo + "') AND (HISTORY.EDACT = 'A' AND HISTORY.EDRFC NOT IN ('RS','DA'))  AND (HRSDAT.COCODE='" + CompanyCode + "')";


            return DbHelper.GetDataTable(sql, conn);

        }

        public string GetCoCode(string empCode)
        {
            DataTable dTable = new DataTable();
            string coCode = null;
            string sql = "";
            sql = "SELECT COCODE FROM HRDDAT WHERE EDACT='A'  ";
            sql += "AND EDRFC NOT IN ('RS','DA') AND COCODE='SBL'  ";
            sql += "AND EDCODE='" + empCode + "'  ";

            dTable = new DataTable();
            dTable = DbHelper.GetDataTable(sql, conn);
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
                    dTable = DbHelper.GetDataTable(sql, conn);
                    if (dTable.Rows.Count > 0)
                    {
                        coCode = dTable.Rows[0]["COCODE"].ToString().Trim();
                    }
                }
            }
            return coCode;
        }

        internal bool IsCorrectStaff(string StaffNo)
        {
            string sql="select * from hrsdat where escode='"+StaffNo +"'";
            DataTable dt = DbHelper.GetDataTable(sql, conn);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}