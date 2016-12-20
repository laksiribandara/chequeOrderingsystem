using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Cheque_Ordering_system.Common
{
    public class Message
    {
        public void SendEMail(string EmailAddress, string CCAdderss, string Subject, string Message)
        {
            SmtpClient sc = new SmtpClient();
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(AppSettings.MailAddress);
            msg.To.Add(new MailAddress(EmailAddress));
            if (CCAdderss != null && CCAdderss != string.Empty)
            {
                msg.CC.Add(new MailAddress(CCAdderss));
            }
            msg.Subject = Subject;
            msg.Body = Message;
            msg.IsBodyHtml = true;
            sc.Host = AppSettings.MailServer;
            sc.Credentials = new NetworkCredential(AppSettings.MailUser, AppSettings.MailPassword);
            sc.Send(msg);
            //sc.SendAsync(msg, null);
        }

        public void SendEMailManager(string EmailAddress, string CCAdderss, string Subject, string Message)
        {
            SmtpClient sc = new SmtpClient();
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(AppSettings.MailAddress);
            msg.To.Add(new MailAddress(EmailAddress));
            if (CCAdderss != null && CCAdderss != string.Empty)
            {
                msg.CC.Add(new MailAddress(CCAdderss));
            }
            msg.Subject = Subject;
            msg.Body = Message;
            msg.IsBodyHtml = true;
            sc.Host = AppSettings.MailServer;
            sc.Credentials = new NetworkCredential(AppSettings.MailUser, AppSettings.MailPassword);
            //sc.Send(msg);
        }

        internal string GenerateBodyEmp(string LvDesc, string KnownName, string NoOfDays, string LateDays)
        {
            string EmpBody;

            EmpBody = "<html><body>Dear " + KnownName + ",<br/><br/>" + NoOfDays + " " + LvDesc + " have been deducted from your leave balance for Late Attendances as per Circular No. SCL 2004/023.<br/><br/><u>Days Considered</u><br/><br/>" + LateDays + "<br/>Thank You, HRD</body></html>";

            return EmpBody;
        }

        internal string GenerateBodyMgr(string LvDesc, string FullName, string EmpNo, string NoOfDays, string LateDays)
        {
            string MgrBody;

            MgrBody = "<html><body>Dear Sir/Madam <br/><br/>" + NoOfDays + " " + LvDesc + " have been deducted from " + FullName + " (" + EmpNo + ")'s leave balance for Late Attendances as per Circular No. SCL 2004/023.<br/><br/><u>Days Considered</u><br/><br/>" + LateDays + "<br/>Thank You, HRD</body></html>";

            return MgrBody;
        }

        internal string GenerateBodyAttenModification(string EmpNo, string FullName, string JobCode, string BrCode, string DeptCode, string TransactionDate, string RecTimeIn, string RecTimeOut, string ActTimeIn, string ActTimeOut, string HrReason)
        {
            string MgrBody;

            MgrBody = "<html><body>Dear Sir/Madam, <br/><br/>The attendance record of " + FullName + " (" + EmpNo + ") of your department for the date " + TransactionDate + " is amended due to " + HrReason + " as follows.<br/><br/> ";
            MgrBody += " <table border=1px> ";
            MgrBody += " <tr> ";
            MgrBody += " <th>Staff No</th><th>Name</th><th>Branch</th><th>Department</th><th>Job Title</th><th>Recorded Time-In</th><th>Recorded Time-Out</th><th>Amended Time-In</th><th>Amended Time-Out</th> ";
            MgrBody += " </tr> ";
            MgrBody += " <tr> ";
            MgrBody += " <td>" + EmpNo + "</td><td>" + FullName + "</td><td>" + BrCode + "</td><td>" + DeptCode + "</td><td>" + JobCode + "</td><td>" + RecTimeIn + "</td><td>" + RecTimeOut + "</td><td>" + ActTimeIn + "</td><td>" + ActTimeOut + "</td> ";
            MgrBody += " </tr> ";
            MgrBody += " </table> <br/><br/>";
            MgrBody += " Thank You, HRD</body></html> ";

            return MgrBody;
        }
    }
}