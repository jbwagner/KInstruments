﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KInstrumentsService
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

        public void UpdateFromVessel(Vessel vessel)
        {
            var vesselMiddle = vessel.findWorldCenterOfMass();
            var up = (vesselMiddle - vessel.mainBody.position).normalized;
            var north = Vector3d.Exclude(up, (vessel.mainBody.position + vessel.mainBody.transform.up * (float)vessel.mainBody.Radius) - vesselMiddle).normalized;

            var rotSurface = Quaternion.LookRotation(north, up);
            var rotVesselToSurface = Quaternion.Inverse(Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(vessel.GetTransform().rotation) * rotSurface);

            var heading = rotVesselToSurface.eulerAngles.y;
            var pitch = (rotVesselToSurface.eulerAngles.x > 180) ? (360.0 - rotVesselToSurface.eulerAngles.x) : -rotVesselToSurface.eulerAngles.x;
            var roll = (rotVesselToSurface.eulerAngles.z > 180) ? (rotVesselToSurface.eulerAngles.z - 360.0) : rotVesselToSurface.eulerAngles.z;

            Heading = heading;
            Pitch = pitch;
            Roll = roll;
            Altitude = vessel.altitude;
            CurrentStage = vessel.currentStage;
            SurfaceVelocity = vessel.rb_velocity.magnitude;

#if false
            print(string.Format("pitch {0:0.0} heading {1:0.0}, roll {2:0.0} ",
                pitch,
                heading,
                roll
                ));
#endif
        }
    }
}