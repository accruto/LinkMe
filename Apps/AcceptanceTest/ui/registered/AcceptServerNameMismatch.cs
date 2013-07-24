using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace LinkMe.AcceptanceTest.ui.registered
{
	public class AcceptServerNameMismatch : ICertificatePolicy
	{
		// HACK: This is a workaround.  The .NET Framwork should expose these, but they don't.
		public enum CertificateProblem : long
		{
			CertEXPIRED = 2148204801,
			CertVALIDITYPERIODNESTING = 2148204802,
			CertROLE = 2148204803,
			CertPATHLENCONST = 2148204804,
			CertCRITICAL = 2148204805,
			CertPURPOSE = 2148204806,
			CertISSUERCHAINING = 2148204807,
			CertMALFORMED = 2148204808,
			CertUNTRUSTEDROOT = 2148204809,
			CertCHAINING = 2148204810,
			CertREVOKED = 2148204812,
			CertUNTRUSTEDTESTROOT = 2148204813,
			CertREVOCATION_FAILURE = 2148204814,
			CertCN_NO_MATCH = 2148204815,
			CertWRONG_USAGE = 2148204816,
			CertUNTRUSTEDCA = 2148204818
		}

		/// <summary>
		/// Implement CheckValidationResult to ignore problems that we are willing to accept.
		/// </summary>
		public bool CheckValidationResult(ServicePoint sp, X509Certificate cert,
		                                  WebRequest request, int problem)
		{
			int CertificateNameDoesntMatch = unchecked((int) CertificateProblem.CertCN_NO_MATCH);
			if (problem == CertificateNameDoesntMatch) // only accept server name failed match
				return true;

			// The 1.1 framework calls this method with a problem of 0, even if nothing is wrong
			return (problem == 0);
		}
	}
}