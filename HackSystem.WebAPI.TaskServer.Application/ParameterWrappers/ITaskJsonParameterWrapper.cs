namespace HackSystem.WebAPI.TaskServer.Application.ParameterWrappers;

public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
{
    object? WrapTaskParameters(string taskParameters, Type type);
}
