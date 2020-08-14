using Moq;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using Xunit;

namespace SoftwareSurvey.UnitTests.Services
{
    public class SurveyNavigationServiceUnitTests
    {
        private readonly SurveyNavigationService _sut;
        private readonly Mock<INavigationManagerWrapper> _navigationManagerWrapper;
        private readonly Mock<ISteps> _steps;

        public SurveyNavigationServiceUnitTests()
        {
            _steps = new Mock<ISteps>();
            _navigationManagerWrapper = new Mock<INavigationManagerWrapper>();
            _sut = new SurveyNavigationService(_steps.Object, _navigationManagerWrapper.Object);
        }

        [Fact]
        public void HandleNext_NavigatesToTheNextStep()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.NextPath("Current"))
                .Returns("Next");

            _sut.HandleNext();

            _navigationManagerWrapper.Verify(x => x.NavigateTo("Next"), Times.Once);
        }

        [Fact]
        public void HandleNext_DoesNotNavigateToIfNoneExists()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.NextPath("Current"))
                .Returns<string>(null);

            _sut.HandleNext();

            _navigationManagerWrapper.Verify(x => x.NavigateTo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void HandlePrevious_NavigatesToThePreviousStep()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.PreviousPath("Current"))
                .Returns("Previous");

            _sut.HandlePrevious();

            _navigationManagerWrapper.Verify(x => x.NavigateTo("Previous"), Times.Once);
        }

        [Fact]
        public void HandlePrevious_DoesNotNavigateToIfNoneExists()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.PreviousPath("Current"))
                .Returns<string>(null);

            _sut.HandlePrevious();

            _navigationManagerWrapper.Verify(x => x.NavigateTo(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void HasNext_True_IfHasNext()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.HasNext("Current"))
                .Returns(true);

            var result = _sut.HasNext;

            Assert.True(result);
        }

        [Fact]
        public void HasNext_False_IfDoesntHaveNext()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.HasNext("Current"))
                .Returns(false);

            var result = _sut.HasNext;

            Assert.False(result);
        }

        [Fact]
        public void HasPrevious_True_IfHasPrevious()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.HasPrevious("Current"))
                .Returns(true);

            var result = _sut.HasPrevious;

            Assert.True(result);
        }

        [Fact]
        public void HasPrevious_False_IfDoesntHavePrevious()
        {
            _navigationManagerWrapper
                .Setup(x => x.CurrentPath)
                .Returns("Current");
            _steps
                .Setup(x => x.HasPrevious("Current"))
                .Returns(false);

            var result = _sut.HasPrevious;

            Assert.False(result);
        }
    }
}
