using IrcBot.Networking;
using IrcBot.Utilities;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IrcBot.Io;

[assembly: InternalsVisibleToAttribute("IrcBot.Tests")]
[assembly: InternalsVisibleToAttribute("DynamicProxyGenAssembly2")]

namespace IrcBot
{
    public class Program
    {
        public static void Main()
        {
            Task.Run(() => Start());
            Console.ReadKey();
        }

        private static async Task Start()
        {
            using (var networkingService = new NetworkingService(
                    new TcpClientWrapper(),
                    new IoService(),
                    new ConfigurationProvider()))
            {
                await networkingService.Connect();
            }
        }
    }
}
