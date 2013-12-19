using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KInstrumentsService
{
    public abstract class InstrumentCommand
    {
    }
       

    public class AdjustableCommand : InstrumentCommand
    {
        public double Adjust { get; set; }
        public double Set { get; set; }
    }

    public class ThrottleCommand : AdjustableCommand
    {

    }

    public class StageCommand : InstrumentCommand
    {

    }

    public class ToggleCommand : InstrumentCommand
    {

    }

    public class ToggleGearCommand : ToggleCommand
    {

    }

    public class ToggleLightsCommand : ToggleCommand
    {

    }
}
