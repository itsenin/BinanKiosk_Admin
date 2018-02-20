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

        string roomname, roomtext;
        

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

            Config.CallHome(this);

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
            //Button[] btnarray = { r101, r102, r103, r104, r105, r106, r107, r108, r109, r110, r111, r112 };
            //loadbuttonnames(btnarray);

            MySqlConnection conn = Config.conn;
            MySqlDataReader reader;

            //if (Config.currentfloor == 1)
            //{
            //    panelfloor1.Visible = true;
            //    panelfloor2.Visible = false;
            //}
            //else if (Config.currentfloor == 2)
            //{
            //    panelfloor2.Visible = true;
            //    panelfloor1.Visible = false;

            //}

            //else
            //{
            //    MessageBox.Show(Config.currentfloor.ToString());

            //}

            foreach (Control child in panelfloor1.Controls)
            {
                if (child is Button)
                {
                    conn.Open();
                    string queryStr = "SELECT room_label from floors WHERE room_id = '" + child.Name + "' ";
                    MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            child.Text = reader.GetString(0);
                        }
                    }
                    conn.Close();

                    child.Click += new EventHandler(clickbait);
                }
            }

            foreach (Control child in panelfloor2.Controls)
            {
                if (child is Button)
                {
                    conn.Open();
                    string queryStr = "SELECT room_label from floors WHERE room_id = '" + child.Name + "' ";
                    MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            child.Text = reader.GetString(0);
                        }
                    }
                    conn.Close();

                    child.Click += new EventHandler(clickbait);
                }
            }


        }

        private void clickbait(object sender, EventArgs e)
        {

            var bttn = sender as Button;
            getnames(bttn.Name, bttn.Text);

        }

        

        private void getnames(string roomname2,string roomtxt2)
        {
            roomtxt.Text = roomtxt2;
            valuelbl.Text = roomname2;

        }

        //edit button
        private void button6_Click(object sender, EventArgs e)
        {
            if (roomtxt.Text!= "")
            {
                enabling();
            }
            
        }

        private void enabling()
        {

            roomtxt.Enabled = true;
            savebtn.Enabled = true;

        }

        private void disabling ()
        {
            roomtxt.Enabled = false;
            savebtn.Enabled = false;

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (roomtxt.Text == "")
            {
                roomtxt.Text = "Empty Room";

            }


            MySqlConnection conn = Config.conn;
            MySqlDataReader reader;
            conn.Open();
            string queryStr = "UPDATE floors SET room_label = '" + roomtxt.Text + "' WHERE room_id = '"+valuelbl.Text+"' ";
            MySqlCommand cmd = new MySqlCommand(queryStr, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
            disabling();
            
            //if (panelfloor1.Visible = true)
            //{

            //    Config.CallMap1(this);

            //}

            //else if (panelfloor2.Visible = true)
            //{
            //    Config.currentfloor = 2;
            //    Config.CallMap1(this);
            //}

            //else
            //{
            //    MessageBox.Show("oh no");
            //}
           

        }

        private void gfbutton_Click(object sender, EventArgs e)
        {
            panelfloor1.Visible = true;
            panelfloor2.Visible = false;

        }

        private void secondfbutton_Click(object sender, EventArgs e)
        {
            panelfloor1.Visible = false;
            panelfloor2.Visible = true;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        
    }
}
