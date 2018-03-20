using BinanKiosk_Admin.Models;
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
    public partial class Services : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        List<int> svcIds;
        string selectedServiceImagePath = "";

        public Services()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }
        private void Services_Load(object sender, EventArgs e)
        {
            svcIds = new List<int>();
            loadServiceList();
        }

        #region navigation
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
        #endregion

        private void disableOtherButtons(object sender)
        {
            foreach(Control child in pnl_buttons.Controls)
            {
                Button btn = child as Button;
                if(btn!=sender)
                    btn.Enabled = false;
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            lst_servicePics.ClearSelected();

            disableOtherButtons(sender);
            pnl_Save.Show();

            txt_serviceName.Enabled = true;
            txt_serviceName.Text = "";

            pb_preview.Image = Properties.Resources.plus;
            pb_preview.Enabled = true;

            btn_save.Visible = true;
        }

        private void pb_preview_Click(object sender, EventArgs e)
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
                        txt_serviceName.Text = openFile.SafeFileName.Split('.')[0];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            conn.Open();
            var serializedImage = Config.ImageToByteArray(pb_preview.Image);

            if (txt_serviceName.Text != String.Empty)
            {
                //Create Picture Model to send to API
                Picture pic = new Picture { Name = txt_serviceName.Text, FolderName = "Services", image = serializedImage };

                //send the picture to the API(returns path)
                string path = Config.SavePic(pic);

                //INSERT IMAGE
                using (var cmd = new MySqlCommand("INSERT INTO services(service_id, service_name, image_path) VALUES(NULL, @name, @path)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", txt_serviceName.Text);
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved to Database!");
                }
                conn.Close();
                loadServiceList();
                pnl_Save.Visible = false;
            }
            else
            {
                MessageBox.Show("Cannot save with an empty name!");
            }
            conn.Close();
        }

        private void loadServiceList()
        {
            btn_add.Enabled = true;
            btn_delete.Enabled = false;
            btn_edit.Enabled = false;

            pb_preview.Image = null;
            lst_servicePics.Items.Clear(); //clear image name list on reloads
            svcIds.Clear(); //clear image ID list on reloads

            conn.Open();

            using (var cmd = new MySqlCommand("SELECT * from services", conn))
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lst_servicePics.Items.Add(reader["service_name"].ToString());
                        svcIds.Add((int)reader["service_id"]);
                    }
                }
            }
            conn.Close();
        }

        private void lst_servicePics_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lst_servicePics.SelectedIndex;
            if (index > -1) //if there is something currently selected
            {
                
                //RETRIEVE using Name and ID
                conn.Open();
                //using (var cmd = new MySqlCommand("SELECT * from services WHERE service_name = @name AND service_id = @id", conn))
                using (var cmd = new MySqlCommand("SELECT * from services WHERE service_id = @id", conn))
                {
                    string name = lst_servicePics.SelectedItem.ToString();
                    //cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", svcIds[index]);

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        /*byte[] deserializedImage = (byte[])reader["image_byte"];
                        pb_preview.Image = Config.GetDataToImage(deserializedImage);*/
                        txt_serviceName.Text = reader["service_name"].ToString();
                        selectedServiceImagePath = reader["image_path"].ToString();
                        pb_preview.Image = Config.GetDataToImage(Config.GetPic(selectedServiceImagePath).image);
                    }
                }

                pnl_Save.Visible = true;

                btn_delete.Enabled = true; //enable delete on "View" mode
                btn_edit.Enabled = true;

                txt_serviceName.Enabled = false;
                pb_preview.Enabled = false;
                btn_save.Visible = false; //hide save button to just preview Image of item selected from the list

                conn.Close();
            }
            else
                return;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string name = lst_servicePics.SelectedItem.ToString();
            int id = svcIds[lst_servicePics.SelectedIndex];

            if (MessageBox.Show("Delete the selected item?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //delete from files
                Config.DeletePic(selectedServiceImagePath);

                //delete from dbase
                conn.Open();
                using (var cmd = new MySqlCommand("DELETE from services WHERE service_name = @name AND service_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                loadServiceList();
                pnl_Save.Visible = false;
            }
            else
                return;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Edit the selected item?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                disableOtherButtons(sender);

                txt_serviceName.Enabled = true;

                pb_preview.Enabled = true;

                btn_save.Visible = true;

            }
            else
                return;
     
        }

        private void btnOfficers_Click_1(object sender, EventArgs e)
        {
            Config.CallOfficers(this);
        }

        private void btnOffices_Click_1(object sender, EventArgs e)
        {
            Config.CallOffices(this);
        }

        private void btnMaps_Click_1(object sender, EventArgs e)
        {
            Config.CallMap1(this);
        }

        private void btnJobs_Click_1(object sender, EventArgs e)
        {
            Config.CallJobs(this);
        }

        private void btnServices_Click_1(object sender, EventArgs e)
        {
            Config.CallServices(this);
        }
    }
}
