using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace information_system
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "information.csv";
            Menu menu = new Menu(path);
            menu.MyMenu();
        }
    }
}
