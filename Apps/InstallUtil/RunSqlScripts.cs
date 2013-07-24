using System;
using System.IO;
using LinkMe.Utility.Utilities;

namespace LinkMe.InstallUtil
{
	/// <summary>
	/// Runs a list of SQL scripts in order. Stops if any of the scripts fail.
	/// </summary>
	public static class RunSqlScripts
	{
        public const string DefaultServer = ScriptHelper.DefaultScriptListServer;
        public const string DefaultDatabase = ScriptHelper.DefaultScriptListDatabase;

        private const string scriptListFilename = "install.db.txt";

		public static void RunScriptList(string[] args)
		{
			if (args.Length < 2 || args.Length > 6)
			{
				Program.Usage();
				return;
			}

            string scriptList = Path.Combine(args[1], scriptListFilename);
            ScriptHelper.RunScriptList(Console.Out, Console.Error, scriptList, (args.Length > 2 ? args[2] : null),
                (args.Length > 3 ? args[3] : null), (args.Length > 4 ? args[4] : null), (args.Length > 5 ? args[5] : null));
		}
	}
}
