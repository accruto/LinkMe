using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using LinkMe.Common.Integration;
using LinkMe.Framework.Utility;

namespace LinkMe.InstallUtil
{
	public static class SynonymSql
	{
        private static readonly char[] DisallowedCharacters = new[] { '\"', '(', ')', ',', ':', ';' };
        private const string listStartSentinel = "*LISTSTART*";

		public static void XlsFileToSqlFile(string[] args)
		{
			if (args.Length < 2 || args.Length > 3)
			{
				Program.Usage();
				return;
			}

			XlsFileToSqlFile(args[1], (args.Length == 3 ? args[2] : null));
		}

		/// <summary>
		/// Generates an SQL script file to insert synonym values from a CSV file containing
		/// one set of synonyms per line. The SQL file is overwritten if it already exists.
		/// </summary>
		public static void XlsFileToSqlFile(string inputXlsFile, string outputSqlFile)
		{
			if (string.IsNullOrEmpty(inputXlsFile))
				throw new ArgumentException("The input XLS file must be specified.", "inputXlsFile");
			if (!File.Exists(inputXlsFile))
			{
                throw new FileNotFoundException("The input XLS file, '" + inputXlsFile + "', does not exist.",
					inputXlsFile);
			}

			if (string.IsNullOrEmpty(outputSqlFile))
			{
				outputSqlFile = Path.ChangeExtension(inputXlsFile, "sql");
			}

			if (string.Compare(inputXlsFile, outputSqlFile, true) == 0)
				throw new ArgumentException("The output file name must not be the same as the input file name.");

			int ignoredSets;
            IList<IList<string>> synonymSets = ReadSynonymSets(inputXlsFile, out ignoredSets);

            if (synonymSets.Count == 0)
            {
                if (ignoredSets == 0)
                {
                    Console.WriteLine("ERROR: The input file does contain any synonym sets.");
                }
                else
                {
                    Console.WriteLine("ERROR: The input file does contain any valid synonym sets."
                        + " {0} invalid sets were ignored.", ignoredSets);
                }
            }
            else
            {
                string[] duplicates = FindDuplicates(synonymSets);
                if (duplicates != null && duplicates.Length > 0)
                {
                    Console.WriteLine("ERROR: The following terms are duplicated between sets: '{0}'.",
                        string.Join("', '", duplicates));
                    return;
                }

                using (StreamWriter writer = new StreamWriter(outputSqlFile, false))
                {
                    WriteSqlScriptForSynonymSets(writer, synonymSets);
                }

                Console.WriteLine();
                Console.WriteLine("Excel file '{0}' was successfully converted to SQL script '{1}'."
                    + " {2} sets of synonym were processed, {3} invalid sets were ignored.",
                    inputXlsFile, outputSqlFile, synonymSets.Count, ignoredSets);
            }
		}

        private static string[] FindDuplicates(IList<IList<string>> strings)
		{
            Dictionary<string, object> dictionary = new Dictionary<string, object>(
                StringComparer.CurrentCultureIgnoreCase);
            Dictionary<string, object> duplicates = new Dictionary<string, object>(
                StringComparer.CurrentCultureIgnoreCase);

			foreach (IList<string> stringList in strings)
			{
				foreach (string str in stringList)
				{
					if (dictionary.ContainsKey(str))
					{
						duplicates[str] = null;
					}
					else
					{
						dictionary.Add(str, null);
					}
				}
			}

			if (duplicates.Count == 0)
				return null;

			string[] duplicateArray = new string[duplicates.Count];
			duplicates.Keys.CopyTo(duplicateArray, 0);

			return duplicateArray;
		}

        private static void WriteSqlScriptForSynonymSets(TextWriter writer, IList<IList<string>> synonymSets)
		{
			const string tableName = "EquivalentTerms";
			const string columnTerm = "searchTerm";
			const string columnGroupId = "equivalentGroupId";

			Debug.Assert(synonymSets.Count > 0, "synonymSets.Length > 0");

			writer.WriteLine("DECLARE @equivalentId UNIQUEIDENTIFIER");
			writer.WriteLine();
			writer.WriteLine("DELETE FROM dbo." + tableName);

            foreach (IList<string> synonymSet in synonymSets)
			{
				writer.WriteLine();
				writer.WriteLine("SET @equivalentId = '{0}'", Guid.NewGuid().ToString("D"));
				writer.WriteLine();

				foreach (string synonym in synonymSet)
				{
					writer.WriteLine("INSERT INTO dbo." + tableName
						+ "(" + columnTerm + ", " + columnGroupId + ") VALUES ('{0}', @equivalentId)",
						TextUtil.EscapeSqlText(synonym));
				}
			}

			writer.WriteLine();
			writer.WriteLine("GO");
		}

