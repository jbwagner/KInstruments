using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace kinstruments
{
    public class KinstrumentsWebserver
    {
        static object webLock = new object();
        public HttpListener WebServer { get; private set; }
        IAsyncResult pendingWebAccept = null;

        public KinstrumentsWebserver(int port) : this( port, false )
        {
        }

        public KinstrumentsWebserver(int port, bool localhost)
        {
            string host = localhost ? "localhost" : "*";
            lock (webLock)
            {
                if (WebServer == null)
                {
                    var h = new HttpListener();
                    h.Prefixes.Add("http://" + host + ":" + port + "/");
                    h.Start();
                    WebServer = h;
                }
            }
        }

        public void Once(PartModule part)
        {
            var data = new Dictionary<string, object>
                        {
                            { "rb_velocity", part.vessel.rb_velocity.magnitude },
                            { "obt_velocity", part.vessel.obt_velocity.magnitude },
                            { "x", part.vessel.upAxis.x },
                            { "y", part.vessel.upAxis.y },
                            { "z", part.vessel.upAxis.z },
                        };

            DataOnce(data);
        }

        public void DataOnce(IDictionary<string, object> data)
        {

            lock (webLock)
            {
                if (pendingWebAccept == null)
                {
                    pendingWebAccept = WebServer.BeginGetContext(null, this);
                }

                if (pendingWebAccept.AsyncWaitHandle.WaitOne(0))
                {
                    var ctx = WebServer.EndGetContext(pendingWebAccept);
                    try
                    {
                        ProcessHttpRequest(ctx, data);
                    }
                    finally
                    {
                        pendingWebAccept = null;
                    }
                }
            }
        }

        public void ProcessHttpRequest(HttpListenerContext ctx, IDictionary<string,object> data)
        {
            ctx.Response.StatusCode = (int)HttpStatusCode.OK;
            ctx.Response.ContentType = "text/plain";
            using (var tw = new System.IO.StreamWriter(ctx.Response.OutputStream))
            {
                tw.WriteLine(data["rb_velocity"]);
            }
        }
    }
}
