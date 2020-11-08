using AngleSharp.Media.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SoftwareSurvey.Components;
using SoftwareSurvey.Services;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SoftwareSurvey.UnitTests.Components
{
    public class NavigationButtonsUnitTests: IDisposable
    {
        private IRenderedComponent<NavigationButtons> _cut;

        private TestContext _context;
        private Mock<IEventLoggingService> _eventLoggingService;
        private Mock<Wrappers.INavigationManager> _navigationManager;
        private Model _model;
        private EditContext _editContext;

        public NavigationButtonsUnitTests()
        {
            _context = new TestContext();

            _eventLoggingService = new Mock<IEventLoggingService>();
            _context.Services.AddSingleton(_eventLoggingService.Object);

            _navigationManager = new Mock<Wrappers.INavigationManager>();
            _context.Services.AddSingleton(_navigationManager.Object);

            _model = new Model();
            _editContext = new EditContext(_model);
            _editContext.AddDataAnnotationsValidation();

            _cut = _context.RenderComponent<NavigationButtons>(parameters =>
                parameters.Add(p => p.PreviousUrl, "https://test.com/previous")
                          .Add(p => p.NextUrl, "https://test.com/next")
                          .AddCascadingValue(_editContext)
            );
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void WillNavigateToNext_IfEditContextIsValid()
        {
            _model.Name = "Valid value";

            var nextButton = _cut.Find(".next-navigation-button");
            nextButton.Click();

            _navigationManager.Verify(x => x.NavigateTo("https://test.com/next"), Times.Once);
        }

        [Fact]
        public void WillNotNavigateToNext_IfEditContextIsinvalid()
        {
            var nextButton = _cut.Find(".next-navigation-button");
            nextButton.Click();

            _navigationManager.Verify(x => x.NavigateTo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void WillNavigateToPrevious_IfEditContextIsValid()
        {
            _model.Name = "Valid value";

            var previousButton = _cut.Find(".previous-navigation-button");
            previousButton.Click();

            _navigationManager.Verify(x => x.NavigateTo("https://test.com/previous"), Times.Once);
        }

        [Fact]
        public void WillNotNavigateToPrevious_IfEditContextIsinvalid()
        {
            var previousButton = _cut.Find(".previous-navigation-button");
            previousButton.Click();

            _navigationManager.Verify(x => x.NavigateTo(It.IsAny<string>()), Times.Never);
        }

        private class Model
        {
            [Required]
            public string Name { get; set; }
        }
    }
}
