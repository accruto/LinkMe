namespace LinkMe.Framework.Instrumentation
{
	public interface IInstrumentationWriter
	{
		void Write(string key, object value);
		void Write(object value);
	}

	public interface IInstrumentable
	{
		void Write(IInstrumentationWriter writer);
	}
}
