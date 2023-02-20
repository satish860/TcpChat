using System.Net;
using System.Net.Sockets;
using System.Text;

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
            var message = Encoding.UTF8.GetBytes("Hello World!!");
            await _clientConnection.SendAsync(message, SocketFlags.None);
            Console.ReadKey();
        }
    }
}