using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTestsHomeworkWiki
{
    public class WikiTest
    {
        private WebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Url = "https://en.wikipedia.org";
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]

        public void ShutDown()
        {
            driver.Close();
        }

        [Test]
        public void Test_WikipediaForQA()
        {
            var inputField = driver.FindElement(By.Id("searchInput"));

            inputField.Click();
            inputField.SendKeys("QA" + Keys.Enter);
            Thread.Sleep(1000);

            Assert.AreEqual("https://en.wikipedia.org/wiki/QA", driver.Url);
        }
    }
}