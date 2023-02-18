using System.Net;
using System.Net.Sockets;

namespace Chat.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press Enter to Start the Client connection with the server.");

            Console.ReadKey();

            var _clientConnection = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            await _clientConnection.ConnectAsync("localhost", 33333);

            Console.WriteLine($"Connected to server at {_clientConnection.RemoteEndPoint}");
            Console.ReadKey();
        }
    }
}