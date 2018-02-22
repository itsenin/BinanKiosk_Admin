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
        List<int> imgIds;

        public Home()
        {
            DoubleBuffered = true;
            InitializeComponent();
            lst_sliderPics.AllowDrop = true;
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            btn_delete.Enabled = false;
            imgIds = new List<int>();
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
            lst_sliderPics.Items.Clear(); //clear image name list on reloads
            imgIds.Clear(); //clear image ID list on reloads

            conn.Open();

            using (var cmd = new MySqlCommand("SELECT image_id,image_name from images WHERE image_type = 1", conn))
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        lst_sliderPics.Items.Add(reader["image_name"].ToString());
                        imgIds.Add((int)reader["image_id"]);
                    }
                }
            }
            conn.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            lst_sliderPics.ClearSelected();
            btn_delete.Enabled = false;

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
                        lbl_imageName.Text = openFile.SafeFileName;
                        pnl_Save.Visible = true;
                        btn_save.Visible = true; //show save button in case it is disabled by review
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            conn.Open();
            var serializedImage = ImageToByteArray(pb_preview.Image, pb_preview);

            //INSERT
            using (var cmd = new MySqlCommand("INSERT INTO images(image_id, image_type, image_name, image_byte) VALUES(NULL, 1, @name, @image)", conn))
            {
                cmd.Parameters.AddWithValue("@name", lbl_imageName.Text);
                cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Saved to Database!");
            }
            conn.Close();

            loadImageList();
            pnl_Save.Visible = false;

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

        private void lst_sliderPics_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lst_sliderPics.SelectedIndex;
            if (index > -1) //if there is something currently selected
            {
                //RETRIEVE using Name and ID
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT image_byte from images WHERE image_name = @name AND image_id = @id", conn))
                {
                    string name = lst_sliderPics.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", imgIds[index]);

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        byte[] deserializedImage = (byte[])reader["image_byte"];
                        pb_preview.Image = GetDataToImage(deserializedImage);
                    }
                }

                pnl_Save.Visible = true;

                btn_delete.Enabled = true; //enable delete on "View" mode

                btn_save.Visible = false; //hide save button to just preview Image of item selected from the list

                lbl_imageName.Text = lst_sliderPics.SelectedItem.ToString();
                conn.Close();
            }
            else
                return;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string name = lst_sliderPics.SelectedItem.ToString();
            int id = imgIds[lst_sliderPics.SelectedIndex];

            if (MessageBox.Show("Delete the selected item?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                conn.Open();
                using (var cmd = new MySqlCommand("DELETE from images WHERE image_name = @name AND image_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                btn_delete.Enabled = false;
                loadImageList();
                pnl_Save.Visible = false;
            }
            else
                return;

        }
    }
}
