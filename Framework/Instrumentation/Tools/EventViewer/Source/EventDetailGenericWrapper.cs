using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Type.Tools.Net;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class EventDetailGenericWrapper : GenericWrapper
	{
		internal EventDetailGenericWrapper(IEventDetail detail)
			: base(detail)
		{
		}

		public override MemberWrappers GetMembers()
		{
			object detail = GetWrappedObject();
			Debug.Assert(detail != null, "detail != null");

			MemberWrappers properties = new MemberWrappers();

            // Do some special processing for the generic detail.

            if (detail is GenericDetail)
            {
                foreach (var value in ((GenericDetail)detail).Values)
                    properties.Add(new ConstantMemberWrapper(value.Name, value.Value));
            }
            else
            {
                // Use reflection to read all the properties, except "Name".

                PropertyInfo[] infos = detail.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                Debug.Assert(infos.Length > 0, "Event details object '" + ((IEventDetail)detail).Name
                    + "' doesn't seem to have any public properties.");

                foreach (PropertyInfo info in infos)
                {
                    if (info.Name == "Name")
                        continue;

                    object value;
                    try
                    {
                        value = info.GetValue(detail, null);
                    }
                    catch (System.Exception ex)
                    {
                        value = ex;
                    }

                    properties.Add(new ConstantMemberWrapper(info.Name, value));
                }
            }

			return properties;
		}
	}
}
