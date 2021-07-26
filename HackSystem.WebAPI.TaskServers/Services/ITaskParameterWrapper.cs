using System.Collections.Generic;

namespace HackSystem.WebAPI.TaskServers.Services
{
    public interface ITaskParameterWrapper
    {
        Dictionary<string, string>? WrapTaskParameters(string taskParameters);
    }
}
