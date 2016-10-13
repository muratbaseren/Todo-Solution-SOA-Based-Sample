using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using Newtonsoft.Json;
using Todo.Entities;
using System.Collections.Specialized;

namespace Todo.Winforms
{
    public partial class Form1 : Form
    {
        private DateTime last_update = DateTime.Now;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            string result = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update");

            last_update = DateTime.Parse(result.Replace("\"", "").Replace(@"\", ""));
            LoadData();

            timer1.Start();
        }

        private void LoadData()
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            string jsonData = wc.DownloadString("http://localhost:51844/api/Home/GetTodoItemList");

            List<TodoItem> items = JsonConvert.DeserializeObject<List<TodoItem>>(jsonData);

            listBox1.DataSource = items;
            listBox1.DisplayMember = "Subject";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAddTodoItem_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };

            NameValueCollection values = new NameValueCollection();
            values.Add("subject", txtSubject.Text);
            values.Add("desc", txtDescription.Text);

            var response = wc.UploadValues("http://localhost:51844/api/Home/AddTodoItem", values);

            bool result = bool.Parse(System.Text.Encoding.UTF8.GetString(response));

            if (result)
            {
                last_update = DateTime.Now;

                wc = new WebClient();

                values = new NameValueCollection();
                values.Add("key", "last_update");
                values.Add("value", last_update.ToString());

                wc.UploadValues("http://localhost:51844/api/Home/SetCache", values);

                // Verileri tekrar yükle..
                LoadData();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            string result = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update");

            DateTime api_last_update = DateTime.Parse(result.Replace("\"", "").Replace(@"\", ""));

            if (api_last_update != last_update)
            {
                timer1.Enabled = false;
                MessageBox.Show("Güncellemeler var.");
            }
        }
    }
}
