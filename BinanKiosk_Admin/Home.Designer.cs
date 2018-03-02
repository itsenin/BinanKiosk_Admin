namespace BinanKiosk_Admin
{
    partial class Home
    { 
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timestamp = new System.Windows.Forms.Timer(this.components);
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.pb_preview = new System.Windows.Forms.PictureBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.lbldate = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl_imageName = new System.Windows.Forms.Label();
            this.lbl_sliderPics = new System.Windows.Forms.Label();
            this.lbl_imagePrv = new System.Windows.Forms.Label();
            this.lbl_Home = new System.Windows.Forms.Label();
            this.lst_sliderPics = new System.Windows.Forms.ListBox();
            this.pnl_Save = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOffices = new System.Windows.Forms.Button();
            this.btnOfficers = new System.Windows.Forms.Button();
            this.btnJobs = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnMaps = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lbl_navigation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnl_Save.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // timestamp
            // 
            this.timestamp.Tick += new System.EventHandler(this.timestamp_Tick);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Transparent;
            this.btn_save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_save.BackgroundImage")));
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.Transparent;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_save.Location = new System.Drawing.Point(327, 0);
            this.btn_save.MaximumSize = new System.Drawing.Size(142, 68);
            this.btn_save.MinimumSize = new System.Drawing.Size(142, 68);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(142, 68);
            this.btn_save.TabIndex = 248;
            this.btn_save.Text = "SAVE";
            this.btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_delete.BackgroundImage")));
            this.btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_delete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_delete.FlatAppearance.BorderSize = 0;
            this.btn_delete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delete.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.ForeColor = System.Drawing.Color.Transparent;
            this.btn_delete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_delete.Location = new System.Drawing.Point(598, 323);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(142, 68);
            this.btn_delete.TabIndex = 247;
            this.btn_delete.Text = "DELETE";
            this.btn_delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_delete.UseVisualStyleBackColor = false;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // pb_preview
            // 
            this.pb_preview.BackColor = System.Drawing.Color.Transparent;
            this.pb_preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_preview.Location = new System.Drawing.Point(0, 31);
            this.pb_preview.Name = "pb_preview";
            this.pb_preview.Size = new System.Drawing.Size(469, 334);
            this.pb_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_preview.TabIndex = 244;
            this.pb_preview.TabStop = false;
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.Transparent;
            this.btn_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_add.BackgroundImage")));
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.Transparent;
            this.btn_add.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_add.Location = new System.Drawing.Point(598, 249);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(142, 68);
            this.btn_add.TabIndex = 243;
            this.btn_add.Text = "ADD";
            this.btn_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            this.btn_add.MouseLeave += new System.EventHandler(this.btn_add_MouseLeave);
            this.btn_add.MouseHover += new System.EventHandler(this.btn_add_MouseHover);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.ForestGreen;
            this.pictureBox7.BackgroundImage = global::BinanKiosk_Admin.Properties.Resources.binancity_nav_logo;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(503, 5);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(74, 74);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 241;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.ForestGreen;
            this.pictureBox8.BackgroundImage = global::BinanKiosk_Admin.Properties.Resources.Binan_city;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(593, 5);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(209, 74);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 95;
            this.pictureBox8.TabStop = false;
            // 
            // lbldate
            // 
            this.lbldate.BackColor = System.Drawing.Color.ForestGreen;
            this.lbldate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbldate.Font = new System.Drawing.Font("Arial", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldate.ForeColor = System.Drawing.Color.White;
            this.lbldate.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbldate.Location = new System.Drawing.Point(970, 691);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(396, 81);
            this.lbldate.TabIndex = 94;
            this.lbldate.Text = ".";
            this.lbldate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(94)))), ((int)(((byte)(13)))));
            this.pictureBox4.BackgroundImage = global::BinanKiosk_Admin.Properties.Resources.uppermenu;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1366, 86);
            this.pictureBox4.TabIndex = 92;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(94)))), ((int)(((byte)(13)))));
            this.pictureBox3.BackgroundImage = global::BinanKiosk_Admin.Properties.Resources.undermenu;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(0, 677);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1459, 94);
            this.pictureBox3.TabIndex = 51;
            this.pictureBox3.TabStop = false;
            // 
            // lbl_imageName
            // 
            this.lbl_imageName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_imageName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_imageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_imageName.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_imageName.Location = new System.Drawing.Point(0, 0);
            this.lbl_imageName.Name = "lbl_imageName";
            this.lbl_imageName.Size = new System.Drawing.Size(160, 68);
            this.lbl_imageName.TabIndex = 255;
            this.lbl_imageName.Text = "imageName";
            this.lbl_imageName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_sliderPics
            // 
            this.lbl_sliderPics.AutoSize = true;
            this.lbl_sliderPics.BackColor = System.Drawing.Color.Transparent;
            this.lbl_sliderPics.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_sliderPics.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_sliderPics.Location = new System.Drawing.Point(232, 206);
            this.lbl_sliderPics.Name = "lbl_sliderPics";
            this.lbl_sliderPics.Size = new System.Drawing.Size(204, 31);
            this.lbl_sliderPics.TabIndex = 256;
            this.lbl_sliderPics.Text = "Slider Pictures";
            // 
            // lbl_imagePrv
            // 
            this.lbl_imagePrv.AutoSize = true;
            this.lbl_imagePrv.BackColor = System.Drawing.Color.Transparent;
            this.lbl_imagePrv.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_imagePrv.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_imagePrv.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl_imagePrv.Location = new System.Drawing.Point(0, 0);
            this.lbl_imagePrv.Name = "lbl_imagePrv";
            this.lbl_imagePrv.Size = new System.Drawing.Size(206, 31);
            this.lbl_imagePrv.TabIndex = 257;
            this.lbl_imagePrv.Text = "Image Preview";
            // 
            // lbl_Home
            // 
            this.lbl_Home.AutoSize = true;
            this.lbl_Home.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Home.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Home.ForeColor = System.Drawing.Color.Black;
            this.lbl_Home.Location = new System.Drawing.Point(598, 102);
            this.lbl_Home.Name = "lbl_Home";
            this.lbl_Home.Size = new System.Drawing.Size(172, 55);
            this.lbl_Home.TabIndex = 258;
            this.lbl_Home.Text = "HOME";
            // 
            // lst_sliderPics
            // 
            this.lst_sliderPics.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_sliderPics.FormattingEnabled = true;
            this.lst_sliderPics.ItemHeight = 20;
            this.lst_sliderPics.Location = new System.Drawing.Point(237, 249);
            this.lst_sliderPics.Name = "lst_sliderPics";
            this.lst_sliderPics.Size = new System.Drawing.Size(301, 184);
            this.lst_sliderPics.TabIndex = 254;
            this.lst_sliderPics.SelectedIndexChanged += new System.EventHandler(this.lst_sliderPics_SelectedIndexChanged);
            // 
            // pnl_Save
            // 
            this.pnl_Save.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Save.Controls.Add(this.pb_preview);
            this.pnl_Save.Controls.Add(this.panel1);
            this.pnl_Save.Controls.Add(this.lbl_imagePrv);
            this.pnl_Save.Location = new System.Drawing.Point(792, 208);
            this.pnl_Save.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_Save.Name = "pnl_Save";
            this.pnl_Save.Size = new System.Drawing.Size(469, 433);
            this.pnl_Save.TabIndex = 259;
            this.pnl_Save.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_imageName);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 365);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 68);
            this.panel1.TabIndex = 258;
            // 
            // btnOffices
            // 
            this.btnOffices.BackColor = System.Drawing.Color.DarkGreen;
            this.btnOffices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOffices.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnOffices.FlatAppearance.BorderSize = 2;
            this.btnOffices.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnOffices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOffices.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOffices.ForeColor = System.Drawing.Color.Transparent;
            this.btnOffices.Location = new System.Drawing.Point(0, 257);
            this.btnOffices.Name = "btnOffices";
            this.btnOffices.Size = new System.Drawing.Size(155, 50);
            this.btnOffices.TabIndex = 276;
            this.btnOffices.Text = "DEPARTMENTS";
            this.btnOffices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOffices.UseVisualStyleBackColor = false;
            this.btnOffices.Click += new System.EventHandler(this.btnOffices_Click);
            // 
            // btnOfficers
            // 
            this.btnOfficers.BackColor = System.Drawing.Color.DarkGreen;
            this.btnOfficers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOfficers.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnOfficers.FlatAppearance.BorderSize = 2;
            this.btnOfficers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnOfficers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOfficers.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOfficers.ForeColor = System.Drawing.Color.Transparent;
            this.btnOfficers.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOfficers.Location = new System.Drawing.Point(0, 208);
            this.btnOfficers.Name = "btnOfficers";
            this.btnOfficers.Size = new System.Drawing.Size(155, 50);
            this.btnOfficers.TabIndex = 275;
            this.btnOfficers.Text = "OFFICERS";
            this.btnOfficers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOfficers.UseVisualStyleBackColor = false;
            this.btnOfficers.Click += new System.EventHandler(this.btnOfficers_Click);
            // 
            // btnJobs
            // 
            this.btnJobs.BackColor = System.Drawing.Color.DarkGreen;
            this.btnJobs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJobs.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnJobs.FlatAppearance.BorderSize = 2;
            this.btnJobs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnJobs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJobs.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJobs.ForeColor = System.Drawing.Color.Transparent;
            this.btnJobs.Location = new System.Drawing.Point(0, 355);
            this.btnJobs.Name = "btnJobs";
            this.btnJobs.Size = new System.Drawing.Size(155, 50);
            this.btnJobs.TabIndex = 280;
            this.btnJobs.Text = "JOBS";
            this.btnJobs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnJobs.UseVisualStyleBackColor = false;
            this.btnJobs.Click += new System.EventHandler(this.btnJobs_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGreen;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(0, 404);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 50);
            this.button2.TabIndex = 279;
            this.button2.Text = "SERVICES";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // btnMaps
            // 
            this.btnMaps.BackColor = System.Drawing.Color.DarkGreen;
            this.btnMaps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaps.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnMaps.FlatAppearance.BorderSize = 2;
            this.btnMaps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaps.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaps.ForeColor = System.Drawing.Color.Transparent;
            this.btnMaps.Location = new System.Drawing.Point(0, 306);
            this.btnMaps.Name = "btnMaps";
            this.btnMaps.Size = new System.Drawing.Size(155, 50);
            this.btnMaps.TabIndex = 278;
            this.btnMaps.Text = "MAPS";
            this.btnMaps.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMaps.UseVisualStyleBackColor = false;
            this.btnMaps.Click += new System.EventHandler(this.btnMaps_Click);
            // 
            // btnHome
            // 
            this.btnHome.AutoSize = true;
            this.btnHome.BackColor = System.Drawing.Color.DarkGreen;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnHome.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btnHome.FlatAppearance.BorderSize = 2;
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.Transparent;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHome.Location = new System.Drawing.Point(0, 159);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(155, 50);
            this.btnHome.TabIndex = 277;
            this.btnHome.Text = "HOME";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 177);
            this.pictureBox2.TabIndex = 282;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.DarkGreen;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(0, 453);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(155, 319);
            this.pictureBox5.TabIndex = 281;
            this.pictureBox5.TabStop = false;
            // 
            // lbl_navigation
            // 
            this.lbl_navigation.BackColor = System.Drawing.Color.DarkGreen;
            this.lbl_navigation.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_navigation.ForeColor = System.Drawing.Color.LimeGreen;
            this.lbl_navigation.Location = new System.Drawing.Point(1, 114);
            this.lbl_navigation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_navigation.Name = "lbl_navigation";
            this.lbl_navigation.Size = new System.Drawing.Size(153, 42);
            this.lbl_navigation.TabIndex = 294;
            this.lbl_navigation.Text = "Navigation";
            this.lbl_navigation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::BinanKiosk_Admin.Properties.Resources.bgopacity3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1370, 772);
            this.Controls.Add(this.lbl_navigation);
            this.Controls.Add(this.btnOffices);
            this.Controls.Add(this.btnOfficers);
            this.Controls.Add(this.btnJobs);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnMaps);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.lbl_Home);
            this.Controls.Add(this.lbl_sliderPics);
            this.Controls.Add(this.lst_sliderPics);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.lbldate);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pnl_Save);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnl_Save.ResumeLayout(false);
            this.pnl_Save.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Timer timestamp;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.Label lbldate;
    private System.Windows.Forms.PictureBox pictureBox8;
    private System.Windows.Forms.PictureBox pictureBox7;
    private System.Windows.Forms.Button btn_add;
    private System.Windows.Forms.PictureBox pb_preview;
    private System.Windows.Forms.Button btn_delete;
    private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label lbl_imageName;
        private System.Windows.Forms.Label lbl_sliderPics;
        private System.Windows.Forms.Label lbl_imagePrv;
        private System.Windows.Forms.Label lbl_Home;
        private System.Windows.Forms.ListBox lst_sliderPics;
        private System.Windows.Forms.Panel pnl_Save;
        private System.Windows.Forms.Button btnOffices;
        private System.Windows.Forms.Button btnOfficers;
        private System.Windows.Forms.Button btnJobs;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnMaps;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbl_navigation;
        private System.Windows.Forms.Panel panel1;
    }
}
