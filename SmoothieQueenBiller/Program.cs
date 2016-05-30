using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoothieQueenBiller
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

            //declare splash
            SmoothieQueenBiller.SplashScreen splashForm = new SmoothieQueenBiller.SplashScreen();
           
            //show splash
            splashForm.ShowDialog();
            
            Application.Run(new billingForm());
        }
    }
}
