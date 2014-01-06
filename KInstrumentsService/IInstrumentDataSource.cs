using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KInstrumentsService
{
    public interface IInstrumentDataSource
    {
        InstrumentData GetData();
    }
}
