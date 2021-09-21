namespace HackSystem.WebAPI.TaskServer.Services;

public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
{
    object? WrapTaskParameters(string taskParameters, Type type);
}
