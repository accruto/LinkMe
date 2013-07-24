using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LinkMe.Apps.Services.External.JobG8;
using LinkMe.Apps.Services.External.JobG8.Commands;
using LinkMe.Apps.Services.External.JobG8.Schema;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    public class MockSendJobG8Command
        : ISendJobG8Command
    {
        private readonly IChannelManager<IMockJobG8Service> _serviceManager;

        public MockSendJobG8Command(IChannelManager<IMockJobG8Service> serviceManager)
        {
            _serviceManager = serviceManager;
        }

        string ISendJobG8Command.SendApplication(ICommunicationUser user, JobAdEntry jobAd, string resumeFileName, FileContents resumeContents, InternalApplication application, IEnumerable<ApplicationAnswer> answers)
        {
            var request = new UploadRequestMessage
            {
                Body = new UploadRequestBody
                {
                    ApplicationXml = Serialize(CreateApplicationResponse(user, jobAd, resumeFileName, resumeContents, application, answers))
                }
            };

            string result;
            var service = _serviceManager.Create();
            try
            {
                var response = service.Send(request);
                result = response.Body.Result;
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);

            return result;
        }

        private static ApplicationResponse CreateApplicationResponse(ICommunicationRecipient user, JobAdEntry jobAd, string resumeFileName, FileContents resumeContents, InternalApplication application, IEnumerable<ApplicationAnswer> answers)
        {
            // Files.

            var cv = jobAd.Integration.ApplicationRequirements != null && jobAd.Integration.ApplicationRequirements.IncludeResume
                ? GetEncodedCv(resumeFileName, resumeContents)
                : null;

            var coverLetter = jobAd.Integration.ApplicationRequirements != null && jobAd.Integration.ApplicationRequirements.IncludeCoverLetter
                ? GetEncodedCoverLetter(application)
                : null;

            return new ApplicationResponse
            {
                ApplicationAnswer = new Answer
                {
                    Questions = new AnswerQuestions
                    {
                        Question = GetAnswers(user).Concat(GetAnswers(answers)).ToArray(),
                    },
                    Files = new AnswerFiles
                    {
                        CV = cv,
                        CoverLetter = coverLetter
                    },
                    JobReference = jobAd.Integration.IntegratorReferenceId,
                    JobBoardID = jobAd.Integration.JobBoardId,
                }
            };
        }

        private static IEnumerable<AnswerQuestion> GetAnswers(ICommunicationRecipient user)
        {
            return new[]
            {
                new AnswerQuestion { ID = "2", FormatID = 2, FormatIDSpecified = true, Items = new[] {user.FirstName}},
                new AnswerQuestion { ID = "3", FormatID = 5, FormatIDSpecified = true, Items = new[] {user.LastName}},
                new AnswerQuestion { ID = "11", FormatID = 15, FormatIDSpecified = true, Items = new[] {user.EmailAddress}},
            };
        }

        private static IEnumerable<AnswerQuestion> GetAnswers(IEnumerable<ApplicationAnswer> answers)
        {
            if (answers == null)
                return new AnswerQuestion[0];
            return (from a in answers
                    let answer = GetAnswer(a)
                    where answer != null
                    select answer);
        }

        private static AnswerQuestion GetAnswer(ApplicationAnswer answer)
        {
            if (string.IsNullOrEmpty(answer.Value))
                return null;

            return new AnswerQuestion
            {
                ID = answer.Question.Id,
                FormatID = int.Parse(answer.Question.FormatId),
                FormatIDSpecified = true,
                Items = new[] { answer is MultipleChoiceAnswer ? (object)int.Parse(answer.Value) : answer.Value },
            };
        }

        private static AnswerCV GetEncodedCv(string resumeFileName, FileContents resumeContents)
        {
            using (var stream = resumeContents.GetStream())
            {
                var length = (int)stream.Length;
                var buffer = new byte[length];
                stream.Read(buffer, 0, length);

                return new AnswerCV
                {
                    Filename = resumeFileName,
                    Value = Convert.ToBase64String(buffer)
                };
            }
        }

        private static AnswerCoverLetter GetEncodedCoverLetter(InternalApplication application)
        {
            if (string.IsNullOrEmpty(application.CoverLetterText))
                return null;

            return new AnswerCoverLetter
            {
                Filename = "Cover Letter.txt",
                Value = Convert.ToBase64String(Encoding.ASCII.GetBytes(application.CoverLetterText))
            };
        }

        private static string Serialize<T>(T response)
        {
            var serializer = new XmlSerializer(typeof(T));
            var buffer = new StringBuilder();

            using (var writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, response);
            }

            return buffer.ToString();
        }
    }
}
