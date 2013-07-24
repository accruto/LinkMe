using System.IO;
using System.Text;

namespace LinkMe.Framework.Tools.Performance.Http
{
    public class FilePostValue
        : PostValue
    {
        private readonly string _fileName;
        private readonly byte[] _fileContents;

        public FilePostValue(string name, string fileName, byte[] fileContents)
            : base(name)
        {
            _fileName = fileName;
            _fileContents = fileContents;
        }

        internal override void Write(BinaryWriter writer, Encoding encoding, string boundary)
        {
            if (_fileName.Trim().Length > 0)
            {
                writer.Write(encoding.GetBytes(string.Format("--{0}\r\n", boundary)));
                writer.Write(encoding.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n", Name, _fileName)));
                writer.Write(encoding.GetBytes("Content-Type: application/octet-stream"));
                writer.Write(encoding.GetBytes("\r\n\r\n"));
                if (_fileContents != null)
                    writer.Write(_fileContents);
                writer.Write(encoding.GetBytes("\r\n"));
            }
        }

        internal override void Write(StringBuilder sb, bool first)
        {
        }
    }
}
