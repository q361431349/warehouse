using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace IWebSocketEcho
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IWebSocketEchoCallback
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void Send(Message message);
    }

    [ServiceContract(CallbackContract = typeof(IWebSocketEchoCallback))]
    public interface IWebSocketEcho
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void Receive(Message message);
    }
}
