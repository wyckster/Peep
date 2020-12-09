using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peep
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
            MainForm form = new MainForm();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2) {
                // The first arg is the full path of this executable
                // The second arg is the file to process
                form.Filename = args[1];
            }
            Application.Run(form);
        }
    }
}
