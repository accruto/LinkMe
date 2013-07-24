using System.Windows.Forms;

namespace LinkMe.Framework.Configuration
{
	#region IElement

	public interface IElement
	{
		string Name { get; }
		void Refresh();
		void Commit();
		IElement Parent { get; }
	}

	#endregion

	#region IQualifiedElement

	public interface IQualifiedElement
		:	IElement
	{
		string FullName { get; }
	}

	#endregion

	#region ICommitConnection

	public enum CommitStatus
	{
		Continue,
		DoNotCommitChildren,
		Discontinue
	}

	public interface ICommitConnection
	{
		void Commit();
	}

	#endregion

	#region IElementIdentification

	public interface IElementIdentification
	{
		string CataloguePath { get; }
		string ElementPath { get; }
		string ElementKey { get; }
		string ElementType { get; }
	}

	#endregion

	#region IElementReference

	public interface IElementReference
	{
		string FullyQualifiedReference { get; }
	}

	#endregion

	#region IContainerInitialise

	public interface IContainerInitialise
	{
        string InitialisationString { get; set; }
		bool Prompt(IWin32Window parent);
	}

	#endregion

	#region IRepositoryLink

	public interface IRepositoryLink
	{
		string Name { get; set; }
		string RepositoryType { get; set; }
        string InitialisationString { get; set; }
        bool IsLocal { get; set; }
        string Computer { get; set; }
        bool IsReadOnly { get; set; }
        bool Prompt(IWin32Window parent);
		void AddReference(IRepositoryLink referenceLink, bool ignoreVersions);

        T GetConnection<T>() where T : class;
	}

	#endregion
}
