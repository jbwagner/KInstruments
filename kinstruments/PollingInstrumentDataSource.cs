using KInstrumentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kinstruments
{
    public class PollingInstrumentDataSource : IInstrumentDataSource
    {
        public InstrumentData GetData()
        {
            var id = new InstrumentData();
            var current_vessel = FlightGlobals.ActiveVessel;
            id.UpdateFromVessel(current_vessel);
            return id;
        }
    }
}
