using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Auth;

namespace CineReserve.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<ApiResponse<AuthResponseDto>> GetProfileAsync(int userId);
        Task<ApiResponse<AuthResponseDto>> TopUpBalanceAsync(int userId, decimal amount);
    }
}
