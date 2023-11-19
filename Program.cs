using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace _2023_Marchuk_A_I_Lab_5_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Net Client is starting ....\nWait please ..........");
            try
            {
                Console.Write("Server listen IP: ");
                string serverIP = Console.ReadLine();
                IPAddress ipAddr = IPAddress.Parse(serverIP);

                Console.Write("Server listen Port: ");
                string serverPort = Console.ReadLine();
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, int.Parse(serverPort));

                SendMessage(ipAddr, ipEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.Write("Client is stopped \nPress any key to continue . . . ");
                Console.ReadKey(true);
            }
        }

        static void SendMessage(IPAddress ipAddr, IPEndPoint ipEndPoint)
        {
            using (Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                sender.Connect(ipEndPoint);

                byte[] bytes = new byte[1024];
                Console.Write("Input message: ");
                string message = Console.ReadLine();
                Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                byte[] msg = Encoding.UTF8.GetBytes(message);
                int bytesSent = sender.Send(msg);

                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("\nAnswer: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));

                if (message.IndexOf("<TheEnd>") == -1)
                    SendMessage(ipAddr, ipEndPoint);

                sender.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
