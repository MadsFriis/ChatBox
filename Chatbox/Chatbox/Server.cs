using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chatbox
{
    internal class Server
    {
        internal static void ExecuteServer(int port)
        {
            // Establish the local endpoint
            // for the socket. Dns.GetHostName
            // returns the name of the host
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);
                Console.WriteLine("Waiting connection ... ");

                // Suspend while waiting for
                // incoming connection Using
                // Accept() method the server
                // will accept connection of client
                Socket clientSocket = listener.Accept();
                try
                {
                    string data = null;
                    do
                    {
                        // Data buffer
                        byte[] bytes = new Byte[1024];
                        int numByte = clientSocket.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, numByte);
                        Console.WriteLine("Text received -> {0} ", data);
                        byte[] message = Encoding.ASCII.GetBytes("Test Server");

                        // Send a message to Client
                        // using Send() method
                        clientSocket.Send(message);

                    } while (!data.Contains("close"));
                }
                finally
                {
                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket
                    // for a new Client Connection
                    Console.WriteLine("---------------Server closed---------------");
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                }
                //while (true)

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
