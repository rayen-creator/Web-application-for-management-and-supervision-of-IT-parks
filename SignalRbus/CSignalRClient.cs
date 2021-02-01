using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRbus
{
    class CSignalRClient
    {
        public void OnConnected()
        {
            //using (var hubConnection = new HubConnection("http://www.contoso.com/"))
            //{
            //    IHubProxy stockTickerHubProxy = hubConnection.CreateHubProxy("StockTickerHub");
            //    stockTickerHubProxy.On<Stock>("UpdateStockPrice", stock => Console.WriteLine("Stock update for {0} new price {1}", stock.Symbol, stock.Price));
            //    await hubConnection.Start(new LongPollingTransport());
            //}
        }
    }
}
