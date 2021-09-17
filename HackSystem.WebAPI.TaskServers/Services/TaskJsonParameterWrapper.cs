﻿using System.Text.Json;

namespace HackSystem.WebAPI.TaskServers.Services;

public class TaskJsonParameterWrapper : ITaskJsonParameterWrapper
{
    public object? WrapTaskParameters(string taskParameters, Type type)
    {
        try
        {
            var result = JsonSerializer.Deserialize(taskParameters, type);
            return result;
        }
        catch
        {
            return default;
        }
    }
}
