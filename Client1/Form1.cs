using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceStack.Redis;

namespace Client1
{
    public partial class Form1 : Form
    {
        RedisClient client = null;

        public Form1()
        {
            InitializeComponent();
            client = new RedisClient("192.168.0.60", 6379, "123456");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.Add<string>("key1", "string1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string value = client.Get<string>("key1");
            if (value == null)
            {
                MessageBox.Show("不存在key1或已过期!");
            }
            else
            {
                MessageBox.Show(value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client.Add<string>("key2", "string2", DateTime.Now.AddMinutes(1));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string value = client.Get<string>("key2");
            if (value == null)
            {
                MessageBox.Show("不存在key2或已过期!");
            }
            else
            {
                MessageBox.Show(value);
            }
        }
    }
}
