using System.Drawing.Imaging;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Images;
using LinkMe.Domain.Images.Commands;

namespace LinkMe.Domain.Users.Members.Photos.Commands
{
    public class MemberPhotosCommand
        : IMemberPhotosCommand
    {
        private readonly IImagesCommand _imagesCommand;
        private readonly IFilesCommand _filesCommand;

        private const string StorageExtension = ".jpg";
        private static readonly ImageFormat StorageFormat = ImageFormat.Jpeg;

        public MemberPhotosCommand(IImagesCommand imagesCommand, IFilesCommand filesCommand)
        {
            _imagesCommand = imagesCommand;
            _filesCommand = filesCommand;
        }

        FileReference IMemberPhotosCommand.SavePhoto(FileContents fileContents, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new InvalidImageExtensionException(extension);
            extension = extension.TrimStart('.');
            if (!_imagesCommand.IsValidFileExtension(extension))
                throw new InvalidImageExtensionException(extension);
            fileName = Path.ChangeExtension(fileName, StorageExtension);

            // Save it after converting it to an appropriate size and format.

            using (var stream = _imagesCommand.Resize(fileContents.GetStream(), Domain.Contacts.Constants.PhotoMaxSize, StorageFormat))
            {
                return _filesCommand.SaveFile(FileType.ProfilePhoto, new StreamFileContents(stream), fileName);
            }
        }
    }
}