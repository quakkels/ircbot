using IrcBot.Io;

namespace IrcBot.Behaviors
{
    internal class PongBehavior : IBehavior
    {
        private IIoService _ioService;

        public PongBehavior(IIoService ioService)
        {
            _ioService = ioService;
        }

        public void Evaluate(string input)
        {
            if (!input.StartsWith("PING"))
            {
                return;
            }

            var inputChars = input.ToCharArray();
            inputChars[1] = 'O';
            var response = new string(inputChars);

            _ioService.WriteLine(response);
            _ioService.Flush();
        }
    }
}