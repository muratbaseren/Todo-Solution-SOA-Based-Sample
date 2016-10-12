using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Todo.Entities;

namespace Todo.DataAccessLayer
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext()
        {
            Database.SetInitializer(new MyDatabaseCreator());
        }
    }

    public class MyDatabaseCreator : CreateDatabaseIfNotExists<TodoContext>
    {
        protected override void Seed(TodoContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                context.TodoItems.Add(new TodoItem()
                {
                    Subject = FakeData.NameData.GetCompanyName(),
                    Description = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(50,150)),
                    IsCompleted = (i % 2 == 0) ? true : false
                });
            }

            context.SaveChanges();
        }
    }
}
