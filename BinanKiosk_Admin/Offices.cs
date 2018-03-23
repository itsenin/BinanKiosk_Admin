﻿using System;
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
    public partial class Offices : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        MySqlCommand cmd;

        String selectedValue, selectedID;
        String imageString;

        bool add = false, availalbe = false, officeAvailable = false;

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

            using (var cmd = new MySqlCommand("SELECT Department_image_path from departments WHERE department_id = '" + Convert.ToInt32(selectedID) + "' ", conn))
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
            }
            
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
            add = true;
            txtID.Enabled = true;
            officeInformation.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtID.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                conn.Open();

                //Delete the entry in Departments
                cmd = new MySqlCommand("DELETE FROM departments WHERE departments.department_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

                //Delete picture in IIS
                cmd = new MySqlCommand("DELETE FROM departments_pictures WHERE departments_pictures.department_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

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
                //serialize the Image (IIS can't encode/decode Image objects to JSON)
                var serializedImage = Config.ImageToByteArray(officeLogo.Image);

                //Create Picture Model to send to API
                Picture pic = new Picture { Name = txtDeptName.Text + ".jpg", FolderName = "Departments", image = serializedImage };

                //send the picture to the API(returns path)
                string path = Config.SavePic(pic);

                //save Department with picture path to database
                cmd = new MySqlCommand("insert into departments (department_id, department_name, Dep_description, Department_image_path) values('" + Convert.ToInt32(txtID.Text) + "', '" + txtDeptName.Text + "','" + txtDescription.Text + "',@path )", conn);
                cmd.Parameters.AddWithValue("@path", path);
                cmd.ExecuteNonQuery();

                ////Picture Saving
                //var serializedImage = ImageToByteArray(officeLogo.Image, officeLogo);
                //cmd = new MySqlCommand("INSERT INTO departments_pictures(picture_id, department_id, picture_string) VALUES('" + Convert.ToInt32(txtID.Text) + "', '" + Convert.ToInt32(txtID.Text) + "', @image)", conn);
                //cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                //cmd.ExecuteNonQuery();
            }
            else //save after edit
            {
                //update Department info
                cmd = new MySqlCommand("UPDATE departments SET department_name = '" + txtDeptName.Text + "', Dep_description = '" + txtDescription.Text + "' WHERE department_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

                using (var cmd = new MySqlCommand("SELECT Department_image_path from departments WHERE department_id = '" + Convert.ToInt32(txtID.Text) + "' ", conn))
                {
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        availalbe = true;
                    }
                    else
                    {
                        availalbe = false;
                    }
                }

                //serialize the Image (IIS can't encode/decode Image objects to JSON)
                var serializedImage = Config.ImageToByteArray(officeLogo.Image);

                //Create Picture Model to send to API
                Picture pic = new Picture { Name = txtDeptName.Text, FolderName = "Departments", image = serializedImage };

                //send the picture to the API(returns path)
                string path = Config.SavePic(pic);

                //save all with picture path to database
                cmd = new MySqlCommand("insert into departments (department_id, department_name, Dep_description, Department_image_path) values('" + Convert.ToInt32(txtID.Text) + "', '" + txtDeptName.Text + "','" + txtDescription.Text + "',@path )", conn);
                cmd.Parameters.AddWithValue("@path", path);
                cmd.ExecuteNonQuery();

                //if (availalbe == true)
                //{
                //    //Picture Saving
                //    var serializedImage = ImageToByteArray(officeLogo.Image, officeLogo);
                //    cmd = new MySqlCommand("UPDATE departments_pictures SET picture_string = @image WHERE department_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                //    cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                //    cmd.ExecuteNonQuery();
                //}
                //else
                //{
                //    //Picture Saving
                //    var serializedImage = ImageToByteArray(officeLogo.Image, officeLogo);
                //    cmd = new MySqlCommand("INSERT INTO departments_pictures(picture_id, department_id, picture_string) VALUES('" + Convert.ToInt32(txtID.Text) + "', '" + Convert.ToInt32(txtID.Text) + "', @image)", conn);
                //    cmd.Parameters.Add("@image", MySqlDbType.MediumBlob).Value = serializedImage;
                //    cmd.ExecuteNonQuery();
                //}
            }

            conn.Close();

            MessageBox.Show("Updated!");

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
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(openFile.FileName);
                officeLogo.Image = img;
                imageString = openFile.SafeFileName;
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
