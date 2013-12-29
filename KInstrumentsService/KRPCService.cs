using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonFxRPCServer;

namespace KInstrumentsService
{
    public class KRPCService : IKRPCService
    {
        public KService Service { get; set; }

        public InstrumentData PollData()
        {
            return Service.GetData();
        }

        public void Throttle(ThrottleCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void ToggleGear()
        {
            throw new NotImplementedException();
        }
    }
}
