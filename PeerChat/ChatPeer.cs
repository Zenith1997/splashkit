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
            if (newConnecton.IsOpen)
            {
                Console.WriteLine($"Connection to {address}:{port} successful");
                newConnection.SendMessage("Hello from " + _server.Port);
            }
            else
            {
                Console.WriteLine($"Connection to {address}:{port} failed");
            }
        }

        public void BroadcastMessage(string message)
        {
            Splashkit.BroadcastMessage(message);
        }
        public void PrintNewMessages()
        {
            Splashkit.CheckNetworkActivity();
            while (Splashkit.HasNewMessage())
            {
                using (Message m = SplashKit.ReadMessage())
                {
                    Console.WriteLine($"Message from {m.Host}: {m.Port}");
                    Console.ReadLine(m.Data);
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