using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Common;
using LinkMe.Domain.Location.Queries;
using LinkMe.Environment;
using LinkMe.Utility.Configuration;
using LinkMe.TaskRunner.Test.Tasks;
using LinkMe.Web;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest
{
    // The name of this class isn't entirely pointless - it's meant to be run early, so we don't waste time
    // if some tests are broken and R# goes through classes in alphabetical order.
    [TestClass]
    public class ATestOfTests : WebTestClass
    {
        [TestMethod]
        public void CheckWebServerEnvironment()
        {
            // Check that not only the current process, but also ASP.NET are running in dev.
            // It's possible to switch to UAT without restarting one of them, so eg. the tests run
            // in dev, while ASP.NET is still running in UAT.

            Assert.AreEqual(ApplicationEnvironment.Dev, RuntimeEnvironment.Environment);

            var url = NavigationManager.GetUrlForPage<dev>(dev.ActionParam, dev.ActionGetEnvironment);
            Get(url);

            Assert.AreEqual("dev", Browser.CurrentPageText, "The ASP.NET process is not running in the dev environment.");
        }

        [TestMethod]
        public void TestThatAllTestMethodsThatStartWithTestHaveTheTestAttribute()
        {
            var badMethods = new List<string>();

            AddBadTestMethods(typeof(Domain.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Domain.Roles.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Domain.Users.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Query.Search.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Query.Search.Engine.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Query.Reports.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(WebTestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Agents.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Api.Test.WebTestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Asp.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Integration.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Management.Test.WebTestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Presentation.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Services.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Utility.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Web.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(Apps.Workflow.Test.TestClass).Assembly, badMethods);
            AddBadTestMethods(typeof(TaskTests).Assembly, badMethods);

            if (badMethods.Count > 0)
            {
                Assert.Fail("The following test methods are not decorated with the [TestMethod] attribute:\r\n\r\n"
                    + string.Join("\r\n", badMethods.ToArray()));
            }
        }

        [TestMethod, Ignore]
        public void CheckPageNamespaceConflicts()
        {
            // Ensure that no ASPX/ASHX page has the same name as a namespace - this makes it a pain
            // to code unit tests, because you often have to specify the fully qualified type name of
            // the page.

            IDictionary<string, string> badPages = new Dictionary<string, string>();

            AddBadPages(badPages, typeof(Global).Assembly,
                typeof(ApplicationContext).Assembly, typeof(DomainConstants).Assembly,
                typeof(ILocationQuery).Assembly, typeof(ControlUtils).Assembly,
                typeof(Global).Assembly, typeof(ITask).Assembly, typeof(ATestOfTests).Assembly);

            if (badPages.Count > 0)
            {
                var message = badPages.Aggregate("The following page names conflict with namespace names (which is bad!):\r\n", (current, kvp) => current + ("\r\n" + kvp.Key + " -> " + kvp.Value));
                Assert.Fail(message);
            }
        }

        private static void AddBadTestMethods(Assembly assembly, ICollection<string> badMethods)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(TestClassAttribute), true).Length > 0)
                {
                    // Found a test fixture, check each method.

                    MethodInfo[] methdods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    foreach (MethodInfo method in methdods)
                    {
                        if (method.Name != "TestInitialize" && method.Name != "TestCleanup" && method.Name != "TestClassInitialize")
                        {
                            if (method.Name.StartsWith("Test"))
                            {
                                // Found a method name starting with "Test - does it have the [TestMethod] attribute?

                                if (method.GetCustomAttributes(typeof(TestMethodAttribute), false).Length == 0)
                                {
                                    badMethods.Add(string.Format("{0}.{1}", type.FullName, method.Name));
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void AddBadPages(IDictionary<string, string> badPages, Assembly pagesAssembly,
            params Assembly[] namespacesAssemblies)
        {
            IDictionary<string, string> pageNames = new Dictionary<string, string>(); // name -> full name
            AddPageNames(pageNames, pagesAssembly);

            IDictionary<string, string> namespaceNames = new Dictionary<string, string>(); // name part -> full name
            foreach (Assembly namespaceAssembly in namespacesAssemblies)
            {
                AddNamespaceNames(namespaceNames, namespaceAssembly);
            }

            foreach (string pageName in pageNames.Keys)
            {
                string namespaceName;
                if (namespaceNames.TryGetValue(pageName, out namespaceName))
                {
                    badPages.Add(pageNames[pageName], namespaceName);
                }
            }
        }

        private static void AddPageNames(IDictionary<string, string> names, Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (typeof(IHttpHandler).IsAssignableFrom(type))
                {
                    // If there are multiple pages with the same base name add them all - the value is only
                    // used to display to the user.

                    string existingName;
                    if (names.TryGetValue(type.Name, out existingName))
                    {
                        names[type.Name] = existingName + ", " + type.FullName;
                    }
                    else
                    {
                        names.Add(type.Name, type.FullName);
                    }
                }
            }
        }

        private static void AddNamespaceNames(IDictionary<string, string> names, Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                string ns = type.Namespace;
                if (ns != null)
                {
                    foreach (string nsPart in ns.Split('.'))
                    {
                        names[nsPart] = ns;
                    }
                }
            }
        }
    }
}
