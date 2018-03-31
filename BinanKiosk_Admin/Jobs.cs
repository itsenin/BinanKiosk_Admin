using BinanKiosk_Admin.APIServices;
using BinanKiosk_Admin.Models;
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
    public partial class Jobs : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlCommand cmdAdd;
        MySqlCommand cmdSave;
        MySqlCommand cmdDelete;
        MySqlCommand cmdJobLookUp;
        MySqlCommand gv_cmd;
        MySqlCommand cmdImgLookUp;
        String dropdownQuery;
        String query;
        String jobtype;
        String jobID;

        //textbox values
        String type_id;
        String types;
        String job_id;
        String desc;
        String location;
        String company;
        String category;

        String imgFileName;
        String logoFileName;
        String prepare_imgFileName;
        String prepare_logoFileName;
        String imgPath;
        String logoPath;

        OpenFileDialog openImg;
        OpenFileDialog openLogo;

        public Jobs()
        {
            InitializeComponent();
            populateDropDown();
            setDefaults();
            populateGridView();
        }

        private void setDefaults ()
        {
            txtJobType.SelectedIndex = 0;
            txtJobCategory.SelectedIndex = 0;
        }
        
        private void populateDropDown ()
        {
            dropdownQuery = "SELECT job_name from jobs;";
            DataTable jobTypes = new DataTable();
            txtJobType.Items.Add("All");
            using (conn)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(dropdownQuery, conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        txtJobType.Items.Add(reader["job_name"].ToString());
                        txtJobCategory.Items.Add(reader["job_name"].ToString());
                    }

                    reader.Close();
                    conn.Close();
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        private string generateQuery ()
        {
            if (string.IsNullOrEmpty(txtSearch.Text) && txtJobType.SelectedIndex == 0)
            {
                return query = "SELECT * FROM jobtypes;";
            }

            else if (!(string.IsNullOrEmpty(txtSearch.Text)) && txtJobType.SelectedIndex == 0)
            {
                jobtype = txtSearch.Text;
                return query = "SELECT * FROM jobtypes WHERE job_types LIKE @tags ;";
            }

            else if (string.IsNullOrEmpty(txtSearch.Text) && txtJobType.SelectedIndex > 0)
            {
                jobID = txtJobType.SelectedIndex.ToString();
                return query = "SELECT * FROM jobtypes WHERE job_id = @jobID";
            }

            else
            {
                jobID = txtJobType.SelectedIndex.ToString();
                jobtype = txtSearch.Text;
                return query = "SELECT * FROM jobtypes WHERE job_id = @jobID AND job_types LIKE @tags ;";
            }
        }

        private void populateGridView ()
        {
            query = generateQuery();
            using (conn)
            {
                conn.Open();
                gv_cmd = new MySqlCommand(query, conn);
                gv_cmd.Parameters.AddWithValue("@jobID", jobID);
                gv_cmd.Parameters.AddWithValue("@tags", "%" + jobtype + "%");
                MySqlDataAdapter gv_adapter = new MySqlDataAdapter(gv_cmd);
                DataTable dt = new DataTable();
                gv_adapter.Fill(dt);
                gridview.DataSource = dt;
            }
        }

        private void updateValues ()
        {
            jobID = txtJobType.SelectedIndex.ToString();
            jobtype = txtSearch.ToString();
        }

        private void displayImage(String ID)
        {
            conn.Open();
            string imgquery = "SELECT job_image_path from jobtypes WHERE job_typeID = @id";
            using (var cmdImg = new MySqlCommand(imgquery, conn))
            {
                cmdImg.Parameters.AddWithValue("@id", ID);
                MySqlDataReader r = cmdImg.ExecuteReader();
                if (r.HasRows)
                {
                    try
                    {
                        while (r.Read())
                        {
                            imgFileName = r["job_image_path"].ToString();
                            
                            pictureBoxPrev.Image = Config.GetImage(imgFileName, Subfolders.Jobs);
                        }
                    }
                    catch
                    {
                        r.Close();
                        cmdImg.Parameters.AddWithValue("@placeholder", "Jobs/test.jpg");
                        imgquery = "UPDATE jobtypes set job_image_path = @placeholder WHERE job_typeID = @id;";
                        cmdImg.ExecuteNonQuery();
                    }
                }
            }
            conn.Close();
        }

        private void displayLogo (String ID)
        {
            conn.Open();
            string imgquery = "SELECT Logo_image_path from jobtypes WHERE job_typeID = @id";
            using (var cmdImg = new MySqlCommand(imgquery, conn))
            {
                cmdImg.Parameters.AddWithValue("@id", ID);
                MySqlDataReader r = cmdImg.ExecuteReader();
                if (r.HasRows)
                {
                    try
                    {
                        while (r.Read())
                        {
                            logoFileName = r["Logo_image_path"].ToString();

                            pictureBoxLogo.Image = Config.GetImage(logoFileName, Subfolders.Jobs_CompanyLogo);
                        }
                    }
                    catch
                    {
                        r.Close();
                        cmdImg.Parameters.AddWithValue("@placeholder", "Jobs_CompanyLogo/test.jpg");
                        imgquery = "UPDATE jobtypes set Logo_image_path = @placeholder WHERE job_typeID = @id;";
                        cmdImg.ExecuteNonQuery();
                    }
                }
            }
            conn.Close();
        }

        private void displayValues (int row)
        {
            row = 0;
            txtJobTypeID.Text = gridview.SelectedRows[row].Cells[0].Value.ToString();
            txtJobTitle.Text = gridview.SelectedRows[row].Cells[1].Value.ToString();
            txtJobCategory.SelectedIndex = int.Parse(gridview.SelectedRows[row].Cells[2].Value.ToString());
            txtJobDescription.Text = gridview.SelectedRows[row].Cells[3].Value.ToString();
            txtJobLocation.Text = gridview.SelectedRows[row].Cells[4].Value.ToString();
            txtJobCompany.Text = gridview.SelectedRows[row].Cells[5].Value.ToString();
            displayImage(gridview.SelectedRows[row].Cells[0].Value.ToString());
            displayLogo(gridview.SelectedRows[row].Cells[0].Value.ToString());
            getValues();
        }

        private void getValues ()
        {
            type_id = txtJobTypeID.Text;
            types = txtJobTitle.Text;
            job_id = (txtJobCategory.SelectedIndex).ToString();
            desc = txtJobDescription.Text;
            location = txtJobLocation.Text;
            company = txtJobCompany.Text;
            category = txtJobCategory.Text;
        }

        private bool queryCheck (int q)
        {
            if (q == 1)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private bool checkEmpty ()
        {
            if (string.IsNullOrEmpty(txtJobTitle.Text) ||
                string.IsNullOrEmpty(txtJobTypeID.Text) ||
                string.IsNullOrEmpty(txtJobLocation.Text) ||
                string.IsNullOrEmpty(txtJobCompany.Text) ||
                string.IsNullOrEmpty(txtJobDescription.Text) ||
                (pictureBoxLogo.Image == null) ||
                (pictureBoxPrev.Image == null))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void clearFields ()
        {
            txtJobTitle.Text = string.Empty;
            txtJobTypeID.Text = string.Empty;
            txtJobLocation.Text = string.Empty;
            txtJobCompany.Text = string.Empty;
            txtJobDescription.Text = string.Empty;
        }

        private bool jobExists (String id)
        {

            String qry = "SELECT * FROM jobtypes WHERE job_typeID LIKE @job_typeID;";
            cmdJobLookUp = new MySqlCommand(qry, conn);
            cmdJobLookUp.Parameters.AddWithValue("@job_typeID", id);
            using (conn)
            {
                conn.Open();
                using (cmdJobLookUp)
                {
                    MySqlDataReader reader = cmdJobLookUp.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        private bool imageStillBeingUsed(String ImgFileName)
        {
            String qry = "SELECT job_image_path, Logo_image_path FROM jobtypes WHERE job_image_path = @image OR Logo_image_path = @image;";
            cmdImgLookUp = new MySqlCommand(qry, conn);
            cmdImgLookUp.Parameters.AddWithValue("@image", ImgFileName);
            using (conn)
            {
                conn.Open();
                using (cmdImgLookUp)
                {
                    MySqlDataReader reader = cmdImgLookUp.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        private bool add ()
        {
            bool success;
            string imgPath = imgFileName;
            string logoPath = logoFileName;
            string insert = "INSERT INTO jobtypes (job_typeID, job_types, job_id, job_description, job_location, job_company, job_image_path, Logo_image_path) VALUES (@job_typeID, @job_types, @job_id, @job_description, @job_location, @job_company, @job_image_path, @Logo_image_path);";
            cmdAdd = new MySqlCommand(insert, conn);
            cmdAdd.Parameters.AddWithValue("@job_typeID", type_id);
            cmdAdd.Parameters.AddWithValue("@job_types", types);
            cmdAdd.Parameters.AddWithValue("@job_id", job_id);
            cmdAdd.Parameters.AddWithValue("@job_description", desc);
            cmdAdd.Parameters.AddWithValue("@job_location", location);
            cmdAdd.Parameters.AddWithValue("@job_company", company);
            cmdAdd.Parameters.AddWithValue("@job_image_path", imgPath);
            cmdAdd.Parameters.AddWithValue("@Logo_image_path", logoPath);

            using (conn)
            {
                conn.Open();
                Config.SaveImage(openImg, Subfolders.Jobs);
                Config.SaveImage(openLogo, Subfolders.Jobs_CompanyLogo);
                return success = queryCheck(cmdAdd.ExecuteNonQuery());
            }
            
        }
        /*
        private string prepareImage ()
        {
            imgFileName = type_id + ".jpg";
            var serializedImage = Config.ImageToByteArray(pictureBoxPrev.Image);
            ////Create Picture Model to send to API
            //Picture pic = new Picture { Name = prepare_imgFileName, FolderName = "Jobs_CompanyLogo", image = serializedImage };

            ////send the picture to the API(returns path)
            //string path = Config.SavePic(pic);

            return path;
        }

        private string prepareLogo ()
        {
            prepare_logoFileName = company + "_logo.jpg";
            var serializedImage = Config.ImageToByteArray(pictureBoxLogo.Image);
            //Create Picture Model to send to API
            Picture pic = new Picture { Name = prepare_logoFileName, FolderName = "Jobs", image = serializedImage };
            
            //send the picture to the API(returns path)
            string path = Config.SavePic(pic);

            return path;
        }
        */
        private bool save ()
        {
            bool success;
            getValues();
            string imgPath = imgFileName;
            string logoPath = logoFileName;

            string update = "UPDATE jobtypes SET job_types = @job_types, job_id = @job_id, job_description = @job_description, job_location = @job_location, job_company = @job_company, job_image_path = @job_image_path, Logo_image_path = @Logo_image_path WHERE job_typeID = @job_typeID;";
            cmdSave = new MySqlCommand(update, conn);
            cmdSave.Parameters.AddWithValue("@job_typeID", type_id);
            cmdSave.Parameters.AddWithValue("@job_types", types);
            cmdSave.Parameters.AddWithValue("@job_id", job_id);
            cmdSave.Parameters.AddWithValue("@job_description", desc);
            cmdSave.Parameters.AddWithValue("@job_location", location);
            cmdSave.Parameters.AddWithValue("@job_company", company);
            cmdSave.Parameters.AddWithValue("@job_image_path", imgPath);
            cmdSave.Parameters.AddWithValue("@Logo_image_path", logoPath);
            using (conn)
            {
                conn.Open();
                Config.SaveImage(openImg, Subfolders.Jobs);
                Config.SaveImage(openLogo, Subfolders.Jobs_CompanyLogo);
                return success = queryCheck(cmdSave.ExecuteNonQuery());
            }
        }

        private bool delete (bool deleteImage)
        {
            bool success;
            getValues();
            imgFileName = type_id;
            prepare_logoFileName = company + "_logo";
            string delete = "DELETE FROM jobtypes WHERE job_typeID = @job_typeID;";
            cmdDelete = new MySqlCommand(delete, conn);
            cmdDelete.Parameters.AddWithValue("@job_typeID", type_id);
            pictureBoxLogo.Image = null;
            pictureBoxPrev.Image = null;
            if (deleteImage)
            {
                Config.DeleteImage(Subfolders.Jobs_CompanyLogo, logoFileName);
                Config.DeleteImage(Subfolders.Jobs, imgFileName);
            }
            using (conn)
            {
                conn.Open();
                return success = queryCheck(cmdDelete.ExecuteNonQuery());
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            updateValues();
            populateGridView();
        }

        private void txtJobType_SelectedValueChanged(object sender, EventArgs e)
        {
            updateValues();
            populateGridView();
        }

        private void gridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            displayValues(gridview.CurrentCell.RowIndex);
        }

        private void gridview_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            displayValues(gridview.CurrentCell.RowIndex);
        }

        private void addToDatabase()
        {
            getValues();
            if (!jobExists(type_id))
            {
                if (add())
                {
                    MessageBox.Show("data added");
                }

                else
                {
                    MessageBox.Show("data insertion failed");
                }
            }
            else
            {
                MessageBox.Show("data insertion failed. Job_typeID already exists");
            }
            populateGridView();
        }

        private void updateDatabase()
        {
            getValues();
            if (jobExists(type_id))
            {
                if (save())
                {
                    MessageBox.Show("changes saved");
                }

                else
                {
                    MessageBox.Show("update failed");
                }
            }

            else
            {
                MessageBox.Show("update failed. Job_typeID doesn't exists");
            }
            populateGridView();
        }

        private void deleteFromDatabase()
        {
            getValues();
            if (jobExists(type_id))
            {
                if ((!imageStillBeingUsed(imgFileName))&&(!imageStillBeingUsed(logoFileName)))
                {
                    if (delete(true))
                    {
                        MessageBox.Show("delete successful");
                        clearFields();
                    }

                    else
                    {
                        MessageBox.Show("delete failed");
                    }
                }
                else
                {
                    if (MessageBox.Show("The image file used in this entry is still being used by another entry." +
                        "Delete the entry without deleting the image file?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (delete(false))
                        {
                            MessageBox.Show("delete successful");
                            clearFields();
                        }

                        else
                        {
                            MessageBox.Show("delete failed");
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("delete failed. Job_typeID doesn't exists");
            }
            populateGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!checkEmpty())
            {
                addToDatabase();
            }
            else
            {
                MessageBox.Show("Add failed. Please complete the fields.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save the changes made?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!checkEmpty())
                {
                    updateDatabase();
                }
                else
                {
                    MessageBox.Show("Save failed. Please complete the fields.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete the selected item?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!checkEmpty())
                {
                    deleteFromDatabase();
                }
                else
                {
                    MessageBox.Show("Delete failed. Please select a row from the table and try again.");
                }
            }
            
        }

        private void displayImg ()
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearFields();
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openImg = openFile;
                    Image img = new Bitmap(openFile.FileName);
                    {
                        pictureBoxPrev.Image = img;
                        imgFileName = openFile.SafeFileName;
                        btnSave.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
                return;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Choose Logo Image";
            openFile.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openLogo = openFile;
                    Image img = new Bitmap(openFile.FileName);
                    {
                        pictureBoxLogo.Image = img;
                        logoFileName = openFile.SafeFileName;
                        btnSave.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " error");
                }
            }
        }

        private void gridview_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (IndexOutOfRangeException)
            {

            }
        }

       
    }
}
