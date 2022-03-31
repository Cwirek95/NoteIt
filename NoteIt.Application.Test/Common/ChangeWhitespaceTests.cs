using Xunit;
using FluentAssertions;

namespace NoteIt.Application.Common.Tests
{
    public class ChangeWhitespaceTests
    {
        [Theory()]
        [InlineData("Example test name", "example-test-name")]
        [InlineData("Example test%$ name", "example-test%$-name")]
        [InlineData("Example test- name", "example-test-name")]
        [InlineData("Example test-   -  name", "example-test-name")]
        public void ChangeToDash_ForExampleData_ChangeWhiteSpaceToDash(string inData, string outData)
        {
            // Act
            var result = ChangeWhitespace.ChangeToDash(inData);

            // Assert
            result.Should().Be(outData);
        }
    }
}