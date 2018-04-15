using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaUpdateController
{
    interface ICommand
    {
        void Execute(String[] work);
    }
}
