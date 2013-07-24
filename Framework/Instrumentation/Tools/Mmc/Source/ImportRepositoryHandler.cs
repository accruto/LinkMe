using System.Windows.Forms;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Configuration.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Tools.Controls;
using LinkMe.Framework.Tools.Mmc;

namespace LinkMe.Framework.Instrumentation.Tools.Mmc
{
	public class ImportRepositoryHandler
	{
		public ImportRepositoryHandler(RepositoryType repositoryType, IRepositoryLink repositoryLink, IWin32Window parent)
		{
			m_repositoryType = repositoryType;
			m_repositoryLink = repositoryLink;
			m_parent = parent;
		}

		public void Import(object item, SnapinNode node)
		{
			try
			{
				// Need to create a reader.

                IRepositoryReader fromReader = m_repositoryType.GetRepositoryConnection<IRepositoryReader>(true, m_parent);
				if ( fromReader != null )
				{
					// Save selection.

					string selectedPath = ((SnapinNode) node.Snapin.CurrentScopeNode).GetPathRelativeTo(node);

					// Run it.

                    IRepositoryWriter toWriter = m_repositoryLink.GetConnection<IRepositoryWriter>();
					ImportRepositoryResultsRunner runner = new ImportRepositoryResultsRunner(fromReader, toWriter);
					runner.Run();

					using ( new LongRunningMonitor(node.Snapin) )
					{
						// Refresh the catalogue itself.

						IRepositoryConnection repositoryConnection = m_repositoryLink as IRepositoryConnection;
						if ( repositoryConnection != null )
						{
							using (ConnectionState state = new ConnectionState())
							{
								repositoryConnection.Connect(state).Refresh();
							}
						}

						// Refresh the node and selection.

						node.Refresh(true);
						SnapinNode selectedNode = node.FindNodeInTree(selectedPath);
						if ( selectedNode != null )
							selectedNode.Select();
					}
				}
			}
			catch ( System.Exception e )
			{
				new ExceptionDialog(e, "The following exception has occurred while trying to read the information:").ShowDialog();
			}
		}

		private RepositoryType m_repositoryType;
		private IRepositoryLink m_repositoryLink;
		private IWin32Window m_parent;
	}
}
