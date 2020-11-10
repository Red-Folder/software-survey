using Xunit;

namespace SoftwareSurvey.Models.UnitTests
{
    public class OptionUnitTests
    {
        private Option _sut;
        private Sample Model;

        public OptionUnitTests()
        {
            Model = new Sample();
            var property = Model.GetType().GetProperty("Value1");
            _sut = new Option(this, Model, property);
        }

        [Fact]
        public void Id_IsSet()
        {
            Assert.Equal("value-1", _sut.Id);
        }

        [Fact]
        public void Label_IsSet()
        {
            Assert.Equal("Value 1 Name", _sut.Label);
        }

        [Fact]
        public void Description_IsSet()
        {
            Assert.Equal("Value 1 Description", _sut.Description);
        }

        [Fact]
        public void For_IsSet()
        {
            Model.Value1 = 999;

            var expression = _sut.For;
            var function = expression.Compile();
            var value1 = function.Invoke();

            Assert.Equal(999, value1);
        }

        [Fact]
        public void Getter_WillReadModel()
        {
            Model.Value1 = 99;
            Assert.Equal(99, _sut.ValueGetter());
        }

        [Fact]
        public void Setter_WillUpdateModel()
        {
            _sut.ValueSetter(5);
            Assert.Equal(5, Model.Value1);
        }

        public class Sample
        {
            [System.ComponentModel.DisplayName("Value 1 Name")]
            [System.ComponentModel.Description("Value 1 Description")]
            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Value 1 is required")]
            public int Value1 { get; set; }
        }
    }
}
