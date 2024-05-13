using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using kurs060324;

namespace UnitTestProject
{
    [TestClass]
    public class database
    {
        [TestMethod]
        public void OpensConnection()
        {
            // Arrange and Act
            DB db = new DB();
            db.openConnection();
            // Assert
            Assert.IsTrue(db.con.State == ConnectionState.Open);
            db.closeConnection();
        }
    }
}
