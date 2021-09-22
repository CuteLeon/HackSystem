namespace HackSystem.WebAPI.TaskServer.Application.ParameterWrappers;

public interface ITaskPairParameterWrapper : ITaskParameterWrapper
{
    IDictionary<string, string>? WrapTaskParameters(string taskParameters);
}
