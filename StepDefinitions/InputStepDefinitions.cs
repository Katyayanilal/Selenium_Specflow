
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium_Specflow.StepDefinitions
{
    [Binding]
    public class InputStepDefinitions
    {
        private readonly IWebDriver? _driver;
        private readonly ScenarioContext _scenarioContext;
        private const string InputXPathTemplate = "//label[contains(., '{0}')]//following-sibling::div/input";

        public InputStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }
        
        [Given(@"I open (.*) playground")]
        public void WhenIOpenInputPlayground(string element)
        {
            _driver?.Navigate().GoToUrl($"https://letcode.in/{element}");
        }

        [When(@"I check the status of the (.*) field")]
        public void WhenICheckTheStatusOfTheField(string labelName)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            var inputField = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
            string status = "enabled";

            if (!inputField.Enabled)
            {
                status = $"{labelName} field is disabled";
            }
            else if (inputField.GetAttribute("readonly") != null)
            {
                status = $"{labelName} field is readonly";
            }

            _scenarioContext.Add($"{labelName}Status", status);
        }
        [When(@"I enter (.*) in the (.*) field if it's enabled and not readonly")]
        public void WhenIEnterValueInTheFieldIfEditable(string value, string labelName)
        {
            string status = _scenarioContext.Get<string>($"{labelName}Status");
            if (status == "enabled" )
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("_driver is not initialized.");
                }
                var inputField = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
                inputField.Clear();
                inputField.SendKeys(value);
            }
        }
        [When(@"I clear the existing value from the (.*) field if it's enabled and not readonly")]
        public void WhenIClearTheExistingValueFromTheField(string labelName)
        {
            string status = _scenarioContext.Get<string>($"{labelName}Status");
            if (status == "enabled")
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("_driver is not initialized.");
                }
                var inputField = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
                inputField.Clear();
            }
        }

        [Then(@"I verify that (.*) is entered in the (.*) field if it was editable or NOT readonly")]
        public void ThenIVerifyThatIsEnteredInTheField(string expectedValue, string labelName)
        {
            string status = _scenarioContext.Get<string>($"{labelName}Status");
            if (status == "enabled" || !(status == "readonly"))
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("_driver is not initialized.");
                }
                var inputField = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, labelName)));
                string actualValue = inputField.GetAttribute("value");
                Assert.AreEqual(expectedValue, actualValue, $"The value in the {labelName} field does not match the expected value.");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }

    }
}