using System;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	public interface IElementBrowserInfo : IComparable
	{
		/// <summary>
		/// True if the element node is checked.
		/// </summary>
		bool Checked { get; set; }
		/// <summary>
		/// The name to be used for telling the user about this element, for example exception messages.
		/// </summary>
		string DisplayName { get; }
		/// <summary>
		/// The text value of this element's node.
		/// </summary>
		string NodeText { get; }
		/// <summary>
		/// The text that should be shown in the description pane.
		/// </summary>
		DescriptionText Description { get; }
		/// <summary>
		/// The index of this element's icon in the ImageList supplied by the settings object.
		/// </summary>
		int ImageIndex { get; }
	}
}
