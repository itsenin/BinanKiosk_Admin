using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BinanKiosk_Admin
{
    public partial class Signup : Form
    {
        MySqlConnection conn = new MySqlConnection("SERVER=" + "localhost" + ";" + "DATABASE=" + "binan_kiosk" + ";" + "UID=" + "root" + ";" + "PASSWORD=" + "" + ";");
        MySqlCommand cmdAccountLookup;
        MySqlCommand cmdAccountCreation;

        String userName;
        String firstName;
        String lastName;
        String middleInitial;
        String designation;
        String office;
        String email;
        String password;
        String confPassword;

        String qryAccountLookup;
        String qryCreateAccount;

        public Signup()
        {
            InitializeComponent();
        }

        private void initialize ()
        {
            this.userName = txtUsername.Text.ToString();
            this.firstName = txtFirstName.Text.ToString();
            this.lastName = txtLastName.Text.ToString();
            this.middleInitial = txtMI.Text.ToString();
            this.designation = txtDesignation.Text.ToString();
            this.office = txtOffice.Text.ToString();
            this.email = txtEmail.Text.ToString();
            this.password = txtPassword.Text.ToString();
            this.confPassword = txtConfirmPass.Text.ToString();
        }

        private void test ()
        {
            MessageBox.Show(userName + firstName + lastName + middleInitial + designation + office + email + password + confPassword);
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            initialize();
            //test();
            retainData();
            alertmsg();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Go back to the Main screen? (Current registration info will be discarded)", "Confirm Action", MessageBoxButtons.YesNo)==DialogResult.Yes)
                Config.CallMain(this);
        }

        private bool valueCheck () //true if email and user is ok
        {
            bool isUserName = Regex.IsMatch(userName, @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$");
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_'{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_'{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isUserName && isEmail;
        }

        private bool nullCheck () //true if at least one value is nullxxx
        {
            if (((string.IsNullOrEmpty(userName)) ||((string.IsNullOrEmpty(firstName)) ||
                ((string.IsNullOrEmpty(middleInitial)) ||((string.IsNullOrEmpty(lastName)) ||
                ((string.IsNullOrEmpty(designation)) ||((string.IsNullOrEmpty(office)) ||((string.IsNullOrEmpty(password)) ||
                ((string.IsNullOrEmpty(confPassword)))))))))))
            {
                return false;
            }

            else
            {
                return true;
            }
        }

        private bool passwordCheck () //true if passwords match
        {
            if (password.Equals(confPassword) &&
                ((!(string.IsNullOrEmpty(password)) && !(string.IsNullOrEmpty(password)))))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private void alertmsg () //temp
        {
            if (!valueCheck()&&!nullCheck())
            {
                MessageBox.Show("invalid");
            }

            else if (!passwordCheck())
            {
                MessageBox.Show("passwords dont match");
            }

            else
            {
                register();
            }
        }

        private void retainData ()
        {
            txtDesignation.Text = designation;
            txtEmail.Text = email;
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            txtMI.Text = middleInitial;
            txtOffice.Text = office;
            txtUsername.Text = userName;
        }

        private void prepareSQL ()
        {
            //queries
            this.qryAccountLookup = "SELECT * FROM user WHERE username LIKE @uName";
            this.qryCreateAccount = "INSERT INTO user (username, password, first_name, last_name, middle_initial, position, department, email) VALUES (@userName, @password, @firstName, @lastName, @middleInitial, @designation, @office, @email )";
            
            //account lookup
            cmdAccountLookup = new MySqlCommand(qryAccountLookup, conn);
            cmdAccountLookup.Parameters.AddWithValue("@uName", userName);

            //account creation
            cmdAccountCreation = new MySqlCommand(qryCreateAccount, conn);
            cmdAccountCreation.Parameters.AddWithValue("@userName", userName);
            cmdAccountCreation.Parameters.AddWithValue("@password", password);
            cmdAccountCreation.Parameters.AddWithValue("@firstName", firstName);
            cmdAccountCreation.Parameters.AddWithValue("@lastName", lastName);
            cmdAccountCreation.Parameters.AddWithValue("@middleInitial", middleInitial);
            cmdAccountCreation.Parameters.AddWithValue("@designation", designation);
            cmdAccountCreation.Parameters.AddWithValue("@office", office);
            cmdAccountCreation.Parameters.AddWithValue("@email", email);
        }

        private bool accountExists () //true if account already exists
        {
            using (conn)
            {
                conn.Open();
                using (cmdAccountLookup)
                {
                    MySqlDataReader reader = cmdAccountLookup.ExecuteReader();
                    return reader.HasRows;
                }
            }
        }

        private bool createAccount () //true if account creation is successful 
        {
            //test();
            int check;
            using (conn)
            {
                conn.Open();
                using (cmdAccountCreation)
                {
                    check = cmdAccountCreation.ExecuteNonQuery();
                }
            }
            if (check == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void register ()
        {
            prepareSQL();
            if (!accountExists())
            {

                if (createAccount())
                {
                    MessageBox.Show("registration complete");
                    
                }

                else
                {
                    MessageBox.Show("registration failed");
                }
            }

            else
            {
                MessageBox.Show("account already exists");
            }
        }
    }
}
