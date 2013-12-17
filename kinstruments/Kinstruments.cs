using kinstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KI = kinstruments;

namespace kinstruments
{
    public class Kinstruments : PartModule
    {
        static KI.KinstrumentsWebserver httpd;

        public override void OnStart(PartModule.StartState state)
        {
            httpd = new KI.KinstrumentsWebserver(8881);
            print("hello");
            base.OnStart(state);
        }

        public override void OnActive()
        {
            base.OnActive();
            print("active..");
        }

        public override void OnInactive()
        {
            base.OnInactive();
        }

        public override void OnUpdate()
        {

            base.OnUpdate();

            var vesselMiddle = vessel.findWorldCenterOfMass();
            var up = (vesselMiddle - vessel.mainBody.position).normalized;
            var north = Vector3d.Exclude(up, (vessel.mainBody.position + vessel.mainBody.transform.up * (float)vessel.mainBody.Radius) - vesselMiddle).normalized;

            var rotSurface = Quaternion.LookRotation(north, up);
            var rotVesselToSurface = Quaternion.Inverse(Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(vessel.GetTransform().rotation) * rotSurface);

            var heading = rotVesselToSurface.eulerAngles.y;
            var pitch = (rotVesselToSurface.eulerAngles.x > 180) ? (360.0 - rotVesselToSurface.eulerAngles.x) : -rotVesselToSurface.eulerAngles.x;
            var roll = (rotVesselToSurface.eulerAngles.z > 180) ? (rotVesselToSurface.eulerAngles.z - 360.0) : rotVesselToSurface.eulerAngles.z;

            var data = new InstrumentData()
            {
                Heading = heading,
                Pitch = pitch,
                Roll = roll,
                Altitude = this.vessel.altitude,
                CurrentStage = this.vessel.currentStage,
                SurfaceVelocity = this.vessel.rb_velocity.magnitude,
                
                
            };

            print(string.Format("pitch {0:0.0} heading {1:0.0}, roll {2:0.0} ",
                pitch,
                heading,
                roll
                ));

            httpd.DataOnce(data);
        }
    }
}

