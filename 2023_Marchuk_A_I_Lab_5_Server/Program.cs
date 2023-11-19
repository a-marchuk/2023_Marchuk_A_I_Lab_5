using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _2023_Marchuk_A_I_Lab_5_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Net Server is starting ....\nWait please ..........");

            try
            {
                Console.Write("Server listen IP: ");
                string serverIP = Console.ReadLine();
                IPAddress ipAddr = IPAddress.Parse(serverIP);

                Console.Write("Server listen Port: ");
                string serverPort = Console.ReadLine();
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, int.Parse(serverPort));

                using (Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    sListener.Bind(ipEndPoint);
                    sListener.Listen(10);

                    while (true)
                    {
                        Console.WriteLine("Waiting for connection on port {0}", ipEndPoint);
                        using (Socket handler = sListener.Accept())
                        {
                            string data = null;
                            byte[] bytes = new byte[1024];
                            int bytesRec = handler.Receive(bytes);
                            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                            Console.Write("Received message: " + data + "\n\n");

                            string reply = "Thank you for the reply in " + data.Length.ToString() + " symbols";
                            byte[] msg = Encoding.UTF8.GetBytes(reply);
                            handler.Send(msg);

                            if (data.IndexOf("<TheEnd>") > -1)
                            {
                                Console.WriteLine("Connection closed");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.Write("Press any key to continue . . . ");
                Console.ReadKey(true);
            }
        }
    }
}
