using System.Windows.Forms;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	public interface IElementPropertyInfo : System.IComparable
	{
		object Element { get; }

		/// <summary>
		/// The name to be used for telling the user about this element, for example exception messages.
		/// </summary>
		string Name { get; }
		/// <summary>
		/// The index of this element's icon in the ImageList supplied by the settings object.
		/// </summary>
		int ImageIndex { get; }
		/// <summary>
		/// Returns the parent element for this element within the tree.
		/// </summary>
		IElementPropertyInfo Parent { get; }
		/// <summary>
		/// The child elements of this element, if any.
		/// </summary>
		IElementPropertyInfo[] Elements { get; }
		/// <summary>
		/// The property page for displaying the element information.
		/// </summary>
		ElementPropertyPage PropertyPage { get; }
		/// <summary>
		/// The object information for this element.
		/// </summary>
		IObjectPropertyInfo ObjectInfo { get; }
		/// <summary>
		/// Indicates whether the element can be deleted.
		/// </summary>
		bool CanDeleteElement { get; }
		/// <summary>
		/// Delete the element itself.
		/// </summary>
		void DeleteElement();
		/// <summary>
		/// Indicates whether a new child element can be created.
		/// </summary>
		bool CanCreateElement { get; }
		string CreateElementName { get; }
		IElementPropertyInfo CreateElement();
		bool CanRenameElement { get; }
		void RenameElement(string newName);
		void RefreshParentElement(object parentElement);

		string[] Views { get; }
		void ViewElement(string view);
		string CurrentView { get; }
	}
}
