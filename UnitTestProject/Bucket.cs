using kurs060324;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class Bucket
    {
        string us = "";
        [TestMethod]
        public void Buckets()
        {
            var form = new Form4(us);
            // Act
            form.f();
        }
    }
}
