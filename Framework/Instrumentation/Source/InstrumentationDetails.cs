using System.Collections.Generic;
using System.IO;
using System.Collections;
using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Type;

namespace LinkMe.Framework.Instrumentation
{
	public class InstrumentationEntry
	{
		public InstrumentationEntry(string key, object value)
		{
			m_key = key;
			m_value = value;
		}

		public string Key
		{
			get { return m_key; }
		}

		public object Value
		{
			get { return m_value; }
		}

		private string m_key;
		private object m_value;
	}

	public class InstrumentationDetails
		:	IBinarySerializable,
			IInstrumentationWriter,
            ICustomWrapper,
            IEnumerable<InstrumentationEntry>
	{
        private List<InstrumentationEntry> m_entries;

		public InstrumentationDetails()
		{
            m_entries = new List<InstrumentationEntry>();
		}

		#region IInstrumentationWriter Members

		void IInstrumentationWriter.Write(string key, object value)
		{
			m_entries.Add(new InstrumentationEntry(key, GetValue(value)));
		}

		void IInstrumentationWriter.Write(object value)
		{
			m_entries.Add(new InstrumentationEntry(null, GetValue(value)));
		}

		#endregion
	
		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			BinaryWriteAdaptor adaptor = new BinaryWriteAdaptor(writer);
			Write(adaptor, m_entries);
		}

        private void Write(BinaryWriteAdaptor adaptor, List<InstrumentationEntry> entries)
		{
			adaptor.Write(entries.Count);
			foreach ( InstrumentationEntry entry in entries )
			{
				// Key.

				if ( entry.Key == null )
				{
					adaptor.Write(true);
				}
				else
				{
					adaptor.Write(false);
					adaptor.Write(entry.Key);
				}

				// Value.
			
				if ( entry.Value == null )
				{
					adaptor.Write(true);
				}
				else
				{
					adaptor.Write(false);
					InstrumentationDetails details = entry.Value as InstrumentationDetails;
					if ( details != null )
					{
						adaptor.Write(true);
						Write(adaptor, details.m_entries);
					}
					else
					{
						adaptor.Write(false);
						adaptor.Write(entry.Value);
					}
				}
			}
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
            m_entries = new List<InstrumentationEntry>();
			BinaryReadAdaptor adaptor = new BinaryReadAdaptor(reader);
			Read(adaptor, m_entries);
		}

        private void Read(BinaryReadAdaptor adaptor, List<InstrumentationEntry> entries)
		{
			int count = adaptor.ReadInt32();
			for ( int index = 0; index < count; ++index )
			{
				string key = null;
				object value = null;

				// Key.

				if ( !adaptor.ReadBoolean() )
					key = adaptor.ReadString();

				// Value.

				if ( !adaptor.ReadBoolean() )
				{
					if ( adaptor.ReadBoolean() )
					{
						// Details.

						InstrumentationDetails details = new InstrumentationDetails();
						Read(adaptor, details.m_entries);
						value = details;
					}
					else
					{
						value = adaptor.ReadObject();
					}
				}

				entries.Add(new InstrumentationEntry(key, value));
			}
		}

		#endregion

		private object GetValue(object value)
		{
			if ( value == null )
				return null;

			// Check for primitive type.

			PrimitiveTypeInfo typeInfo = PrimitiveTypeInfo.GetPrimitiveTypeInfo(value.GetType());
			if ( typeInfo != null )
				return TypeClone.Clone(value);

			// Check if it is instrumentable.

			IInstrumentable instrumentable = value as IInstrumentable;
			if ( instrumentable != null )
			{
				InstrumentationDetails details = new InstrumentationDetails();
				instrumentable.Write(details);
				return details;
			}

			// Return the string.

			return value.ToString();
		}

	    GenericWrapper ICustomWrapper.CreateWrapper()
	    {
            return new InstrumentationDetailsWrapper(m_entries);
	    }

	    bool ICustomWrapper.MayHaveChildren
	    {
	        get { return true; }
	    }

	    IEnumerator<InstrumentationEntry> IEnumerable<InstrumentationEntry>.GetEnumerator()
	    {
            return m_entries.GetEnumerator();
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return m_entries.GetEnumerator();
	    }
	}

    internal class InstrumentationDetailsWrapper : GenericWrapper
    {
        internal InstrumentationDetailsWrapper(List<InstrumentationEntry> value)
            : base(value)
        {
        }

        public override MemberWrappers GetMembers()
        {
            var entries = (List<InstrumentationEntry>)GetWrappedObject();
            var members = new MemberWrappers();
            foreach (var entry in entries)
                members.Add(new InstrumentationEntryWrapper(entry));
            return members;
        }
    }

    internal class InstrumentationEntryWrapper : MemberWrapper
    {
        private readonly InstrumentationEntry m_value;

        internal InstrumentationEntryWrapper(InstrumentationEntry value)
        {
            m_value = value;
        }

        public override string Name
        {
            get { return m_value.Key; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override System.Type GetMemberType()
        {
            return null;
        }

        protected override object GetValueImpl()
        {
            return m_value.Value;
        }

        protected override void SetValueImpl(object value)
        {
            throw new System.NotSupportedException();
        }

        public override bool MayHaveChildren
        {
            get { return true; }
        }
    }
}
