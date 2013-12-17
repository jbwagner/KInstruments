using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kinstruments
{
    public class InstrumentData
    {
        public double Heading { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }

        public double Altitude { get; set; }
        public double SurfaceVelocity { get; set; }
        public double OrbitalVelocity { get; set; }

        public double Throttle { get; set; }

        public int CurrentStage { get; set; }
    }
}
