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
    public partial class Login : Form
    {
        MySqlConnection conn = Config.conn;
        MySqlCommand cmdAccountLookup;

        String userName;
        String firstName;
        String lastName;
        String middleInitial;
        String designation;
        String office;
        String email;
        String password;
        String qryAccountLookup;

        public Login()
        {
            InitializeComponent();
        }

        private void initialize()
        {
            this.userName = txtUsername.Text.ToString();
            this.password = txtPassword.Text.ToString();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            initialize();
            prepareSQL();
            login();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            Config.CallSignup(this);
        }

        private void prepareSQL ()
        {
            this.qryAccountLookup = "SELECT * FROM user WHERE username = @uName AND password = @password";
            cmdAccountLookup = new MySqlCommand(qryAccountLookup, conn);
            cmdAccountLookup.Parameters.AddWithValue("@uName", userName);
            cmdAccountLookup.Parameters.AddWithValue("@password", password);
        }

        private bool accountExists()
        {
            using (conn)
            {
                conn.Open();
                using (cmdAccountLookup)
                {
                    using (MySqlDataReader reader = cmdAccountLookup.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            userName = reader[0].ToString();
                            password = reader[1].ToString();
                            firstName = reader[2].ToString();
                            lastName = reader[3].ToString();
                            middleInitial = reader[4].ToString();
                            designation = reader[5].ToString();
                            office = reader[6].ToString();
                            email = reader[7].ToString();
                        }
                        return reader.HasRows;
                    }
                }
            }
        }

        private bool nullCheck() //true if at least one value is nullxxx
        {
            if ((string.IsNullOrEmpty(userName)) || (string.IsNullOrEmpty(password)))
            {
                MessageBox.Show("please fill out the fields");
                return false;
            }

            else
            {
                return true;
            }
        }

        private void login ()
        {
            if(nullCheck())
            {
                if(accountExists())
                {

                    User.userName = userName;
                    User.firstName = firstName;
                    User.lastName = lastName;
                    User.middleInitial = middleInitial;
                    User.email = email;
                    User.password = password;
                    User.office = office;
                    User.designation = designation;

                    Config.CallMain(this);
                }
                else
                {
                    MessageBox.Show("account doesn't exist");
                }
            }
        }

        private void btnForgotPass_Click(object sender, EventArgs e)
        {

        }
    }
}
