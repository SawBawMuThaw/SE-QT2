using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex4Tests
{
    public class ItemTest
    {
        [Fact]
        public void AddItem_test()
        {
            IWebDriver driver = new ChromeDriver();

            string url = "http://localhost:5261/Items";
            driver.Navigate().GoToUrl(url);

            IWebElement createLink = driver.FindElement(By.PartialLinkText("Create"));

            Assert.NotNull(createLink);
            createLink.Click();

            string expectedUrl = "http://localhost:5261/Items/Create";
            string actualUrl = driver.Url;
            Assert.Equal(expectedUrl, actualUrl);

            IWebElement txtItemName = driver.FindElement(By.Id("ItemName"));
            IWebElement txtSize = driver.FindElement(By.Id("Size"));
            IWebElement txtStock = driver.FindElement(By.Id("Stock"));
            IWebElement itemform = driver.FindElement(By.TagName("form"));

            txtItemName.SendKeys("Big Mug");
            txtSize.SendKeys("50cm radius");
            txtStock.SendKeys("55");
            itemform.Submit();

            expectedUrl = "http://localhost:5261/Items";
            actualUrl = driver.Url;
            Assert.Equal(expectedUrl, actualUrl);

            IWebElement row = driver.FindElement(By.XPath("//td[contains(text(),'Big Mug')]"));
            Assert.NotNull(row);

            driver.Close();
        }
    }
}
