using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HackSystem.Web.AuthenticationTests.Extensions.Mock
{
    public class WebElementReferenceContext : ElementReferenceContext
    {
        internal IJSRuntime JSRuntime { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="WebElementReferenceContext"/>.
        /// </summary>
        /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
        public WebElementReferenceContext(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }
    }
}
