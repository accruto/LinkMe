using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Framework.Utility.Test
{
    [TestClass]
    public class TextUtilTests
    {
        [TestMethod]
        public void EscapeRtfText()
        {
            Assert.IsNull(TextUtil.EscapeRtfText(null));
            Assert.AreEqual("", TextUtil.EscapeRtfText(""));
            Assert.AreEqual("Plain text (no escaping) [here]", TextUtil.EscapeRtfText("Plain text (no escaping) [here]"));
            Assert.AreEqual("Some \\{braces\\} here", TextUtil.EscapeRtfText("Some {braces} here"));
            Assert.AreEqual("And sla\\\\shes/here", TextUtil.EscapeRtfText("And sla\\shes/here"));
        }
    }
}
