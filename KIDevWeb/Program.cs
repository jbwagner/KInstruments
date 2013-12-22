using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kinstruments;

namespace KIDevWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new KinstrumentsWebserver(8881, true);
            s.Service.WwwRootDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "www");
            s.Start();

            Console.ReadLine();
        }
    }
}
