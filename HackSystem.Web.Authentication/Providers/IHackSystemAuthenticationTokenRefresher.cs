using System.Threading.Tasks;

namespace HackSystem.Web.Authentication.Providers;

    public interface IHackSystemAuthenticationTokenRefresher
    {
        bool IsRunning { get; }

        void StartRefresher();

        void StopRefresher();

        Task<string> RefreshTokenAsync();
    }
