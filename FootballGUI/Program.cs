using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Football.Data;
using Football.Engine;

namespace FootballGUI
{
    static class Program
    {
        
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigReader.Init("..//..//..//Football//bin//Debug//Data//config.cfg.txt");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
