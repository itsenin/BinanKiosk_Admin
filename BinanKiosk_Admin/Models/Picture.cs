using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanKiosk_Admin.Models
{
    public class Picture
    {
        public string Name { get; set; }
        public string FolderName { get; set; }
        public byte[] img { get; set; }
    }
}
