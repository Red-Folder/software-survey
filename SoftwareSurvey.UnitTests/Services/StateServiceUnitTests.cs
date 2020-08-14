using Moq;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoftwareSurvey.UnitTests.Services
{
    public class StateServiceUnitTests
    {
        private readonly StateService _sut;
        private readonly Mock<IPersistanceManager> _persistanceManager;

        public StateServiceUnitTests()
        {
            _persistanceManager = new Mock<IPersistanceManager>();
            _sut = new StateService(_persistanceManager.Object);
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

        [Fact]
        public async Task Persist_WillCallPersistanceManager()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });

            await _sut.Persist();

            _persistanceManager.Verify(x => x.Persist(It.IsAny<List<IStateObject>>()), Times.Once);
        }

        [Fact]
        public async Task Persist_ReturnTrue_OnSuccess()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });
            _persistanceManager
                .Setup(x => x.Persist(It.IsAny<List<IStateObject>>()))
                .ReturnsAsync(true);

            var result = await _sut.Persist();

            Assert.True(result);
        }

        [Fact]
        public async Task Persist_ReturnFalse_OnFailure()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });
            _persistanceManager
                .Setup(x => x.Persist(It.IsAny<List<IStateObject>>()))
                .ReturnsAsync(false);

            var result = await _sut.Persist();

            Assert.False(result);
        }

        [Fact]
        public async Task Persist_ReturnFalse_OnException()
        {
            _sut.Save(new TestState
            {
                Value = "Original"
            });
            _persistanceManager
                .Setup(x => x.Persist(It.IsAny<List<IStateObject>>()))
                .Throws(new Exception("Something broke"));

            var result = await _sut.Persist();

            Assert.False(result);
        }

        private class TestState: IStateObject
        {
            public string Value { get; set; }
        }

        private class TestState2 : IStateObject
        {
            public string Value { get; set; }
        }

        private class TestState3 : IStateObject
        {
            public string Value { get; set; }
        }
    }
}
