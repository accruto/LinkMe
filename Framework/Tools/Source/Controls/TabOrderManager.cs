using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Specialized;

namespace LinkMe.Framework.Tools.Controls
{
	public enum TabOrder
	{
		None,
		AcrossFirst,
		DownFirst
	}

	public class TabOrderManager
		:	Component,
			ISupportInitialize
	{
		public TabOrderManager()
		{
		}

		public TabOrderManager(Control control)
		{
			m_control = control;
		}

		public TabOrderManager(ContainerControl container, TabOrder order)
			:	this(container)
		{
			Order = order;
		}

		public void SetTabOrder()
		{
			if ( m_control == null )
				return;

			int tabIndex = 0;
			ArrayList controlsSorted = new ArrayList(m_control.Controls);
			ArrayList controlsBottomDocked = new ArrayList();
			controlsSorted.Sort(new TabOrderComparer(m_order));

			foreach ( Control control in controlsSorted )
			{
				// Controls docked on the bottom.

				if ( control.Dock == DockStyle.Bottom )
				{
					controlsBottomDocked.Add(control);
					continue;
				}

				control.TabIndex = tabIndex++;
				if ( control.Controls.Count > 0 )
					new TabOrderManager(control as Control).SetTabOrder(m_order);
			}

			// Controls docked on the bottom.

			controlsBottomDocked.Sort(new TabOrderComparer(m_order));
			foreach ( Control control in controlsBottomDocked )
				control.TabIndex = tabIndex++;
		}

		public void SetTabOrder(TabOrder order)
		{
			Order = order;
			SetTabOrder();
		}

		#region Properties

		/// <summary>
		/// The tab order to apply to the container control. The default is AcrossFirst.
		/// </summary>
		[DefaultValue(m_defaultOrder)]
		public TabOrder Order
		{
			get { return m_order; }
			set { m_order = value; }
		}

		/// <summary>
		/// The container control to which the specified tab order is applied.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public Control Control
		{
			get { return m_control; }
			set { m_control = value; }
		}

		#endregion

		#region ISupportInitialize Members

		public void BeginInit()
		{
		}

		public void EndInit()
		{
			SetTabOrder();
		}

		#endregion

		private class TabOrderComparer
			:	IComparer
		{
			public TabOrderComparer(TabOrder order)
			{
				m_order = order;
			}

			public int Compare(object x, object y)
			{
				Control control1 = x as Control;
				Control control2 = y as Control;
				
				if ( control1 == null || control2 == null )
					return 0;
				if ( m_order == TabOrder.None )
					return 0;

				if ( m_order == TabOrder.AcrossFirst )
				{
					if ( control1.Top < control2.Top )
						return -1;
					else if ( control1.Top > control2.Top )
						return 1;
					else
						return control1.Left.CompareTo(control2.Left);
				}
				else
				{
					if ( control1.Left < control2.Left )
						return -1;
					else if ( control1.Left > control2.Left )
						return 1;
					else
						return control1.Top.CompareTo(control2.Top);
				}
			}

			private TabOrder m_order;
		}

		private const TabOrder m_defaultOrder = TabOrder.AcrossFirst;

		private Control m_control = null;
		private TabOrder m_order = m_defaultOrder;
	}
}
