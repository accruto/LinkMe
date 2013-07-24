using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Environment.Util.Commands
{
    public abstract class ReportsCommand
        : UtilCommand
    {
        protected static string GetPath(string path)
        {
            // Fix up the missing "/" from the start of paths.

            if (!path.StartsWith("/"))
                path = "/" + path;
            return path;
        }

        protected static string[] GetDataSources(IEnumerable<string> values)
        {
            return (from v in values
                    select GetPath(v)).ToArray();
        }
    }
}
