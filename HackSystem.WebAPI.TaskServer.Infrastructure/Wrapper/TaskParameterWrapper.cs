using System.Text.RegularExpressions;
using HackSystem.WebAPI.TaskServer.Infrastructure.Wrapper;

namespace HackSystem.WebAPI.TaskServer.Services;

public class TaskParameterWrapper : ITaskParameterWrapper
{
    protected Regex ParameterRegex { get; init; } = new Regex(
        "\\|?(?<Name>[^=\\|]+)=(?<Value>[^\\|]+)",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <remarks>
    /// Shouldn't have '=' or '|' in parameter name.
    /// Shouldn't have '|' in value, to support BASE64 value.
    /// </remarks>
    public Dictionary<string, string>? WrapTaskParameters(string taskParameters)
    {
        if (string.IsNullOrWhiteSpace(taskParameters))
        {
            return default;
        }

        var matches = this.ParameterRegex.Matches(taskParameters);
        if (matches.Count == 0)
        {
            return default;
        }

        var parameterDictionary = new Dictionary<string, string>();
        foreach (var match in matches.Cast<Match>().Where(m => m.Success))
        {
            var name = match.Groups["Name"].Value;
            var value = match.Groups["Value"].Value;
            parameterDictionary[name] = value;
        }
        return parameterDictionary;
    }
}
