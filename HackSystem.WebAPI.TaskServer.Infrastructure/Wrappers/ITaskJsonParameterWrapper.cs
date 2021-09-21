namespace HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;

public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
{
    object? WrapTaskParameters(string taskParameters, Type type);
}
