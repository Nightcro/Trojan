using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Troian
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient tcp = new TcpClient();

                tcp.Connect(System.Net.IPAddress.Parse("192.168.21.89"), 30021);
                NetworkStream ns = tcp.GetStream();
                String str = "salut mihai si alex :P";
                byte[] mesaj = Encoding.ASCII.GetBytes(str);
                ns.Write(mesaj, 0, mesaj.Length);
                Console.WriteLine(str);
                while (true)
                {
                    ;
                }
            }
            catch (Exception)
            {
                Console.Write("eroare");
            }
        }
    }
}
