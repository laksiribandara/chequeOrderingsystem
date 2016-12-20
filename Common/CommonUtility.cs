using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Cheque_Ordering_system.Common
{
    public class CommonUtility
    {

        public List<int> GetAllYears()
        {
            return Enumerable.Range(1994, 20).ToList();
        }

        public List<int> GetAllMonths()
        {
            return Enumerable.Range(1, 12).ToList();
        }

        public static Tuple<int, int, int> DateDiff(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == toDate)
            { return new Tuple<int, int, int>(0, 0, 0); }

            int y = 0;
            int m = 0;
            int d = 0;

            DateTime tempDate = fromDate;

            toDate = toDate.AddDays(1); // This is to consider To Date within the period

            if (tempDate <= toDate)
            {
                //Years
                while (tempDate <= toDate)
                {
                    tempDate = tempDate.AddYears(1);
                    y = y + 1;
                }

                y = y - 1;
                tempDate = tempDate.AddYears(-1);

                //Moths
                while (tempDate <= toDate)
                {
                    tempDate = tempDate.AddMonths(1);
                    m = m + 1;
                }

                m = m - 1;
                tempDate = tempDate.AddMonths(-1);

                fromDate = fromDate.AddYears(y);
                fromDate = fromDate.AddMonths(m);

                //Days
                while (fromDate < toDate)
                {
                    fromDate = fromDate.AddDays(1);
                    d = d + 1;
                }

            }

            //Output
            return new Tuple<int, int, int>(y, m, d);

        }

        public static bool IsValidFromTo(int fromYear, int fromMonth, int ToYear, int toMonth)
        {
            return ((fromYear > ToYear) || (fromYear == ToYear && (fromMonth > toMonth)));
        }

        public static bool IsDate(string inputDate)
        {
            DateTime temp;
            return DateTime.TryParse(inputDate, out temp) ? true : false;
        }

        public static bool IsNumeric(string stringToTest)
        {
            Double temp;
            return Double.TryParse(stringToTest, out temp);
        }

        public static bool IsValidEmailAddress(string email)
        {

            string patten = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
            Match match = Regex.Match(email.Trim(), patten, RegexOptions.IgnoreCase);
            if (match.Success)
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