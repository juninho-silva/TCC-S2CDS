using S2CDS.Api.Dtos.v1.Authentication;

namespace S2CDS.Api.Services.v1.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateToken(AuthenticationRequest request);
        Task<string> KeepAliveToken(string token);
    }
}
