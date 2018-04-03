using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BinanKiosk_Admin
{
    public partial class Officers : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        MySqlCommand cmd;

        OpenFileDialog openFile;

        String comboPosition, comboDept, imageFileName, imageFileNameNew;
        String insertOfficial, selectOfficial, selectDepartment, selectPosition, retrieveInfo, deleteInfo;
        String selectedValue, officialsID, firstName, middleInitial, lastName, suffix, position, department, description, imageString;

        bool add = false;

        public Officers()
        {
            InitializeComponent();
        }

        private void initialize()
        {
            this.officialsID = txtID.Text.ToString();
            this.firstName = txtFirstName.Text.ToString();
            this.lastName = txtLastName.Text.ToString();
            this.middleInitial = txtMI.Text.ToString();
            this.suffix = txtSuffix.Text.ToString();
            this.position = comboBoxPosition.Text.ToString();
            this.department = comboBoxDepartment.Text.ToString();
            this.description = txtDescription.Text.ToString();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            officers();
            departments();
            positions();
        }

        public void officers()
        {
            conn.Open();

            selectOfficial = "SELECT CONCAT (officials.first_name, ' ', officials.middle_initial, ' ', officials.last_name, ' ', officials.suffex) AS name FROM officials";
            cmd = new MySqlCommand(selectOfficial, conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    officersList.Items.Add(reader.GetString(0));
                }
            }

            reader.Close();
            conn.Close();
        }

        public void departments()
        {
            conn.Open();

            selectDepartment = "SELECT office_name FROM offices";
            cmd = new MySqlCommand(selectDepartment, conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBoxDepartment.AutoCompleteCustomSource.Add(reader.GetString(0));
                    comboBoxDepartment.Items.Add(reader.GetString(0));
                }
            }

            reader.Close();
            conn.Close();
        }

        public void positions()
        {
            conn.Open();

            selectPosition = "SELECT position_name FROM positions";
            cmd = new MySqlCommand(selectPosition, conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBoxPosition.AutoCompleteCustomSource.Add(reader.GetString(0));
                    comboBoxPosition.Items.Add(reader.GetString(0));
                }
            }

            reader.Close();
            conn.Close();
        }

        public void clear()
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtSuffix.Text = "";
            txtMI.Text = "";
            comboBoxDepartment.Text = "";
            comboBoxPosition.Text = "";
            officerPicture.Image = null;
        }

        private bool isImageExisting(String img)
        {
            conn.Open();

            cmd = new MySqlCommand("SELECT image_path FROM officials WHERE image_path = @img", conn);
            cmd.Parameters.AddWithValue("@img", img);

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
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

        private void officersList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (officersList.SelectedIndex > -1)
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnAdd.Enabled = true;

                officerInformation.Enabled = false;
                selectedValue = officersList.GetItemText(officersList.SelectedItem);

                conn.Open();

                this.retrieveInfo = "SELECT officials.officials_id, officials.first_name, officials.last_name, officials.middle_initial, officials.suffex, offices.office_name, positions.position_name FROM officials,offices,positions WHERE positions.position_id = officials.position_id AND offices.office_id = officials.office_id AND CONCAT (officials.first_name, ' ', officials.middle_initial, ' ', officials.last_name, ' ', officials.suffex) LIKE @selectedValue";

                cmd = new MySqlCommand(retrieveInfo, conn);
                cmd.Parameters.AddWithValue("@selectedValue", selectedValue);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    txtID.Text = reader["officials_id"].ToString();
                    txtFirstName.Text = reader["first_name"].ToString();
                    txtLastName.Text = reader["last_name"].ToString();
                    txtMI.Text = reader["middle_initial"].ToString();
                    txtSuffix.Text = reader["suffex"].ToString();
                    comboBoxDepartment.Text = reader["office_name"].ToString();
                    comboBoxPosition.Text = reader["position_name"].ToString();
                }

                reader.Close();

                string imgquery = "SELECT image_path from officials WHERE officials.officials_id = @id";

                cmd = new MySqlCommand(imgquery, conn);
                cmd.Parameters.AddWithValue("@id", txtID.Text);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    imageFileName = reader["image_path"].ToString();
                    imageFileNameNew = reader["image_path"].ToString();

                    if (imageFileName.Equals(""))
                    {
                        officerPicture.Image = null;
                    }
                    else
                    {
                        officerPicture.Image = Config.GetImage(imageFileName, Subfolders.Officials);
                    }
                }
                conn.Close();
                reader.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            officersList.ClearSelected();
            clear();

            imageFileName = "";

            add = true;
            txtID.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            officerInformation.Enabled = true;

        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtFirstName.Text + " " + txtLastName.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                conn.Open();

                deleteInfo = "DELETE FROM officials WHERE officials_id = @ID";

                cmd = new MySqlCommand(deleteInfo, conn);
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.ExecuteNonQuery();

                Config.DeleteImage(Subfolders.Officials, imageFileName);

                conn.Close();

                MessageBox.Show("Deleted!");

                officersList.Items.Clear();
                officers();
                clear();
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                MessageBox.Show("Not Deleted!");

                officersList.Items.Clear();
                officers();
                clear();
                btnAdd.Enabled = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;
            add = false;
            txtID.Enabled = false;
            officerInformation.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "" || txtFirstName.Text == "" || txtMI.Text == "" || txtLastName.Text == "" || comboBoxDepartment.Text == "" || comboBoxPosition.Text == "" || imageFileName == "")
            {
                MessageBox.Show("Please fill up all informations!", "Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                initialize();

                comboPosition = comboBoxPosition.Text;
                comboDept = comboBoxDepartment.Text;

                conn.Open();
                
                this.insertOfficial = "SELECT offices.office_id, positions.position_id FROM offices,positions WHERE offices.office_name = @departments AND positions.position_name = @positions ";

                cmd = new MySqlCommand(insertOfficial, conn);

                cmd.Parameters.AddWithValue("@departments", comboDept);
                cmd.Parameters.AddWithValue("@positions", comboPosition);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    department = reader["office_id"].ToString();
                    position = reader["position_id"].ToString();
                    
                }
                reader.Close();
                conn.Close();
                
                //Insert
                if (add == true)
                {
                    bool checkImageExist = isImageExisting(imageFileName);

                    if (checkImageExist == true)
                    {
                        MessageBox.Show("Image Exist! Select another Image!");
                    }
                    else
                    {
                        conn.Open();

                        this.insertOfficial = "insert into officials (officials_id, first_name, last_name, middle_initial, suffex, position_id, office_id, image_path) values (@officials_id, @first_name, @last_name, @middle_initial, @suffex, @position_id, @department_id, @img)";

                        cmd = new MySqlCommand(insertOfficial, conn);
                        cmd.Parameters.AddWithValue("@officials_id", officialsID);
                        cmd.Parameters.AddWithValue("@first_name", firstName);
                        cmd.Parameters.AddWithValue("@last_name", lastName);
                        cmd.Parameters.AddWithValue("@middle_initial", middleInitial);
                        cmd.Parameters.AddWithValue("@suffex", suffix);
                        cmd.Parameters.AddWithValue("@position_id", position);
                        cmd.Parameters.AddWithValue("@department_id", department);
                        cmd.Parameters.AddWithValue("@img", imageFileName);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            using (conn)
                            {
                                Config.SaveImage(openFile, Subfolders.Officials);
                            }

                            MessageBox.Show("Inserted!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            //MessageBox.Show("Insert Unsuccessful!");
                        }


                        /*
                        //Picture Saving
                        var serializedImage = ImageToByteArray(officerPicture.Image, officerPicture);

                        this.insertPicture = "INSERT INTO officials_pictures(picture_id, officials_id, picture_string) VALUES(@picture_id, @department_id, @image)";

                        cmd = new MySqlCommand(insertPicture, conn);
                        cmd.Parameters.AddWithValue("@picture_id", txtID.Text);
                        cmd.Parameters.AddWithValue("@department_id", txtID.Text);
                        cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                        cmd.ExecuteNonQuery();
                        */

                        conn.Close();

                        officerInformation.Enabled = false;
                        officersList.Items.Clear();
                        officers();
                        clear();
                    }
                }
                
                //Update
                else
                {
                    conn.Open();

                    cmd = new MySqlCommand("UPDATE officials SET first_name = @firstName, last_name = @lastName," +
                        " middle_initial = @MI, suffex = @suffix, position_id = @position, office_id = @department," +
                        " image_path = @img WHERE officials_id = @ID", conn);

                    //cmd = new MySqlCommand("UPDATE officials SET first_name = @firstName, last_name = @lastName, middle_initial = @MI, suffex = @suffix, image_path = @img WHERE officials_id = @ID ", conn);
                    
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@MI", txtMI.Text);
                    cmd.Parameters.AddWithValue("@suffix", txtSuffix.Text);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@img", imageFileName);

                    try
                    {
                        cmd.ExecuteNonQuery();

                        if (imageFileName != imageFileNameNew) {
                            using (conn)
                            {
                                Config.SaveImage(openFile, Subfolders.Officials);
                                Config.DeleteImage(Subfolders.Officials, imageFileNameNew);
                            }
                        }

                        MessageBox.Show("Updated!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update Unsuccessful!");
                    }


                    /*
                    using (var cmd = new MySqlCommand("SELECT picture_string from officials_pictures WHERE officials_id = @ID ", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);

                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            available = true;
                        }
                        else
                        {
                            available = false;
                        }
                    }

                    if (available == true)
                    {
                        //Picture Saving
                        var serializedImage = ImageToByteArray(officerPicture.Image, officerPicture);
                        cmd = new MySqlCommand("UPDATE officials_pictures SET picture_string = @image WHERE officials_id = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //Picture Saving
                        var serializedImage = ImageToByteArray(officerPicture.Image, officerPicture);

                        this.insertPicture = "INSERT INTO officials_pictures(picture_id, officials_id, picture_string) VALUES(@picture_id, @department_id, @image)";

                        cmd = new MySqlCommand(insertPicture, conn);
                        cmd.Parameters.AddWithValue("@picture_id", txtID.Text);
                        cmd.Parameters.AddWithValue("@department_id", txtID.Text);
                        cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                        cmd.ExecuteNonQuery();
                    }
                    */

                    conn.Close();
                    
                    officerInformation.Enabled = false;
                    officersList.Items.Clear();
                    officers();
                    clear();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            officersList.ClearSelected();
            clear();
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            officerInformation.Enabled = false;
        }

        private void officerPicture_Click(object sender, EventArgs e)
        {
            openFile = new OpenFileDialog();
            openFile.Title = "Choose Logo Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image img = new Bitmap(openFile.FileName);
                    {
                        officerPicture.Image = img;
                        imageFileName = openFile.SafeFileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }

            /*OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(openFile.FileName);
                officerPicture.Image = img;
                imageString = openFile.SafeFileName;
            }*/
        }

        #region default buttons
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
            if(MessageBox.Show("Logout?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Config.CallLogin(this);
        }
        #endregion

    }
}