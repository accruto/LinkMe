using System;
using System.Diagnostics;
using System.Web;
using System.Web.Compilation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest
{
    /// <summary>
    /// This "test" precompiles all ASPX files in the application. When running the entire test suite from
    /// Visual Studio it runs first and the rest of the test cases run slightly faster (about 45 seconds saved).
    /// In the release build this test is not run, since during the "real" build there is a separate step to
    /// precompile the application.
    /// </summary>
    [TestClass]
    public class AAspxPrecompile
        : WebTestClass
    {
        public AAspxPrecompile()
        {
        }

#if DEBUG
        [TestMethod]
        public void PrecompileAspx()
        {
            PrecompileAspx(Application.Current.ApplicationPath);
        }
#endif

        public static void PrecompileAspx(string appVirtualDir)
        {
            Console.Write("Precompiling ASPX files in virtual directory '" + appVirtualDir + "'...");
            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                ClientBuildManager buildManager = new ClientBuildManager(appVirtualDir, null);
                buildManager.PrecompileApplication();
            }
            catch (HttpParseException ex)
            {
                throw new ApplicationException("Failed to precompile ASPX file '" + ex.VirtualPath + "' - error on line "
                    + ex.Line + ": " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to precompile ASPX files in '" + appVirtualDir + "'.", ex);
            }

            stopwatch.Stop();
            Console.WriteLine(" Done ({0:n1} seconds)", stopwatch.Elapsed.TotalSeconds);
        }
    }
}
