using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XR.Server.Json;

namespace KInstrumentsService
{
    public interface IKRPCService : IJsonRpcServiceContract
    {
        InstrumentData PollData();

        void Throttle(ThrottleCommand cmd);

        void ToggleGear();
    }
}
