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
            s.Service.Log = new KInstrumentsService.BCLLogger();
            s.Service.WwwRootDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "www");
            s.Start();


            string k = string.Empty;
            do
            {
                var d = s.Service.GetData();
                k = Console.ReadKey(true).KeyChar.ToString();
                if (k == "q")
                {
                    d.Roll -= 2;
                }
                if (k == "e")
                {
                    d.Roll += 2;
                }

                if (k == "w")
                {
                    d.Pitch -= 4;
                }

                if (k == "s")
                {
                    d.Pitch += 4;
                }

                Console.Error.WriteLine("p={0} r={1}", d.Pitch, d.Roll);

            } while (true);
        }
    }
}
