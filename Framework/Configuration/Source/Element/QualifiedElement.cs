using System.Collections;

namespace LinkMe.Framework.Configuration
{
	public abstract class QualifiedElement
		:	Element,
		IQualifiedElement
	{
		#region Constructors

		protected QualifiedElement(IQualifiedElement parent, string name)
			:	base(parent, name)
		{
		}

		#endregion

		#region Properties

		public string FullName
		{
			get
			{
				if ( Parent == null )
				{
					return Name;
				}
				else
				{
					string parentFullName = Parent.FullName;
					return (parentFullName.Length == 0) ? Name : parentFullName + "." + Name;
				}
			}
		}

		#endregion

		#region Relationships

		public new IQualifiedElement Parent
		{
			get { return (IQualifiedElement) base.Parent; }
		}

		IElement IElement.Parent
		{
			get { return Parent; }
		}

		#endregion

		#region Operations

		public override int GetHashCode()
		{
			return FullName.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is QualifiedElement) )
				return false;
			if ( !base.Equals(other) )
				return false;
			return FullName == ((QualifiedElement) other).FullName;
		}

		#endregion
	}
}
