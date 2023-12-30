using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeSalesSystem.Helpers;
using ShoeSalesSystem.Forms;


namespace ShoeSalesSystem
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //Application.Run(new Form1());
            //SqlHelper.Test();
        }
    }
}
