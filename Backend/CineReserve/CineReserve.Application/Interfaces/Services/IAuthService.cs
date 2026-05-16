using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Auth;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
