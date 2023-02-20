using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(IPAddress.Any, 33333));
            listenSocket.Listen();
            Console.WriteLine($"Starting Server... in thread {Thread.CurrentThread.ManagedThreadId}");
            while (true)
            {
                var connectedSocket = await listenSocket.AcceptAsync();
                Console.WriteLine($"Connection accepted from {connectedSocket.RemoteEndPoint} " +
                    $"on {connectedSocket.LocalEndPoint}, {Thread.CurrentThread.ManagedThreadId}");
                _ = ProcessSocket(connectedSocket);
            }
        }

        private static async Task ProcessSocket(Socket connectedSocket)
        {
            while (true)
            {
                var buffer = new byte[1024];
                var byterecieved = await connectedSocket.ReceiveAsync(buffer, SocketFlags.None);
                if(byterecieved == 0)
                {
                    break;
                }
                var message = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"Message received from client {message}");
            }
        }
    }
}