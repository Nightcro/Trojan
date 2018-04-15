using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JavaUpdateController;

namespace JavaUpdateController
{
    class Messages
    {
        public static String ReceiveMessage(NetworkStream stream)
        {
            Byte[] dir = new byte[1024];
            int size = stream.Read(dir, 0, dir.Length);

            Byte[] aux = new byte[size];
            Array.Copy(dir, aux, size);
            String msg = Encoding.ASCII.GetString(aux);

            return msg;
        }

        internal static void SendMessage(object stream, string v)
        {
            throw new NotImplementedException();
        }

        public static void SendMessage(NetworkStream stream, String msg)
        {
            Byte[] bytes = Encoding.ASCII.GetBytes(msg);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
