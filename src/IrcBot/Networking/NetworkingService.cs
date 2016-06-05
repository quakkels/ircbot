using IrcBot.Utilities;
using System;
using System.Threading.Tasks;
using IrcBot.Io;

namespace IrcBot.Networking
{
    internal class NetworkingService : INetworkingService
    {
        private IConfigurationProvider _config;
        private ITcpClientWrapper _tcpClient;
        private IIoService _ioService;

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
            await _tcpClient.ConnectAsync(_config.Host, _config.Port);
            
            _ioService.Setup(_tcpClient.GetStream());
            _ioService.WriteLine(
                $"NICK {_config.Nickname}\r\nUSER {_config.Username} 0 * :{_config.RealName}\r\n");
            _ioService.Flush();

            string input = null;
            while ((input = _ioService.ReadLine()) != null)
            {
                if (input.StartsWith("PING"))
                {
                    var inputChars = input.ToCharArray();
                    inputChars[1] = 'O';
                    var response = new string(inputChars);
                    _ioService.WriteLine(response);
                    _ioService.Flush();
                }
            }
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