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
        bool editable = false;
        List<KeyValuePair<string, string>> mapchange;
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        public Mapground()
        {
            InitializeComponent();
            timer1.Interval = 5000;
            timer1.Start();
            mapchange = new List<KeyValuePair<string, string>>();

        }


        public int seconds = 0;

        private void OnTimerEvent(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
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

            Button[] btnarray = { r101, r102, r103, r104, r105, r106, r107, r108, r109, r110, r111, r112, r201, r202,
             r203, r204, r205, r206, r207, r208, R209, r210, r211, r212, r213, r214, r215, r216, r217, r218, r219
            , r220, r221, r301, r302, r303, r304, r305, r306, r307, r308, r309, r310, r311};

       
            conn.Open();
            string queryStr2 = "SELECT office_name from offices WHERE room_name = 'No Room' ";
            MySqlCommand cmd2 = new MySqlCommand(queryStr2, conn);
            reader = cmd2.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //child.Text = reader.GetString(0);
                    unassignrooms.Items.Add(reader["office_name"].ToString());
                }
            }
            conn.Close();

            Visiblemap();
            panelmap();

            for (int i = 0; i < btnarray.Length; i++)
            {

                if (btnarray[i].Text.ToString() == "Empty Room")
                {

                    btnarray[i].BackColor = Color.LimeGreen;

                }
                else
                {
                    btnarray[i].BackColor = Color.Red;
                }


            }


        }

        private void panelmap()
        {
            foreach (Control child in panelfloor1.Controls)
            {
                if (child is Button)
                {
                    conn.Open();
                    string queryStr = "SELECT office_name from offices WHERE room_name = '" + child.Name + "' ";
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
                    string queryStr = "SELECT office_name from offices WHERE room_name = '" + child.Name + "' ";
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

            foreach (Control child in panelfloor3.Controls)
            {
                if (child is Button)
                {
                    conn.Open();
                    string queryStr = "SELECT office_name from offices WHERE room_name = '" + child.Name + "' ";
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

        private void Visiblemap()
        {

            if (Config.currentfloor == "f1")
            {
                panelfloor1.Visible = true;
                panelfloor2.Visible = false;
                panelfloor3.Visible = false;
            }
            else if (Config.currentfloor == "f2")
            {
                panelfloor1.Visible = false;
                panelfloor2.Visible = true;
                panelfloor3.Visible = false;

            }
            else if (Config.currentfloor == "f3")
            {
                panelfloor1.Visible = false;
                panelfloor2.Visible = false;
                panelfloor3.Visible = true;

            }

            else
            {
                MessageBox.Show(Config.currentfloor.ToString());

            }


        }

        private void clickbait(object sender, EventArgs e)
        {

            var bttn = sender as Button;
            string unassigned;
            getnames(bttn.Name, bttn.Text);

            if (editable)
            {
                if(unassignrooms.SelectedIndex == -1)//nothing is selected
                {

                    if (bttn.Text !="Empty Room")
                    {
                      unassignrooms.Items.Add(bttn.Text);
                        
                       

                    }
                    
                    
                    //mapchange.Remove(bttn.Text);
                    //var templist = mapchange.re.Where(stringtocheck => stringtocheck.ToString() == bttn.Text);
                    var templist = mapchange;
                    int index = 0;
                    foreach (var roomname in templist)
                    {
                        if (roomname.Value == bttn.Text)
                        {

                            mapchange.RemoveAt(index);
                            break;

                        }
                        index++;
                    }
                    mapchange = templist.ToList<KeyValuePair<string,string>>();
                    bttn.Text = "";
                }

                else //something is selected
                {
                    unassigned = unassignrooms.SelectedItem.ToString();
                    if (bttn.Text == "Empty Room")
                    {
                        mapchange.Add(new KeyValuePair<string, string>(bttn.Name, unassigned));
                        bttn.Text = unassigned;
                        var templistindex = unassignrooms.SelectedIndex;
                        unassignrooms.Items.RemoveAt(templistindex);
                    }
                    else //conflict
                    {
                        MessageBox.Show("Room is currently occupied! Empty the room first.");
                    }
                }

                

               

            }
            

        }

        

        private void getnames(string roomname2,string roomtxt2)
        {
            roomtxt.Text = roomname2;
            valuelbl.Text = roomtxt2;

        }

        //edit button
        private void button6_Click(object sender, EventArgs e)
        {
            editable = true;
            if (editable)
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

            editable = false;
            if (editable)
            {
                roomtxt.Text = "No Room";
                

            }


            foreach (var saveroom in mapchange)
            {

                MySqlConnection conn = Config.conn;
                MySqlDataReader reader;
                conn.Open();
                string queryStr = "UPDATE offices SET room_name = '" + saveroom.Key + "' WHERE office_name = '" + saveroom.Value + "' ";
                MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                reader = cmd.ExecuteReader();
                conn.Close();

            }

            foreach (var unusedoffice in unassignrooms.Items)
            {

                MySqlConnection conn = Config.conn;
                MySqlDataReader reader;
                conn.Open();
                string queryStr = "UPDATE offices SET room_name = 'No Room' WHERE office_name = '" + unusedoffice.ToString() + "' ";
                MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                reader = cmd.ExecuteReader();
                conn.Close();


            }
            
            disabling();

            if (panelfloor1.Visible == true)
            {
                Config.currentfloor = "f1";
                Config.CallMap1(this);

            }

            else if (panelfloor2.Visible == true)
            {
                Config.currentfloor = "f2";
                Config.CallMap1(this);
            }

            else if (panelfloor3.Visible == true)
            {
                Config.currentfloor = "f3";
                Config.CallMap1(this);
            }

            else
            {
                MessageBox.Show("oh no");
            }


        }

        private void gfbutton_Click(object sender, EventArgs e)
        {
            panelfloor1.Visible = true;
            panelfloor2.Visible = false;
            panelfloor3.Visible = false;

        }

        private void secondfbutton_Click(object sender, EventArgs e)
        {
            panelfloor1.Visible = false;
            panelfloor2.Visible = true;
            panelfloor3.Visible = false;

        }

        private void removerooms_Click(object sender, EventArgs e)
        {

            //MySqlConnection conn = Config.conn;
            //MySqlDataReader reader;
            //conn.Open();
            //string queryStr = "UPDATE offices SET room_name = '" + roomtxt.Text + "' WHERE office_name = '" + valuelbl.Text + "' ";
            //MySqlCommand cmd = new MySqlCommand(queryStr, conn);
            //reader = cmd.ExecuteReader();
            //conn.Close();
            //disabling();

        }

        private void thirdfbutton_Click(object sender, EventArgs e)
        {
            panelfloor1.Visible = false;
            panelfloor2.Visible = false;
            panelfloor3.Visible = true;
            

        }

        private void btnOfficers_Click(object sender, EventArgs e)
        {
            Config.CallOfficers(this);
        }

        private void btnOffices_Click(object sender, EventArgs e)
        {
            Config.CallDepartments(this);
        }

        private void btnJobs_Click(object sender, EventArgs e)
        {
            Config.CallJobs(this);
        }

        private void btn_registration_Click(object sender, EventArgs e)
        {
            Config.CallSignup(this);
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Config.CallLogin(this);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            

        }

        
    }
}
