using System.Text;

namespace LinkMe.Apps.Presentation.Domain
{
    public class DocFile
    {
        private readonly string _fileName;
        private readonly byte[] _contents;

        public DocFile(string fileName, string contents)
        {
            _fileName = fileName;
            _contents = Encoding.UTF8.GetBytes(contents);
        }

        public string Name
        {
            get { return _fileName; }
        }

        public byte[] Contents
        {
            get { return _contents; }
        }
    }
}