        private static IList<IList<string>> ReadSynonymSets(string inputXlsFile, out int ignoredSets)
		{
			// Each line should contain a comma-separated set of terms. Lines with only one term are ignored,
			// so they can be used as headings.

            IList<IList<string>> sets = new List<IList<string>>();
			ignoredSets = 0;

			// Use the MS text ODBC driver to read the CSV file, so that commas inside the text and
			// escaped quotes are handled correctly.

            using (IDbConnection connection = ExcelDataHelper.GetOpenDbConnection(inputXlsFile,
                ExcelDataHelper.ExcelOptions.None))
			{
				using (IDbCommand command = connection.CreateCommand())
				{
                    command.CommandText = "SELECT * FROM [Sheet1$]";

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (!FindStartSentinel(reader))
                        {
                            throw new ApplicationException("The input file does not contain a start marker. It"
                                + " must contain a row with " + listStartSentinel + " in the first column.");
                        }

                        while (reader.Read())
                        {
                            ReadSynonymRow(reader, sets, ref ignoredSets);
                        }
                    }
				}
			}

			return sets;
		}

	    private static void ReadSynonymRow(IDataReader reader, IList<IList<string>> sets, ref int ignoredSets)
	    {
            List<string> terms = new List<string>();

            object[] rowValues = new object[reader.FieldCount];
            reader.GetValues(rowValues);

            for (int i = 0; i < rowValues.Length; i++)
	        {
                string term = rowValues[i] as string;

                if (term != null)
                {
                    term = term.Trim();

                    if (term.Length > 0)
                    {
                        terms.Add(term);
                    }
                }
	        }

	        if (terms.Count > 1)
	        {
	            if (ValidateTerms(terms))
	            {
	                sets.Add(terms);
	            }
	            else
	            {
	                ignoredSets++;
	                Console.WriteLine("WARNING: skipping invalid set of synonyms: '{0}'.",
	                    string.Join("', '", terms.ToArray()));
	            }
	        }
	    }

	    private static bool FindStartSentinel(IDataReader reader)
	    {
            while (reader.Read())
            {
                if (reader.FieldCount > 0 && (reader.GetValue(0) as string) == listStartSentinel)
                    return true;
            }
            
            return false;
	    }

	    private static bool ValidateTerms(IList<string> terms)
		{
            Debug.Assert(terms.Count > 1 && terms.Count < 100, "terms.Length > 1 && terms.Length < 100");

			bool valid = true;
			foreach (string term in terms)
			{
                // Check for diallowed characters.

				int index = term.IndexOfAny(DisallowedCharacters);
				if (index != -1)
				{
					Console.WriteLine("ERROR: Equivalent term '{0}' contains a disallowed" 
						+ " character, '{1}' at index {2}.", term, term[index], index);
					valid = false;
				}
			}

			if (!valid)
				return false;

            for (int i = 0; i < terms.Count; i++)
			{
                for (int j = 0; j < terms.Count; j++)
				{
					if (j == i)
						continue;

					if (string.Compare(terms[i], terms[j], true) == 0)
					{
						if (i < j)
						{
							// Only print the error for one of the duplicate terms, not both.

							Console.WriteLine("ERROR: the set contains a duplicate term: '{0}'.", terms[i]);
						}
						valid = false;
					}
					else if (IsSubsetOf(terms[i], terms[j]))
					{
						// Print out a warning about terms that are subsets of other terms, but still
						// allow them to be imported.

						Console.WriteLine("WARNING: term '{0}' is a subset of term '{1}'.",
							terms[i], terms[j]);
					}
				}
			}

			return valid;
		}

		private static bool IsSubsetOf(string subTerm, string termToSearch)
		{
			Debug.Assert(subTerm != termToSearch, "subTerm != termToSearch");

			// One term is a subset of the other if it contains all the words of the other term, in the same
			// order, plus more words. Only whole words are considered. Eg. "project manager" is a subset of
			// "senior project manager", but "account" is not a subset of "accountant".

			return (TextUtil.IndexOfWholeWordIgnoreCase(termToSearch, subTerm, 0) != -1);
		}
	}
}
