using System.Reflection;

namespace HackSystem.Web.ProgramSchedule.Application.AssemblyLoader;

public interface IProgramAssemblyLoader
{
    bool CheckAssemblyLoaded(string assemblyName);

    Task<IEnumerable<Assembly>> LoadProgramAssembly(string programId);
}
