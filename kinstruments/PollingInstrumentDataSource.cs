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
            if (current_vessel != null)
            {
                id.UpdateFromVessel(current_vessel);
            }
            return id;
        }
    }
}
