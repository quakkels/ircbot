namespace IrcBot.Utilities
{
    internal class ConfigurationProvider : IConfigurationProvider
    {
        public string Host => "chat.freenode.net";
        public int Port => 6667;
        public string Nickname => "IrcsomeBot02062016";
        public string Username => "IrcBotUserName";
        public string RealName => "IrcBotRealName";
        public string MessageRegex => @"^(?:[:](\S+) )?(\S+)(?: (?!:)(.+?))?(?: [:](.+))?$";
    }
}
