﻿using Microsoft.JSInterop;

namespace HackSystem.Web.Component.Popover;

public class PopoverHandler : IPopoverHandler, IAsyncDisposable
{
    private static readonly SemaphoreSlim SyncSemaphore = new(1, 1);
    private IJSObjectReference popoverModuleReference;
    private readonly IJSRuntime jsRuntime;
    private bool initialized = false;

    public PopoverHandler(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        if (this.initialized) return;
        await SyncSemaphore.WaitAsync();
        try
        {
            if (this.initialized) return;
            this.popoverModuleReference = await jsRuntime
                .InvokeAsync<IJSObjectReference>("import", "./_content/HackSystem.Web.Component/hacksystem.popover.js");
            this.initialized = true;
        }
        finally
        {
            SyncSemaphore.Release();
        }
    }

    public async Task SetupPopovers(string targetElementFilter)
    {
        if (!this.initialized) await this.InitializeAsync();
        await popoverModuleReference.InvokeVoidAsync("setupPopovers", targetElementFilter);
    }

    public async Task<string?> SetupPopover(PopoverDetail popoverDetail)
    {
        if (!this.initialized) await this.InitializeAsync();
        return await popoverModuleReference.InvokeAsync<string?>(
            "setupPopover",
            popoverDetail.TargetElemantFilter,
            popoverDetail.Title,
            popoverDetail.IsHtmlContent,
            popoverDetail.Content,
            popoverDetail.Trigger.ToString().ToLower(),
            popoverDetail.Placement.ToString().ToLower(),
            popoverDetail.Offset,
            popoverDetail.ShowDelay,
            popoverDetail.HideDelay,
            popoverDetail.ReplaceContent);
    }

    public async Task UpdatePopover(string targetElementFilter, string action)
    {
        if (!this.initialized) await this.InitializeAsync();
        await popoverModuleReference.InvokeVoidAsync("updatePopover", targetElementFilter, action);
    }

    public async Task RefreshContent(string popoverId, string replacementTargetId, string originSourceId)
    {
        if (!this.initialized) await this.InitializeAsync();
        await popoverModuleReference.InvokeVoidAsync("refreshContent", popoverId, replacementTargetId, originSourceId);
    }

    public async ValueTask DisposeAsync()
    {
        if (this.initialized)
            await this.popoverModuleReference.DisposeAsync();
    }
}