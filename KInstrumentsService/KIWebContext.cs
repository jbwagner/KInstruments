using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XR.Include;

namespace KInstrumentsService
{
    public class KIWebContext : BaseContext
    {
        public string PagePath { get; set; }

        public string IsActive(string arg)
        {
            if (arg == PagePath)
            {
                return "active";
            }
            return string.Empty;
        }


    }
}
