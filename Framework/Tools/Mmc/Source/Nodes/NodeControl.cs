using System.Diagnostics;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Mmc
{
	internal class NodeControl
		:	Control
	{
		private SnapinNode m_node;

		public NodeControl(SnapinNode node)
		{
			m_node = node;
			CreateHandle();
		}

		public void RefreshNode()
		{
			m_node.RefreshNode();
		}

		public void RefreshNodeResults()
		{
			m_node.RefreshResults();
		}

		public void InvokeRefreshNode()
		{
			Invoke(new MethodInvoker(RefreshNode));
		}

		public void InvokeRefreshNodeResults()
		{
			Invoke(new MethodInvoker(RefreshNodeResults));
		}
	}
}
