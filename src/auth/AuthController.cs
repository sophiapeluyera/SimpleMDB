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
            string html = HtmlTemplates.Base("SimpleMDB", "Landing Page", "Hello World!2");
            await HttpUtils.Respond(req, res, options, (int) HttpStatusCode.OK, html);
        }
    }
}
