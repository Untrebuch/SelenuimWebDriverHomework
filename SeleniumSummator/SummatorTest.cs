using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumSummator
{
    public class SummatorTest
    {
        private WebDriver driver;
        IWebElement field1;
        IWebElement field2;
        IWebElement calc;
        IWebElement reset;
        IWebElement sum;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Url = "https://sum-numbers.nakov.repl.co";
            driver.Manage().Window.Maximize();

            field1 = driver.FindElement(By.Id("number1"));
            field2 = driver.FindElement(By.Id("number2"));
            calc = driver.FindElement(By.Id("calcButton"));
            reset = driver.FindElement(By.Id("resetButton"));
            sum = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]

        public void ShutDown()
        {
            driver.Close();
        }

        [Test]
        public void Test_SumValidNumbers()
        {
            field1.Click();
            field1.SendKeys("5");
            field2.Click();
            field2.SendKeys("10");
            calc.Click();

            Assert.AreEqual("Sum: 15", sum.Text);

            reset.Click();
        }

        [Test]
        public void Test_SumInvalidData()
        {
            field1.Click();
            field1.SendKeys("5");
            field2.Click();
            field2.SendKeys("asd");
            calc.Click();

            Assert.AreEqual("Sum: invalid input", sum.Text);

            reset.Click();
        }

        [Test]
        public void Test_ResetButton()
        {
            field1.Click();
            field1.SendKeys("1");
            field2.Click();
            field2.SendKeys("2");
            calc.Click();

            Assert.AreEqual("1", field1.GetAttribute("value"));
            Assert.AreEqual("2", field2.GetAttribute("value"));

            reset.Click();

            Assert.AreEqual("", field1.GetAttribute("value"));
            Assert.AreEqual("", field2.GetAttribute("value"));
        }
    }
}