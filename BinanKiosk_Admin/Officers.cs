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

        String comboPosition, comboDept;
        String insertOfficial, insertPicture, selectOfficial, selectPicture, selectDepartment, selectPosition, retrieveInfo, deleteInfo;
        String selectedValue, officialsID, firstName, middleInitial, lastName, suffix, position, department, description, imageString;

        bool add = false, available = false;

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

            selectDepartment = "SELECT department_name FROM departments";
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

                this.retrieveInfo = "SELECT officials.officials_id, officials.first_name, officials.last_name, officials.middle_initial, officials.suffex, departments.department_name, positions.position_name FROM officials,departments,positions WHERE positions.position_id = officials.position_id AND departments.department_id = officials.department_id AND CONCAT (officials.first_name, ' ', officials.middle_initial, ' ', officials.last_name, ' ', officials.suffex) LIKE @selectedValue ";

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
                    comboBoxDepartment.Text = reader["department_name"].ToString();
                    comboBoxPosition.Text = reader["position_name"].ToString();
                }

                reader.Close();

                using (var cmd = new MySqlCommand("SELECT picture_string from officials_pictures WHERE officials_id = @ID ", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        byte[] deserializedImage = (byte[])reader["picture_string"];
                        officerPicture.Image = GetDataToImage(deserializedImage);
                    }
                    else
                    {
                        officerPicture.Image = null;
                    }
                }
                conn.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            officersList.ClearSelected();
            clear();

            add = true;
            txtID.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            officerInformation.Enabled = true;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtID.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                conn.Open();

                deleteInfo = "DELETE FROM officials_pictures WHERE officials_pictures.officials_id = @ID ";
                cmd = new MySqlCommand(deleteInfo, conn);
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.ExecuteNonQuery();

                deleteInfo = "DELETE FROM officials WHERE officials_id = @ID";

                cmd = new MySqlCommand(deleteInfo, conn);
                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.ExecuteNonQuery();

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
            if (txtID.Text == "" || txtFirstName.Text == "" || txtMI.Text == "" || txtLastName.Text == "" || comboBoxDepartment.Text == "" || comboBoxPosition.Text == "")
            {
                MessageBox.Show("Please enter all credentials!", "Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (add == true)
                {
                    initialize();
                    conn.Open();

                    comboPosition = comboBoxPosition.Text;
                    comboDept = comboBoxDepartment.Text;

                    this.insertOfficial = "SELECT departments.department_id, positions.position_id FROM departments,positions WHERE departments.department_name = @departments AND positions.position_name = @positions ";

                    cmd = new MySqlCommand(insertOfficial, conn);

                    cmd.Parameters.AddWithValue("@departments", comboDept);
                    cmd.Parameters.AddWithValue("@positions", comboPosition);

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        department = reader["department_id"].ToString();
                        position = reader["position_id"].ToString();
                    }
                    reader.Close();

                    this.insertOfficial = "insert into officials (officials_id, first_name, last_name, middle_initial, suffex, position_id, department_id) values (@officials_id, @first_name, @last_name, @middle_initial, @suffex, @position_id, @department_id)";

                    cmd = new MySqlCommand(insertOfficial, conn);
                    cmd.Parameters.AddWithValue("@officials_id", officialsID);
                    cmd.Parameters.AddWithValue("@first_name", firstName);
                    cmd.Parameters.AddWithValue("@last_name", lastName);
                    cmd.Parameters.AddWithValue("@middle_initial", middleInitial);
                    cmd.Parameters.AddWithValue("@suffex", suffix);
                    cmd.Parameters.AddWithValue("@position_id", position);
                    cmd.Parameters.AddWithValue("@department_id", department);

                    cmd.ExecuteNonQuery();

                    //Picture Saving
                    var serializedImage = ImageToByteArray(officerPicture.Image, officerPicture);

                    this.insertPicture = "INSERT INTO officials_pictures(picture_id, officials_id, picture_string) VALUES(@picture_id, @department_id, @image)";

                    cmd = new MySqlCommand(insertPicture, conn);
                    cmd.Parameters.AddWithValue("@picture_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@department_id", txtID.Text);
                    cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Inserted!");

                    officerInformation.Enabled = false;
                    officersList.Items.Clear();
                    officers();
                    clear();
                }
                else
                {
                    initialize();
                    conn.Open();

                    comboPosition = comboBoxPosition.Text;
                    comboDept = comboBoxDepartment.Text;

                    this.insertOfficial = "SELECT departments.department_id, positions.position_id FROM departments,positions WHERE departments.department_name = @department AND positions.position_name = @position ";

                    cmd = new MySqlCommand(insertOfficial, conn);

                    cmd.Parameters.AddWithValue("@department", comboDept);
                    cmd.Parameters.AddWithValue("@position", comboPosition);

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        department = reader["department_id"].ToString();
                        position = reader["position_id"].ToString();
                    }
                    reader.Close();

                    cmd = new MySqlCommand("UPDATE officials SET first_name = @firstName, last_name = @lastName, middle_initial = @MI, suffex = @suffix, position_id = @position, department_id = @department WHERE officials_id = @ID ", conn);

                    cmd.Parameters.AddWithValue("@ID", txtID.Text);
                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@MI", txtMI.Text);
                    cmd.Parameters.AddWithValue("@suffix", txtSuffix.Text);
                    cmd.Parameters.AddWithValue("@position", position);
                    cmd.Parameters.AddWithValue("@department", department);

                    cmd.ExecuteNonQuery();

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

                    conn.Close();

                    MessageBox.Show("Updated!");

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
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(openFile.FileName);
                officerPicture.Image = img;
                imageString = openFile.SafeFileName;
            }
        }

        #region
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

    }
}