using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Common.Integration
{
	public static class ExcelDataHelper
	{
        [Flags]
        public enum ExcelOptions
        {
            None,
            HeaderRow,
            Writeable
        }

	    public static IDbConnection GetOpenDbConnection(string filePath, ExcelOptions options)
		{
            // Without the "IMEX=1" Excel returns NULL for all column values except the first column.

			OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;"
                + "Data Source={0};Extended Properties=\"Excel 8.0; HDR={1}; {2}\";", filePath,
                (options & ExcelOptions.HeaderRow) == ExcelOptions.HeaderRow ? "Yes" : "No",
                (options & ExcelOptions.Writeable) == ExcelOptions.Writeable ? "" : "IMEX=1"));

            try
            {
                conn.Open();
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.IndexOf("provider is not registered") != -1)
                {
                    throw new ApplicationException("This operation cannot be run in x64 mode."
                        + " Run the process as 32-bit (use CorFlags.exe or a 32-bit machine).", ex);
                }

                throw;
            }

	        return conn;
		}

        public static int FindStartingRow(IDbConnection conn, string worksheetName)
		{
			const int rowsToCheck = 20;

			Debug.Assert(conn != null, "conn != null");
			Debug.Assert(conn.State == ConnectionState.Open, "conn.State == ConnectionState.Open");

			using (IDbCommand command = conn.CreateCommand())
			{
				command.CommandText = string.Format("SELECT TOP {0} * FROM [{1}$]", rowsToCheck, worksheetName);

				using (IDataReader reader = command.ExecuteReader())
				{
					int row = 2; // It should be 1, but it's 2. Just how Excel is!

					while (reader.Read())
					{
						object[] values = new object[reader.FieldCount];
						reader.GetValues(values);

						if (!DatabaseHelper.AreAllNull(values))
							return row;

						row++;
					}
				}
			}

			throw new ApplicationException("Failed to find a header within the first "
				+ rowsToCheck + " rows of the spreadsheet.");
		}

		/// <param name="columnStatus">An array of flags for each column, in the same order as columnNames.
		/// On input contains true if the column is mandatory, false if it is optional (ie. the column itself
		/// can be absent from the spreadsheet). If null on input all columns are considered mandatory.
		/// On output contains true if the column is present, false if it is absent.</param>
        public static void VerifyColumnNames(IDbConnection conn, string adoTableName, string[] columnNames,
			bool[] columnStatus)
		{
			Debug.Assert(conn != null && columnNames != null && columnNames.Length > 0,
				"conn != null && columnNames != null && columnNames.Length > 0");
			Debug.Assert(columnStatus == null || columnStatus.Length == columnNames.Length,
				"columnStatus == null || columnStatus.Length == columnNames.Length");
			Debug.Assert(conn.State == ConnectionState.Open, "conn.State == ConnectionState.Open");

			ArrayList missing = new ArrayList();

			using (IDbCommand command = conn.CreateCommand())
			{
				command.CommandText = "SELECT TOP 1 * FROM " + adoTableName;

				using (IDataReader reader = command.ExecuteReader())
				{
					if (!reader.Read())
						throw new ApplicationException("There are no rows in the source spreadsheet.");

					for (int n = 0; n < columnNames.Length; n++)
					{
						string columnName = columnNames[n];
						bool mandatory = (columnStatus == null || columnStatus[n]);
						bool found = false;

						for (int f = 0; f < reader.FieldCount; f++)
						{
							if (reader.GetName(f) == columnName)
							{
								found = true;
								break;
							}
						}

						if (columnStatus != null)
						{
							columnStatus[n] = found;
						}

						if (mandatory && !found)
						{
							missing.Add(columnName);
						}
					}
				}
			}

			if (missing.Count > 0)
			{
				string[] missingArray = (string[])missing.ToArray(typeof(string));

				if (missingArray.Length == columnNames.Length)
				{
					throw new ApplicationException("The spreadsheet does not have any of the expected colums: "
						+ string.Join(", ", missingArray));
				}
				else
				{
					throw new ApplicationException("The spreadsheet is missing some of the expected columns: "
						+ string.Join(", ", missingArray));
				}
			}
		}

		// Must not return null.
        public static string GetOptionalString(object[] values, int i, string[] columnNames)
		{
			Debug.Assert(values != null && columnNames != null, "values != null && columnNames != null");
			Debug.Assert(values.Length == columnNames.Length, "values.Length == columnNames.Length");
			Debug.Assert(i >= 0 && i < values.Length, "i >= 0 && i < values.Length");

			object value = values[i];

			string str = value as string;
			if (str != null)
				return str;

			if (value is DBNull || value == null)
				return "";

			throw new ApplicationException(string.Format("The value in column {0} ({1}) is of type {2} when"
				+ " a string was expected.", i, columnNames[i], value.GetType().Name));
		}

        public static int GetOptionalInt(object[] values, int i, string[] columnNames, int defaultValue)
		{
			Debug.Assert(values != null && columnNames != null, "values != null && columnNames != null");
			Debug.Assert(values.Length == columnNames.Length, "values.Length == columnNames.Length");
			Debug.Assert(i >= 0 && i < values.Length, "i >= 0 && i < values.Length");

			object value = values[i];
			if (value is DBNull || value == null)
				return defaultValue;

			try
			{
				return Convert.ToInt32(value);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("The value in column {0} ({1}) could not be"
					+ " converted to an integer.", i, columnNames[i]), ex);
			}
		}
	}
}
