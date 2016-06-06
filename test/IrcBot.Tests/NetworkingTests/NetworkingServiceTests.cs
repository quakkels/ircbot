using System.Net.Sockets;
using System.Threading.Tasks;
using IrcBot.Networking;
using IrcBot.Utilities;
using IrcBot.Io;
using Moq;
using Xunit;
using System.IO;

namespace IrcBot.Tests.NetworkingTests
{
    public class NetworkingServiceTests
    {
        private string _host = "host";
        private int _port = 6667;
        private Mock<IConfigurationProvider> _configMock;
        private Mock<ITcpClientWrapper> _tcpClientMock;
        private Mock<IIoService> _ioServiceMock;
        private NetworkingService _networkingService;

        public void SetupDefaultTestValues()
        {
            _configMock = new Mock<IConfigurationProvider>();
            _configMock.Setup(m => m.Host).Returns(_host);
            _configMock.Setup(m => m.Port).Returns(_port);
            _configMock.Setup(m => m.Nickname).Returns("nickname");
            _configMock.Setup(m => m.Username).Returns("username");
            _configMock.Setup(m => m.RealName).Returns("realname");

            _tcpClientMock = new Mock<ITcpClientWrapper>();
            _tcpClientMock.Setup(m => m.ConnectAsync(_host, _port)).Returns(Task.CompletedTask);
            _tcpClientMock.Setup(m => m.GetStream()).Returns((NetworkStream)null);

            _ioServiceMock = new Mock<IIoService>();

            _networkingService = new NetworkingService(
                _tcpClientMock.Object,
                _ioServiceMock.Object,
                _configMock.Object);
        }

        [Fact]
        public async Task WillConnectAsync()
        {
            // arrange 
            SetupDefaultTestValues();

            // act
            await _networkingService.Connect();

            // assert
            _tcpClientMock.Verify(x => x.ConnectAsync(_host, _port), Times.Once());
        }
    }
}