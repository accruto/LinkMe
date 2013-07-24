using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Images;
using LinkMe.Domain.Images.Commands;

namespace LinkMe.Domain.Users.Employers.Logos.Commands
{
    public class EmployerLogosCommand
        : IEmployerLogosCommand
    {
        private readonly IImagesCommand _imagesCommand;
        private readonly IFilesCommand _filesCommand;

        public EmployerLogosCommand(IImagesCommand imagesCommand, IFilesCommand filesCommand)
        {
            _imagesCommand = imagesCommand;
            _filesCommand = filesCommand;
        }

        FileReference IEmployerLogosCommand.SaveLogo(FileContents fileContents, string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new InvalidImageExtensionException(extension);
            extension = extension.TrimStart('.');
            if (!_imagesCommand.IsValidFileExtension(extension))
                throw new InvalidImageExtensionException(extension);

            // Save it after cropping it to an appropriate size.

            using (var stream = _imagesCommand.Crop(fileContents.GetStream(), Roles.JobAds.Constants.LogoMaxSize, _imagesCommand.GetFormat(extension)))
            {
                return _filesCommand.SaveFile(FileType.CompanyLogo, new StreamFileContents(stream), fileName);
            }
        }
    }
}
