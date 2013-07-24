using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace LinkMe.Environment.Build.Tasks.Sql
{
    internal class SqlMessageListener
    {
        private readonly List<string> _messages = new List<string>();
        private readonly TextWriter _output;

        internal SqlMessageListener(TextWriter output)
        {
            _output = output;
        }

        internal IList<string> Messages
        {
            get { return _messages; }
        }

        internal void connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            _messages.Add(e.Message);
            _output.WriteLine();
            _output.WriteLine(e.Message);
            _output.WriteLine();
        }
    }
}