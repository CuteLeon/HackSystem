namespace HackSystem.WebAPI.TaskServer.Services;

public interface ITaskPairParameterWrapper : ITaskParameterWrapper
{
    IDictionary<string, string>? WrapTaskParameters(string taskParameters);
}
