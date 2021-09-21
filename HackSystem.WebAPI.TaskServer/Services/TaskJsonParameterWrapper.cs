using System.Text.Json;
using HackSystem.WebAPI.TaskServer.Application.Services;

namespace HackSystem.WebAPI.TaskServer.Services;

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
