using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SoftwareSurvey.Components;
using SoftwareSurvey.Services;
using Xunit;

namespace SoftwareSurvey.UnitTests.Components
{
    public class PreRenderLoadingMessageUnitTests
    {
        [Fact]
        public void ShowsLoadingMessage_OnFirstRender()
        {
            using var context = new TestContext();
            context.Services.AddSingleton(new Mock<IEventLoggingService>().Object);

            IRenderedComponent<PreRenderLoadingMessage> cut = context.RenderComponent<PreRenderLoadingMessage>(parameters =>
                parameters
                    .Add(p => p.UnitTestMode, true)
                    .AddChildContent("<div>Hello World</div>")
            );

            Assert.Contains("Loading ...", cut.Markup);
        }

        [Fact]
        public void ShowsChildContent_OnSecondRender()
        {
            using var context = new TestContext();
            context.Services.AddSingleton(new Mock<IEventLoggingService>().Object);

            IRenderedComponent<PreRenderLoadingMessage> cut = context.RenderComponent<PreRenderLoadingMessage>(parameters => 
                parameters
                    .Add(p => p.UnitTestMode, true)
                    .AddChildContent("<div>Hello World</div>")
            );

            Assert.DoesNotContain("<div>Hello World</div>", cut.Markup);

            cut.Render();
            Assert.Contains("<div>Hello World</div>", cut.Markup);
        }

        [Fact]
        public void ShowsChildContent_IfInDefaultMode()
        {
            using var context = new TestContext();
            context.Services.AddSingleton(new Mock<IEventLoggingService>().Object);

            IRenderedComponent<PreRenderLoadingMessage> cut = context.RenderComponent<PreRenderLoadingMessage>(parameters =>
                parameters.AddChildContent("<div>Hello World</div>")
            );

            Assert.Contains("<div>Hello World</div>", cut.Markup);
        }
    }
}
