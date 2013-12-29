using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using kinstruments;
using KInstrumentsService;

using JsonFx.Json;
using JsonFxRPCServer;

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

    [TestFixture]
    public class JsonFxProxyTests
    {
        [Test]
        public void Encode()
        {
            var id = new InstrumentData() { Pitch = 31, Roll = 89 };

            var x = new JsonWriter();
            var js = x.Write(id);

            var y = new JsonReader();
            var decoded = y.Read<InstrumentData>(js);

            Assert.AreEqual(id.Roll, decoded.Roll);
            Assert.AreEqual(id.Pitch, decoded.Pitch);

        }

        [JsonRPCMethod]
        public string TestHello(string inputname)
        {
            return "Hello " + inputname;
        }

        [Test]
        public void MissingMethod()
        {
            string str = "{\"method\": \"notamethod\",      \"params\": [\"fred\"],              \"id\": null}";

            var p = new ServerProxy();

            p.AddHandlers(this);

            var m = p.ReadMethod(str);

            var res = p.RunMethod(m);
            Assert.IsNotNull(res.error);
            Console.WriteLine(res.error);

        }

        [Test]
        public void GoodMethod()
        {
            string str = "{\"method\": \"TestHello\",      \"params\": [\"fred\"],              \"id\": null}";
            
            var p = new ServerProxy();

            p.AddHandlers(this);

            var m = p.ReadMethod(str);

            var res = p.RunMethod(m);
            Assert.IsNull(res.error);
            Assert.IsTrue(res.result.Equals(TestHello("fred")));

        }
    }
}
