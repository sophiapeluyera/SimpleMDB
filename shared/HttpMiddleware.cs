using System.Collections;
using System.Net;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public delegate Task HttpMiddleware(HttpListenerRequest req, HttpListenerResponse res, Hashtable options);  
}


