using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoftwareSurvey.E2ETests
{
    public class End2End : IDisposable
    {
        private const string URL = "https://software-survey.red-folder.com/?IsTest";
        private readonly By PAGE_TITLE = By.CssSelector("h1");
        private readonly By NEXT_BUTTON = By.XPath("//button[@type='submit' and text()='Next']");

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

        private IWebDriver _driver;
        private string _testRunId;
        private DataStore _dataStore;

        public End2End()
        {
            _driver = new ChromeDriver();
            _testRunId = Guid.NewGuid().ToString();
            _dataStore = new DataStore();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Fact]
        [Trait("End2End", "Chrome")]
        public async Task HappyPathRun()
        {
            _driver.Navigate().GoToUrl(URL);
            await WaitForPageTitle("WELCOME");

            await ClickNextFor("DEMOGRAPHICS");
            
            var companySize = new SelectElement(_driver.FindElement(COMPANY_SIZE));
            companySize.SelectByText("201-500 employees");
            var jobSeniority = new SelectElement(_driver.FindElement(JOB_SENORITY));
            jobSeniority.SelectByText("CXO");
            _driver.FindElement(JOB_TITLE).SendKeys("The boss");
            var ukBased = new SelectElement(_driver.FindElement(UK_BASED));
            ukBased.SelectByText("Partially");

            await ClickNextFor("SOFTWARE TYPES");
            _driver.FindElements(ECOMMERCE)[3].Click();
            _driver.FindElements(INFORMATION_WEBSITE)[3].Click();
            _driver.FindElements(MOBILE_APPS)[3].Click();
            _driver.FindElements(LINE_OF_BUSINESS)[3].Click();
            _driver.FindElements(SOFTWARE_AS_A_SERVICE)[3].Click();
            _driver.FindElements(OTHER)[3].Click();

            await ClickNextFor("YOUR EXPERIENCES");
            _driver.FindElements(RETURN_ON_INVESTMENT)[3].Click();
            _driver.FindElements(KEEPING_PACE)[3].Click();
            _driver.FindElements(RECRUITMENT)[3].Click();
            _driver.FindElements(RETENTION)[3].Click();
            _driver.FindElements(QUALITY)[3].Click();
            _driver.FindElements(PREDICABILITY)[3].Click();

            await ClickNextFor("ONE CHANGE");
            _driver.FindElement(TEXT).SendKeys(_testRunId);

            // FURTHER CONTACT
            await ClickNextFor("FURTHER CONTACT");
            _driver.FindElement(SURVEY_RESULTS).Click();
            _driver.FindElement(FOLLOW_UP_QUESTIONS).Click();
            _driver.FindElement(FURTHER_SURVEYS).Click();
            await WaitForElement(EMAIL);
            _driver.FindElement(EMAIL).SendKeys("test@red-folder.com");

            await ClickNextAndWaitForSaveThenWaitFor("THANK YOU");

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

        private async Task ClickNextAndWaitForSaveThenWaitFor(string pageTitle)
        {
            await ClickNext();
            await WaitForSave();
            await WaitForPageTitle(pageTitle);
        }

        private async Task ClickNextFor(string pageTitle)
        {
            await ClickNext();
            await WaitForPageTitle(pageTitle);
        }

        private async Task ClickNext()
        {
            await WaitForElement(NEXT_BUTTON);
            _driver.FindElement(NEXT_BUTTON).Click();
        }

        private async Task WaitForPageTitle(string pageTitle)
        {
            // Allow time for Blazor to do its thing (fail after 60 attempt - 30 seconds)
            for (int i = 0; i < 60; i++)
            {
                await Task.Delay(500);
                try
                {
                    if (_driver.FindElement(PAGE_TITLE).Text == pageTitle) return;
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
            }

            Assert.Equal(pageTitle, _driver.FindElement(PAGE_TITLE).Text);
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

        private async Task WaitForSave()
        {
            // Allow time for Blazor to do its thing (fail after 60 attempt - 30 seconds)
            for (int i = 0; i < 60; i++)
            {
                await Task.Delay(500);

                try
                {
                    var currentPageTitle = _driver.FindElement(PAGE_TITLE).Text;
                    if (currentPageTitle != "SAVING ..." && currentPageTitle != "FURTHER CONTACT") return;
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
            }
        }
    }
}
