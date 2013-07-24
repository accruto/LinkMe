using System.IO;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class EventType
		:	CatalogueQualifiedElement,
			ICatalogueElement,
			IElementReference,
			IElementIdentification
	{
		#region Constructors

		internal EventType(Catalogue catalogue, string name, bool isEnabled)
			:	base(catalogue, name)
		{
			// Check.

			const string method = ".ctor";
			if ( catalogue == null )
				throw new NullParameterException(typeof(EventType), method, "catalogue");
			if ( !CatalogueName.IsName(name) )
				throw new InvalidParameterFormatException(typeof(EventType), method, "name", name, Constants.Validation.CompleteNamePattern);

			// Assign.

			_isEnabled = isEnabled;
		}

		internal EventType(Catalogue catalogue, string name)
			:	this(catalogue, name, false)
		{
		}

		#endregion

		#region Properties

		public bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}
			set
			{
				_isEnabled = value;
				UpdateProperty();
			}
		}

		#endregion

		#region Relationships

		public Catalogue Catalogue
		{
			get { return ((ICatalogueElement) Parent).Catalogue; }
		}

		CatalogueElements ICatalogueElement.Element
		{
			get { return CatalogueElements.Event; }
		}

		string IElementIdentification.CataloguePath
		{
			get
			{
				return ((IElementIdentification)Catalogue).CataloguePath + ", "
					+ ((IElementIdentification)this).ElementPath;
			}
		}

		string IElementIdentification.ElementPath
		{
			get { return "[Event]" + FullName; }
		}

		string IElementIdentification.ElementKey
		{
			get { return Name; }
		}

		string IElementIdentification.ElementType
		{
			get { return CatalogueElements.Event.ToString(); }
		}

		string IElementReference.FullyQualifiedReference
		{
			get { return FullName; }
		}

		#endregion

		#region Operations

		public EventType Clone(Catalogue catalogue, string name)
		{
			EventType eventType = catalogue.CreateEventType(catalogue, name);
			eventType.CopyMembers(this, false);
			CopyChildCollections(this);
			return eventType;
		}

        public void CopyProperties(EventType eventType)
		{
			CopyMembers(eventType, true);
		}

		public EventType CloneProperties(Catalogue catalogue, string name)
		{
			EventType eventType = catalogue.CreateEventType(catalogue, name);
			eventType.CopyMembers(this, false);
			return eventType;
		}

        private void CopyMembers(EventType eventType, bool updateProperty)
		{
			_isEnabled = eventType.IsEnabled;
			if ( updateProperty )
				UpdateProperty();
		}

        private static void CopyChildCollections(EventType eventType)
		{
		}

		#endregion

		#region Equality

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is EventType) )
				return false;
			return Equals((EventType) other);
		}

        public bool Equals(EventType other)
		{
			if ( other == null )
				return false;

			// Check base.

			if ( !base.Equals(other) )
				return false;

			// Check details.

			if ( !EqualDetails(other) )
				return false;

			// Check children.

			return EqualChildren(other);
		}

		public static bool Equals(EventType c1, EventType c2)
		{
			return object.Equals(c1, c2);
		}

		public static bool operator==(EventType c1, EventType c2)
		{
			return Equals(c1, c2);
		}

		public static bool operator!=(EventType c1, EventType c2)
		{
			return !Equals(c1, c2);
		}

		private bool EqualDetails(EventType other)
		{
			return IsEnabled == other.IsEnabled;
		}

		private static bool EqualChildren(EventType other)
		{
			return true;
		}

		#endregion

		#region Overrides

		protected override CommitStatus Create()
		{
			return Catalogue.CatalogueConnection.Create(this);
		}

		protected override CommitStatus Update()
		{
			return Catalogue.CatalogueConnection.Update(this);
		}

		protected override CommitStatus Delete()
		{
			return Catalogue.CatalogueConnection.Delete(this);
		}

		#endregion

		#region Members

		private bool _isEnabled;

		#endregion
	}

	public class EventTypes
		:	SortedElementCollection
	{
		internal EventTypes(Element parent)
			:	base(parent)
		{
		}

		public new EventType this[int index]
		{
			get { return (EventType) base[index]; }
		}

		public new EventType this[string name]
		{
			get { return (EventType) base[name]; }
		}

		protected override string Name
		{
			get { return Constants.Collections.EventTypes; }
		}

        internal void Add(EventType eventType)
		{
			base.Add(eventType);
		}

		internal void AddIfNotLoaded(EventType[] eventTypes)
		{
			base.AddIfNotLoaded(eventTypes);
		}

        internal void Remove(EventType eventType)
		{
			base.Remove(eventType);
		}

		internal void Replace(EventType oldEventType, EventType newEventType)
		{
			base.Replace(oldEventType, newEventType);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is EventTypes) )
				return false;
			return base.Equals(other);
		}

		internal bool Equals(EventTypes other)
		{
			return base.Equals(other);
		}

		protected override Element CreateElement(Element parent, string key)
		{
			return ((ICatalogueElement) parent).Catalogue.CreateEventType((Catalogue) parent, key);
		}

		internal void Write(BinaryWriter writer)
		{
			((IBinarySerializable) this).Write(writer);
		}

		internal void Read(BinaryReader reader)
		{
			((IBinarySerializable) this).Read(reader);
		}
	}
}
