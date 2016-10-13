using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todo.WebMvcCache.Controllers
{
    public class MemCacheController : Controller
    {
        // ..../MemCache/Get?key=last_update
        public JsonResult Get(string key)
        {
            string result = string.Empty;

            if (HttpContext.Cache[key] != null)
            {
                result = HttpContext.Cache[key].ToString();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // ..../MemCache/Get?key=last_update&value=abc
        public JsonResult Set(string key, string value)
        {
            HttpContext.Cache[key] = value;

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}