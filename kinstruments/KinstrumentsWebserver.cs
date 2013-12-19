using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using KInstrumentsService;

namespace kinstruments
{
    public class KinstrumentsWebserver
    {
        public KinstrumentsWebserver(int port) : this( port, false )
        {
        }

        public KService Service { get; private set; }
        public KinstrumentsWebserver(int port, bool localhost)
        {
            var tmp = new KService(port);
            tmp.Start();
            Service = tmp;
        }

        public void OnUpdate(Vessel v)
        {

        }

        public InstrumentCommand PopCommand()
        {
            return Service.GetNextCommand();
        }

        public void Stop()
        {
            Service.Stop();
        }
    }
}
