using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            MathTestClass mtc = new MathTestClass();
            int a = 10, b = 20;
            int expected = 30;

            // Act
            int result = mtc.Add(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
