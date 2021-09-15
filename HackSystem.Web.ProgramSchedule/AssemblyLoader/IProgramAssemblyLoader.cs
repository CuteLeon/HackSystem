using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader;

public interface IProgramAssemblyLoader
{
    bool CheckAssemblyLoaded(string assemblyName);

    Task<IEnumerable<Assembly>> LoadProgramAssembly(string programId);
}
