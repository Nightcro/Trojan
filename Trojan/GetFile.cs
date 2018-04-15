using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JavaUpdateController
{
    class GetFile : ICommand
    {
        private NetworkStream stream;

        public GetFile(NetworkStream stream)
        {
            this.stream = stream;
        }

        public void Execute(string[] work)
        {
            int bytes_sent = -1;
            Byte[] bytes = new Byte[1024];

            try
            {
                FileStream fs = new FileStream(work[1], FileMode.Open, FileAccess.Read);
                Messages.SendMessage(stream, Path.GetFileName(work[1]));
                Messages.ReceiveMessage(stream);
                while (bytes_sent != 0)
                {
                    bytes_sent = fs.Read(bytes, 0, bytes.Length);
                    Messages.SendMessage(stream, bytes_sent.ToString());
                    Messages.ReceiveMessage(stream);
                    stream.Write(bytes, 0, bytes_sent);
                    Messages.ReceiveMessage(stream);
                }
            }
            catch (Exception)
            {
                Messages.SendMessage(stream, "!!");
            }
        }
    }
}
