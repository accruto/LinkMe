using System;

namespace LinkMe.Domain
{
	// The numeric values for this enum are used in the database and domain model - do NOT change them.
	// And of course any addition to that should be in the power of 2.
	[Flags]
	public enum JobTypes
	{
		None	 = 0,
		FullTime = 1,
		PartTime = 2,
		Contract = 4,
		Temp	 = 8,
		JobShare = 0x10,

		All		 = FullTime | PartTime | Contract | Temp | JobShare
	}
}
