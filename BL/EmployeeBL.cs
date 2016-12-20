    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cheque_Ordering_system.DA;
using Cheque_Ordering_system.Models;

namespace Cheque_Ordering_system.BL
{
    public class EmployeeBL
    {
        EmployeeDA _EmployeeDA = new EmployeeDA();

        internal EmpDetailsDOM EmployeeDetails(string EmpCode, string CoCode)
        {
            return _EmployeeDA.GetEmployeeDetails(EmpCode, CoCode);
        }

        internal string GetCoCode(string EmpCode)
        {
            return _EmployeeDA.GetCoCode(EmpCode);
        }

      
    }
}