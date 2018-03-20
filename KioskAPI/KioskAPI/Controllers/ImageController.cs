using KioskAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KioskAPI.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        public void DeletePicture(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
            }
        }

        [HttpGet]
        public Picture GetPicture(string path)
        {
            try
            {
                string[] str = path.Split('/');

                Picture img = new Picture
                {
                    Name = str[2],
                    FolderName = str[1],
                    image = ImageToByteArray(Image.FromFile(path))
                };

                return img;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public string SavePicture(Picture picture)
        {
            string savePath = Config.imageRootPath + "/" + picture.FolderName;
            //save image locally
            try
            {
                Image img = GetDataToImage(picture.image);
                Bitmap bmp = new Bitmap(img);
                if (bmp != null)
                {
                    if(!Directory.Exists(Config.imageRootPath))
                    {
                        Directory.CreateDirectory(Config.imageRootPath);
                    }
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    bmp.Save(savePath + "/" + picture.Name, System.Drawing.Imaging.ImageFormat.Png);
                    bmp.Dispose();
                    img.Dispose();
                    img = null;
                    bmp = null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return savePath + "/" + picture.Name;
        }

        public Image GetDataToImage(byte[] pData)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(pData) as Image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] ImageToByteArray(Image img)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                if (img != null)
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                    img = null;
                }
                return ms.ToArray();
            }
        }
    }
}

