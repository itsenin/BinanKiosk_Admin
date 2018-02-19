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
    public partial class Home : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        public Home()
        {
            DoubleBuffered = true;
            InitializeComponent();
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timestamp.Interval = 1;
            timestamp.Start();
            loadImageList();
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
        }

        private void timestamp_Tick(object sender, EventArgs e)
        {
            timestamp.Enabled = true;
            timestamp.Tick += new System.EventHandler(OnTimerEvent);
        }

        private void loadImageList()
        {
            conn.Open();

            using (var cmd = new MySqlCommand("SELECT image_name from images", conn))
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        lst_sliderPics.Items.Add(reader["image_name"].ToString());
                    }
                }
            }
            conn.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image img = new Bitmap(openFile.FileName);
                    {
                        pb_preview.Image = img;
                        lb_imageName.Text = openFile.SafeFileName;
                        pnl_Save.Visible = true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            conn.Open();
            var serializedImage = ImageToByteArray(pb_preview.Image, pb_preview);

            //INSERT
            using (var cmd = new MySqlCommand("INSERT INTO images(image_id, image_name, image_byte) VALUES(NULL, @name, @image)", conn))
            {
                cmd.Parameters.AddWithValue("@name", lb_imageName.Text);
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved to Database!");
            }

            //RETRIEVE using Name
            using (var cmd = new MySqlCommand("SELECT image_byte from images WHERE image_name = @name", conn))
            {
                cmd.Parameters.AddWithValue("@name", lb_imageName.Text);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    byte[] deserializedImage = (byte[])reader["image_byte"];
                    pb_test.Image = GetDataToImage(deserializedImage);
                }
            }
            conn.Close();

        }

        public static byte[] ImageToByteArray(Image img, PictureBox pb)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (pb.Image != null)
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return null;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Config.CallHome(this);
        }
        private void btnOfficers_Click(object sender, EventArgs e)
        {
            Config.CallOfficers(this);
        }
        private void btnOffices_Click(object sender, EventArgs e)
        {
            Config.CallOffices(this);
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            Config.CallMap1(this);
        }
        private void btnJobs_Click(object sender, EventArgs e)
        {
            Config.CallJobs(this);
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            Config.CallServices(this);
        }
    }
}
