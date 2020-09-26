using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SoftwareSurvey.Components;
using SoftwareSurvey.Models;
using SoftwareSurvey.Wrappers;
using Xunit;

namespace SoftwareSurvey.UnitTests.Components
{
    public class EnsureBeenThroughStartUnitTests
    {
        [Fact]
        public void WillRedirectIfNotBeenThroughStart()
        {
            using var context = new TestContext();

            context.Services.AddSingleton(new SurveyResponse
            {
                HasBeenToStart = false
            });

            var navigateToCalled = false;
            var navigationManager = new Mock<INavigationManager>();
            navigationManager
                .Setup(x => x.Uri)
                .Returns(() => navigateToCalled ? "https://test.com/" : "https://test.com/some-parth-other-than-start");
            navigationManager
                .Setup(x => x.NavigateTo(""))
                .Callback(() => navigateToCalled = true);
            context.Services.AddSingleton(navigationManager.Object);

            var cut = context.RenderComponent<EnsureBeenThroughStart>();

            navigationManager.Verify(x => x.NavigateTo(""), Times.Once);
        }

        [Fact]
        public void WillShowChildContentIfStarting()
        {
            using var context = new TestContext();

            context.Services.AddSingleton(new SurveyResponse
            {
                HasBeenToStart = false
            });

            var navigationManager = new Mock<INavigationManager>();
            navigationManager
                .Setup(x => x.Uri)
                .Returns("https://test.com/");
            context.Services.AddSingleton(navigationManager.Object);

            var cut = context.RenderComponent<EnsureBeenThroughStart>(x => x.AddChildContent("<h1>Test</h1>"));

            cut.MarkupMatches("<h1>Test</h1>");
        }

        [Fact]
        public void WillShowChildContentHasBeenThroughStart()
        {
            using var context = new TestContext();

            context.Services.AddSingleton(new SurveyResponse
            {
                HasBeenToStart = true
            });

            var navigationManager = new Mock<INavigationManager>();
            navigationManager
                .Setup(x => x.Uri)
                .Returns("https://test.com/some-parth-other-than-start");
            context.Services.AddSingleton(navigationManager.Object);

            var cut = context.RenderComponent<EnsureBeenThroughStart>(x => x.AddChildContent("<h1>Test</h1>"));

            cut.MarkupMatches("<h1>Test</h1>");
        }
    }
}
