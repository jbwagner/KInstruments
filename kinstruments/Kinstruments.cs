using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using UnityEngine;
using KI = kinstruments;

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
        httpd.Once(this);
        print(string.Format("upaxis {0},{1},{2}", this.vessel.upAxis.x, this.vessel.upAxis.y, this.vessel.upAxis.z));        
    }

}

