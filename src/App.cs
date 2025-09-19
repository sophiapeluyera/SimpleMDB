using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace SimpleMDB
{
    public class App
    {
        private HttpListener server;
        private HttpRouter router;

        public App()
        {
            string host = "http://127.0.0.1:8080/";
            server = new HttpListener();
            server.Prefixes.Add(host);
            Console.WriteLine("Server listening on... " + host);

            var authController = new AuthController();
            router = new HttpRouter();

            router.AddGet("/", authController.LandingPageAGet);
        }

        public async Task Start()
        {
            server.Start();
            while (server.IsListening)
            {
                var ctx = await server.GetContextAsync();
                await HandleContextAsync(ctx);
            }
        }

        public void Stop()
        {
            server.Stop();
            server.Close();
        }

        private async Task HandleContextAsync(HttpListenerContext ctx)
        {
            var req = ctx.Request;
            var res = ctx.Response;
            var options = new Hashtable();

            await router.Handle(req, res, options);
        }
    }
}
