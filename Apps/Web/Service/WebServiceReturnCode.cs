namespace LinkMe.Web.Service
{
	public enum WebServiceReturnCode
	{
		/// <summary>
		/// The web service successfully completed all the requested work.
		/// </summary>
		Success,
		/// <summary>
		/// The entire input was processed and some errors occurred, but some work may have been completed
		/// successfully. The errors collection should contain one or more errors.
		/// </summary>
		Errors,
		/// <summary>
		/// A fatal error occurred and processing was stopped.. Most likely no work has been completed,
		/// but this cannot be guaranteed. The errors collection should contain one error.
		/// </summary>
		Failure
	}
}
