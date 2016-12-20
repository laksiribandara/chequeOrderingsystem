using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cheque_Ordering_system.Common
{
    public class AppSettings
    {
        public static string ConnectionString;
        public static string KapitiConn;
        public static int CallId;
        public static string MailDomain { get; set; }
        public static string MailServer { get; set; }
        public static string MailSubject { get; set; }
        public static string MailUser { get; set; }
        public static string MailPassword { get; set; }
        public static string MailAddress { get; set; }
        public static string AdminMail { get; set; }
        public static string MailLink { get; set; }
    }

}