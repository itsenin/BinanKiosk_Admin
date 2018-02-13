using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public static class Config
    {
        public static MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        private static async void changeForm(Form current, Form next)
        {
            next.Show();
            await Task.Delay(100);
            current.Hide();
            current.Close();
        }


        public static void CallLogin(Form current)
        {
            Login lg = new Login();
            changeForm(current, lg);
        }

        public static void CallSignup(Form current)
        {
            Signup sg = new Signup();
            changeForm(current, sg);
        }

        public static void loadbuttonnames(Button[] buttonarray)
        {
            foreach (Button btn in buttonarray)
            {
                MySqlConnection conn = Config.conn;
                MySqlDataReader reader;
                conn.Open();
                string queryStr = "SELECT room_label from floors WHERE room_id = '" + btn.Name + "' ";
                MySqlCommand cmd = new MySqlCommand(queryStr, conn);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        btn.Text = reader.GetString(0);
                    }
                }
                conn.Close();
            }


        }

        public static void CallHome(Form current)
        {
            Home hm = new Home();
            changeForm(current, hm);
        }

        public static void CallServices(Form current)
        {
            Services sv = new Services();
            changeForm(current, sv);
        }

        public static void CallMain(Form current)
        {
            MainForm mf = new MainForm();
            changeForm(current, mf);
        }

        public static void CallOfficers(Form current)
        {
            Officer ofc = new Officer();
            changeForm(current, ofc);
        }

        public static void CallOffices(Form current)
        {
            Offices ofs = new Offices();
            changeForm(current, ofs);
        }

        public static void CallMap1(Form current)
        {
            Mapground mg = new Mapground();
            changeForm(current, mg);
        }

        public static void CallSearch(Form current)
        {
            Officer ofc = new Officer();
            changeForm(current, ofc);
        }
    }
}
