using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace kinstruments
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class KInstrumentsStartup : MonoBehaviour
    {
        static object lck = new object();
        static bool started = false;
        public void Start()
        {
            lock (lck)
            {
                if (!started)
                {
                    var ws = KinstrumentsWebserver.GetInstance();
                    ws.Service.InstrumentDataSource = new PollingInstrumentDataSource();
                    ws.Start();
                    started = true;
                }
            }
        }
    }
}
