using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Lib.Models;

namespace Admin.Lib.Models
{
    public class Provider
    {
        public List<int> Prefix = new List<int>();
        public string LogoUrl { get; set; }
        public string NameCompany { get; set; }
        public double Percent { get; set; }
        public Provider()
        {

        }
    }
}
