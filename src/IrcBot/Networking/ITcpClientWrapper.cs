using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IrcBot.Networking
{
    internal interface ITcpClientWrapper : IDisposable
    {
        Task ConnectAsync(string host, int port);
        NetworkStream GetStream();
    }
}