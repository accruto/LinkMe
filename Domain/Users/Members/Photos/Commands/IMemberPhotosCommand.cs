using LinkMe.Domain.Files;

namespace LinkMe.Domain.Users.Members.Photos.Commands
{
    public interface IMemberPhotosCommand
    {
        FileReference SavePhoto(FileContents fileContents, string fileName);
    }
}