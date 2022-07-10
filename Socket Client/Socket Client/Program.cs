using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Socket_Client
{
    class Program
    {

        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            Console.Title = "Client";
            LoopConnect();
            SendLoop();
            Console.ReadLine();
        }

        private static void LoopConnect()
        {

            int attempts = 0;

            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;

                    _clientSocket.Connect(IPAddress.Loopback, 100);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connection attempts: " + attempts.ToString());
                }
            }

            Console.Clear();
            Console.WriteLine("Connected to the Server.");
            
        }

        private static void SendLoop()
        {
            while (true)
            {
                Console.WriteLine("Enter request: ");
                string request = Console.ReadLine();

                byte[] buffer = Encoding.ASCII.GetBytes(request);
                _clientSocket.Send(buffer);

                byte[] received_buffer = new byte[1024];
                int received = _clientSocket.Receive(received_buffer);

                byte[] data = new byte[received];
                Array.Copy(received_buffer, data, received);
                Console.WriteLine("Received: " + Encoding.ASCII.GetString(data));
            }
        }
    }
}
