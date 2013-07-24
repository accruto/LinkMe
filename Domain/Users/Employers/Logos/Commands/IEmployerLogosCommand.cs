using LinkMe.Domain.Files;

namespace LinkMe.Domain.Users.Employers.Logos.Commands
{
    public interface IEmployerLogosCommand
    {
        FileReference SaveLogo(FileContents fileContents, string fileName);
    }
}
