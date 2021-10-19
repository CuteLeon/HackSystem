using HackSystem.Web.Authentication.TokenHandlers;

namespace HackSystem.Web.ProgramDevelopmentKit.Authentication;

public class DevelopmentKitAuthenticationTokenHandler : IHackSystemAuthenticationTokenHandler
{
    private readonly ILogger<DevelopmentKitAuthenticationTokenHandler> logger;

    private string Token { get; set; }

    public DevelopmentKitAuthenticationTokenHandler(
        ILogger<DevelopmentKitAuthenticationTokenHandler> logger)
    {
        this.logger = logger;
        this.Token =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTGVvbiIsIm" +
            "h0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imxlb25AaGFjay5jb20iLCJQcm9mZXNzaW9uYWwiO" +
            "iJ0cnVlIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbIkhhY2tlciIsIkNvbW1hbmRlciJdLCJleHAi" +
            "OjIxNDc0NTQ4NDcsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.6g2mG4uf3kylqi9wULXxr6miapl0Uv8NC2PeixVxliQ";
    }

    public async ValueTask<string> GetTokenAsync()
    {
        this.logger.LogInformation("HackSystem Get Token ...");
        return this.Token;
    }

    public async Task UpdateTokenAsync(string token)
    {
        this.logger.LogInformation("HackSystem Update Token ...");
        this.Token = token;
    }

    public async Task RemoveTokenAsync()
    {
        this.logger.LogInformation("HackSystem Remove Token ...");
        this.Token = string.Empty;
    }
}
