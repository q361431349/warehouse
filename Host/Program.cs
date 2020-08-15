using IWebSocketEcho;
using Microsoft.ServiceModel.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("ws://localhost:20001/EchoService2");
            using (var host = new WebSocketHost<EchoService2>(baseAddress))
            {
                host.AddWebSocketEndpoint();
                host.Open();
                Console.WriteLine(baseAddress.ToString() + " Opened ...");

                var task = new Task(() =>
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(10000);
                        try
                        {
                            Console.WriteLine("Service Instance Count:" + EchoService2.ActivityServices.Count);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                });
                task.Start();
                Console.Read();
            }
        }
    }
}
