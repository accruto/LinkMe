using System.Diagnostics;

namespace LinkMe.Utility
{
	/// <summary>
	/// A trace listener that breaks into the debugger (if attached) when an assertion fails.
	/// </summary>
	[DebuggerStepThrough]
	public class BreakOnFailListener : TraceListener
	{
		public BreakOnFailListener()
		{
		}

		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);

			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}

		public override void Write(string message)
		{
		}

		public override void WriteLine(string message)
		{
		}
	}
}
