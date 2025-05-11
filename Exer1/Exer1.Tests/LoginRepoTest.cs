using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exer1.Tests
{
    public class LoginRepoTest
    {
        private LoginRepo repo { get; set; }

        public LoginRepoTest()
        {
            repo = new LoginRepo();
        }

        [Fact]
        public void AuthEmail_pass()
        {
            string email = "user1@example.com";
            string password = "password123";
            bool res = repo.authenticateEmail(email, password);
            Assert.True(res);
        }

        [Theory]
        [InlineData("user1","password123")] // incorrect format
        [InlineData("toget@hotmail.org","Mesa6&4")] // non-existant user
        [InlineData("user1@example.com","password333")] // incorrect password
        public void authEmail_fail(string email, string password)
        {
            bool res = repo.authenticateEmail(email, password);
            Assert.False(res);
        }

        [Fact]
        public void AuthUsername_pass()
        {
            string username = "testuser";
            string password = "securepass";
            bool res = repo.authenticateUsername(username, password);
            Assert.True(res);
        }

        [Theory]
        [InlineData("sample@email.org","complex!~@#")]
        [InlineData("toget","Mesa6&4")]
        [InlineData("testuser","incorrectPass")]
        public void authUsername_fail(string username, string password)
        {
            bool res = repo.authenticateUsername(username, password);
            Assert.False(res);
        }

        [Fact]
        public void IsEmail_pass()
        {
            string input = "create@art.com";
            bool res = repo.isEmail(input);
            Assert.True(res);
        }

        [Fact]
        public void IsEmail_fail()
        {
            string input = "creator";
            bool res = repo.isEmail(input);
            Assert.False(res);
        }
    }
}
