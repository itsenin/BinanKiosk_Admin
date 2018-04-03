using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinanKiosk_Admin.Models;
using MySql.Data.MySqlClient;

namespace BinanKiosk_Admin
{
    public partial class Departments : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        MySqlCommand cmd;

        String selectedValue, selectedID;
        String imageString, imageFileName, imageFileNameNew;
        String deptID, deptName, deptDesc;

        OpenFileDialog openFile;

        bool add = false, availalbe = false, officeAvailable = false;

        public Departments()
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

        private void initialize()
        {
            this.deptID = txtID.Text;
            this.deptName = txtDeptName.Text;
            this.deptDesc = txtDescription.Text;
        }

        public void offices()
        {

            conn.Open();

            cmd = new MySqlCommand("SELECT department_name FROM departments WHERE department_name NOT LIKE 'others%' ", conn);
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

        private bool isImageExisting(String img)
        {
            conn.Open();

            cmd = new MySqlCommand("SELECT Department_image_path FROM officers WHERE Department_image_path = @img", conn);
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

        public void clear()
        {
            txtID.Text = "";
            txtDeptName.Text = "";
            txtDescription.Text = "";
            officeLogo.Image = null;
        }

        private void officesList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (officeList.SelectedIndex > -1)
            {
            

            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
            btnAdd.Enabled = true;

            selectedValue = officeList.GetItemText(officeList.SelectedItem);

            conn.Open();

            cmd = new MySqlCommand("SELECT departments.department_id FROM departments, offices WHERE departments.department_name  = '" + selectedValue + "' ", conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                selectedID = reader["department_id"].ToString();
            }

            reader.Close();

            cmd = new MySqlCommand("SELECT offices.department_id FROM offices WHERE offices.department_id  = '" + Convert.ToInt32(selectedID) + "' ", conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                officeAvailable = true;
            }
            else
            {
                officeAvailable = false;
            }

            reader.Close();

            if (officeAvailable == true)
            {
                cmd = new MySqlCommand("SELECT departments.department_name, offices.room_name, departments.Dep_description FROM departments, offices WHERE departments.department_id  = '" + Convert.ToInt32(selectedID) + "' AND offices.department_id  = '" + Convert.ToInt32(selectedID) + "' ", conn);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    txtID.Text = selectedID;
                    txtDeptName.Text = reader["department_name"].ToString();
                    txtDescription.Text = reader["Dep_description"].ToString();
                }

                reader.Close();
            }
            else
            {
                cmd = new MySqlCommand("SELECT departments.department_name, offices.room_name, departments.Dep_description FROM departments, offices WHERE departments.department_id  = '" + Convert.ToInt32(selectedID) + "' ", conn);
                reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    txtID.Text = selectedID;
                    txtDeptName.Text = reader["department_name"].ToString();
                    txtDescription.Text = reader["Dep_description"].ToString();
                }

                reader.Close();
            }

            string imgquery = "SELECT Department_image_path from departments WHERE departments.department_id = @id";
            
            cmd = new MySqlCommand(imgquery, conn);
            cmd.Parameters.AddWithValue("@id", txtID.Text);
            
            reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                reader.Read();
                imageFileName = reader["Department_image_path"].ToString();
                imageFileNameNew = reader["Department_image_path"].ToString();
                
                if (imageFileName.Equals(""))
                {
                    officeLogo.Image = null;
                }
                else
                {
                    officeLogo.Image = Config.GetImage(imageFileName, Subfolders.Departments);
                }
            }

            /*using (var cmd = new MySqlCommand("SELECT Department_image_path from departments WHERE department_id = '" + Convert.ToInt32(selectedID) + "' ", conn))
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    byte[] deserializedImage = Config.GetPic(reader["Department_image_path"].ToString()).image;
                    officeLogo.Image = Config.GetDataToImage(deserializedImage);
                }
                else
                {
                    officeLogo.Image = null;
                }
            }*/

            conn.Close();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            officeList.ClearSelected();
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            clear();

            imageFileName = "";

            add = true;
            txtID.Enabled = true;
            officeInformation.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtID.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool checkImageExist = isImageExisting(imageFileName);

                conn.Open();

                initialize();

                //Delete the entry in Departments
                cmd = new MySqlCommand("DELETE FROM departments WHERE departments.department_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", deptID);
                cmd.ExecuteNonQuery();

                if(checkImageExist == false)
                {
                    Config.DeleteImage(Subfolders.Departments, imageFileName);
                }
                
                conn.Close();

                MessageBox.Show("Deleted!");

                officeList.Items.Clear();
                offices();
                clear();
            }
            else
            {
                MessageBox.Show("Not Deleted!");

                officeList.Items.Clear();
                offices();
                clear();
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnAdd.Enabled = false;
            add = false;
            txtID.Enabled = false;
            officeInformation.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            
            if (add == true)
            {
                if (txtID.Text != "" && txtDeptName.Text != "" && txtDescription.Text != "" && imageFileName != "")
                {
                    initialize();

                    //save Department with picture path to database
                    cmd = new MySqlCommand("insert into departments (department_id, department_name, Dep_description, Department_image_path) values(@id, @deptName, @deptDesc, @path)", conn);
                    cmd.Parameters.AddWithValue("@path", imageFileName);
                    cmd.Parameters.AddWithValue("@id", deptID);
                    cmd.Parameters.AddWithValue("@deptName", deptName);
                    cmd.Parameters.AddWithValue("@deptDesc", deptDesc);

                    try
                    {
                        cmd.ExecuteNonQuery();

                        using (conn)
                        {
                            Config.SaveImage(openFile, Subfolders.Departments);
                        }

                        MessageBox.Show("Inserted!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        //MessageBox.Show("Insert Unsuccessful!");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill up all informations!", "Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else //save after edit
            {
                initialize();

                if (txtID.Text != "" && txtDeptName.Text != "" && txtDescription.Text != "" && imageFileName != "")
                {
                    //update Department info
                    cmd = new MySqlCommand("UPDATE departments SET department_name = @deptName, Dep_description = @deptDesc, Department_image_path = @path WHERE department_id = @id", conn);
                    cmd.Parameters.AddWithValue("@path", imageFileName);
                    cmd.Parameters.AddWithValue("@id", deptID);
                    cmd.Parameters.AddWithValue("@deptName", deptName);
                    cmd.Parameters.AddWithValue("@deptDesc", deptDesc);

                    MessageBox.Show(imageFileName + " " + imageFileNameNew);

                    try
                    {
                        cmd.ExecuteNonQuery();

                        if (imageFileName != imageFileNameNew)
                        {
                            using (conn)
                            {
                                Config.SaveImage(openFile, Subfolders.Departments);
                            }
                        }

                        MessageBox.Show("Updated!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update Unsuccessful!");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill up all informations!", "Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            conn.Close();
            
            officeInformation.Enabled = false;
            officeList.Items.Clear();
            offices();
            clear();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            officeInformation.Enabled = false;
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
                        officeLogo.Image = img;
                        imageFileName = openFile.SafeFileName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
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
            Config.CallDepartments(this);
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
