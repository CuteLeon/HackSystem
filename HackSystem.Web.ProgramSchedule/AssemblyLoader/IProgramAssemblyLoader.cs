using System.Reflection;

namespace HackSystem.Web.ProgramSchedule.AssemblyLoader
{
    public interface IProgramAssemblyLoader
    {
        bool CheckAssemblyLoaded(string assemblyName);

        bool CheckProgramLoaded(string programId);

        Task<IEnumerable<Assembly>> LoadProgramAssembly(string programId);
    }
}
