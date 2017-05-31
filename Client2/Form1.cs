using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ServiceStackHelper;

namespace Client2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServiceStackHelper.ServiceStackHelper.CreateClient("192.168.0.60", 6379, "123456");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string value = ServiceStackHelper.ServiceStackHelper.getValueString("username");
            MessageBox.Show(value);
        }
    }
}
