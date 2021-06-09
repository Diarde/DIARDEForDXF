using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiardeToDXF
{
        public partial class LoginScreen : Form
        {
            private Point MouseDownLocation;
            public LoginScreen()
            {
                InitializeComponent();
                if (File.Exists("user.json"))
                {
                    loggingINPanel.Visible = true;
                    AutoLoginAsync();
                }
                this.Deactivate += (o, e) => this.Hide();
            }

            private void btnLogin_Click(object sender, EventArgs e)
            {
                btnLogin.BackColor = Color.LightGray;
                btnLogin.FlatAppearance.MouseOverBackColor = Color.LightGray; //229, 62, 0
                btnLogin.Cursor = Cursors.WaitCursor;
                txtEmail.Enabled = txtPassword.Enabled = rememberMe.Enabled = false;
                txtError.Text = ""; txtError.Visible = false;
                LoginAsync();
            }

            private async Task LoginAsync()
            {
                bool loginSuccessful = await Task.Run(() => API.Login(txtEmail.Text, txtPassword.Text));
                if (loginSuccessful is true)
                {
                    // save credentials
                    string key = "74e7dd00794df4d6817c418f80d839da";
                    if (rememberMe.Checked is true)
                    {
                        string[] credentials = { txtEmail.Text, Encryption.EncryptString(key, txtPassword.Text) };
                        File.WriteAllText("user.json", JsonConvert.SerializeObject(credentials));
                    }
                    else
                    {
                        if (File.Exists("user.json")) try { File.Delete("user.json"); } catch { }

                    }
                    Program.LoggedIn = true;
                    Program.mainWindow = new MainWindow(txtEmail.Text);
                    Program.mainWindow.Show();
                    btnLogin.BackColor = Color.FromArgb(255, 69, 0);
                    btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 62, 0);
                    btnLogin.Cursor = Cursors.Arrow;
                    txtEmail.Focus();
                    loggingINPanel.Visible = false;
                    this.Hide();
                    txtEmail.Text = txtPassword.Text = "";
                    rememberMe.Checked = false;
                }
                else
                {
                    btnLogin.BackColor = Color.FromArgb(255, 69, 0);
                    btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 62, 0);
                    btnLogin.Cursor = Cursors.Arrow;
                    txtEmail.Enabled = txtPassword.Enabled = true;
                    txtError.Text = API.AuthErrorMessage;
                    txtError.Visible = true;
                    txtEmail.Focus();
                }
            }

            private async Task AutoLoginAsync()
            {
                try
                {
                    string key = "74e7dd00794df4d6817c418f80d839da";
                    string[] credentials = JsonConvert.DeserializeObject<string[]>(File.ReadAllText("user.json"));
                    credentials[1] = Encryption.DecryptString(key, credentials[1]);
                    txtAutoLoggingIn.Text = $"Logging in as \n{credentials[0]}...";
                    bool loginSuccessful = await Task.Run(() => API.Login(credentials[0], credentials[1]));
                    if (loginSuccessful is true)
                    {
                        Program.LoggedIn = true;
                        Program.mainWindow = new MainWindow(credentials[0]);
                        Program.mainWindow.Show();
                        btnLogin.BackColor = Color.FromArgb(255, 69, 0);
                        btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 62, 0);
                        btnLogin.Cursor = Cursors.Arrow;
                        txtEmail.Enabled = txtPassword.Enabled = true;
                        txtError.Visible = false;
                        txtEmail.Focus();
                        loggingINPanel.Visible = false;
                        this.Hide();
                        txtEmail.Text = txtPassword.Text = "";
                        rememberMe.Checked = false;
                        txtEmail.Focus();
                    }
                    else
                    {
                        txtError.Text = API.AuthErrorMessage;
                        txtError.Visible = true;
                        txtEmail.Text = credentials[0];
                        loggingINPanel.Visible = false;
                    }
                }
                catch
                {
                    txtError.Text = "Something went wrong";
                    txtError.Visible = true;
                    loggingINPanel.Visible = false;
                }

            }
            private void LoginScreen_Load(object sender, EventArgs e)
            {

            }
            private void titlebar_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    MouseDownLocation = e.Location;
            }

            private void titlebar_MouseMove(object sender, MouseEventArgs e)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Left = e.X + Left - MouseDownLocation.X;
                    Top = e.Y + Top - MouseDownLocation.Y;
                }
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                Hide();
                txtEmail.Text = txtPassword.Text = "";
                rememberMe.Checked = false;
                txtEmail.Focus();
            }
        }
}
