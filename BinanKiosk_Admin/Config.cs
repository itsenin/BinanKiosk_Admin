using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public static class Config
    {
        public static MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public static string currentfloor = "f13";

        private static async void changeForm(Form current, Form next)
        {
            next.Show();
            await Task.Delay(250);
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

        public static void CallMain(Form current)
        {
            MainForm mf = new MainForm();
            changeForm(current, mf);
        }
        public static void CallHome(Form current)
        {
            Home hm = new Home();
            changeForm(current, hm);
        }
        public static void CallOfficers(Form current)
        {
            Officers ofc = new Officers();
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

        public static void CallJobs(Form current)
        {
            Jobs jb = new Jobs();
            changeForm(current, jb);
        }

        public static void CallServices(Form current)
        {
            Services sv = new Services();
            changeForm(current, sv);
        }
    }
}
