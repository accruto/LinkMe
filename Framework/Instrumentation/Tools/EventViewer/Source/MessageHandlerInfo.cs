using System;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal abstract class MessageHandlerInfo
	{
		private readonly string _name;
		private readonly string _displayName;

	    protected MessageHandlerInfo(string name, string displayName)
		{
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

	    public abstract IMessageHandler CreateInstance();
	}

    internal class MessageHandlerInfo<TMessageHandler>
        : MessageHandlerInfo
        where TMessageHandler : IMessageHandler
    {
        public MessageHandlerInfo(string name, string displayName)
            : base(name, displayName)
        {
        }

        public override IMessageHandler CreateInstance()
        {
            IMessageHandler messageHandler;

            try
            {
                messageHandler = (IMessageHandler)Activator.CreateInstance(typeof(TMessageHandler));
            }
            catch (MissingMethodException ex)
            {
                throw new ApplicationException("Unable to create a default instance of type '" + typeof(TMessageHandler) + "'. Make sure this type implements a public parameterless constructor.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create a default instance of type '" + typeof(TMessageHandler) + "'.", ex);
            }

            return messageHandler;
        }
    }
}
