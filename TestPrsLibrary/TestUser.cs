using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpToSql;

namespace TestPrsLibrary
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void TestGetAllUsers()
        {
            try
            {
                var users = User.GetAllUsers();
                Assert.IsInstanceOfType(users, typeof(User[]), "users is not a user[]");
                Assert.AreEqual(6, users.Length, "There should be 6 users in my db");
                var firstUser = users[0];
                Assert.AreEqual(1, firstUser.Id);
                Assert.AreEqual("user1", firstUser.Username);
                Assert.AreEqual("pass1", firstUser.Password);
                Assert.IsNull(firstUser.Phone);
                Assert.IsFalse(firstUser.IsReviewer);
                Assert.IsTrue(firstUser.IsAdmin);

                var lastUser = users[users.Length - 1];
                Assert.AreEqual(18, lastUser.Id);
                Assert.AreEqual("test321", lastUser.Username);
                Assert.AreEqual("123", lastUser.Password);
                Assert.AreEqual("a", lastUser.Phone);
                Assert.IsFalse(lastUser.IsReviewer);
            }
            catch(Exception)
            {
                Assert.Fail("May be a open connection problem");
            }
        }
    }
}
