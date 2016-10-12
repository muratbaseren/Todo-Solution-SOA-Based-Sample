using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
