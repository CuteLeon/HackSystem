using System.Collections.Generic;

namespace HackSystem.WebAPI.TaskServers.Services;

    public interface ITaskPairParameterWrapper : ITaskParameterWrapper
    {
        IDictionary<string, string>? WrapTaskParameters(string taskParameters);
    }
