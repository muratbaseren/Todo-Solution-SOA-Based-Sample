using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Todo.Entities;

namespace Todo.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            string jsonData = wc.DownloadString("http://localhost:51844/api/Home/GetTodoItemList");

            List<TodoItem> items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonData);

            return View(items);
        }


        public ActionResult Create()
        {
            return View(new TodoItem());
        }

        [HttpPost]
        public ActionResult Create(TodoItem model)
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };

            NameValueCollection values = new NameValueCollection();
            values.Add("subject", model.Subject);
            values.Add("desc", model.Description);

            var response = wc.UploadValues("http://localhost:51844/api/Home/AddTodoItem", values);

            bool result = bool.Parse(System.Text.Encoding.UTF8.GetString(response));

            if (result)
            {
                // Insert 'ü ben yaptım.
                // =======================================================================
                DateTime date = DateTime.Now;

                wc = new WebClient();

                values = new NameValueCollection();
                values.Add("key", "last_update");
                values.Add("value", date.ToString());

                wc.UploadValues("http://localhost:51844/api/Home/SetCache", values);

                wc = new WebClient();
                string result2 = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update");

                HttpContext.Cache["last_update"] = DateTime.Parse(result2.Replace("\"", "").Replace(@"\", ""));
                // =======================================================================

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Hata oluştu.");
                return View(model);
            }

        }


        public JsonResult IsUpdated()
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            string result = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update");

            if (HttpContext.Cache["last_update"] != null)
            {
                DateTime mvc_last_update = (DateTime)HttpContext.Cache["last_update"];
                DateTime api_last_update = DateTime.Parse(result.Replace("\"", "").Replace(@"\", ""));

                if (mvc_last_update != api_last_update)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}