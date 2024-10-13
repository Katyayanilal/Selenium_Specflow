using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;

namespace Selenium_Specflow.StepDefinitions
{
    [Binding]
    public class ButtonClicksStepDefinitions
    {
        private readonly IWebDriver? _driver;
        private readonly ScenarioContext _scenarioContext;
        private const string InputXPathTemplate = "//label[contains(., '{0}')]//following-sibling::div/button";

        public ButtonClicksStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Given(@"I opened (.*) playground")]
        public void GivenIOpenedButtonsPlayground(string element)
        {
            _driver?.Navigate().GoToUrl($"https://letcode.in/{element}");
        }

        [When(@"I click on the (.*) button")]
        public void WhenIClickOnTheGotoHomeButton(string button)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            IWebElement homeButton = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, button)));
            homeButton.Click();
        }

        [When(@"I navigate to the homepage")]
        public void ThenINavigateToTheHomepage()
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            string currentUrl = _driver.Url;
            Assert.AreEqual("https://letcode.in/", currentUrl);
        }

        [When(@"I go back to the previous page")]
        public void ThenIGoBackToThePreviousPage()
        {
            _driver?.Navigate().Back();
        }

        [When(@"I get the X & Y coordinates of the (.*) button")]
        public void ThenIGetTheXYCoordinatesOfTheXYButton(string myButton)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            IWebElement findLocation = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
            var btnLocation = findLocation.Location;
            Console.WriteLine($"Button X: {btnLocation.X}, Y: {btnLocation.Y}");
        }

        [When(@"I find the color of the (.*) button")]
        public void ThenIFindTheColorOfTheColorButton(string myButton)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            IWebElement findColor = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
            var btnColor = findColor.GetCssValue("background-color");
            Console.WriteLine($"Button Color - {btnColor}");
        }

        [When(@"I get the height & width of the (.*) button")]
        public void ThenIGetTheHeightWidthOfTheHeightWidthButton(string myButton)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            IWebElement findSize = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
            var btnSize = findSize.Size;
            Console.WriteLine($"Button Width: {btnSize.Width}, \nButton Height: {btnSize.Height}");
        }

        [When(@"I confirm that the (.*) button is disabled")]
        public void ThenIConfirmThatTheDisabledButtonIsDisabled(string myButton)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            IWebElement findStatus = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
            Assert.IsFalse(findStatus.Enabled, $"{myButton} is not Disabled.");
            Console.WriteLine($"{myButton} is Disabled.");
        }

        [When(@"I click and hold the (.*) button")]
        public void WhenIClickAndHoldTheHoldButtonButton(string myButton)
        {
            if (_driver == null)
            {
                throw new InvalidOperationException("_driver is not initialized.");
            }
            Console.WriteLine("If Condition Worked");
            IWebElement findStatus = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
            Console.WriteLine(string.Format(InputXPathTemplate, myButton));
            Actions btnAction = new Actions(_driver);
            btnAction.ClickAndHold(findStatus).Perform();
            System.Threading.Thread.Sleep(2000);
            btnAction.Release(findStatus).Perform();
        }

        //[Then(@"The (.*) button name changes to (.*)")]
        //public void ThenTheMyButtonButtonNameChangesToButtonHasBeenLongPressed(string myButton, string expectedButtonText)
        //{
        //    if (_driver == null)
        //    {
        //        throw new InvalidOperationException("_driver is not initialized.");
        //    }
        //    IWebElement button = _driver.FindElement(By.XPath(string.Format(InputXPathTemplate, myButton)));
        //    string actualButtonText = button.Text;
        //    Assert.AreEqual(expectedButtonText, actualButtonText, "The button text did not change as expected.");
        //}
        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }
    }
}
