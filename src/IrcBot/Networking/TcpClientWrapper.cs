using System.Net.Sockets;
using System.Threading.Tasks;

namespace IrcBot.Networking
{
    internal class TcpClientWrapper : ITcpClientWrapper
    {
        private bool isDisposed = false;
        private TcpClient _tcpClient = new TcpClient();

        public async Task ConnectAsync(string host, int port)
        {
            await _tcpClient.ConnectAsync(host, port);            
        }
        
        public NetworkStream GetStream()
        {
            return _tcpClient.GetStream();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }
            
            if (!disposing)
            {
                return;
            }

            // free unmanaged resources (unmanaged objects) and override a finalizer below.
            // set large fields to null.
            _tcpClient.Dispose();

            isDisposed = true;
        }
        
        void System.IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}