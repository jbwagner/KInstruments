using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using JsonFx.Json;
using System.Reflection;
using System.Runtime.Serialization;

namespace JsonFxRPCServer
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JsonRPCMethodAttribute : Attribute
    {
    }

    public class ServerProxy
    {
        Dictionary<string, Func<object[], object>> methodHandlers = new Dictionary<string, Func<object[], object>>();

        public void SetMethodHandler(string name, Func<object[], object> m)
        {
            methodHandlers[name] = m;
        }

        public void AddHandlers<T>( T service )
        {
            var t = typeof(T);
            var ml = t.GetMethods();
            foreach (var m in ml)
            {
                var attrs = m.GetCustomAttributes(typeof(JsonRPCMethodAttribute), true);
                if (attrs != null)
                {
                    if (attrs.Length > 0)
                    {
                        SetMethodHandler(m.Name, 
                            (x) => { return m.Invoke(service, x); });
                    }
                }

            }
        }

        public void WriteResult(RPCResult res, TextWriter w)
        {
            var jw = new JsonWriter();
            jw.Write(res, w);
        }

        public RPCResult RunMethod(RPCMethod m)
        {
            var rv = new RPCResult();
            rv.id = m.Id;
            Func<object[], object> act;

            if (methodHandlers.TryGetValue(m.Name, out act))
            {
                try
                {
                    rv.result = act.Invoke(m.Args);
                }
                catch (Exception e)
                {
                    var err = new Dictionary<string, object> {
                        { "message" , "method threw exception" },
                        { "exception" , e.ToString() },
                    };
                    rv.error = err;
                }
            }
            else
            {
                var err = new Dictionary<string, object> {
                    { "message" , "no such method" },
                    { "method" , m.Name },
                };
                rv.error = err;
            }
            return rv;
        }

        public Dictionary<string, object> ReadJson(TextReader r)
        {
            var rdr = new JsonReader();
            var obj = rdr.Read<Dictionary<string, object>>(r);
            return obj;
        }

        static T CoaxTo<T>(object o)
        {
            T rv = default(T);
            if (o is T)
            {
                rv = (T)o;
            }
            return rv;
        }


        public RPCMethod ReadMethod(string m)
        {
            using (var tr = new StringReader(m))
            {
                return ReadMethod(tr);
            }
        }

        public RPCMethod ReadMethod(TextReader r)
        {
            var rv = new RPCMethod();
            var o = ReadJson(r);
            if (o != null)
            {
                if (o.ContainsKey("method"))
                {
                    rv.Name = CoaxTo<string>(o["method"]);
                }
                if (o.ContainsKey("id"))
                {
                    rv.Id = CoaxTo<int>(o["id"]); 
                }
                if (o.ContainsKey("params"))
                {
                    rv.Args = o["params"] as object[];
                }
            }
            return rv;
        }
    }

    public class RPCMethod
    {
        public string Name { get; set; }
        public object[] Args { get; set; }
        public int Id { get; set; }

        public RPCMethod ()
        {
            Args = new object[0];
        }

        public T ConvertArgs<T>()
        {
            var js = new JsonWriter();
            var str = js.Write(Args);
            var jr = new JsonReader();
            return jr.Read<T>(str);
        }
        
    }

    [DataContract]
    public class RPCResult
    {
        public int id { get; set; }

        public object result { get; set; }

        public Dictionary<string, object> error { get; set; }
    }
}
