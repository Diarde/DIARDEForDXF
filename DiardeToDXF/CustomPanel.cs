using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiardeToDXF
{
    public partial class CustomPanel : UserControl
    {
        private Point MouseDownLocation;
        private int MOUSE_WHEEL_STEP = 10;
        private bool enableScrolling = false;
        int scrollingSpeed = 10;
        float scrollBarNavSpace = 0;
        float projectsNavSpace = 0;

        public Project Project { get; set; }
        public Room Room { get; set; }
        public Geometry Geometry { get; set; }
        public Revision Revision { get; set; }
        public CustomPanel()
        {
            InitializeComponent();
            MouseWheel += OnMouseWheel;
        }

        public void ResetBGColor()
        {
            foreach (Control button in projects.Controls)
                if (button.BackColor != Color.White) button.BackColor = Color.White;
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (enableScrolling == false) return;
            if (e.Delta > 0) // up
            {
                if (bar.Top > MOUSE_WHEEL_STEP)
                {
                    bar.Top -= MOUSE_WHEEL_STEP;
                    float offset = (bar.Top / scrollBarNavSpace) * projectsNavSpace;
                    projects.Top = (int)-offset;
                }
                else
                {
                    bar.Top = projects.Top = 0;
                }
            }
            else // down
            {
                if (bar.Top + bar.Height < (Height - MOUSE_WHEEL_STEP))
                {
                    bar.Top += MOUSE_WHEEL_STEP;
                    float offset = (bar.Top / scrollBarNavSpace) * projectsNavSpace;
                    projects.Top = (int)-offset;
                }
                else
                {
                    bar.Top = Height - bar.Height;
                    float offset = (bar.Top / scrollBarNavSpace) * projectsNavSpace;
                    projects.Top = (int)-offset;
                }
            }
        }
        public void HideScrollbar()
        {
            if (bar.Visible is true && scrollbar.Width != 3) scrollbar.Width = 3;
        }
        public void ShowScrollbar()
        {
            if (scrollbar.Width != 6) scrollbar.Width = 6;
        }

        private void projects_MouseEnter(object sender, EventArgs e)
        {
            if (MainWindow.focusedPanel != null)
            {
                if (MainWindow.focusedPanel != this)
                {
                    MainWindow.focusedPanel.HideScrollbar();
                    MainWindow.focusedPanel = this;
                }
            }
            else
            {
                MainWindow.focusedPanel = this;
            }

            scrollbar.Width = 6;

        }

        public Button CreateItem(string text, object tag, bool grayOut = false)
        {
            #region create button
            Button button = new Button();
            button.Margin = new Padding(0);
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (grayOut is true) button.ForeColor = Color.DarkGray;
            button.Size = new System.Drawing.Size(163, 30);
            button.TabIndex = 0;
            button.Text = text;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button.UseVisualStyleBackColor = true;
            button.MouseMove += (o, e) => ShowScrollbar();
            button.MouseEnter += projects_MouseEnter;
            button.Tag = tag;
            projects.Controls.Add(button);
            #endregion 

            if (projects.Controls.Count > 9)
            {
                bar.Visible = true;
                enableScrolling = true;

                if (projects.Controls.Count > 40)
                    bar.Height = 100;
                else bar.Height = 280 - projects.Controls.Count * 6;

                scrollBarNavSpace = this.Height - bar.Height;
                projectsNavSpace = projects.Height - this.Height;
            }
            return button;
        }

        public PictureBox CreatePreview(string url)
        {
            PictureBox box = new PictureBox();
            box.Size = new System.Drawing.Size(80, 55);
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.MouseMove += (o, e) => ShowScrollbar();
            box.MouseEnter += projects_MouseEnter;
            projects.Controls.Add(box);
            LoadImageAsync(box, url);
            if (projects.Controls.Count > 4)
            {
                bar.Visible = true;
                enableScrolling = true;
                if (projects.Controls.Count > 4)
                    bar.Height = 220;
                else bar.Height = 220 - projects.Controls.Count * 6;

                scrollBarNavSpace = this.Height - bar.Height;
                projectsNavSpace = projects.Height - this.Height;
            }
            return box;
        }

        public void Clear()
        {
            projects.Controls.Clear();
            bar.Visible = false;
            enableScrolling = false;
        }

        private async Task LoadImageAsync(PictureBox box, string url) =>
            await Task.Run(() => box.Load(url));

        private void scrollbar_MouseEnter(object sender, EventArgs e)
        {
            scrollbar.Width = 6;
        }

        private void bar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                MouseDownLocation = e.Location;
            //projects.Top = -180;
        }

        private void bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                bar.Top = e.Y + bar.Top - MouseDownLocation.Y;
            else return;
            if (bar.Top < 0) { bar.Top = 0; return; }
            if (bar.Top + bar.Height > this.Height) { bar.Top = Height - bar.Height; return; }
            float offset = (bar.Top / scrollBarNavSpace) * projectsNavSpace;
            projects.Top = (int)-offset;
        }

    }
}
