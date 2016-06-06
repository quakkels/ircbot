using IrcBot.Utilities;
using System;
using System.Threading.Tasks;
using IrcBot.Io;
using System.Collections.Generic;
using IrcBot.Behaviors;

namespace IrcBot.Networking
{
    internal class NetworkingService : INetworkingService
    {
        private IConfigurationProvider _config;
        private ITcpClientWrapper _tcpClient;
        private IIoService _ioService;
        private List<IBehavior> _behaviors = new List<IBehavior>();

        public NetworkingService(
            ITcpClientWrapper tcpClient, 
            IIoService ioService, 
            IConfigurationProvider config)
        {
            _tcpClient = tcpClient;
            _ioService = ioService;
            _config = config;
        }

        public async Task Connect()
        {
            RegisterBehaviors();
            
            await _tcpClient.ConnectAsync(_config.Host, _config.Port);
            
            _ioService.Setup(_tcpClient.GetStream());
            _ioService.WriteLine(
                $"NICK {_config.Nickname}\r\nUSER {_config.Username} 0 * :{_config.RealName}\r\n");
            _ioService.Flush();

            string input = null;
            while ((input = _ioService.ReadLine()) != null)
            {
                foreach (var behavior in _behaviors)
                {
                    behavior.Evaluate(input);
                }
            }
        }
        
        private void RegisterBehaviors()
        {
            _behaviors.Add(new PongBehavior(_ioService));
        }
        
        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            
            if (!disposing)
            {
                return;
            }
            
            _tcpClient.Dispose();
            _ioService.Dispose();
            
            _isDisposed = true;
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}