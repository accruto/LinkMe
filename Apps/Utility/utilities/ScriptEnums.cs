using System;

namespace LinkMe.Utility.Utilities
{
    public enum RunScriptResult
    {
        Succeeded,
        Failed,
        Skipped
    }

    [Flags]
    public enum RunScriptOptions
    {
        /// <summary>
        /// Default options.
        /// </summary>
        None = 0,
        /// <summary>
        /// Attempt to run all statements in the script even if errors occur. The default is to stop on
        /// the first error.
        /// </summary>
        ContinueOnError = 1,
        /// <summary>
        /// Use the DatabaseScript table (if it exists) to determine whether the script needs to be run
        /// and add it to the table. The default is to always run the script and not add it to the table.
        /// </summary>
        UseDatabaseScriptRecords = 2
    }
}
