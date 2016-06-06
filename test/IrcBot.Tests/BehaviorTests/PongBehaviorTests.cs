using Xunit;
using IrcBot.Behaviors;
using Moq;
using IrcBot.Io;

namespace IrcBot.Tests.BehaviorTests
{
    public class PongBehaviorTests
    {
        private string _input;
        private Mock<IIoService> _ioServiceMock;
        private PongBehavior _pongBehavior;
        
        private void SetupDefaults()
        {
            _input = "PING qwerasdfzxc";
            
            _ioServiceMock = new Mock<IIoService>();
            _ioServiceMock.Setup(x => x.WriteLine("PONG qwerasdfzxc"));
            _ioServiceMock.Setup(x => x.Flush());
            
            _pongBehavior = new PongBehavior(_ioServiceMock.Object);
        }
        
        [Fact]
        public void WillNotActWhenInputDoesNotMatch()
        {
            // arrange
            SetupDefaults();
            _input = "wrong input";
            _ioServiceMock.Setup(x => x.WriteLine(It.IsAny<string>()));

            // act
            _pongBehavior.Evaluate(_input);

            // assert
            _ioServiceMock.Verify(
                x => x.WriteLine(It.IsAny<string>()),
                Times.Never());
            _ioServiceMock.Verify(x => x.Flush(), Times.Never());
        }
        
        [Fact]
        public void WillActWhenInputMatches()
        {
            // arrange
            SetupDefaults();
            
            // act
            _pongBehavior.Evaluate(_input);
            
            // assert
            _ioServiceMock.Verify(
                x => x.WriteLine("PONG qwerasdfzxc"), 
                Times.Once());
            _ioServiceMock.Verify(x => x.Flush(), Times.Once());
        }
    }
}