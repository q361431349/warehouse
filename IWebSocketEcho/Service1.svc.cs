using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace IWebSocketEcho
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class EchoService : IWebSocketEcho
    {
        IWebSocketEchoCallback callback = null;
       
        public EchoService()
        {
            callback = OperationContext.Current.GetCallbackChannel<IWebSocketEchoCallback>();
        }
        public void Receive(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            WebSocketMessageProperty property =
                    (WebSocketMessageProperty)message.Properties["WebSocketMessageProperty"];
            WebSocketContext context = property.WebSocketContext;

            string content = string.Empty;

            if (!message.IsEmpty)
            {
                byte[] body = message.GetBody<byte[]>();
                content = Encoding.UTF8.GetString(body);
            }


            string str;
            if (string.IsNullOrEmpty(content)) 
            {
               
                str = "连接成功";
              
                
            }
            else 
            {
                str = "发送消息: " + content;
               
            }
            callback.Send(CreateMessage(str));
        }

        private Message CreateMessage(string content)
        {
            Message message = ByteStreamMessage.CreateMessage(
                    new ArraySegment<byte>(
                        Encoding.UTF8.GetBytes(content)));
            message.Properties["WebSocketMessageProperty"] =
                    new WebSocketMessageProperty
                    { MessageType = WebSocketMessageType.Text };

            return message;
        }
    }
}
