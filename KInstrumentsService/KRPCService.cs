using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XR.Server.Json;
using Jayrock.JsonRpc;

namespace KInstrumentsService
{
    public class KRPCService : JsonRpcService, IKRPCService
    {
        public KService Service { get; set; }

        [JsonRpcMethod]
        public InstrumentData PollData()
        {
            return Service.GetData();
        }

        [JsonRpcMethod]
        public void Throttle(ThrottleCommand cmd)
        {
            throw new NotImplementedException();
        }

        [JsonRpcMethod]
        public void ToggleGear()
        {
            throw new NotImplementedException();
        }
    }
}
