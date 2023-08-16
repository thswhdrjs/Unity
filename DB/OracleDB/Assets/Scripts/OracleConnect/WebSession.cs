using System;
using System.Net;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace OracleConnect
{
    public class WebSession : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

            string data = WebSocketServerManager.instance.PacketProcessing(e.Data);

            if(!string.IsNullOrEmpty(data))
                Send(data);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine($"OnError : {ID}");
            Console.WriteLine(e.Message);

            base.OnError(e);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"OnClose : {ID}");
            Console.WriteLine(e.Reason);
            WebSocketServerManager.instance.Remove_WebSession(this);

            base.OnClose(e);
        }

        protected override void OnOpen()
        {
            Console.WriteLine($"Client Connect!! : {ID}");
            WebSocketServerManager.instance.Add_WebSession(this);

            base.OnOpen();
        }

        public void SendPacket(string _data)
        {
            Send(_data);
        }

        public void BroadCastingPacket(string _data)
        {
            Sessions.Broadcast(_data);
        }
    }
}
