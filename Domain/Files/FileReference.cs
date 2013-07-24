using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Files
{
    public class FileReference
    {
        public Guid Id { get; set; }
        [IsSet]
        public DateTime CreatedTime  { get; set; }
		public string MediaType { get; set; }
        [Required]
		public string FileName { get; set; }
        [Required]
        public FileData FileData { get; set; }
    }
}
