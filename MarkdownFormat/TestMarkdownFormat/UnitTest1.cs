using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMarkdownFormat
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MarkdownFormat.UdoUtils.TestTypeNames();
        }
    }
}
