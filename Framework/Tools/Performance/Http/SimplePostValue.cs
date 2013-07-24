using System.IO;
using System.Text;
using System.Web;

namespace LinkMe.Framework.Tools.Performance.Http
{
    public class SimplePostValue
        : PostValue
    {
        private readonly string _value;

        internal SimplePostValue(string name, string value)
            : base(name)
        {
            _value = value;
        }

        internal override void Write(BinaryWriter writer, Encoding encoding, string boundary)
        {
            writer.Write(encoding.GetBytes(string.Format("--{0}\r\n", boundary)));
            writer.Write(encoding.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n", Name)));
            writer.Write(encoding.GetBytes("\r\n"));
            writer.Write(encoding.GetBytes(_value));
            writer.Write(encoding.GetBytes("\r\n"));
        }

        internal override void Write(StringBuilder sb, bool first)
        {
            if (_value != null)
            {
                if (!first)
                    sb.Append("&");
                sb.Append(HttpUtility.UrlEncode(Name)).Append("=").Append(HttpUtility.UrlEncode(_value));
            }
        }
    }
}