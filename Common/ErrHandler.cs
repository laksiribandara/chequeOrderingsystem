using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web;

    [DebuggerStepThrough]
    public class ErrLogger
    {
        public static void WriteError(string errorMessage, string param , bool isUnhandled = false)
        {
            try
            {
                var path = "~/Error/" + DateTime.Today.ToString("dd-MMM-yy") + ".txt";
                if ((!File.Exists(HttpContext.Current.Server.MapPath(path))))
                {
                    File.Create(HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (var w = File.AppendText(HttpContext.Current.Server.MapPath(path)))
                {
                    lock (w)
                    {
                        w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                        string err;
                        if (isUnhandled)
                        {
                            err = errorMessage;
                        }
                        else
                        {
                            var thisStackTrace = new StackTrace();
                            var thisStackFrame = thisStackTrace.GetFrame(1);
                            var thisMethodBase = thisStackFrame.GetMethod();

                            err = "Error in: " + HttpContext.Current.Request.Url + Environment.NewLine +
                                  "Method: " + thisMethodBase.Name + Environment.NewLine +
                                  "Error Message: " + errorMessage + Environment.NewLine +
                                  "AppId: " + param;
                        }
                        w.WriteLine(err);
                        w.WriteLine(Environment.NewLine);
                    }
                    w.Flush();
                    w.Close();
                }

            }

            catch (Exception)
            {
                //Ignore
            }
        }
    }
