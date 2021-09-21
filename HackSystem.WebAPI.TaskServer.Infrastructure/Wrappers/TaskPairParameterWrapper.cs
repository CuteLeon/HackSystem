using System.Text.RegularExpressions;

namespace HackSystem.WebAPI.TaskServer.Infrastructure.Wrappers;

public class TaskPairParameterWrapper : ITaskPairParameterWrapper
{
    protected Regex ParameterRegex { get; init; } = new Regex(
        "\\|?(?<Name>[^=\\|]+)=(?<Value>[^\\|]+)",
        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

    /// <remarks>
    /// Shouldn't have '=' or '|' in parameter name.
    /// Shouldn't have '|' in value, to support BASE64 value.
    /// </remarks>
    public IDictionary<string, string>? WrapTaskParameters(string taskParameters)
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
