namespace HackSystem.WebAPI.TaskServer.Infrastructure.Wrapper;

public interface ITaskJsonParameterWrapper : ITaskParameterWrapper
{
    object? WrapTaskParameters(string taskParameters, Type type);
}
