using System.Globalization;
using System.Windows.Forms;

using LinkMe.Interop.Msdasc;
using LinkMe.Interop.Adodb;

namespace LinkMe.Framework.Tools
{
	/// <summary>
	/// Provides static methods for manipulating OLE DB connection strings.
	/// </summary>
	public sealed class OleDbUtil
	{
		private OleDbUtil()
		{
		}

		/// <summary>
		/// Prompts the user for an OLE DB connection string using the Data Link Properties dialog.
		/// </summary>
		/// <param name="initialString">The initial connection string to display in the dialog. May be null.</param>
		/// <param name="dialogParent">The control to be used as the parent of the Data Link Properties dialog or
		/// null to show it as a non-modal dialog.</param>
		/// <returns>The new connection configured by the user or null if the user clicked Cancel in the dialog.</returns>
		public static string PromptForConnectionString(string initialString, IWin32Window dialogParent)
		{
			return PromptForConnectionString(initialString, dialogParent, null, null);
		}

		/// <summary>
		/// Prompts the user for an OLE DB connection string using the Data Link Properties dialog.
		/// </summary>
		/// <param name="initialString">The initial connection string to display in the dialog. May be null.</param>
		/// <param name="dialogParent">The control to be used as the parent of the Data Link Properties dialog or
		/// null to show it as a non-modal dialog.</param>
		/// <param name="allowedProviderProgID">If the user is only allowed to select one provider the ProgID of
		/// that provider, otherwise null.</param>
		/// <param name="allowedProviderDisplayName">The name of the allowed provider, as shown on the Providers
		/// tab in the Data Link Properties dialog. This parameter is ignored if
		/// <paramref name="allowedProviderProgID"/> is null.</param>
		/// <returns>The new connection configured by the user or null if the user clicked Cancel in the dialog.</returns>
		public static string PromptForConnectionString(string initialString, IWin32Window dialogParent,
			string allowedProviderProgID, string allowedProviderDisplayName)
		{
			DataLinks datalinks = new DataLinksClass();
			datalinks.hWnd = (dialogParent == null ? 0 : (int)dialogParent.Handle);

			while (true)
			{
				Connection connection = new ConnectionClass();

				if (initialString != null && initialString.Length > 0)
				{
					// If the string doesn't have a Provider specified, but we have allowedProviderProgID then
					// set it.

					if (allowedProviderProgID != null && CultureInfo.CurrentCulture.CompareInfo.IndexOf(
						initialString, "provider", CompareOptions.IgnoreCase) < 0)
					{
						initialString = "Provider=" + allowedProviderProgID + ";" + initialString;
					}

					connection.ConnectionString = initialString;
				}
				else
				{
					// Default to MS SQL.
					connection.ConnectionString = "Provider=" + Constants.OleDb.MsSqlProviderProgID;
				}

				object refConnection = connection;

				if (datalinks.PromptEdit(ref refConnection))
				{
					connection = (Connection)refConnection;

					if (allowedProviderProgID == null)
						return connection.ConnectionString;

					// Check that the provider is the allowed one.

					if (connection.Provider == allowedProviderProgID || connection.Provider.StartsWith(allowedProviderProgID + "."))
						return connection.ConnectionString;

					// It's not - tell the user and prompt again.

					string displayName = (allowedProviderDisplayName == null || allowedProviderDisplayName.Length == 0
						? allowedProviderProgID : allowedProviderDisplayName);

					MessageBox.Show(dialogParent, "You must select '" + displayName + "' on the Provider tab.",
						"Edit database connection string", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
					return null;
			}
		}
	}
}
