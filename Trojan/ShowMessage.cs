using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JavaUpdateController
{
    class ShowMessage : ICommand
    {
        public ShowMessage() { }
        public void Execute(string[] work)
        {
            MessageBox.Show(work[2], work[1]);
        }
    }
}
