using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Files
{
    public class FileData
    {
        // The number of bytes in the file hash. This can be changed, but the database must be changed too.

        public const int HashSize = 16;

        [IsSet]
		public Guid Id { get; set; }
        public FileType FileType { get; set; }
        [Required]
        public string FileExtension { get; set; }
        public int ContentLength { get; set; }
        [Required, ArrayLength(HashSize, HashSize)]
        public byte[] ContentHash { get; set; }
    }
}
