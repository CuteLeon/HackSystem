using System.IO;
using HackSystem.WebAPI.Services.API.FileStores;
using HackSystem.WebAPI.Services.FileStores.Configurations;
using Microsoft.Extensions.Options;

namespace HackSystem.WebAPI.Services.FileStores
{
    public class ProgramFileStoreService : IProgramFileStoreService
    {
        private readonly FileStoreConfiguration configuration;

        public ProgramFileStoreService(IOptionsMonitor<FileStoreConfiguration> options)
        {
            this.configuration = options.CurrentValue;
        }

        public string GetProgramIconFile(string programId)
        {
            return Path.Combine(this.configuration.ProgramIconDirectory, Path.ChangeExtension(programId, "png"));
        }
    }
}
