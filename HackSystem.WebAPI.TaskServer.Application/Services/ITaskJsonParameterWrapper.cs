namespace HackSystem.WebAPI.TaskServer.Application.Services;

public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
{
    object? WrapTaskParameters(string taskParameters, Type type);
}
