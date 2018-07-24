using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonefyConsole
{
    class TranslationService
    {
        public static string Translate(string from, string to, string translate)
        {
            string page = null;
            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                wc.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
                wc.Encoding = Encoding.UTF8;

                string url = string.Format(@"http://translate.google.com.tr/m?hl=en&sl={0}&tl={1}&ie=UTF-8&prev=_m&q={2}",
                                            from, to, Uri.EscapeUriString(translate));

                page = wc.DownloadString(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            page = page.Remove(0, page.IndexOf("<div dir=\"ltr\" class=\"t0\">")).Replace("<div dir=\"ltr\" class=\"t0\">", "");
            int last = page.IndexOf("</div>");
            page = page.Remove(last, page.Length - last);

            return page;
        }
    }
}
