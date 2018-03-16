using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;


namespace KioskAPI.Models
{
    public class Picture
    {
        public string Name { get; set; }
        public string FolderName { get; set; }
        public byte[] img { get; set; }
    }
}