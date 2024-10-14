using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using System.Xml.Linq;
using NUnit.Framework;

namespace Selenium_Specflow.StepDefinitions
{
    [Binding]
    public class AlertsStepDefinitions
    {
        private readonly IWebDriver? _driver;
        private readonly ScenarioContext _scenarioContext;
        private const string InputXPathTemplate = "//label[contains(., '{0}')]//following-sibling::div/button";

        public AlertsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }
        [Given(@"I have opened (.*) playground")]
        public void GivenIHaveOpenedAlertsPlayground(string element)
        {
            _driver?.Navigate().GoToUrl($"https://letcode.in/{element}");
        }

        [When(@"I click on (.*) button")]
        public void WhenIClickOnAcceptTheAlertButton(string labelName)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            var alertBtn = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
            alertBtn.Click();

            //Handle Alert
            IAlert alert = _driver.SwitchTo().Alert();
            alert.Accept();
            Console.WriteLine($"Accpted {labelName} Alert");
        }

        [When(@"I clicked on (.*) button")]
        public void WhenIClickOnDismissTheAlertButton(string labelName)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            var alertBtn = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
            alertBtn.Click();

            //Handle Alert
            IAlert alert = _driver.SwitchTo().Alert();
            alert.Accept();
            Console.WriteLine($"Accpted {labelName} Alert");
        }

        [When(@"I click on (.*) button on alert sending name as (.*)")]
        public void WhenIClickOnYourNameButtonOnAlert(string labelName, string userName)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            var alertBtn = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
            alertBtn.Click();

            //Handle Alert
            IAlert alert = _driver.SwitchTo().Alert();
            alert.SendKeys(userName);
            alert.Accept();
            Console.WriteLine($"Accpted {labelName} Alert");
            string expectedText = $"Your name is: {userName}";
            string actualText = _driver.FindElement(By.XPath("//p[contains(text(), 'Your name is:')]")).Text;
            Assert.AreEqual(expectedText, actualText, "The text displayed does not match the expected value.");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }
    }
}
