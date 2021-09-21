namespace HackSystem.WebAPI.TaskServer.Infrastructure.Wrapper;

public interface ITaskPairParameterWrapper : ITaskParameterWrapper
{
    IDictionary<string, string>? WrapTaskParameters(string taskParameters);
}
