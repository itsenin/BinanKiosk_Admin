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
    public partial class Search : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlDataReader reader;
        MySqlCommand cmd;

        String selectedValue;
        String position;
        String department;

        bool add = false;

        public Search()
        {
            InitializeComponent();
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

            cmd = new MySqlCommand("SELECT CONCAT (officials.first_name, ' ', officials.middle_initial, ' ', officials.last_name, ' ', officials.suffex) AS name FROM officials", conn);
            cmd.ExecuteNonQuery();
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
            cmd = new MySqlCommand("SELECT department_name FROM departments", conn);
            cmd.ExecuteNonQuery();
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
        }

        private void officersList_SelectedValueChanged(object sender, EventArgs e)
        {
            officerInformation.Enabled = false;
            selectedValue = officersList.GetItemText(officersList.SelectedItem);

            conn.Open();
            cmd = new MySqlCommand("SELECT officials.officials_id, officials.first_name, officials.last_name, officials.middle_initial, officials.suffex, departments.department_name, positions.position_name, departments.room_id FROM officials,departments,positions WHERE positions.position_id = officials.position_id AND departments.department_id = officials.department_id AND CONCAT (officials.first_name, ' ', officials.middle_initial, ' ', officials.last_name, ' ', officials.suffex) LIKE '%" + selectedValue + "%' ", conn);
            cmd.ExecuteNonQuery();
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
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clear();
            add = true;
            txtID.Enabled = true;
            officerInformation.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the record of " + txtID.Text + "?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                conn.Open();

                cmd = new MySqlCommand("DELETE FROM officials WHERE officials.officials_id = '" + Convert.ToInt32(txtID.Text) + "'", conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Deleted!");

                officersList.Items.Clear();
                officers();
                clear();
            }
            else
            {
                MessageBox.Show("Not Deleted!");

                officersList.Items.Clear();
                officers();
                clear();
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            add = false;
            txtID.Enabled = false;
            officerInformation.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                conn.Close();

                MessageBox.Show("Inserted!");

                officerInformation.Enabled = false;
                officersList.Items.Clear();
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

                officerInformation.Enabled = false;
                officersList.Items.Clear();
                officers();
                clear();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Config.CallHome(this);
        }
    }
}
