using HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;
using Xunit;

namespace HackSystem.WebAPI.TaskServer.Services.Tests;

public class TaskParameterWrapperTests
{
    [Theory]
    [InlineData(null, 0, null, null, null, null, null, null)]
    [InlineData("", 0, null, null, null, null, null, null)]
    [InlineData(" ", 0, null, null, null, null, null, null)]
    [InlineData("ABC", 0, null, null, null, null, null, null)]
    [InlineData("ABC|", 0, null, null, null, null, null, null)]

    [InlineData("ABC=123", 1, "ABC", "123", null, null, null, null)]
    [InlineData("ABC=123|", 1, "ABC", "123", null, null, null, null)]
    [InlineData("|ABC=123", 1, "ABC", "123", null, null, null, null)]
    [InlineData("|ABC=123|", 1, "ABC", "123", null, null, null, null)]
    [InlineData("ABC=123|ABC=456", 1, "ABC", "456", null, null, null, null)]

    [InlineData("ABC=123|DEF=456|", 2, "ABC", "123", "DEF", "456", null, null)]
    [InlineData("|ABC=123|DEF=456", 2, "ABC", "123", "DEF", "456", null, null)]
    [InlineData("|ABC=123|DEF=456|", 2, "ABC", "123", "DEF", "456", null, null)]

    [InlineData("ABC=123|DEF=456|GHI=789", 3, "ABC", "123", "DEF", "456", "GHI", "789")]
    [InlineData("ABC=123|DEF=456|GHI=789|", 3, "ABC", "123", "DEF", "456", "GHI", "789")]
    [InlineData("|ABC=123|DEF=456|GHI=789", 3, "ABC", "123", "DEF", "456", "GHI", "789")]
    [InlineData("|ABC=123|DEF=456|GHI=789|", 3, "ABC", "123", "DEF", "456", "GHI", "789")]

    [InlineData("ABC=1=2=3|DEF=456|GHI=789", 3, "ABC", "1=2=3", "DEF", "456", "GHI", "789")]
    [InlineData("ABC=123|DEF=4=5=6|GHI=789", 3, "ABC", "123", "DEF", "4=5=6", "GHI", "789")]
    [InlineData("ABC=123|DEF=456|GHI=7=8=9", 3, "ABC", "123", "DEF", "456", "GHI", "7=8=9")]

    [InlineData("ABC=1|2|3|DEF=456|GHI=789", 3, "ABC", "1", "DEF", "456", "GHI", "789")]
    [InlineData("ABC=123|DEF=4|5|6|GHI=789", 3, "ABC", "123", "DEF", "4", "GHI", "789")]
    [InlineData("ABC=123|DEF=456|GHI=7|8|9", 3, "ABC", "123", "DEF", "456", "GHI", "7")]

    [InlineData("A=B=C=123|DEF=456|GHI=789", 3, "A", "B=C=123", "DEF", "456", "GHI", "789")]
    [InlineData("ABC=123|D=E=F=456|GHI=789", 3, "ABC", "123", "D", "E=F=456", "GHI", "789")]
    [InlineData("ABC=123|DEF=456|G=H=I=789", 3, "ABC", "123", "DEF", "456", "G", "H=I=789")]

    [InlineData("A|B|C=123|DEF=456|GHI=789", 3, "C", "123", "DEF", "456", "GHI", "789")]
    [InlineData("ABC=123|D|E|F=456|GHI=789", 3, "ABC", "123", "F", "456", "GHI", "789")]
    [InlineData("ABC=123|DEF=456|G|H|I=789", 3, "ABC", "123", "DEF", "456", "I", "789")]
    public void WrapTaskParametersTest(
        string taskParameters,
        int expectedCount,
        string name_1,
        string value_1,
        string name_2,
        string value_2,
        string name_3,
        string value_3)
    {
        var parameterDictionary = new TaskPairParameterWrapper().WrapTaskParameters(taskParameters);
        Assert.Equal(expectedCount, parameterDictionary?.Count ?? 0);
        if (parameterDictionary != null)
        {
            if (parameterDictionary.Count > 0)
            {
                Assert.True(parameterDictionary.TryGetValue(name_1, out var value));
                Assert.Equal(value_1, value);
            }
            if (parameterDictionary.Count > 1)
            {
                Assert.True(parameterDictionary.TryGetValue(name_2, out var value));
                Assert.Equal(value_2, value);
            }
            if (parameterDictionary.Count > 2)
            {
                Assert.True(parameterDictionary.TryGetValue(name_3, out var value));
                Assert.Equal(value_3, value);
            }
        }
    }
}
