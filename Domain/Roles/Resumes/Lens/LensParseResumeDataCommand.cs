using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using com.bgt.lens;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Domain.Roles.Resumes.Lens
{
    public class LensParseResumeDataCommand
        : IParseResumeDataCommand
    {
        private const string LensUnavailableError = "Error in socket select: #0 [No error]";
        private const string LensBusyError = "server busy";
        private const string LensRemoveResumeNotFoundError = "document not found: couldn't get key";
        private const string LensDocumentNotFound = "document not found";
        private const string LensResumeCouldNotBeFetched = "couldn't fetch document: invalid key";
        private const string LensDuplicateKeyError = "couldn't create document: duplicate key";
        private const string LensTagFileErrorStart = "(tag:";
        private const string LensTagFileErrorEnd = " text not generated";

        private LensSession _lensSession;
        private const ulong Timeout = 60000;
        private const int TotalAttemptsIfBusy = 3;
        private const int DelayIfBusy = 1000;

        private static readonly EventSource EventSource = new EventSource<LensParseResumeDataCommand>();

        private readonly string _host;
        private readonly int _port;

        public LensParseResumeDataCommand(string host, int port)
        {
            _host = host;
            _port = port;
        }

        string IParseResumeDataCommand.ParseResumeData(byte[] data, string docType)
        {
            const string method = "ParseResumeData";

            try
            {
                if (data == null || docType == null)
                    throw new LensException("Invalid argument");

                // If busy try again.

                for (var attempt = 1; attempt < TotalAttemptsIfBusy; attempt++)
                {
                    try
                    {
                        return AttemptSend(method, data, docType);
                    }
                    catch (LensBusyException)
                    {
                        EventSource.Raise(Event.Warning, method, string.Format("Lens server is busy on attempt {0}, waiting {1} ms to retry.", attempt, DelayIfBusy));
                        Thread.Sleep(DelayIfBusy);
                    }
                }

                return TagResume(data, docType);
            }
            catch (LensInvalidDocumentException ex)
            {
                EventSource.Raise(Event.Warning, method, ex, null);
                throw new InvalidResumeException(ex);
            }
            catch (LensException ex)
            {
                EventSource.Raise(Event.Error, method, ex, null);
                throw new ParserUnavailableException(ex);
            }
        }

        private string AttemptSend(string method, byte[] data, string docType)
        {
            EventSource.Raise(Event.MethodEnter, method, Event.Arg("data", data), Event.Arg("docType", docType));

            string response = null;
            try
            {

                if (!LensSession.IsOpen())
                    LensSession.Open();

                EventSource.Raise(Event.Information, method, "Sending request to Lens.", Event.Arg("Host", LensSession.GetHost()), Event.Arg("Port", LensSession.GetPort()), Event.Arg("Timeout", Timeout));

                response = TagResume(data, docType);

                EventSource.Raise(Event.Information, method, "Received response from Lens.", Event.Arg("Response", response));

                ParseForErrors(response);
                return response;
            }
            catch (LensException ex)
            {
                EventSource.Raise(Event.Error, method, ex, null, Event.Arg("data", data), Event.Arg("docType", docType), Event.Arg("response", response));
                throw;
            }
            catch (Exception ex)
            {
                EventSource.Raise(Event.Error, method, ex, null, Event.Arg("data", data), Event.Arg("docType", docType), Event.Arg("response", response));

                if (ex.InnerException is SocketException)
                    throw new LensUnavailableException(string.Format("Failed to connect to Lens server {0}:{1}.", LensSession.GetHost(), LensSession.GetPort()), ex);
                else if (ex.InnerException is XmlException)
                    throw new LensXmlInvalidException("The XML returned from Lens is invalid.", ex);
                else
                    throw new LensException(ex.Message, ex);
            }
            finally
            {
                LensSession.Close();

                EventSource.Raise(Event.MethodExit, method, Event.Arg("data", data), Event.Arg("docType", docType), Event.Arg("response", response));
            }
        }

        private string TagResume(byte[] data, string docType)
        {
            var returnMessage = LensSession.TagBinaryData(data, docType, MSLens.RESUME_TYPE);
            var result = returnMessage.GetMessageData();
            return result.TrimEnd('\0').TrimEnd('\n');
        }

        private LensSession LensSession
        {
            get
            {
                if (_lensSession == null)
                {
                    _lensSession = MSLens.CreateSession(_host, (uint) _port);
                    _lensSession.SetEnableTransactionTimeout(true);
                    _lensSession.SetTransactionTimeout(Timeout);
                }
                return _lensSession;
            }
        }

        private static void ParseForErrors(string result)
        {
            ParseForLensStringError(result);

            try
            {
                ParseForLensXmlError(result);
            }
            catch (LensException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to parse Lens response XML:\r\n" + result, ex);
            }
        }

        private static void ParseForLensXmlError(string lensResult)
        {
            const string method = "ParseForLensXmlError";

            var textReader = new StringReader(lensResult);
            var xPathDocument = new XPathDocument(textReader);
            var xPathNavigator = xPathDocument.CreateNavigator();
            var expression = xPathNavigator.Compile("bgtres/error");
            var iterator = xPathNavigator.Select(expression);

            if (iterator.MoveNext())
            {
                var error = iterator.Current.Value;
                EventSource.Raise(Event.Trace, method, "Lens error found lens result = " + lensResult + ", error = " + error);

                if (error == LensUnavailableError)
                    throw new LensUnavailableException(error);
                if (error.IndexOf(LensDuplicateKeyError) > -1)
                    throw new LensDuplicateKeyException(error);
                if (error == LensRemoveResumeNotFoundError
                    || error == LensDocumentNotFound
                    || error.StartsWith(LensResumeCouldNotBeFetched))
                    throw new LensResumeDoesNotExistException(error);
                throw new LensException(error);
            }
        }

        private static void ParseForLensStringError(string errorMessage)
        {
            if (errorMessage == LensUnavailableError)
                throw new LensUnavailableException(errorMessage);
            if (errorMessage.StartsWith(LensBusyError))
                throw new LensBusyException(errorMessage);
            if (errorMessage.StartsWith(LensTagFileErrorStart) && errorMessage.EndsWith(LensTagFileErrorEnd))
                throw new LensInvalidDocumentException(errorMessage);
        }
    }
}