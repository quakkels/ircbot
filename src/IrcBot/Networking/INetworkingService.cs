using System;
using System.Threading.Tasks;

namespace IrcBot.Networking
{
    internal interface INetworkingService : IDisposable
    {
        Task Connect();
    }
}