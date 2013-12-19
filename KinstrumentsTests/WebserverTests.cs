using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using kinstruments;
using KInstrumentsService;

namespace KinstrumentsTests
{
    [TestFixture]
    public class WebserverTests
    {

        [Test]
        public void NoOp()
        {
            var ks = new KinstrumentsWebserver(8889, true);
            ks.Service.WwwRootDirectory = "C:\\Users\\inb\\Dropbox\\git\\ksp\\KinstrumentsTests\\bin\\Debug\\wwwroot";
            System.Threading.Thread.Sleep(15000);
            ks.Stop();
        }
    }
}
