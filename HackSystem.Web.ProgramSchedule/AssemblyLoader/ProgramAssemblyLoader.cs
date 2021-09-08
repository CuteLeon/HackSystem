using Microsoft.Extensions.Logging;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader
{
    public class ProgramAssemblyLoader : IProgramAssemblyLoader
    {
        private readonly HashSet<string> loadedAssemblies;
        private readonly ILogger<ProgramAssemblyLoader> logger;

        public ProgramAssemblyLoader(
            ILogger<ProgramAssemblyLoader> logger)
        {
            this.logger = logger;
            this.loadedAssemblies = new(AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName().Name));
        }
    }
}
