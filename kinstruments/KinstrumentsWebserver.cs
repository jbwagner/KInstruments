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

        bool started = false;

        public KService Service { get; private set; }
        public KinstrumentsWebserver(int port, bool localhost)
        {
            Service = new KService(port);
        }

        public void Start()
        {
            Service.Start();
            started = true;
        }

        public void OnUpdate(Vessel v)
        {
            if (!started) Start();
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
