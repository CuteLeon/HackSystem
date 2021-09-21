namespace HackSystem.WebAPI.TaskServer.Application.Services;

public interface ITaskPairParameterWrapper : ITaskParameterWrapper
{
    IDictionary<string, string>? WrapTaskParameters(string taskParameters);
}
