using System;
using System.IO;

namespace IrcBot.Io
{
    internal class IoService : IIoService
    {
        private bool _isDisposed = true;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        
        public void Setup(Stream stream)
        {
            if (!_isDisposed)
            {
                throw new Exception("Setup IoService multiple times.");
            }
            
            _streamWriter = new StreamWriter(stream);
            _streamReader = new StreamReader(stream);
            _isDisposed = false;
        }
        
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
            _streamWriter.WriteLine(message);
        }
        
        public void Flush()
        {
            _streamWriter.Flush();
        }
        
        public string ReadLine()
        {
            var input = _streamReader.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                Console.WriteLine(input);
            }
            return input;
        }

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
            
            _streamReader.Dispose();
            _streamWriter.Dispose();

            _isDisposed = true;
        }

        void System.IDisposable.Dispose()
        {
            Dispose(true);
        }
    }
}