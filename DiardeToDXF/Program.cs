using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiardeToDXF
{
    static class Program
    {
        public static bool LoggedIn = false;
        public static MainWindow mainWindow;
        public static LoginScreen loginScreen;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            loginScreen = new LoginScreen();

            Application.Run(loginScreen);
            // else mainWindow.Show();
        }

    }
}
