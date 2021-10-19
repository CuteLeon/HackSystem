using System;
using System.Collections.Generic;
using Xunit;

namespace HackSystem.LRU.Tests;

public class LRUContainerTests
{
    [Fact()]
    public void LRUContainerTest()
    {
        var container = new LRUContainer<int>(5);
        container.Add(1);
        Assert.Throws<InvalidOperationException>(() => container.Add(1));
        Assert.Equal(1, container.Count);
        Assert.True(container.TryGetValue(1, out var value));
        Assert.Equal(1, value);
        Assert.Throws<KeyNotFoundException>(() => container.Remove(0));
        container.Remove(1);
        Assert.Equal(0, container.Count);
        container.Add(1);
        container.Add(2);
        container.Add(3);
        container.Add(4);
        container.Add(5);
        container.Add(6);
        container.Add(7);
        container.Add(8);
        Assert.Equal(5, container.Count);
        Assert.Equal(8, container.HeadValue);
        Assert.Equal(4, container.TailValue);
        // 4,5,6,7,8
        container.BringToHead(6);
        // 4,5,7,8,6
        Assert.Equal(6, container.HeadValue);
        Assert.Equal(4, container.TailValue);
        container.BringToHead(6);
        // 4,5,7,8,6
        Assert.Equal(6, container.HeadValue);
        Assert.Equal(4, container.TailValue);
        container.BringToHead(4);
        // 5,7,8,6,4
        Assert.Equal(4, container.HeadValue);
        Assert.Equal(5, container.TailValue);
        container.Add(1);
        container.Add(2);
        container.Add(3);
        // 6,4,1,2,3
        Assert.Equal(3, container.HeadValue);
        Assert.Equal(6, container.TailValue);
        container.Clear();
        Assert.Equal(0, container.Count);
    }
}
