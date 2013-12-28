using kinstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KInstrumentsService;

namespace kinstruments
{
    public class Kinstruments : PartModule
    {
        static KinstrumentsWebserver httpd;

        public override void OnStart(PartModule.StartState state)
        {
            httpd = KinstrumentsWebserver.GetInstance();
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
            if (vessel.isActiveVessel)
            {
                httpd.OnUpdate(vessel);
            }
        }
    }
}

