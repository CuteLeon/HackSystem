using System.Text.Json;
using HackSystem.WebAPI.TaskServer.Application.ParameterWrappers;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.ParameterWrappers;

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
