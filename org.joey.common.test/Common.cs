using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.Joey.Common;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace org.joey.common.test
{
    [TestClass]
    public class Common
    {
        [TestMethod]
        public void FixTags()
        {
            var context = File.ReadAllText("WaitingFix.html");
            context.FixHtmlTags();           
        }
    }
}
