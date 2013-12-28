using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XR.Server.Http;
using XR.Server.Json;
using XR.Include;
using System.IO;

namespace KInstrumentsService
{
 
    public class KService
    {        
        public static bool IsMono
        {
            get
            {
                return Type.GetType("Mono.Runtime") != null;
            }
        }

        InstrumentData idata;
        JsonServer<KRPCService> jserv;

        public string WwwRootDirectory { get; set; }
        public ILogger Log { get; set; }
        HttpServer WebServer { get; set; }

        public KService( int port )
        {
            Log = new UnityLogger();
            WebServer = new HttpServer();
            jserv = new JsonServer<KRPCService>();
            if (!IsMono)
            {
                WebServer.Localhostonly = true;
            }
            WebServer.Port = port;
            jserv.PathMatch = new System.Text.RegularExpressions.Regex("/json/");
            jserv.Service.Service = this;
            WebServer.UriRequested += WebServer_UriRequested;
            WebServer.UriRequested += WebServer_Json;
            WebServer.UriRequested += WebServer_FileServer;
            WebServer.UriRequested += WebServer_FileIndex;
            WebServer.UriRequested += WebServer_FileNotFound;
        }

        void WebServer_UriRequested(object sender, UriRequestEventArgs args)
        {
            Log.Print("requested {0}", args.Request.Url);
        }

        public void Stop()
        {
            WebServer.StopServer();
        }

        void WebServer_Json(object sender, UriRequestEventArgs args)
        {
            if (!args.Handled)
            {
                try
                {
                    Log.Print("json handler");
                    jserv.HandleJsonRequest(sender, args);
                }
                catch (Exception e)
                {
                    Log.Print("exception!: {0}", e);
                    throw;
                }
            }
        }

        void WebServer_FileNotFound(object sender, UriRequestEventArgs args)
        {
            if (!args.Handled)
            {
                args.SetResponseState(404);
                args.SetResponseType("text/plain");
                args.Handled = true;
                args.ResponsStream.WriteLine("not found : {0}", args.Request.Url);
            }
        }

        Dictionary<string, string> ctypes = new Dictionary<string, string>()
        {
            { ".txt", "text/plain" },
            { ".html", "text/html" },
            { ".htm", "text/html" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".jpeg", "image/jpeg" },
            { ".css", "text/css" },
            { ".js", "text/javascript" },
        };

        public string GetContentType(string filename)
        {
            var ext = System.IO.Path.GetExtension(filename).ToLower();
            var rv = "application/x-octet-strem";
            ctypes.TryGetValue(ext, out rv);
            return rv;
        }

        Processor GetProcessor()
        {
            var proc = new Processor();
            if (WwwRootDirectory == null)
            {
                proc.RootDirectory = this.GetType().Assembly.Location;
            }
            else
            {
                proc.RootDirectory = WwwRootDirectory;
            }
            return proc;
        }

        void WebServer_FileServer(object sender, UriRequestEventArgs args)
        {
            if (!args.Handled)
            {
                var path = args.Request.Url.AbsolutePath;
                var proc = GetProcessor();
                var localpath = proc.VirtualToLocalPath(path);
                if (Directory.Exists(localpath))
                {
                    var indexpage = Path.Combine(localpath,"index.html");
                    if (File.Exists(indexpage))
                        localpath = indexpage;
                    path = string.Join("/", new string[] { path, "index.html" });
                }

                if (File.Exists(localpath))
                {
                    args.SetResponseState(200);                    
                    args.SetResponseType(GetContentType(localpath));
                    args.Handled = true;

                    if ( path.StartsWith("/static/") )
                    {
                        using ( var fh = File.OpenRead(localpath) )
                        {
                            var buf = new byte[8192];
                            int count = 0;
                            do {
                                count = fh.Read( buf, 0, buf.Length );
                                if ( count > 0 ) args.ResponsStream.BaseStream.Write( buf, 0, count );
                                if (count < buf.Length) break;
                            } while ( true );
                        }
                    } else {
                        proc.Transform(path, args.ResponsStream);
                    }
                }

            }
        }

        void WebServer_FileIndex(object sender, UriRequestEventArgs args)
        {
            if (!args.Handled)
            {
                var path = args.Request.Url.AbsolutePath;
                if (path == "/" || path.StartsWith("/static/"))
                {
                    var proc = GetProcessor();
                    args.SetResponseState(200);
                    args.SetResponseType("text/html");

                    var dirs = System.IO.Directory.GetDirectories(proc.VirtualToLocalPath(args.Request.Url.AbsolutePath));
                    var files = System.IO.Directory.GetFiles(proc.VirtualToLocalPath(args.Request.Url.AbsolutePath));
                    args.ResponsStream.WriteLine("<html><head><title>Index of {0}</title></head>", args.Request.Url.AbsolutePath);
                    args.ResponsStream.WriteLine("<body><h1>Index Of {0}</h1><hr><ul>", args.Request.Url.AbsolutePath);

                    foreach (var d in dirs)
                    {
                        args.ResponsStream.WriteLine("<li>[DIR] <a href='{0}'>{0}</a></li>", d);
                    }

                    foreach (var f in files)
                    {
                        var fn = System.IO.Path.GetFileName(f);
                        args.ResponsStream.WriteLine("<li><a href='{0}'>{0}</a></li>", fn);
                    }

                    args.ResponsStream.WriteLine("</ul></body></html>");
                    args.Handled = true;
                }
            }
        }

        public void Start()
        {
            WebServer.BeginListen();
        }

        public void OnUpdate(Vessel v)
        {
            lock (WebServer)
            {
                if (idata != null)
                    idata.UpdateFromVessel(v);
            }
        }

        public virtual InstrumentData GetData()
        {
            lock (WebServer)
            {
                if (idata == null) idata = new InstrumentData();
                return idata;
            }
        }

        public InstrumentCommand GetNextCommand()
        {
            throw new NotImplementedException();
        }
    }
}
