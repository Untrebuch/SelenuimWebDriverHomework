using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumNumberCalculatorTests
{
    public class CalculatorTests
    {
        private WebDriver driver;
        IWebElement number1;
        IWebElement number2;
        IWebElement operation;
        IWebElement calcbutton;
        IWebElement resetbutton;
        IWebElement result;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new FirefoxDriver();
            driver.Url = "https://number-calculator.nakov.repl.co/";
            driver.Manage().Window.Maximize();
            number1 = driver.FindElement(By.Id("number1"));
            number2 = driver.FindElement(By.Id("number2"));
            operation = driver.FindElement(By.Id("operation"));
            calcbutton = driver.FindElement(By.Id("calcButton"));
            resetbutton = driver.FindElement(By.Id("resetButton"));
            result = driver.FindElement(By.Id("result"));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [TestCase ("3", "+", "7", "Result: 10")]
        [TestCase("0", "+", "7", "Result: 7")]
        [TestCase("3.23", "+", "5.342", "Result: 8.572")]
        [TestCase("3", "+", "0", "Result: 3")]
        [TestCase("13", "-", "7", "Result: 6")]
        [TestCase("13", "-", "0", "Result: 13")]
        [TestCase("13.25", "-", "8.538", "Result: 4.712")]
        [TestCase("", "-", "7", "Result: invalid input")]
        [TestCase("4", "*", "5", "Result: 20")]
        [TestCase("0", "*", "5", "Result: 0")]
        [TestCase("2.57", "*", "3.142", "Result: 8.07494")]
        [TestCase("4", "*", "0", "Result: 0")]
        [TestCase("1.5e53", "*", "150", "Result: 2.25e+55")]
        [TestCase("15", "/", "5", "Result: 3")]
        [TestCase("0", "/", "5", "Result: 0")]
        [TestCase("5.55", "/", "1.2", "Result: 4.625")]
        [TestCase("1.5e53", "/", "150", "Result: 1e+51")]
        [TestCase("15", "/", "0", "Result: Infinity")]
        [TestCase(".", "+", "5", "Result: invalid input")]
        [TestCase("", "+", "5", "Result: invalid input")]
        [TestCase(".", "-", "5", "Result: invalid input")]
        [TestCase("", "-", "5", "Result: invalid input")]
        [TestCase(".", "*", "5", "Result: invalid input")]
        [TestCase("", "*", "5", "Result: invalid input")]
        [TestCase(".", "/", "5", "Result: invalid input")]
        [TestCase("", "/", "5", "Result: invalid input")]
        [TestCase("5", "+", ",", "Result: invalid input")]
        [TestCase("5", "+", "", "Result: invalid input")]
        [TestCase("5", "-", ",", "Result: invalid input")]
        [TestCase("5", "-", "", "Result: invalid input")]
        [TestCase("5", "*", ",", "Result: invalid input")]
        [TestCase("5", "*", "", "Result: invalid input")]
        [TestCase("5", "/", ",", "Result: invalid input")]
        [TestCase("5", "/", "", "Result: invalid input")]
        [TestCase("6", "m", "8", "Result: invalid operation")]
        [TestCase("6", "", "8", "Result: invalid operation")]
        [TestCase("Infinity", "+", "7", "Result: Infinity")]
        [TestCase("Infinity", "-", "6", "Result: Infinity")]
        [TestCase("Infinity", "*", "5", "Result: Infinity")]
        [TestCase("Infinity", "/", "5", "Result: Infinity")]
        [TestCase("3", "+", "Infinity", "Result: Infinity")]
        [TestCase("4", "-", "Infinity", "Result: -Infinity")]
        [TestCase("5", "*", "Infinity", "Result: Infinity")]
        [TestCase("3", "/", "Infinity", "Result: 0")]
        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "-", "Infinity", "Result: invalid calculation")]
        [TestCase("Infinity", "*", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "/", "Infinity", "Result: invalid calculation")]

        public void Test_CalculatorWebApp(string n1, string op, string n2, string ress)
        {
            number1.SendKeys(n1);
            operation.SendKeys(op);
            number2.SendKeys(n2);
            calcbutton.Click();
            Assert.AreEqual(ress, result.Text);
            resetbutton.Click();
        }
    }
}