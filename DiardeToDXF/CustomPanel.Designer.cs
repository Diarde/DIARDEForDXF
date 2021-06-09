namespace DiardeToDXF
{
    partial class CustomPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bar = new System.Windows.Forms.Panel();
            this.projects = new System.Windows.Forms.FlowLayoutPanel();
            this.scrollbar = new System.Windows.Forms.Panel();
            this.scrollbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.Black;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(6, 80);
            this.bar.TabIndex = 6;
            this.bar.Visible = false;
            this.bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bar_MouseDown);
            this.bar.MouseEnter += new System.EventHandler(this.scrollbar_MouseEnter);
            this.bar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bar_MouseMove);
            // 
            // projects
            // 
            this.projects.AutoSize = true;
            this.projects.BackColor = System.Drawing.Color.White;
            this.projects.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.projects.Location = new System.Drawing.Point(0, 0);
            this.projects.Margin = new System.Windows.Forms.Padding(0);
            this.projects.Name = "projects";
            this.projects.Size = new System.Drawing.Size(150, 150);
            this.projects.TabIndex = 2;
            this.projects.WrapContents = false;
            this.projects.MouseEnter += new System.EventHandler(this.projects_MouseEnter);
            // 
            // scrollbar
            // 
            this.scrollbar.Controls.Add(this.bar);
            this.scrollbar.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollbar.Location = new System.Drawing.Point(147, 0);
            this.scrollbar.Name = "scrollbar";
            this.scrollbar.Size = new System.Drawing.Size(3, 150);
            this.scrollbar.TabIndex = 0;
            // 
            // CustomPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollbar);
            this.Controls.Add(this.projects);
            this.Name = "CustomPanel";
            this.MouseEnter += new System.EventHandler(this.projects_MouseEnter);
            this.scrollbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Panel bar;
        private System.Windows.Forms.FlowLayoutPanel projects;
        private System.Windows.Forms.Panel scrollbar;
    }
}
