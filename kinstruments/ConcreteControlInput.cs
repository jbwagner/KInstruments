using KInstrumentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace kinstruments
{
    internal class ConcreteControlInput : IControlInput
    {

        Vessel CurrentVessel
        {
            get
            {
                return FlightGlobals.ActiveVessel;
            }
        }

        public void DeployGear()
        {
            CurrentVessel.ActionGroups.SetGroup(KSPActionGroup.Gear, true);
        }

        public void StowGear()
        {
            CurrentVessel.ActionGroups.SetGroup(KSPActionGroup.Gear, false);
        }

        public void ToggleStage()
        {
            Staging.ActivateNextStage();
        }
    }
}
