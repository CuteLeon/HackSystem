using System.Reflection;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader
{
    public interface IProgramAssemblyLoader
    {
        bool CheckAssemblyLoaded(string assemblyName);

        Task<IEnumerable<Assembly>> LoadProgramAssembly(string programId);
    }
}
