using System.Net;
using System.Net.Sockets;

namespace Chat.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(IPAddress.Any, 33333));
            listenSocket.Listen();
            Console.WriteLine("Starting Server...");
            while (true)
            {
                var connectedSocket = await listenSocket.AcceptAsync();
                Console.WriteLine($"Connection accepted from {connectedSocket.RemoteEndPoint} on {connectedSocket.LocalEndPoint}");
            }


        }
    }
}