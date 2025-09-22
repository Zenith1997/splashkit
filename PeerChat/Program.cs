using System;
using SplashKitSDK;


namespace ChatProgram
{



    public class Program
    {

        private static void PrintMenu()
        {
            Console.WriteLine("1: Broadcast message");
            Console.WriteLine("2: Connect to server");
            Console.WriteLine("3: Check Messages");
            Console.WriteLine("4: Quit");
        }
 private static void BroadcastMessage(ChatPeer peer)
        {
            Console.Write("What message do you want to send: ");
            string message = Console.ReadLine();
            peer.Broadcast(message);
        }


        private static void MakeNewConnection(ChatPeer peer)
        {
            string address;
            ushort port;

            Console.Write("Enter address: ");
            address = Console.ReadLine();

            Console.Write("Enter port: ");
            port = Convert.ToUInt16(Console.ReadLine());

            peer.ConnectToPeer(address, port);
        }
         public static void Main( string [] args )
        {
            Console.WriteLine("Welcome to Peer Chat!");
            Console.WriteLine("Which port do you want to use?");
            ushort port = Convert.ToUInt16(Console.ReadLine());
            ChatPeer peer = new ChatPeer(port);

            int opt;
            do
            {
                PrintMenu();
                opt = Convert.ToInt32(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        BroadcastMessage(peer);
                        break;
                    case 2:
                        MakeNewConnection(peer);
                        break;
                    case 3:
                        peer.PrintNewMessages();
                        break;
                    case 4:
                        peer.Close();
                        break;
                    default:
                        Console.WriteLine("Enter a value between 1 and 4");
                        break;
                }
            } while (opt != 4);


        }


    }
}
