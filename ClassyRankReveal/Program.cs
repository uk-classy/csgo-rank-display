using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassyRankReveal
{
    static class Program
    {
        public static Process CsgoProcess = null;
        public static Memory Mem = null;

        public static void MainInit()
        {
            Process[] CsgoList = Process.GetProcessesByName("csgo");
            if (CsgoList.Length == 0)
            {
                Utility.DisplayErrorMessage("No CSGO Could be detected, Please Ensure that CS:GO Is running before opening.", "Error - No CSGO");
                Environment.Exit(-1);
            }
            else
            {
                Mem = new Memory(CsgoList[0]);
                MemOffsets.ClientDLLAddress = Mem.GetModuleAddress("client.dll");
                MemOffsets.EngineDLLAddress = Mem.GetModuleAddress("engine.dll");
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainInit();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}