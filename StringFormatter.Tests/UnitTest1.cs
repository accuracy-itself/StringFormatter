using StringFormatter.Core;

namespace StringFormatter.Tests
{
    public class UnitTest1
    {
        private readonly ClassForTests _target = new ClassForTests();

        [Fact]
        public void SimpleTest()
        {
            var expected = $"Hi {_target.Name} with id={_target.Id}";
            var actual = Core.StringFormatter.Shared.Format("Hi {Name} with id={Id}", _target);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DateTest()
        {
            var expected = $"Now {_target.Date} in Minsk";
            var actual = Core.StringFormatter.Shared.Format("Now {Date} in Minsk", _target);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EscapeTest()
        {
            var expected = $"{{Name}}={_target.Name}";
            var actual = Core.StringFormatter.Shared.Format("{{Name}}={Name}", _target);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PropertyTest()
        {
            var expected = $"Hi {_target.Name} with password={_target.Password}";
            var actual = Core.StringFormatter.Shared.Format("Hi {Name} with password={Password}", _target);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ErrorTest()
        {
            var ex = Assert.Throws<Exception>(
                () => Core.StringFormatter.Shared.Format("Hi {{Name} with password={Password}", _target));

            Assert.Equal("Syntax error", ex.Message);
        }

        [Fact]
        public void NoFieldTest()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => Core.StringFormatter.Shared.Format("Hi {Name} with password={Pasword}", _target));

        }
    }
}