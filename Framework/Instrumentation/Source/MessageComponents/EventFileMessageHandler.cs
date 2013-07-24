using System.IO;
using System.Xml;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class EventFileMessageHandler
		:	BaseMessageHandler
	{
		private readonly string _fileName = string.Empty;

		protected override void HandleEventMessage(EventMessage message)
		{
			var messages = new EventMessages {message};
		    HandleEventMessages(messages);
		}

		protected override void HandleEventMessages(EventMessages messages)
		{
			SetMessageSequences(messages);

			using ( var stream = new FileStream(_fileName, FileMode.Create, FileAccess.Write, FileShare.Read) )
			{
				XmlTextWriter writer = null;
				try
				{
					writer = new XmlTextWriter(stream, XmlWriteAdaptor.XmlEncoding)
					             {
					                 Formatting = Formatting.Indented,
					                 Indentation = 4
					             };
				    messages.WriteOuterXml(writer);
				}
				finally
				{
					if ( writer != null )
						writer.Close();
				}
			}
		}
	}
}
