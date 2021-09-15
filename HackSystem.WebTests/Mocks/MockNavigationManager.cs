using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace HackSystem.WebTests.Mocks;

public class MockNavigationManager : NavigationManager
{
    public MockNavigationManager(string baseUri, string uri)
    {
        this.Initialize(baseUri, uri);
    }

    public List<string> NavigationHistory { get; init; } = new List<string>();

    public bool HasNavigated { get => this.NavigationHistory.Any(); }

    protected override void NavigateToCore(string uri, bool forceLoad)
    {
        this.Uri = this.ToAbsoluteUri(uri).AbsoluteUri;
        this.NavigationHistory.Add(uri);
    }

    public void ClearHistory() => this.NavigationHistory.Clear();
}
