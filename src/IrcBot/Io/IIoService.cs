using System;
using System.IO;

namespace IrcBot.Io
{
    internal interface IIoService : IDisposable
    {
        void Setup(Stream stream);
        void WriteLine(string message);
        void Flush();
        string ReadLine();        
    }
}