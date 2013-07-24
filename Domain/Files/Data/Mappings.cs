using System.Data.Linq;

namespace LinkMe.Domain.Files.Data
{
    internal static class Mappings
    {
        public static FileReference Map(this FileReferenceEntity entity)
        {
            return new FileReference
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                FileName = entity.name,
                MediaType = entity.mimeType,
                FileData = entity.FileDataEntity == null ? null : entity.FileDataEntity.Map()
            };
        }

        public static FileData Map(this FileDataEntity entity)
        {
            return new FileData
           {
               Id = entity.id,
               FileType = (FileType) entity.context,
               FileExtension = entity.extension,
               ContentLength = entity.sizeBytes,
               ContentHash = entity.hash.ToArray(),
           };
        }


        public static FileReferenceEntity Map(this FileReference fileReference)
        {
            return new FileReferenceEntity
            {
                id = fileReference.Id,
                createdTime = fileReference.CreatedTime,
                name = fileReference.FileName,
                mimeType = fileReference.MediaType,
                dataId = fileReference.FileData.Id
            };
        }

        public static FileDataEntity Map(this FileData fileData)
        {
            return new FileDataEntity
            {
                id = fileData.Id,
                context = (byte)fileData.FileType,
                extension = fileData.FileExtension,
                sizeBytes = fileData.ContentLength,
                hash = new Binary(fileData.ContentHash),
            };
        }
    }
}
