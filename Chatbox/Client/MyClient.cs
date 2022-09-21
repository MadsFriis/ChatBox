using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MyClient
    {

        // ExecuteClient() Method
        public static void ExecuteClient(int port)
        {
            try
            {
                // Establish the remote endpoint
                // for the socket. This example
                // uses port 11111 on the local
                // computer.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                string msg = "";
                try
                {
                    // Connect Socket to the remote
                    // endpoint using method Connect()
                    sender.Connect(localEndPoint);

                    // We print EndPoint information
                    // that we are connected
                    Console.WriteLine("Socket connected to -> {0} ", sender.RemoteEndPoint.ToString());
                    do
                    {
                        // Creation of message that
                        // we will send to Server
                        Console.Write("Messege to server: ");
                        msg = Console.ReadLine();
                        byte[] messageSent = Encoding.ASCII.GetBytes(msg);
                        int byteSent = sender.Send(messageSent);

                        // Data buffer
                        byte[] messageReceived = new byte[1024];

                        // We receive the message using
                        // the method Receive(). This
                        // method returns number of bytes
                        // received, that we'll use to
                        // convert them to string
                        int byteRecv = sender.Receive(messageReceived);
                        Console.WriteLine("Message from Server -> {0}", Encoding.ASCII.GetString(messageReceived, 0, byteRecv));
                    
                    } while (!msg.Contains("close"));
                }
                finally
                {
                    Console.WriteLine("---------------Closing Client!---------------");
                    // Close Socket using the method Close()
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
            }
            // Manage of Socket's Exceptions
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
    }
}
