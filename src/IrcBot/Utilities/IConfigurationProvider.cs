namespace IrcBot.Utilities
{
    internal interface IConfigurationProvider 
    {        
        string Host { get; }
        int Port { get; }
        string Nickname { get; }
        string Username { get; }
        string RealName { get; }
        string MessageRegex { get; }
    }
}