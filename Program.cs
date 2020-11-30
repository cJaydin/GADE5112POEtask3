using System;
using System.Windows.Forms;

namespace GADE5112POE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        public static bool ShowCoordinates { get; set; }
        public static frmMain MainForm { get; set; }
        public static GameEngine Game { get; set; }
    }
}
