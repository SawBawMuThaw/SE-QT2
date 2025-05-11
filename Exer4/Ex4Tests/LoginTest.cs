using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex4Tests
{
    public class LoginTest
    {
        [Fact]
        public void login_fail()
        {
            IWebDriver driver = new ChromeDriver();

            string url = "http://localhost:5261/";
            driver.Navigate().GoToUrl(url);

            string email = "wrongemail@work.org";
            string username = "wrong user";
            string password = "wrong passwords";

            IWebElement txtemail = driver.FindElement(By.Id("Email"));
            IWebElement txtusername = driver.FindElement(By.Id("Username"));
            IWebElement txtPwd = driver.FindElement(By.Id("Password"));
            IWebElement loginform = driver.FindElement(By.TagName("form"));

            txtemail.SendKeys(email);
            txtusername.SendKeys(username);
            txtPwd.SendKeys(password);
            loginform.Submit();

            IWebElement errorDisplay = driver.FindElement(By.ClassName("validation-summary-errors"));

            Assert.NotNull(errorDisplay);

            driver.Close();
        }

        [Fact]
        public void login_pass()
        {
            IWebDriver driver = new ChromeDriver();

            string url = "http://localhost:5261/";
            driver.Navigate().GoToUrl(url);

            string email = "alice@example.com";
            string username = "alice123";
            string password = "P@$$wOrd1";

            IWebElement txtemail = driver.FindElement(By.Id("Email"));
            IWebElement txtusername = driver.FindElement(By.Id("Username"));
            IWebElement txtPwd = driver.FindElement(By.Id("Password"));
            IWebElement loginform = driver.FindElement(By.TagName("form"));

            txtemail.SendKeys(email);
            txtusername.SendKeys(username);
            txtPwd.SendKeys(password);
            loginform.Submit();

            string expectedUrl = "http://localhost:5261/Home/Main";

            string actualUrl = driver.Url;

            Assert.Equal(expectedUrl, actualUrl);

            driver.Close();
        }




    }
}
