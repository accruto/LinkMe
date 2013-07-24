using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Resumes.Lens;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Test.Candidates.Mocks
{
    public class MockLensParseResumeDataCommand
        : IParseResumeDataCommand
    {
        string IParseResumeDataCommand.ParseResumeData(byte[] data, string docType)
        {
            try
            {
                // Check for special cases first.

                if (MiscUtils.ByteArraysEqual(data, TestResume.Invalid.GetData()))
                    throw new LensInvalidDocumentException();

                if (MiscUtils.ByteArraysEqual(data, TestResume.Unavailable.GetData()))
                    throw new LensUnavailableException("Not working.");

                foreach (var resume in TestResume.AllResumes)
                {
                    if (MiscUtils.ByteArraysEqual(data, resume.GetData()))
                        return resume.GetLensXml();
                }

                return "<ResDoc><contact></contact><resume></resume></ResDoc>";
            }
            catch (LensInvalidDocumentException ex)
            {
                throw new InvalidResumeException(ex);
            }
            catch (LensException ex)
            {
                throw new ParserUnavailableException(ex);
            }
        }
    }
}