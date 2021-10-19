using Xunit;

namespace HackSystem.LRU.Tests;

public class LRUContainerTests
{
    [Fact()]
    public void LRUContainerTest()
    {
        var lruContainer = new LRUContainer<int>();
    }
}
