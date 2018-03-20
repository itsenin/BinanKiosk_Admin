using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace KioskAPI
{
    public static class Config
    {
        //public static MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");
        public static string imageRootPath = HttpContext.Current.Server.MapPath("~/Images");

        public enum Directory{
            HOME,
            JOBS,
            SERVICES,
            OFFICERS,
            DEPARTMENTS
        }
    }
}