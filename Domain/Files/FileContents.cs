using System.IO;
using System.Security.Cryptography;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Files
{
    public class FilePath
    {
        public string Folder { get; set; }
        public string FileName { get; set; }

        public string FullPath
        {
            get { return Path.Combine(Folder, FileName); }
        }
    }

    public abstract class FileContents
    {
        private static readonly HashAlgorithm HashAlgorithm = new MD5CryptoServiceProvider();

        public abstract void Save(FilePath filePath);
        public abstract Stream GetStream();
        public abstract string Type { get; }
        public abstract int Length { get; }
        public abstract byte[] Hash { get; }

        protected static byte[] GetHash(Stream stream)
        {
            stream.Position = 0;
            return HashAlgorithm.ComputeHash(stream);
        }
    }

    public class StreamFileContents
        : FileContents
    {
        private readonly Stream _stream;

        public StreamFileContents(Stream stream)
        {
            _stream = stream;
        }

        public override void Save(FilePath filePath)
        {
            using (var stream = new FileStream(filePath.FullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                _stream.Position = 0;
                StreamUtil.CopyStream(_stream, stream);
            }
        }

        public override Stream GetStream()
        {
            return _stream;
        }

        public override string Type
        {
            get { return null; }
        }

        public override int Length
        {
            get { return (int)_stream.Length; }
        }

        public override byte[] Hash
        {
            get { return GetHash(_stream); }
        }
    }
}
