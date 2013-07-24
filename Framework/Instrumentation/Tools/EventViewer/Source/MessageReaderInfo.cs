using System;
using System.Diagnostics;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
    internal abstract class MessageReaderInfo
    {
        private readonly string _name;
        private readonly string _displayName;

        protected MessageReaderInfo(string name, string displayName)
        {
            Debug.Assert(name != null && displayName != null);

            _name = name;
            _displayName = displayName;
        }

        public string Name
        {
            get { return _name; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public abstract IMessageReader CreateInstance(string initialisationString);
        public abstract string GetInitialisationString();
    }

	internal class MessageReaderInfo<TMessageReader, TInitialise>
        : MessageReaderInfo
        where TMessageReader : IMessageReader
        where TInitialise : IContainerInitialise
	{
	    public MessageReaderInfo(string name, string displayName)
            : base(name, displayName)
	    {
	    }

		public override IMessageReader CreateInstance(string initialisationString)
		{
			IMessageReader messageReader;

			try
			{
				messageReader = (IMessageReader)Activator.CreateInstance(typeof(TMessageReader), initialisationString);
			}
			catch (MissingMethodException ex)
			{
				throw new ApplicationException("Unable to create a default instance of type '"
					+ typeof(TMessageReader) + "'. Make sure this type implements a public constructor with a single string parameter.", ex);
			}
			catch (Exception ex)
			{
                throw new ApplicationException("Failed to create a default instance of type '" + typeof(TMessageReader) + "'.", ex);
			}

			return messageReader;
		}

	    public override string GetInitialisationString()
	    {
	        try
	        {
                var initialise = (IContainerInitialise) Activator.CreateInstance(typeof(TInitialise));
                if ( initialise.Prompt(null) )
                    return initialise.InitialisationString;
	        }
	        catch (Exception ex)
	        {
                throw new ApplicationException("Unable to create a default instance of type '"
                    + typeof(TInitialise) + "'. Make sure this type implements a public constructor with a single string parameter.", ex);
	        }

			return null;
	    }
	}
}
