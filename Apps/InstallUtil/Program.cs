using System;
using System.IO;
using System.Reflection;

namespace LinkMe.InstallUtil
{
	internal class Program
	{
		private const string CommandCreatePerfCounters = "/createPerfCounters";
		private const string CommandDeletePerfCounters = "/deletePerfCounters";
		private const string CommandSynonymXlsToSql = "/synonymXlsToSql";
		private const string CommandRunSql = "/runSql";
        private const string CommandListTables = "/listTables";
        private const string CommandGenerateDataScript = "/genDataScript";
        private const string CommandHashPassword = "/hashPassword";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			try
			{
				if (args.Length == 0)
				{
                    System.Environment.ExitCode = 2;
					Usage();
					return;
				}

                System.Environment.ExitCode = 1;

				if (string.Compare(args[0], CommandCreatePerfCounters, true) == 0)
				{
					PerfCounterUtil.CreatePerfCounters(args);
				}
				else if (string.Compare(args[0], CommandDeletePerfCounters, true) == 0)
				{
					PerfCounterUtil.DeletePerfCounters(args);
				}
				else if (string.Compare(args[0], CommandSynonymXlsToSql, true) == 0)
				{
					SynonymSql.XlsFileToSqlFile(args);
				}
				else if (string.Compare(args[0], CommandRunSql, true) == 0)
				{
					RunSqlScripts.RunScriptList(args);
				}
                else if (string.Compare(args[0], CommandListTables, true) == 0)
                {
                    ListTables.PrintListInDeleteOrder(args);
                }
                else if (string.Compare(args[0], CommandGenerateDataScript, true) == 0)
                {
                    GenerateDataScript.PrintInsertStatements(args);
                }
                else if (string.Compare(args[0], CommandHashPassword, true) == 0)
                {
                    HashPasswordsUtil.HashPasswords(args);
                }
                else
				{
                    System.Environment.ExitCode = 2;
					Usage();
				}

                System.Environment.ExitCode = 0;
			}
			catch (FileNotFoundException ex)
			{
                Console.Error.WriteLine(ex.Message);
			}
			catch (DirectoryNotFoundException ex)
			{
				Console.Error.WriteLine(ex.Message);
			}
			catch (Exception ex)
			{
                System.Environment.ExitCode = 1;
                Console.Error.WriteLine(ex);
			}
		}

        internal static void Usage()
		{
			Console.WriteLine("Usage: {0} <command> <options>",
				Assembly.GetExecutingAssembly().GetName().Name);
			Console.WriteLine();
			Console.WriteLine("Commands:");
			Console.WriteLine("\t{0} <inputCsvFile> [outputSqlFile]\tGenerate an SQL insert script for"
				+ " synonyms from a CSV file.", CommandSynonymXlsToSql);
			Console.WriteLine("\t{0} <scriptDir> [server] [database] [login] [password]\tRun SQL scripts.",
				CommandRunSql);
            Console.WriteLine("\t{0} [server] [database] [format] Lists tables in delete order.",
                CommandListTables);
            Console.WriteLine("\t{0} [server] [database] [login] [password] Generates an INSERT SQL script.",
                CommandGenerateDataScript);
            Console.WriteLine("\t{0} <plaintext> [...]\t\tPrint the hashes for the specified plaintext.",
                CommandHashPassword);
            Console.WriteLine();
			Console.WriteLine("Options:");
			Console.WriteLine("\t<inputCsvFile>\t\tPath to the CSV containing one set of synonyms per line.");
			Console.WriteLine("\t<outputSqlFile>\t\tPath to the SQL file for inserting synonyms. If omitted,"
				+ " the input file name with the extension '.SQL' is used.");
			Console.WriteLine("\t<scriptDir>\t\tDirectory containing SQL scripts to run.");
			Console.WriteLine("\t<server>\t\tSQL server to connect to. Defaults to '{0}'",
				RunSqlScripts.DefaultServer);
			Console.WriteLine("\t<database>\t\tSQL database to use. Default to '{0}'",
				RunSqlScripts.DefaultDatabase);
			Console.WriteLine("\t<login>\t\t\tSQL server login name. If not specified integrated security is used.");
			Console.WriteLine("\t<password>\t\tSQL server password.");
            Console.WriteLine("\t<format>\t\tFormat string for the output.");
		}
    }
}
