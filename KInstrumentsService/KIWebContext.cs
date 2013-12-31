using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XR.Include;

namespace KInstrumentsService
{
    public class KIWebContext : BaseContext
    {
        public KIWebContext(KService svc)
        {
            Service = svc;
        }

        public KService Service { get; private set; }

        public string VesselName
        {
            get
            {
                return Service.Service.PollData().VesselName;
            }
        }

        public string PagePath { get; set; }

        public string IsActive(string arg)
        {
            if (arg == PagePath)
            {
                return "active";
            }
            return string.Empty;
        }

        public string ActiveMenuLink(string arg)
        {
            var args = arg.Split(new char[] { ',' }, 2);

            var path = args[0];
            if (args.Length > 1)
            {
                var txt = args[1];

                return string.Format("<li class='{1}'><a href='{0}'>{2}</a></li>",
                    path, IsActive(path), txt);
            }
            else
            {
                throw new ArgumentException(
                    String.Format("invalid parameter '{0}'", arg));
            }
        }

    }
}
