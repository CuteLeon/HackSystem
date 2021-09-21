namespace HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;

public interface ITaskPairParameterWrapper : ITaskParameterWrapper
{
    IDictionary<string, string>? WrapTaskParameters(string taskParameters);
}
