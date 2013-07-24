using System.IO;
using System.Text;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    internal class BuildLogTextWriter
        : TextWriter
    {
        private readonly TaskLoggingHelper _logger;
        private readonly bool _isError;
        private readonly StringBuilder _logBuffer = new StringBuilder();

        internal BuildLogTextWriter(TaskLoggingHelper logger, bool isError)
        {
            _logger = logger;
            _isError = isError;
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            _logBuffer.Append(buffer, index, count);
        }

        public override void Write(char value)
        {
            _logBuffer.Append(value);
        }

        public override void Write(string value)
        {
            _logBuffer.Append(value);
        }

        public override void WriteLine()
        {
            Flush();
        }

        public override void WriteLine(string value)
        {
            _logBuffer.Append(value);
            Flush();
        }

        public override void Flush()
        {
            if (_logBuffer.Length > 0)
            {
                if (_isError)
                    _logger.LogError(_logBuffer.ToString());
                else
                    _logger.LogMessage(_logBuffer.ToString());
                _logBuffer.Remove(0, _logBuffer.Length);
            }
        }
    }
}