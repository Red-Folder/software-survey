using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace SoftwareSurvey.E2ETests
{
    public class End2End : IDisposable
    {
        private const string E2E_TARGET = "E2ETARGET";
        private const string DEFAULT_URL = "https://software-survey.red-folder.com/";
        private const string SCREENSHOT_LOCATION = "SCREENSHOTLOCATION";

        private readonly By COMPANY_SIZE = By.Id("company-size");
        private readonly By JOB_SENORITY = By.Id("job-seniority");
        private readonly By JOB_TITLE = By.Id("job-title");
        private readonly By UK_BASED = By.Id("uk-based");

        private readonly By ECOMMERCE = By.Name("e-commerce");
        private readonly By INFORMATION_WEBSITE = By.Name("information-website");
        private readonly By MOBILE_APPS = By.Name("mobile-apps");
        private readonly By LINE_OF_BUSINESS = By.Name("line-of-business");
        private readonly By SOFTWARE_AS_A_SERVICE = By.Name("software-as-a-service");
        private readonly By OTHER = By.Name("other");

        private readonly By RETURN_ON_INVESTMENT = By.Name("return-on-investment");
        private readonly By KEEPING_PACE = By.Name("keeping-pace");
        private readonly By RECRUITMENT = By.Name("recruitment");
        private readonly By RETENTION = By.Name("retention");
        private readonly By QUALITY = By.Name("quality");
        private readonly By PREDICABILITY = By.Name("predicability");

        private readonly By TEXT = By.Id("text");

        private readonly By SURVEY_RESULTS = By.Id("survey-results");
        private readonly By FOLLOW_UP_QUESTIONS = By.Id("follow-up-questions");
        private readonly By FURTHER_SURVEYS = By.Id("further-surveys");
        private readonly By EMAIL = By.Id("email");

        private ITestOutputHelper _testOutputHelper;
        private IWebDriver _driver;
        private string _testRunId;
        private DataStore _dataStore;

        public End2End(ITestOutputHelper testOutputHelper)
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.EnableVerboseLogging = true;
            var options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            _driver = new ChromeDriver(service, options);

            _testRunId = Guid.NewGuid().ToString();
            _dataStore = new DataStore();
            _testOutputHelper = testOutputHelper;
        }

        public void Dispose()
        {
            TakeScreenshot("finalscreen");
            OutputBrowserConsoleLog();
            _driver.Quit();
        }

        private void OutputBrowserConsoleLog()
        {
            try
            {
                OutputHeading("Console output");
                foreach (var logEntry in _driver.Manage().Logs.GetLog(LogType.Browser))
                {
                    _testOutputHelper.WriteLine(logEntry.Message);
                }
            }
            catch (Exception ex)
            {
                OutputHeading("Exception while attempting to output the Console");
                _testOutputHelper.WriteLine(ex.Message);
                _testOutputHelper.WriteLine(ex.StackTrace);
            }
        }

        private void TakeScreenshot(string stage)
        {
            try
            {
                var screenshotLocation = Environment.GetEnvironmentVariable(SCREENSHOT_LOCATION);
                if (!string.IsNullOrWhiteSpace(screenshotLocation))
                {
                    Directory.CreateDirectory(screenshotLocation);

                    var filename = Path.Combine(screenshotLocation, $"{stage}.png");

                    if (File.Exists(filename)) File.Delete(filename);

                    var screenshot = _driver.TakeScreenshot();
                    screenshot.SaveAsFile(filename);
                }
            }
            catch (Exception ex)
            {
                OutputHeading("Exception while attempting to take screenshot");
                _testOutputHelper.WriteLine(ex.Message);
                _testOutputHelper.WriteLine(ex.StackTrace);
            }
        }

        private void OutputHeading(string heading)
        {
            _testOutputHelper.WriteLine("===================================");
            _testOutputHelper.WriteLine(heading);
            _testOutputHelper.WriteLine("===================================");
        }

        [Fact]
        [Trait("End2End", "Chrome")]
        public async Task HappyPathRun()
        {
            var url = Environment.GetEnvironmentVariable(E2E_TARGET) ?? DEFAULT_URL;
            _driver.Navigate().GoToUrl($"{url}?isTest");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "WELCOME",
                                "Wait for the Welcome Title");

            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "DEMOGRAPHICS",
                                "Wait for the Demographics Title");

            var companySize = new SelectElement(_driver.FindElement(COMPANY_SIZE));
            companySize.SelectByText("201-500 employees");
            var jobSeniority = new SelectElement(_driver.FindElement(JOB_SENORITY));
            jobSeniority.SelectByText("CXO");
            _driver.FindElement(JOB_TITLE).SendKeys("The boss");
            var ukBased = new SelectElement(_driver.FindElement(UK_BASED));
            ukBased.SelectByText("Partially");

            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "SOFTWARE TYPES",
                                "Wait for the Software Types Title");

            _driver.FindElements(ECOMMERCE)[3].Click();
            _driver.FindElements(INFORMATION_WEBSITE)[3].Click();
            _driver.FindElements(MOBILE_APPS)[3].Click();
            _driver.FindElements(LINE_OF_BUSINESS)[3].Click();
            _driver.FindElements(SOFTWARE_AS_A_SERVICE)[3].Click();
            _driver.FindElements(OTHER)[3].Click();

            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "YOUR EXPERIENCES",
                                "Wait for the Your Experience Title");

            _driver.FindElements(RETURN_ON_INVESTMENT)[3].Click();
            _driver.FindElements(KEEPING_PACE)[3].Click();
            _driver.FindElements(RECRUITMENT)[3].Click();
            _driver.FindElements(RETENTION)[3].Click();
            _driver.FindElements(QUALITY)[3].Click();
            _driver.FindElements(PREDICABILITY)[3].Click();

            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "ONE CHANGE",
                                "Wait for the One Change Title");

            _driver.FindElement(TEXT).SendKeys(_testRunId);

            // FURTHER CONTACT
            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "FURTHER CONTACT",
                                "Wait for the Further Contact Title");

            _driver.FindElement(SURVEY_RESULTS).Click();
            _driver.FindElement(FOLLOW_UP_QUESTIONS).Click();
            _driver.FindElement(FURTHER_SURVEYS).Click();
            await WaitForElement(EMAIL);
            _driver.FindElement(EMAIL).SendKeys("test@red-folder.com");

            await RetryActivity(By.XPath("//button[@type='submit' and text()='Next']"),
                                (element) => { element.Click(); return true; },
                                "Click on next");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text != "SAVING ..." && element.Text != "FURTHER CONTACT",
                                "Wait for the Save to complete");

            await RetryActivity(By.CssSelector("h1"),
                                (element) => element.Text == "THANK YOU",
                                "Wait for the Thank You Title");

            var savedRecord = _dataStore.Retrieve(_testRunId);

            Assert.NotNull(savedRecord);
            Assert.True(savedRecord.IsTest);

            Assert.Equal("201-500 employees", savedRecord.Demographic.CompanySize);
            Assert.Equal("CXO", savedRecord.Demographic.JobSeniority);
            Assert.Equal("The boss", savedRecord.Demographic.JobTitle);
            Assert.Equal("Partially", savedRecord.Demographic.UKBased);

            Assert.Equal(3, savedRecord.SoftwareTypes.ECommerce);
            Assert.Equal(3, savedRecord.SoftwareTypes.InformationWebsite);
            Assert.Equal(3, savedRecord.SoftwareTypes.MobileApps);
            Assert.Equal(3, savedRecord.SoftwareTypes.LineOfBusiness);
            Assert.Equal(3, savedRecord.SoftwareTypes.SoftwareAsAService);
            Assert.Equal(3, savedRecord.SoftwareTypes.Other);

            Assert.Equal(3, savedRecord.Experiences.ReturnOnInvestment);
            Assert.Equal(3, savedRecord.Experiences.KeepingPace);
            Assert.Equal(3, savedRecord.Experiences.Recruitment);
            Assert.Equal(3, savedRecord.Experiences.Retention);
            Assert.Equal(3, savedRecord.Experiences.Quality);
            Assert.Equal(3, savedRecord.Experiences.Predicability);

            Assert.True(savedRecord.Contact.SurveyResults);
            Assert.True(savedRecord.Contact.FollowUpQuestions);
            Assert.True(savedRecord.Contact.FurtherSurveys);
            Assert.Equal("test@red-folder.com", savedRecord.Contact.Email);
        }

        private async Task WaitForElement(By by)
        {
            // Allow time for Blazor to do its thing (fail after 60 attempt - 30 seconds)
            for (int i = 0; i < 60; i++)
            {
                await Task.Delay(500);

                try
                {
                    _driver.FindElement(by);
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
                return;
            }
        }

        private async Task RetryActivity(By by, Func<IWebElement, bool> activity, string activityDescription)
        {
            _testOutputHelper.WriteLine($"Attempting '{activityDescription}'");

            // Allow time for Blazor to do its thing (fail after 60 attempt - 30 seconds)
            for (int i = 0; i < 60; i++)
            {
                await Task.Delay(500);

                try
                {
                    var element = _driver.FindElement(by);

                    if (activity(element)) return;

                    _testOutputHelper.WriteLine("Activity failed to return true");
                }
                catch (NoSuchElementException)
                {
                    _testOutputHelper.WriteLine("NoSuchElementException received");
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    _testOutputHelper.WriteLine("StaleElementReferenceException received");
                    continue;
                }
                catch (Exception ex)
                {
                    _testOutputHelper.WriteLine($"Exception {ex.GetType().Name} encountered - {ex.Message}");
                    throw ex;
                }
            }

            _testOutputHelper.WriteLine("Maximum retries reached");
            throw new Exception($"Maximum retries reached while attempting '{activityDescription}'");
        }
    }
}
