using System.IO;

namespace IrcBot.Behaviors
{
    public class Behavior
    {
        public string Evaluate(string input, StreamWriter writer)
        {
            if (input.StartsWith("PING"))
            {
                return null;
            }
            
            var inputChars = input.ToCharArray();
            inputChars[1] = 'O';
            var response = new string(inputChars);
            writer.WriteLine(response);
            writer.Flush();
            
            return response;
        }
    }
}