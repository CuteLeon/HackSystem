using System;

namespace HackSystem.WebAPI.TaskServers.Services;

    public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
    {
        object? WrapTaskParameters(string taskParameters, Type type);
    }
