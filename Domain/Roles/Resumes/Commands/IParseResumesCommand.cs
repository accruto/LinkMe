using System.IO;
using LinkMe.Domain.Files;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public interface IParseResumesCommand
    {
        ParsedResume ParseResume(FileReference fileReference);
        ParsedResume ParseResume(byte[] data, string fileName);
        ParsedResume ParseResume(Stream data, string fileName);
    }

    public interface IParseResumeDataCommand
    {
        string ParseResumeData(byte[] data, string docType);
    }

    public interface IParseResumeXmlCommand
    {
        ParsedResume ParseResumeXml(string xml);
    }
}