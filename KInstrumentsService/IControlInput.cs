﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KInstrumentsService
{
    public enum ItemState {
        Enabled,
        Disabled,
        Damaged,
        Destroyed
    }

    public interface IControlInput
    {
        void DeployGear();
        void StowGear();
        void ToggleStage();
    }
}
