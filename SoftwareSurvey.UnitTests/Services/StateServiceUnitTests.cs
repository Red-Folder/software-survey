using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using Xunit;

namespace SoftwareSurvey.UnitTests.Services
{
    public class StateServiceUnitTests
    {
        private readonly StateService _sut;

        public StateServiceUnitTests()
        {
            _sut = new StateService();
        }

        [Fact]
        public void GetOrNew_ReturnsNewObject_IfDoesntExist()
        {
            Assert.Null(_sut.GetOrNew<TestState>().Value);
        }

        [Fact]
        public void GetOrNew_ReturnsExistingObject_IfExist()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });

            Assert.Equal("Original", _sut.GetOrNew<TestState>().Value);
        }

        [Fact]
        public void GetOrNew_ReturnsCorrectObject_IfMultipleExist()
        {
            _sut.Save(new TestState
            {
                Value = "Test State"
            });
            _sut.Save(new TestState2
            {
                Value = "Test State 2"
            });
            _sut.Save(new TestState3
            {
                Value = "Test State 3"
            });

            Assert.Equal("Test State", _sut.GetOrNew<TestState>().Value);
            Assert.Equal("Test State 2", _sut.GetOrNew<TestState2>().Value);
            Assert.Equal("Test State 3", _sut.GetOrNew<TestState3>().Value);
        }

        [Fact]
        public void Save_WillReplaceExisting()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });

            _sut.Save(new TestState
            {
                Value = "Updated"
            });

            Assert.Equal("Updated", _sut.GetOrNew<TestState>().Value);
        }

        public class TestState: IStateObject
        {
            public string Value { get; set; }
        }

        public class TestState2 : IStateObject
        {
            public string Value { get; set; }
        }

        public class TestState3 : IStateObject
        {
            public string Value { get; set; }
        }
    }
}
