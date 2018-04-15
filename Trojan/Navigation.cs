using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace JavaUpdateController
{
    class Navigation : ICommand
    {
        private NetworkStream stream;

        public Navigation(NetworkStream stream)
        {
            this.stream = stream;
        }

        public void Execute(string[] work)
        {
            try
            {
                String[] files = Directory.GetFiles(work[1]);
                String[] dirs = Directory.GetDirectories(work[1]);
                Messages.SendMessage(stream, "Y");

                int fds = files.Length + dirs.Length;

                Messages.SendMessage(stream, fds.ToString());
                Messages.ReceiveMessage(stream);

                for (int i = 0; i < dirs.Length; ++i)
                {
                    Messages.SendMessage(stream, dirs[i] + '\\');
                    Messages.ReceiveMessage(stream);
                }

                for (int i = 0; i < files.Length; ++i)
                {
                    Messages.SendMessage(stream, files[i]);
                    Messages.ReceiveMessage(stream);
                }
            }
            catch (Exception)
            {
                Messages.SendMessage(stream, "N");
            }
        }
    }
}
