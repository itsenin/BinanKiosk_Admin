using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public partial class MainForm : Form
    {
        private static int i;
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timestamp.Interval = 1;
            timestamp.Start();
        }

        public int seconds = 0;

        private void OnTimerEvent(object sender, EventArgs e)
        {
            lbldate.Text =  DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
        }

        private void timestamp_Tick_1(object sender, EventArgs e)
        {
            timestamp.Enabled = true;
            timestamp.Tick += new System.EventHandler(OnTimerEvent);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Home hm = new Home();
            this.Hide();
            hm.FormClosed += (s, args) => this.Close();
            hm.ShowDialog();
            hm.Focus();
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {

        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            Services sv = new Services();
            this.Hide();
            sv.FormClosed += (s, args) => this.Close();
            sv.ShowDialog();
            sv.Focus();
        }
    }
}
