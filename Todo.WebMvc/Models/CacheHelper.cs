using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Todo.WebMvc.Models
{
    public class CacheHelper
    {
        public static DateTime GetLastUpdateDateTime()
        {
            WebClient wc = new WebClient();
            string result = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update").Trim('\"');
            return DateTime.Parse(result);
        }
    }
}