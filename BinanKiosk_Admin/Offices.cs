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
    public partial class Offices : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        MySqlCommand cmd;

        String selectedValue;
        String position;
        String department;
        String imageString;

        bool add = false;

        public Offices()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            offices();
            //departments();
            positions();

            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        public void offices()
        {

            conn.Open();

            cmd = new MySqlCommand("SELECT department_name FROM departments", conn);
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    officeList.Items.Add(reader.GetString(0));
                }
            }

            reader.Close();
            conn.Close();

        }
        
        public void positions()
        {
            /*conn.Open();
            cmd = new MySqlCommand("SELECT position_name FROM positions", conn);
            cmd.ExecuteNonQuery();
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
            conn.Close();*/
        }

        public void clear()
        {
            txtID.Text = "";
            txtDeptName.Text = "";
            txtRoomID.Text = "";
            txtDescription.Text = "";
            officerPicture.Image = null;
        }

        private void officesList_SelectedValueChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            selectedValue = officeList.GetItemText(officeList.SelectedItem);

            conn.Open();
            cmd = new MySqlCommand("SELECT departments.department_id, departments.department_name, departments.room_id, departments.Dep_description FROM departments WHERE departments.department_name  = '" + selectedValue + "' ", conn);
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                txtID.Text = reader["department_id"].ToString();
                txtDeptName.Text = reader["department_name"].ToString();
                txtRoomID.Text = reader["room_id"].ToString();
                txtDescription.Text = reader["Dep_description"].ToString();
            }

            reader.Close();

            /*using (var cmd = new MySqlCommand("SELECT picture_string from pictures WHERE officials_id = '" + Convert.ToInt32(txtID.Text) + "' ", conn))
            {
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
            }*/


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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clear();
            add = true;
            txtID.Enabled = true;
            officeInformation.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            /*DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtID.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                conn.Open();

                cmd = new MySqlCommand("DELETE FROM pictures WHERE pictures.officials_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("DELETE FROM officials WHERE officials.officials_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Deleted!");

                officeList.Items.Clear();
                officers();
                clear();
            }
            else
            {
                MessageBox.Show("Not Deleted!");

                officeList.Items.Clear();
                officers();
                clear();
            }*/


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            txtID.Enabled = false;
            officeInformation.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();

            cmd = new MySqlCommand("UPDATE departments SET department_name = '" + txtDeptName.Text + "', room_ID = '" + txtRoomID.Text + "', Dep_description = '" + txtDescription.Text + "' WHERE department_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Updated!");

            officeInformation.Enabled = false;
            officeList.Items.Clear();
            offices();
            clear();

            /*if (txtID.Text == "" || txtFirstName.Text == "" || txtMI.Text == "" || comboBoxDepartment.Text == "" || comboBoxPosition.Text == "")
            {
                MessageBox.Show("Please enter all credentials!", "Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (add == true)
                {
                    conn.Open();

                    cmd = new MySqlCommand("SELECT departments.department_id, positions.position_id FROM departments,positions WHERE departments.department_name = '" + comboBoxDepartment.Text + "' AND positions.position_name = '" + comboBoxPosition.Text + "' ", conn);
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        department = reader["department_id"].ToString();
                        position = reader["position_id"].ToString();
                    }

                    reader.Close();

                    cmd = new MySqlCommand("insert into officials (officials_id, first_name, last_name, middle_initial, suffex, position_id, department_id) values('" + Convert.ToInt32(txtID.Text) + "', '" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtMI.Text + "','" + txtSuffix.Text + "','" + Convert.ToInt32(position) + "','" + Convert.ToInt32(department) + "')", conn);
                    cmd.ExecuteNonQuery();

                    //Picture Saving
                    var serializedImage = ImageToByteArray(officerPicture.Image, officerPicture);
                    cmd = new MySqlCommand("INSERT INTO pictures(picture_id, officials_id, picture_string) VALUES('" + Convert.ToInt32(txtID.Text) + "', '" + Convert.ToInt32(txtID.Text) + "', @image)", conn);
                    cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Inserted!");

                    officeInformation.Enabled = false;
                    officeList.Items.Clear();
                    officers();
                    clear();
                }
                else
                {
                    conn.Open();

                    cmd = new MySqlCommand("SELECT departments.department_id, positions.position_id FROM departments,positions WHERE departments.department_name = '" + comboBoxDepartment.Text + "' AND positions.position_name = '" + comboBoxPosition.Text + "' ", conn);
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        department = reader["department_id"].ToString();
                        position = reader["position_id"].ToString();
                    }

                    reader.Close();

                    cmd = new MySqlCommand("UPDATE officials SET first_name = '" + txtFirstName.Text + "', last_name = '" + txtLastName.Text + "', middle_initial = '" + txtMI.Text + "', suffex = '" + txtSuffix.Text + "', position_id = '" + Convert.ToInt32(position) + "', department_id = '" + Convert.ToInt32(department) + "' WHERE officials_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Updated!");

                    officeInformation.Enabled = false;
                    officeList.Items.Clear();
                    officers();
                    clear();
                }
            }*/
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

        private void btnHome_Click(object sender, EventArgs e)
        {
            Config.CallHome(this);
        }

        private void btnOffices_Click(object sender, EventArgs e)
        {
            Config.CallOffices(this);
        }

        private void btnOfficers_Click(object sender, EventArgs e)
        {
            Config.CallOfficers(this);
        }
    }
}
