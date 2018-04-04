﻿using System;
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
    public partial class MainForm : Form
    {
        private static int i;
        String userName;
        String firstName;
        String lastName;
        String middleInitial;
        String designation;
        String office;
        String email;
        String password;

        public MainForm()
        {
            InitializeComponent();

            //retain navigation place and transparency over picturebox
            var pos = this.PointToScreen(lbl_navigation.Location);
            pos = pictureBox2.PointToClient(pos);
            lbl_navigation.Parent = pictureBox2;
            lbl_navigation.Location = pos;

            this.userName = User.userName;
            this.firstName = User.firstName;
            this.lastName = User.lastName;
            this.middleInitial = User.middleInitial;
            this.designation = User.designation;
            this.office = User.office;
            this.email = User.email;
            this.password = User.password;
            showValues();
        }

        private void showValues()
        {
            MessageBox.Show("Username: " + userName + " Password: " + password + " Name: " + firstName + " " + middleInitial + " " + lastName + " Designation: " + designation + " Office: " + office + " Email: " + email);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timestamp.Enabled = true;
            timestamp.Interval = 1;
            timestamp.Start();
        }

        private void timestamp_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
        }

        public int seconds = 0;

        private void OnTimerEvent(object sender, EventArgs e)
        {
            lbldate.Text =  DateTime.Now.ToLongDateString() + System.Environment.NewLine + DateTime.Now.ToLongTimeString();
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
            if (MessageBox.Show("Logout?", "Confirm Action", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Config.CallLogin(this);
        }
    }
}
