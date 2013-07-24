using System;
using System.Text;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public class SnippetsBuilder
    {
        private const string NORMAL_SEPARATOR = " ";
        public const int MAX_TOTAL_CHARS = 400;

        private readonly string _startMarker;
        private readonly string _endMarker;
        private readonly string _inBetweenMarker;
        private readonly StringBuilder _buffer = new StringBuilder();
        private bool _stripStartMarker;
        private bool _stopAfterThis;
        private string _prev;

        public SnippetsBuilder(string startMarker, string endMarker, string inBetweenMarker)
		{
			_startMarker = startMarker;
			_endMarker = endMarker;
			_inBetweenMarker = inBetweenMarker;
		}

        public bool Add(string next)
        {
            if (next == null)
                throw new ArgumentNullException("next");

            if (next.Length > MAX_TOTAL_CHARS)
                return !_stopAfterThis; // Ignore this snippet as it is too long even by itself.

            if (_prev == null)
            {
                _prev = next;
                return true;
            }

            int startIndex = 0;
            if (_stripStartMarker)
            {
                startIndex = _startMarker.Length;
                _stripStartMarker = false;
            }

            bool useInBetweenMarker = (_prev.EndsWith(_endMarker) && next.StartsWith(_startMarker));

            int lengthWithNextSnippet = _buffer.Length + _prev.Length - startIndex + next.Length;
            if (useInBetweenMarker)
            {
                lengthWithNextSnippet += _inBetweenMarker.Length - _endMarker.Length;
            }
            else
            {
                lengthWithNextSnippet += NORMAL_SEPARATOR.Length;
            }

            _stopAfterThis = (lengthWithNextSnippet > MAX_TOTAL_CHARS);

            if (useInBetweenMarker && !_stopAfterThis)
            {
                _buffer.Append(_prev, startIndex, _prev.Length - startIndex - _endMarker.Length);
                _buffer.Append(_inBetweenMarker);
                _stripStartMarker = true;
            }
            else
            {
                _buffer.Append(_prev, startIndex, _prev.Length - startIndex);
                if (!_stopAfterThis)
                {
                    _buffer.Append(NORMAL_SEPARATOR);
                }
            }

            _prev = next;
            return !_stopAfterThis;
        }

        public override string ToString()
        {
            string result = _buffer.ToString();

            if (/*!_stopAfterThis && */_prev != null)
            {
                if (_stripStartMarker)
                {
                    result += _prev.Substring(_startMarker.Length, _prev.Length - _startMarker.Length);
                }
                else
                {
                    result += _prev;
                }
            }

            return result;
        }
    }
}
