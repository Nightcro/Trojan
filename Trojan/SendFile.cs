using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JavaUpdateController
{
    class SendFile : ICommand
    {
        private NetworkStream stream;

        public SendFile(NetworkStream stream)
        {
            this.stream = stream;
        }

        public void Execute(string[] work)
        {
            int bytes_sent = -1;
            Byte[] bytes = new Byte[1024];
            try
            {
                using (var fs = new FileStream(work[1], FileMode.Create, FileAccess.Write))
                {
                    Messages.SendMessage(stream, "Y");

                    while (bytes_sent != 0)
                    {
                        bytes_sent = Int32.Parse(Messages.ReceiveMessage(stream));
                        Messages.SendMessage(stream, "ACK");

                        if (bytes_sent > 0)
                            Console.WriteLine(stream.Read(bytes, 0, bytes_sent));
                        Messages.SendMessage(stream, "ACK");
                        fs.Write(bytes, 0, bytes_sent);
                    }
                    fs.Close();

                }
            }
            catch (Exception)
            {
                Messages.SendMessage(stream, "N");
            }
        }
    }
}
