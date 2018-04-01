using BinanKiosk_Admin.APIServices;
using BinanKiosk_Admin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
        string selectedImageName = "";
        OpenFileDialog open;

        public Home()
        {
            DoubleBuffered = true;
            InitializeComponent();
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
            pb_preview.Image = null;
            lst_sliderPics.Items.Clear(); //clear image name list on reloads
            imgIds.Clear(); //clear image ID list on reloads

            conn.Open();

            using (var cmd = new MySqlCommand("SELECT * from slider_images", conn))
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
                    open = openFile;

                    //Image img = new Bitmap(openFile.FileName);

                    //pb_preview.Image = img;
                    //lbl_imageName.Text = openFile.SafeFileName;

                    pb_preview.Image = Image.FromFile(openFile.FileName);
                    lbl_imageName.Text = openFile.SafeFileName;

                    pnl_Save.Visible = true;
                    btn_save.Visible = true; //show save button in case it is disabled by view

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                //Check if image with same name exists
                using (var cmd = new MySqlCommand("SELECT * FROM slider_images WHERE image_name = @name", conn))
                {
                    cmd.Parameters.AddWithValue("@name", lbl_imageName.Text);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            throw new ArgumentException("An image with the same name already exists! Please rename the new image, or delete existing entry");
                        }
                    }
                }

                ////serialize Image for Model
                //var serializedImage = Config.ImageToByteArray(pb_preview.Image);

                ////Create Picture Model to send to API
                //Picture pic = new Picture { Name = lbl_imageName.Text, FolderName = "Home", image = serializedImage };

                ////send the picture to the API(returns path)
                //string path = Config.SavePic(pic);


                //Windows Auth
                Config.SaveImage(open, Subfolders.Home);//saves currently opened File to a subfolder in the remote directory


                //INSERT if no duplicates found
                using (var cmd = new MySqlCommand("INSERT INTO slider_images(image_id, image_name) VALUES(NULL, @name)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", open.SafeFileName);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved to Database!");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to Save Image: " + ex.Message);
            }
            finally
            {
                conn.Close();
                loadImageList();
                pnl_Save.Visible = false;
            }
        }

        #region Navigation buttons
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
            Config.CallDepartments(this);
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
        private void btn_registration_Click(object sender, EventArgs e)
        {
            Config.CallSignup(this);
        }
        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Config.CallLogin(this);
        }
        #endregion

        private void lst_sliderPics_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lst_sliderPics.SelectedIndex;
            if (index > -1) //if there is something currently selected
            {
                //RETRIEVE using Name and ID
                conn.Open();
                using (var cmd = new MySqlCommand("SELECT image_name from slider_images WHERE image_id = @id", conn))
                {
                    string name = lst_sliderPics.SelectedItem.ToString();
                    cmd.Parameters.AddWithValue("@id", imgIds[index]);

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        selectedImageName = reader["image_name"].ToString();
                        pb_preview.Image = Config.GetImage(selectedImageName, Subfolders.Home);
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
                pb_preview.Image = null;
                ////delete file
                //Config.DeletePic(selectedImageName);

                //delete in remote
                Config.DeleteImage(Subfolders.Home, name);

                //delete in dbase
                conn.Open();                

                using (var cmd = new MySqlCommand("DELETE from slider_images WHERE image_id = @id", conn))
                {
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

        private void btn_add_MouseHover(object sender, EventArgs e)
        {
            btn_add.BackgroundImage = Properties.Resources.button4;
        }

        private void btn_add_MouseLeave(object sender, EventArgs e)
        {
            btn_add.BackgroundImage = Properties.Resources.button1;
        }


    }
}
