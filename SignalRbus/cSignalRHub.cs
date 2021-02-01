using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Hosting;

namespace SignalRbus
{
    internal class HubConnection : IDisposable
    {
        private string var;

        public HubConnection(string var)
        {
            this.var = var;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
    
    [HubName("SignalRHub")]
    public class SignalRHub : Hub
    {
        public void Start()
        {
            //string url = "http://192.168.1.8:8088/";
            //var server = new Server(url);

            //// Map the default hub url (/signalr)
            //server.MapHubs();

            //// Start the server
            //server.Start();

            //Console.WriteLine("Server running on {0}", url);

            //// Keep going until somebody hits 'x'
            //while (true)
            //{
            //    ConsoleKeyInfo ki = Console.ReadKey(true);
            //    if (ki.Key == ConsoleKey.X)
            //    {
            //        break;
            //    }
            //}
        }

        public override Task OnConnected()
        {
            var version = Context.QueryString["contosochatversion"];
            if (version != "1.0")
            {
                Clients.Caller.notifyWrongVersion();
            }
            return base.OnConnected();
        }

        public string Send(string message)
        {
            return message;
        }

        public void DoSomething(string param)
        {
            Clients.All.addMessage(param);
        }
    }
    

}
