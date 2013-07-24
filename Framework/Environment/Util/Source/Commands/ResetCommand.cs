using System.Collections.Generic;
using System.Linq;
using LinkMe.Environment.CommandLines;
using LinkMe.Framework.Instrumentation.Connection.Wmi;
using LinkMe.Framework.Instrumentation.Management;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class ResetCommand
        : Command
    {
        public override void Execute()
        {
            var sourceOption = Options["source"] == null ? null : Options["source"].Values[0];

            var catalogue = GetCatalogue();
            if (sourceOption == null)
                Execute(catalogue.Namespaces);
            else
                Execute(catalogue.GetSearcher().GetSource(sourceOption));
            catalogue.Commit();
        }

        private static void Execute(Source source)
        {
            source.Parent.Remove(source, true);
        }

        private static void Execute(Namespaces namespaces)
        {
            var nss = new List<Namespace>();
            foreach (Namespace ns in namespaces)
                nss.Add(ns);

            foreach (var ns in nss)
            {
                var sources = new List<Source>();
                foreach (Source source in ns.Sources)
                    sources.Add(source);

                foreach (Source source in sources)
                    ns.Remove(source, true);
                Execute(ns.Namespaces);
            }
        }

        private static Catalogue GetCatalogue()
        {
            var repositoryReader = new WmiConnection(@"\\.\root\LinkMe");
            return repositoryReader.Read();
        }
    }
}
