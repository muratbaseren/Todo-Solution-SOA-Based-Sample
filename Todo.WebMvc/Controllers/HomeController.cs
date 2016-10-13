using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Hata oluştu.");
                return View(model);
            }

        }


    }
}