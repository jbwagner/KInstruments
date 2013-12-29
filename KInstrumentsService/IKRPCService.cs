using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JsonFxRPCServer;

namespace KInstrumentsService
{
    public interface IKRPCService 
    {
        [JsonRPCMethod]
        InstrumentData PollData();

        [JsonRPCMethod]
        void Throttle(ThrottleCommand cmd);

        [JsonRPCMethod]
        void ToggleGear();
    }
}
