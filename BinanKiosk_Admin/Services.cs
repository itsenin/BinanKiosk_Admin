using BinanKiosk_Admin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanKiosk_Admin
{
    public partial class Services : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        List<int> svcIds;
        bool editMode = false;
        string selectedImageName;
        OpenFileDialog open;

        public Services()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }
        private void Services_Load(object sender, EventArgs e)
        {
            timestamp.Enabled = true;
            timestamp.Interval = 1;
            timestamp.Start();

            svcIds = new List<int>();
            loadServiceList();
            populate_officeComboBox();
            cmb_office.DropDownWidth = DropDownWidth(cmb_office);
            cmb_office.SelectedIndex = 0;
        }

        private void timestamp_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
        }

        private void populate_officeComboBox()
        {
            conn.Open();
            using (var cmd = new MySqlCommand("Select * from offices", conn))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            cmb_office.Items.Add(reader["office_name"].ToString());
                        }
                    }
                }
            }
            conn.Close();
        }

        int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth + 3;
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
            cmb_office.Enabled = true;
            cmb_office.SelectedIndex = 0;

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
                    open = openFile;
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

            try
            {
                if (string.IsNullOrWhiteSpace(txt_serviceName.Text))
                    throw new ArgumentException("Service Name can't be empty!");

                conn.Open();

                if (editMode)//EDIT MODE  //2 branches, when image is changed, when not
                {
                    int selectedID = svcIds[lst_servicePics.SelectedIndex];
                    string oldImageName = "";

                    if (open == null)
                    {
                        //BRANCH 1: RETAINED IMAGE
                        using (var cmd = new MySqlCommand("UPDATE services SET service_name=@name, office_id=@officeID image WHERE service_id=@id", conn))
                        {
                            cmd.Parameters.AddWithValue("@name", txt_serviceName.Text);
                            cmd.Parameters.AddWithValue("@officeID", cmb_office.SelectedIndex + 1);//saving to database needs +1(dbase offices_id starts at 1)
                            cmd.Parameters.AddWithValue("@id", selectedID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Changes Successfully Saved to Database!");
                        }
                    }
                    else
                    {
                        //BRANCH 2: CHANGED IMAGE

                        //check if new image name exists in database(don't allow to use same image in different entries)
                        using (var cmd = new MySqlCommand("SELECT * FROM services WHERE image_name = @imgName", conn))
                        {
                            cmd.Parameters.AddWithValue("@imgName", open.SafeFileName);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    throw new ArgumentException("An entry with the same Image already exists! Please select a different image");
                                }
                            }
                        }

                        //Get and old image name
                        using (var cmd = new MySqlCommand("Select image_name from services where service_id = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selectedID);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    oldImageName = reader["image_name"].ToString();
                                }
                            }
                        }

                        //delete old image file in network
                        Config.DeleteImage(Subfolders.Services, oldImageName);


                        //string temp = txt_serviceName.Text;
                        ////======New Image creation=======//

                        //var serializedImage = Config.ImageToByteArray(pb_preview.Image);
                        ////Create Picture Model to send to API
                        //// string replacement = Regex.Replace(txt_serviceName.Text, @"\t|\n|\r", "");
                        //Picture pic = new Picture { Name = txt_serviceName.Text, FolderName = "Services", image = serializedImage };

                        ////send the picture to the API(returns path)
                        //var path = Config.SavePic2(pic);

                        Config.SaveImage(open, Subfolders.Services); //assuming user opened an image file

                        //Update Entry with new Image or Both(Name & Image)
                        using (var cmd = new MySqlCommand("UPDATE services SET service_name=@name, office_id=@officeID,image_name=@imgName WHERE service_id=@id", conn))
                        {
                            cmd.Parameters.AddWithValue("@name", txt_serviceName.Text);
                            cmd.Parameters.AddWithValue("@officeID", cmb_office.SelectedIndex + 1);//saving to database needs +1(dbase offices_id starts at 1)
                            cmd.Parameters.AddWithValue("@imgName", open.SafeFileName);
                            cmd.Parameters.AddWithValue("@id", selectedID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Changes Successfully Saved to Database!");
                        }
                    }
                    editMode = false;

                }
                else //ADD MODE
                {
                    if (open == null)
                        throw new ArgumentException("Service Image can't be empty!");

                    //Check if service with same name or image exists
                    using (var cmd = new MySqlCommand("SELECT * FROM services WHERE service_name = @name OR image_name = @imgName", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txt_serviceName.Text);
                        cmd.Parameters.AddWithValue("@imgName", open.SafeFileName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                throw new ArgumentException("An entry with the same Service Name or Image already exists!");
                            }
                        }
                    }

                    //var serializedImage = Config.ImageToByteArray(pb_preview.Image);
                    ////Create Picture Model to send to API
                    //Picture pic = new Picture { Name = txt_serviceName.Text, FolderName = "Services", image = serializedImage };

                    ////send the picture to the API(returns path)
                    //var path = Config.SavePic(pic);

                    //Windows Authentication
                    Config.SaveImage(open, Subfolders.Services);//saves currently opened File to a subfolder in the remote directory

                    //INSERT to Database
                    using (var cmd = new MySqlCommand("INSERT INTO services(service_id, service_name, image_name, office_id) VALUES(NULL, @name, @imgName, @officeID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", txt_serviceName.Text);
                        cmd.Parameters.AddWithValue("@imgName", open.SafeFileName);
                        cmd.Parameters.AddWithValue("@officeID", cmb_office.SelectedIndex + 1);//saving to database needs +1(dbase offices_id starts at 1)
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Saved to Database!");
                    }
                }


                loadServiceList();
                pnl_Save.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save entry: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void loadServiceList()
        {
            btn_add.Enabled = true;
            btn_delete.Enabled = false;
            btn_edit.Enabled = false;

            open = null;
            selectedImageName = null;
            pb_preview.Image = null;
            lst_servicePics.Items.Clear(); //clear image name list on reloads
            svcIds.Clear(); //clear image ID list on reloads

            if(conn.State == ConnectionState.Closed)
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
                    cmd.Parameters.AddWithValue("@id", svcIds[index]);

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        /*byte[] deserializedImage = (byte[])reader["image_byte"];
                        pb_preview.Image = Config.GetDataToImage(deserializedImage);*/

                        selectedImageName = reader["image_name"].ToString();
                        txt_serviceName.Text = reader["service_name"].ToString();

                        //pb_preview.Image = Config.GetDataToImage(Config.GetPic(selectedServiceImagePath).image);
                        pb_preview.Image = Config.GetImage(selectedImageName, Subfolders.Services);

                        cmb_office.SelectedIndex = int.Parse(reader["office_id"].ToString()) - 1; //retrieving from dbase to list needs -1(combobox items list start at 0, not 1)
                    }
                }

                pnl_Save.Visible = true;

                btn_delete.Enabled = true; //enable delete on "View" mode
                btn_edit.Enabled = true;

                txt_serviceName.Enabled = false;
                pb_preview.Enabled = false;
                cmb_office.Enabled = false;
                btn_save.Visible = false; //hide save button to just preview Image of item selected from the list

                conn.Close();
            }
            else
                return;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            int id = svcIds[lst_servicePics.SelectedIndex];

            if (MessageBox.Show("Delete the selected item?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //delete from files
                //Config.DeletePic(selectedServiceImagePath);
                Config.DeleteImage(Subfolders.Services, selectedImageName);

                //delete from dbase
                conn.Open();
                using (var cmd = new MySqlCommand("DELETE from services WHERE service_id = @id", conn))
                {
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
                editMode = true;

                disableOtherButtons(sender);

                txt_serviceName.Enabled = true;

                pb_preview.Enabled = true;

                cmb_office.Enabled = true;

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
            Config.CallDepartments(this);
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

        private void btn_registration_Click(object sender, EventArgs e)
        {
            Config.CallSignup(this);
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Config.CallLogin(this);
        }

    }
}
