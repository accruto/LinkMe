using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public class StandardJobTests
        : JobTests
    {
        protected override Guid? GetIntegratorUserId()
        {
            return null;
        }
    }
}