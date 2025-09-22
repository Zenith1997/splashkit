using System;
using SplashKitSDK;

namespace ChatProgram
{
    public class ChatPeer
    {
        private ServerSocket _server;

        public ChatPeer(ushort port)
        {
            _server = new ServerSocket("ChatServer", port);

        }
      public void ConnectToPeer(string address, ushort port)
        {
            Connection newConnection = new Connection($"{address}:{port}", address, port);
            if ( newConnection.IsOpen )
            {
                Console.WriteLine($"Connected to {address}:{port}");
            }
        }



        public void Broadcast(string message)
        {
            SplashKit.BroadcastMessage(message);
        }

        public void PrintNewMessages()
        {
            SplashKit.CheckNetworkActivity();
            while (SplashKit.HasMessages())
            {
                using (Message m = SplashKit.ReadMessage())
                {
                    Console.WriteLine($"Got a message from {m.Host}:{m.Port}");
                    Console.WriteLine($"Message: {m.Data}");
                }
            }

        }

        public void Close()
        {
            _server.Close();
            SplashKit.CloseAllConnections();
        }
    }
}