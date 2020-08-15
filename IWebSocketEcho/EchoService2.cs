using Microsoft.ServiceModel.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace IWebSocketEcho
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EchoService2 : WebSocketService
    {
        public static List<EchoService2> ActivityServices { get; } = new List<EchoService2>();

        public override void OnMessage(string value)
        {
            ActivityServices.ToList().ForEach(s =>
            {
                if (s.GetHashCode() != GetHashCode())
                {
                    s.Send(GetHashCode().ToString() + "说:" + value);
                }
            });

        }

        public override void OnOpen()
        {
            base.OnOpen();
            Send("Welcome");
            Console.WriteLine("Client Opened");
            ActivityServices.Add(this);
        }

        protected override void OnError()
        {
            base.OnError();
            Console.WriteLine("Client Error");
        }

        protected override void OnClose()
        {
            base.OnClose();
            Console.WriteLine("Client Closed");
            ActivityServices.Remove(this);
        }
    }
}