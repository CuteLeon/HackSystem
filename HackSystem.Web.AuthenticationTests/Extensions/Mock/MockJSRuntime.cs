using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop.Infrastructure;
using Microsoft.JSInterop.WebAssembly;

namespace HackSystem.Web.AuthenticationTests.Extensions.Mock
{
    public class MockJSRuntime : WebAssemblyJSRuntime
    {
        internal static readonly MockJSRuntime Instance = new MockJSRuntime();

        public ElementReferenceContext ElementReferenceContext
        {
            get;
        }

        public MockJSRuntime()
        {
            this.ElementReferenceContext = new WebElementReferenceContext(this);
            base.JsonSerializerOptions.Converters.Add(new ElementReferenceJsonConverter(this.ElementReferenceContext));
        }

        private static string InvokeDotNet(string assemblyName, string methodIdentifier, string dotNetObjectId, string argsJson)
        {
            DotNetInvocationInfo invocationInfo = new DotNetInvocationInfo(assemblyName, methodIdentifier, (dotNetObjectId == null) ? 0 : long.Parse(dotNetObjectId), null);
            return DotNetDispatcher.Invoke(Instance, in invocationInfo, argsJson);
        }

        private static void EndInvokeJS(string argsJson)
        {
            DotNetDispatcher.EndInvokeJS(Instance, argsJson);
        }

        private static void BeginInvokeDotNet(string callId, string assemblyNameOrDotNetObjectId, string methodIdentifier, string argsJson)
        {
            long dotNetObjectId;
            string assemblyName;
            if (char.IsDigit(assemblyNameOrDotNetObjectId[0]))
            {
                dotNetObjectId = long.Parse(assemblyNameOrDotNetObjectId);
                assemblyName = null;
            }
            else
            {
                dotNetObjectId = 0L;
                assemblyName = assemblyNameOrDotNetObjectId;
            }

            DotNetDispatcher.BeginInvokeDotNet(invocationInfo: new DotNetInvocationInfo(assemblyName, methodIdentifier, dotNetObjectId, callId), jsRuntime: Instance, argsJson: argsJson);
        }
    }
}
