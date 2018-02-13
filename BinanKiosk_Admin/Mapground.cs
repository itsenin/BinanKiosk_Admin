using MySql.Data.MySqlClient;
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
    public partial class Mapground : Form
    {
        

        public Mapground()
        {
            InitializeComponent();
            timer1.Interval = 5000;
            timer1.Start();
            
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
            
            Config.CallMain(this);

        }

        private void btnMaps_Click(object sender, EventArgs e)
        {

        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            Config.CallServices(this);
        }

        private void Mapground_Load_1(object sender, EventArgs e)
        {
            Button[] btnarray = { r101, r102, r103, r104, r105, r106, r107, r108, r109, r110, r111, r112 };
            Config.loadbuttonnames(btnarray);

        }

        private void r101_Click(object sender, EventArgs e)
        {
            roomtxt.Text = r101.Text;
            valuelbl.Text = r101.Name;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            roomtxt.Enabled = true;
            savebtn.Enabled = true;
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = Config.conn;
            MySqlDataReader reader;
            conn.Open();
            string queryStr = "UPDATE floors SET room_label = '" + roomtxt.Text + "' WHERE room_id = '"+valuelbl.Text+"' ";
            MySqlCommand cmd = new MySqlCommand(queryStr, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
            roomtxt.Enabled = false;
            savebtn.Enabled = false;
            Config.CallMap1(this);
        }
    }
}
