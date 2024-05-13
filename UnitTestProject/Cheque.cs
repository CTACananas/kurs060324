using kurs060324;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class Cheque
    {
        string us = "";
        [TestMethod]
        public void Cheques()
        {
            var form = new Form5(us);
            // Act
            form.LoadFullRowsFromDatabase(form.listBox1);;
        }
    }
}
