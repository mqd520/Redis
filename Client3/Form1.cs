using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStack.Redis;
using ServiceStack.CacheAccess;
using System.Diagnostics;

namespace Client3
{
    public partial class Form1 : Form
    {
        private static readonly PooledRedisClientManager prcm;

        static Form1()
        {
            prcm = new PooledRedisClientManager(
                new string[] { "123456@192.168.0.60:6379" },
                new string[] { "123456@192.168.0.60:6380", "123456@192.168.0.60:6381", "123456@192.168.0.60:6382" },
                new RedisClientManagerConfig
            {
                AutoStart = true,
                MaxReadPoolSize = 5,
                MaxWritePoolSize = 5
            });
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IRedisClient client1 = prcm.GetClient();
            IRedisClient client2 = prcm.GetClient();
            IRedisClient client3 = prcm.GetClient();
            //IRedisClient client4 = prcm.GetClient();
            IRedisClient client5 = prcm.GetClient();
            //prcm.DisposeClient(client5 as RedisNativeClient);
            //client5.Dispose();
            IRedisClient client6 = prcm.GetClient();
            client6.Set<string>("username6", "usdsy87dysudus");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                string value = Guid.NewGuid().ToString();
                IRedisClient client1 = prcm.GetClient();
                client1.Set<string>("username2", value);
                Debug.WriteLine(string.Format("{0}:{1} username2={2}", client1.Host, client1.Port, value));
                client1.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                IRedisClient client1 = prcm.GetReadOnlyClient();
                string value = client1.Get<string>("username2");
                Debug.WriteLine(string.Format("{0}:{1} username2={2}", client1.Host, client1.Port, value));
                client1.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                ICacheClient client1 = prcm.GetCacheClient();
                client1.Dispose();
            }
        }
    }
}
