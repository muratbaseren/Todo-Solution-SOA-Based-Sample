using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Todo.DataAccessLayer;
using Todo.Entities;

namespace Todo.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public List<TodoItem> GetTodoItemList()
        {
            TodoContext db = new TodoContext();
            return db.TodoItems.ToList();
        }

        [HttpPost]
        public bool AddTodoItem(FormDataCollection form)
        {
            TodoContext db = new TodoContext();
            db.TodoItems.Add(
                new TodoItem()
                {
                    Subject = form["subject"],
                    Description = form["desc"],
                    IsCompleted = false
                });

            if (db.SaveChanges() > 0)
            {
                SetCache("last_update", DateTime.Now.ToString());

                return true;
            }
            else
            {
                return false;
            }
        }

        public string SetCache([FromUri] string key, [FromUri] string value)
        {
            WebClient wc = new WebClient();

            return wc.DownloadString(
                    "http://localhost:57429/MemCache/Set?key=" + key + "&value=" + value);
        }

        public string GetCache([FromUri]string key)
        {
            WebClient wc = new WebClient();

            return wc.DownloadString(
                "http://localhost:57429/MemCache/Get?key=" + key);
        }

    }
}
