using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using kinstruments;

namespace KinstrumentsTests
{
    [TestFixture]
    public class WebserverTests
    {

        [Test]
        public void NoOp()
        {
            var ki = new KinstrumentsWebserver(8882, true);
            ki.DataOnce(new InstrumentData() { SurfaceVelocity = 10.1 });
        }
    }
}
