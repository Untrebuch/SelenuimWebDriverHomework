using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumTestURLShortener
{
    public class URLShortenerTests
    {
        private WebDriver driver;
        IWebElement link1;
        IWebElement link2;
        IWebElement table;
        IWebElement cell1;
        IWebElement cell2;
        IWebElement field1;
        IWebElement field2;
        IWebElement button;
        IWebElement err;
        IWebElement counter;


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Url = "https://shorturl.nakov.repl.co";
            driver.Manage().Window.Maximize();

            link1 = driver.FindElement(By.CssSelector("body > header:nth-child(1) > a:nth-child(3)"));
            link2 = driver.FindElement(By.CssSelector("body > header:nth-child(1) > a:nth-child(5)"));
            //table = driver.FindElement(By.CssSelector(".urls"));
            //cell1 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(1) > a:nth-child(1)"));
            //cell2 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(2) > a:nth-child(1)"));
            //sum = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]

        public void ShutDown()
        {
            driver.Close();
        }

        [Test]
        public void Test_HomePage()
        {
            Assert.AreEqual("URL Shortener", driver.Title);
        }

        [Test]
        public void Test_ShortURLPage()
        {
            link1.Click();
            table = driver.FindElement(By.CssSelector(".urls"));
            cell1 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(1) > a:nth-child(1)"));
            cell2 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(2) > a:nth-child(1)"));
            
            Assert.AreEqual("Short URLs", driver.Title);
            Assert.IsNotNull(table);
            Assert.IsNotNull(cell1);
            Assert.IsNotNull(cell2);
            Assert.AreEqual("https://nakov.com", cell1.Text);
            Assert.AreEqual("http://shorturl.nakov.repl.co/go/nak", cell2.Text);
        }

        [Test]
        public void Test_AddURLInvalidData()
        {
            link2.Click();
            field1 = driver.FindElement(By.Id("url"));
            field2 = driver.FindElement(By.Id("code"));
            button = driver.FindElement(By.CssSelector("body > main:nth-child(2) > form:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > button:nth-child(1)"));
            field1.Click();
            field1.SendKeys("asd");
            button.Click();
            err = driver.FindElement(By.CssSelector(".err"));

            Assert.AreEqual("Invalid URL!", err.Text);
        }

        [Test]
        public void Test_AddURLValidData()
        {
            link2.Click();
            field1 = driver.FindElement(By.Id("url"));
            field2 = driver.FindElement(By.Id("code"));
            button = driver.FindElement(By.CssSelector("body > main:nth-child(2) > form:nth-child(2) > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > button:nth-child(1)"));
            field1.Click();
            string newurl = "https://www.example.com/" + DateTime.Now;
            field1.SendKeys(newurl);
            button.Click();
            driver.FindElement(By.CssSelector("body > header:nth-child(1) > a:nth-child(3)")).Click();
            IWebElement link3;
            link3 = driver.FindElement(By.PartialLinkText(newurl));
            Assert.IsNotEmpty(link3.Text);
            driver.FindElement(By.PartialLinkText("Home")).Click();
            Assert.Greater(driver.FindElement(By.CssSelector("body > main:nth-child(2) > ul:nth-child(2) > li:nth-child(1) > b:nth-child(1)")).Text, "3");

        }

        [Test]
        public void Test_ValidShortURL()
        {
            counter = driver.FindElement(By.CssSelector("body > main:nth-child(2) > ul:nth-child(2) > li:nth-child(2) > b:nth-child(1)"));
            string countertext = counter.Text;
            link1.Click();
            cell2 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(2) > a:nth-child(1)"));
            cell2.Click();
            //Thread.Sleep(3000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            //Thread.Sleep(2000);
            driver.FindElement(By.PartialLinkText("Home")).Click();
            Assert.AreNotEqual(driver.FindElement(By.CssSelector("body > main:nth-child(2) > ul:nth-child(2) > li:nth-child(2) > b:nth-child(1)")).Text, countertext);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Quit();
        }

        [Test]
        public void Test_InvalidShortURL()
        {
            counter = driver.FindElement(By.CssSelector("body > main:nth-child(2) > ul:nth-child(2) > li:nth-child(2) > b:nth-child(1)"));
            string countertext = counter.Text;
            link1.Click();
            cell2 = driver.FindElement(By.CssSelector(".urls > tbody:nth-child(2) > tr:nth-child(4) > td:nth-child(2) > a:nth-child(1)"));
            cell2.Click();
            Thread.Sleep(1000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Assert.AreEqual(driver.Title, "Server Not Found");
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Quit();
        }
    }
}