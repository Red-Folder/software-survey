using SoftwareSurvey.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoftwareSurvey.UnitTests.Models
{
    public class StepsUnitTests
    {
        private readonly Steps _sut;

        public StepsUnitTests()
        {
            var start = new Step
            {
                Path = "",
                PageTitle = "Start"
            };
            var step1 = new Step
            {
                Path = "Step1",
                PageTitle = "Step 1"
            };
            var step2 = new Step
            {
                Path = "Step2",
                PageTitle = "Step 2"
            };
            var end = new Step
            {
                Path = "End",
                PageTitle = "End"
            };
            start.NextStep = step1;
            step1.PreviousStep = start;
            step1.NextStep = step2;
            step2.PreviousStep = step1;
            step2.NextStep = end;

            _sut = new Steps(new List<Step>
            {
                start,
                step1,
                step2,
                end
            });
        }

        [Fact]
        public void Start_HasNextStep()
        {
            Assert.True(_sut.HasNext(""));
        }

        [Fact]
        public void Step1_HasNextStep()
        {
            Assert.True(_sut.HasNext("Step1"));
        }

        [Fact]
        public void Step2_HasNextStep()
        {
            Assert.True(_sut.HasNext("Step2"));
        }

        [Fact]
        public void End_DoesntHaveNextStep()
        {
            Assert.False(_sut.HasNext("End"));
        }

        [Fact]
        public void Start_DoesntHavePreviousStep()
        {
            Assert.False(_sut.HasPrevious(""));
        }

        [Fact]
        public void Step1_HasPreviousStep()
        {
            Assert.True(_sut.HasPrevious("Step1"));
        }

        [Fact]
        public void Step2_HasPreviousStep()
        {
            Assert.True(_sut.HasPrevious("Step2"));
        }

        [Fact]
        public void End_DoesntHavePreviousStep()
        {
            Assert.False(_sut.HasPrevious("End"));
        }

        [Fact]
        public void Start_NextUrl_ReturnsStep1()
        {
            Assert.Equal("Step1", _sut.NextPath(""));
        }

        [Fact]
        public void Step1_NextUrl_ReturnsStep2()
        {
            Assert.Equal("Step2", _sut.NextPath("Step1"));
        }

        [Fact]
        public void Step2_NextUrl_ReturnsEnd()
        {
            Assert.Equal("End", _sut.NextPath("Step2"));
        }

        [Fact]
        public void End_NextUrl_ReturnsNull()
        {
            Assert.Null(_sut.NextPath("End"));
        }

        [Fact]
        public void Start_PreviousUrl_ReturnsNull()
        {
            Assert.Null(_sut.PreviousPath(""));
        }

        [Fact]
        public void Step1_NextUrl_ReturnsStart()
        {
            Assert.Equal("", _sut.PreviousPath("Step1"));
        }

        [Fact]
        public void Step2_PreviousUrl_ReturnsStep1()
        {
            Assert.Equal("Step1", _sut.PreviousPath("Step2"));
        }

        [Fact]
        public void End_PreviousUrl_ReturnsNull()
        {
            Assert.Null(_sut.PreviousPath("End"));
        }
    }
}
