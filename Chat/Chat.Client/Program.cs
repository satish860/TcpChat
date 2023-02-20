using System.IO.Pipelines;
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

            var _clientConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            await _clientConnection.ConnectAsync("localhost", 33333);

            var pipe = new Pipe();
            _ = PipeLineToSocketAsync(pipe.Reader, _clientConnection);


            Console.WriteLine($"Connected to server at {_clientConnection.RemoteEndPoint}");
            var message = Encoding.UTF8.GetBytes("Hello World!!");
            var memory = pipe.Writer.GetMemory(message.Length);
            message.CopyTo(memory);
            pipe.Writer.Advance(message.Length);
            await pipe.Writer.FlushAsync();
            Console.ReadKey();
        }


        public static async Task PipeLineToSocketAsync(PipeReader pipeReader, Socket socket)
        {
            while (true)
            {
                ReadResult readResult = await pipeReader.ReadAsync();
                var buffer = readResult.Buffer;

                while (true)
                {
                    var memory = buffer.First;
                    if (memory.IsEmpty)
                        break;
                    var bytesSent = await socket.SendAsync(memory, SocketFlags.None);
                    buffer = buffer.Slice(bytesSent);
                    if (bytesSent!= memory.Length)
                    {
                        break;
                    }
                }
                pipeReader.AdvanceTo(buffer.Start);
                if (readResult.IsCompleted)
                {
                    break;
                }
            }
        }
    }
}