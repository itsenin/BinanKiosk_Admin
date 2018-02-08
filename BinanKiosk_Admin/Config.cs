using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static void CallHome(Form current)
        {
            Home hm = new Home();
            changeForm(current, hm);
        }

        public static void CallSearch(Form current)
        {
            Search src = new Search();
            changeForm(current, src);
        }

        public static void CallServices(Form current)
        {
            Services sv = new Services();
            changeForm(current, sv);
        }

    }
}
