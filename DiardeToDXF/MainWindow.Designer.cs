namespace DiardeToDXF
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Label();
            this.previewsPanel = new DiardeToDXF.CustomPanel();
            this.revisionsPanel = new DiardeToDXF.CustomPanel();
            this.geometriesPanel = new DiardeToDXF.CustomPanel();
            this.roomsPanel = new DiardeToDXF.CustomPanel();
            this.projectsPanel = new DiardeToDXF.CustomPanel();
            this.profilePicture = new System.Windows.Forms.PictureBox();
            this.profileMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmText = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.titlebar = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dummy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).BeginInit();
            this.profileMenu.SuspendLayout();
            this.titlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(21, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(22, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Project";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(198, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Room";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(378, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Geometry";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(546, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Revision";
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txtSearch.Location = new System.Drawing.Point(23, 624);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(163, 20);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TabStop = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClose.Location = new System.Drawing.Point(810, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // previewsPanel
            // 
            this.previewsPanel.Geometry = null;
            this.previewsPanel.Location = new System.Drawing.Point(735, 131);
            this.previewsPanel.Name = "previewsPanel";
            this.previewsPanel.Project = null;
            this.previewsPanel.Revision = null;
            this.previewsPanel.Room = null;
            this.previewsPanel.Size = new System.Drawing.Size(90, 470);
            this.previewsPanel.TabIndex = 3;
            // 
            // revisionsPanel
            // 
            this.revisionsPanel.Geometry = null;
            this.revisionsPanel.Location = new System.Drawing.Point(549, 131);
            this.revisionsPanel.Name = "revisionsPanel";
            this.revisionsPanel.Project = null;
            this.revisionsPanel.Revision = null;
            this.revisionsPanel.Room = null;
            this.revisionsPanel.Size = new System.Drawing.Size(163, 470);
            this.revisionsPanel.TabIndex = 3;
            // 
            // geometriesPanel
            // 
            this.geometriesPanel.Geometry = null;
            this.geometriesPanel.Location = new System.Drawing.Point(381, 131);
            this.geometriesPanel.Name = "geometriesPanel";
            this.geometriesPanel.Project = null;
            this.geometriesPanel.Revision = null;
            this.geometriesPanel.Room = null;
            this.geometriesPanel.Size = new System.Drawing.Size(163, 470);
            this.geometriesPanel.TabIndex = 3;
            // 
            // roomsPanel
            // 
            this.roomsPanel.Geometry = null;
            this.roomsPanel.Location = new System.Drawing.Point(201, 131);
            this.roomsPanel.Name = "roomsPanel";
            this.roomsPanel.Project = null;
            this.roomsPanel.Revision = null;
            this.roomsPanel.Room = null;
            this.roomsPanel.Size = new System.Drawing.Size(163, 470);
            this.roomsPanel.TabIndex = 3;
            // 
            // projectsPanel
            // 
            this.projectsPanel.Geometry = null;
            this.projectsPanel.Location = new System.Drawing.Point(23, 131);
            this.projectsPanel.Name = "projectsPanel";
            this.projectsPanel.Project = null;
            this.projectsPanel.Revision = null;
            this.projectsPanel.Room = null;
            this.projectsPanel.Size = new System.Drawing.Size(172, 470);
            this.projectsPanel.TabIndex = 3;
            // 
            // profilePicture
            // 
            this.profilePicture.BackgroundImage = ((System.Drawing.Image)(DiardeToDXF.Properties.Resources.profilePicture_BackgroundImage));
            this.profilePicture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profilePicture.Location = new System.Drawing.Point(771, 9);
            this.profilePicture.Name = "profilePicture";
            this.profilePicture.Size = new System.Drawing.Size(20, 20);
            this.profilePicture.TabIndex = 4;
            this.profilePicture.TabStop = false;
            this.profilePicture.Click += new System.EventHandler(this.profilePicture_Click);
            // 
            // profileMenu
            // 
            this.profileMenu.AutoSize = false;
            this.profileMenu.BackColor = System.Drawing.Color.White;
            this.profileMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmText,
            this.btnLogout});
            this.profileMenu.Name = "profileMenu";
            this.profileMenu.Size = new System.Drawing.Size(150, 50);
            // 
            // cmText
            // 
            this.cmText.BackColor = System.Drawing.Color.White;
            this.cmText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmText.ForeColor = System.Drawing.Color.OrangeRed;
            this.cmText.Name = "cmText";
            this.cmText.ShowShortcutKeys = false;
            this.cmText.Size = new System.Drawing.Size(248, 22);
            this.cmText.Text = "useruseruseruseruser@yahoo.com";
            this.cmText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLogout
            // 
            this.btnLogout.AutoSize = false;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 22);
            this.btnLogout.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // titlebar
            // 
            this.titlebar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.titlebar.Controls.Add(this.panel2);
            this.titlebar.Controls.Add(this.panel1);
            this.titlebar.Controls.Add(this.pictureBox1);
            this.titlebar.Controls.Add(this.profilePicture);
            this.titlebar.Controls.Add(this.btnClose);
            this.titlebar.Controls.Add(this.label6);
            this.titlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlebar.Location = new System.Drawing.Point(0, 0);
            this.titlebar.Name = "titlebar";
            this.titlebar.Size = new System.Drawing.Size(845, 36);
            this.titlebar.TabIndex = 6;
            this.titlebar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.titlebar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(845, 1);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 1);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(DiardeToDXF.Properties.Resources.logo_small));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(36, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "DiardeForAutoCAD";
            this.label6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseDown);
            this.label6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titlebar_MouseMove);
            // 
            // dummy
            // 
            this.dummy.Location = new System.Drawing.Point(23, 424);
            this.dummy.Name = "dummy";
            this.dummy.Size = new System.Drawing.Size(163, 20);
            this.dummy.TabIndex = 0;
            this.dummy.UseVisualStyleBackColor = true;
            this.dummy.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(845, 650);
            this.Controls.Add(this.titlebar);
            this.Controls.Add(this.projectsPanel);
            this.Controls.Add(this.previewsPanel);
            this.Controls.Add(this.revisionsPanel);
            this.Controls.Add(this.geometriesPanel);
            this.Controls.Add(this.roomsPanel);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dummy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Click += new System.EventHandler(this.MainWindow_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.profilePicture)).EndInit();
            this.profileMenu.ResumeLayout(false);
            this.titlebar.ResumeLayout(false);
            this.titlebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label btnClose;
        private CustomPanel projectsPanel;
        private CustomPanel roomsPanel;
        private CustomPanel geometriesPanel;
        private CustomPanel revisionsPanel;
        private CustomPanel previewsPanel;
        private System.Windows.Forms.PictureBox profilePicture;
        private System.Windows.Forms.ContextMenuStrip profileMenu;
        private System.Windows.Forms.ToolStripMenuItem cmText;
        private System.Windows.Forms.ToolStripMenuItem btnLogout;
        private System.Windows.Forms.Panel titlebar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button dummy;
    }
}

