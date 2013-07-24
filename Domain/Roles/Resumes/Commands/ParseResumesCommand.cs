using System;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Resumes.Commands
{
    public class ParseResumesCommand
        : IParseResumesCommand
    {
        private readonly IParseResumeDataCommand _parseResumeDataCommand;
        private readonly IParseResumeXmlCommand _parseResumeXmlCommand;
        private readonly IFilesQuery _filesQuery;

        public ParseResumesCommand(IParseResumeDataCommand parseResumeDataCommand, IParseResumeXmlCommand parseResumeXmlCommand, IFilesQuery filesQuery)
        {
            _parseResumeDataCommand = parseResumeDataCommand;
            _parseResumeXmlCommand = parseResumeXmlCommand;
            _filesQuery = filesQuery;
        }

        ParsedResume IParseResumesCommand.ParseResume(FileReference fileReference)
        {
            var stream = _filesQuery.OpenFile(fileReference);
            return ParseResume(GetByteData(stream), fileReference.FileName);
        }

        ParsedResume IParseResumesCommand.ParseResume(byte[] data, string fileName)
        {
            return ParseResume(data, fileName);
        }

        ParsedResume IParseResumesCommand.ParseResume(Stream data, string fileName)
        {
            return ParseResume(GetByteData(data), fileName);
        }

        private ParsedResume ParseResume(byte[] data, string fileName)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length == 0)
                throw new ArgumentException("The resume to parse is empty.", "data");

            var xml = _parseResumeDataCommand.ParseResumeData(data, GetDocType(fileName));
            if (xml == null)
                throw new ApplicationException("Null XML returned from parsing resume data.");

            return _parseResumeXmlCommand.ParseResumeXml(xml);
        }

        private static byte[] GetByteData(Stream stream)
        {
            return StreamUtil.ReadFully(stream);
        }

        private static string GetDocType(string fileName)
        {
            return Path.GetExtension(fileName).TrimStart('.');
        }
    }
}