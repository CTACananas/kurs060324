using kurs060324;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class New
    {
        string us = "";
        [TestMethod]
        public void News()
        {
            var form = new Form3(us);
            // Act
            form.Tov();
        }
    }
}
