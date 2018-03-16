using BinanKiosk_Admin.APIServices;
using BinanKiosk_Admin.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public static class Config
    {
        //WebServices paths
        public const string BASE_ADDRESS = "http://localhost:8080/api/";
        public const string BASE_ADDRESS_DEBUG = "http://localhost:54470/api/";

        public static MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public static string currentfloor = "f1";

        private static async void changeForm(Form current, Form next)
        {
            next.Show();
            await Task.Delay(250);
            current.Hide();
            current.Close();
        }
        #region callforms
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

        public static void CallJobs(Form current)
        {
            Jobs jb = new Jobs();
            changeForm(current, jb);
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

        #endregion
        #region ImageCommands
        public static byte[] ImageToByteArray(Image img)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (img != null)
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
        }

        public static Image GetDataToImage(byte[] pData)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(pData) as Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return null;
            }
        }
        #endregion
        #region API calls
        public static string SavePic(Picture picture)
        {
            ServiceClientWrapper client = new ServiceClientWrapper();
#if DEBUG
            var Address = Config.BASE_ADDRESS_DEBUG + "Image/SavePicture";
#else
            var Address = Config.BASE_ADDRESS + "Image/SavePicture";
#endif
            var result = client.Send(new ServiceRequest { BaseAddress = Address, HttpProtocol = Protocols.HTTP_POST, Body = JsonConvert.SerializeObject(picture) });
            var path = JsonConvert.DeserializeObject<string>(result.Response);

            return path;
        }

        public static int DeletePic(string path)
        {
            ServiceClientWrapper client = new ServiceClientWrapper();
#if DEBUG
            var Address = Config.BASE_ADDRESS_DEBUG + "Image/DeletePicture";
#else
            var Address = Config.BASE_ADDRESS + "Image/DeletePicture";
#endif
            var Params = new Dictionary<string, string>
            {
                { "path", path }
            };

            var result = client.Send(new ServiceRequest { BaseAddress = Address, HttpProtocol = Protocols.HTTP_POST, RequestParameters = Params });
            var status = JsonConvert.DeserializeObject<int>(result.Response);

            return status;
        }

        public static Picture GetPic(string path)
        {
            ServiceClientWrapper client = new ServiceClientWrapper();
#if DEBUG
            var Address = Config.BASE_ADDRESS_DEBUG + "Image/GetPicture";
#else
            var Address = Config.BASE_ADDRESS + "Image/GetPicture";
#endif
            var Params = new Dictionary<string, string>
            {
                { "path", path }
            };

            var result = client.Send(new ServiceRequest { BaseAddress = Address, HttpProtocol = Protocols.HTTP_GET, RequestParameters = Params });
            var picture = JsonConvert.DeserializeObject<Picture>(result.Response);

            return picture;
        }
        #endregion
    }
}
