using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace SimpleMDB
{
    public class AuthController
    {
        public AuthController()
        {
        }

        public async Task LandingPageAGet(HttpListenerRequest req, HttpListenerResponse res, Hashtable options)
        {
            string html = "Hello World!";
            byte[] content = Encoding.UTF8.GetBytes(html);
            res.StatusCode = (int)HttpStatusCode.OK;
            res.ContentEncoding = Encoding.UTF8;
            res.ContentType = "text/plain";
            res.ContentLength64 = content.LongLength;
            await res.OutputStream.WriteAsync(content, 0, content.Length);
            res.Close();
        }
    }
}
