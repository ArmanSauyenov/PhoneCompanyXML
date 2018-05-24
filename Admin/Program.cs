using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Lib;

namespace Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProvider sp = new ServiceProvider();
            sp.AddProvider();
            //sp.EditProvider();
        }
    }
}
