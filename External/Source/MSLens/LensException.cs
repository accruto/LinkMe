using System;

namespace com.bgt.lens
{
			/// <summary>
			/// An exception object which has to be thrown whenever a communication problem
			/// encounters will communicating with Lens server.
			/// </summary>
			public class LensException : Exception
			{
				private string errorID = "000"; // internal error

				public LensException(string errorID):base(ErrorIndex.GetMessage(errorID))
				{
					this.errorID = errorID;
				}

				public LensException(string errorID, Exception cause):base(ErrorIndex.GetMessage(errorID), cause)
				{
					this.errorID = errorID;
				}
			}
}
