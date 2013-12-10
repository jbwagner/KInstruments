using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace KinstrumentsTests
{
    [TestFixture]
    public class WebserverTests
    {

        [Test]
        public void NoOp()
        {
            var ki = new kinstruments.KinstrumentsWebserver(8882, true);
            ki.DataOnce(new Dictionary<string, object>
                        {
                            { "sb_velocity", 10.1 },
                        });
        }
    }
}
