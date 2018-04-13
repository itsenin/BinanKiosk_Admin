using BinanKiosk_Admin.APIServices;
using BinanKiosk_Admin.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public static class Config
    {
        //Windows Auth paths
        //public const string HOST_IP = @"\\192.168.1.4"; //Fahad IP
        //public const string HOST_IP = @"\\192.168.0.3"; //Gab PC IP
        //public const string HOST_IP = @"\\192.168.137.218"; //Gab Laptop IP
        //public const string HOST_IP = @"\\192.168.0.27"; //Kenneth Laptop IP
        //public const string HOST_IP = @"\\192.168.0.28"; //Kenneth PC IP
        //public const string HOST_IP = @"\\192.168.43.191"; //Genrev PC IP
        public const string HOST_IP = @"\\192.168.43.250"; //Shanine PC IP
        //public const string HOST_IP = @"\\192.168.43.52"; //Shanine IP

        public const string FOLDER = "SharedFolder";
        public const string imageRootPath = "Images";
        public static string basePath = Path.Combine(HOST_IP, FOLDER, imageRootPath); //Remote root Image path
        //WebServices paths
        public const string BASE_ADDRESS = "http://192.168.1.4:8080/api/";
        public const string BASE_ADDRESS_DEBUG = "http://localhost:8080/api/";
        //public const string BASE_ADDRESS_DEBUG = "http://localhost:54470/api/";
        public static MySqlConnection conn = new MySqlConnection("SERVER=" + HOST_IP.TrimStart('\\') + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "myuser" + ";" + "PASSWORD=mypass" + ";");
        //public static MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk_actual" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");

        public static string currentfloor = "f1";

        private static async void changeForm(Form current, Form next)
        {
            next.Show();
            await Task.Delay(250);
            current.Hide();
            current.Close();
        }
        
        #region WindowsAuthentication
        public static void SaveImage(OpenFileDialog openfile, Subfolders sub)
        {
            string savePath = Path.Combine(basePath, sub.ToString()); //Remote SubFolder path
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            //save to remote destination
            var destination = Path.Combine(savePath, openfile.SafeFileName);
            File.Copy(openfile.FileName, destination, true); //Overwrite set to true
        }
        public static Bitmap GetImage(string img_name, Subfolders sub)//get image from source using image name and subfolder
        { 
            using (Image img = Image.FromFile(Path.Combine(basePath, sub.ToString(), img_name)))
            {
                return new Bitmap(img);
            }
        }
        public static void DeleteImage(Subfolders sub, string image_name)
        {
            File.Delete(Path.Combine(basePath,sub.ToString(),image_name));
        }
        #endregion
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

        public static void CallDepartments(Form current)
        {
            Departments dep = new Departments();
            changeForm(current, dep);
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
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
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


        public static Picture SavePic2(Picture picture)
        {
            ServiceClientWrapper client = new ServiceClientWrapper();
#if DEBUG
            var Address = Config.BASE_ADDRESS_DEBUG + "Image/SavePicture2";
#else
            var Address = Config.BASE_ADDRESS + "Image/SavePicture";
#endif
            var result = client.Send(new ServiceRequest { BaseAddress = Address, HttpProtocol = Protocols.HTTP_GET, Body = JsonConvert.SerializeObject(picture) });
            var path = JsonConvert.DeserializeObject<Picture>(result.Response);

            return path;
        }

        public static void DeletePic(string path)
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
            //var status = JsonConvert.DeserializeObject<int>(result.Response);
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
