using AutoMapper;
using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Auth;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;

namespace CineReserve.Infrastruture.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AuthResponseDto>> GetProfileAsync(int userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ApiResponse<AuthResponseDto>.FailResponse("User not found", 404);

            var dto = _mapper.Map<AuthResponseDto>(user);
            return ApiResponse<AuthResponseDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<AuthResponseDto>> TopUpBalanceAsync(int userId, decimal amount)
        {
            if (amount <= 0)
                return ApiResponse<AuthResponseDto>.FailResponse("Amount must be greater than zero");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ApiResponse<AuthResponseDto>.FailResponse("User not found", 404);

            user.CreditBalance += amount;
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();

            var dto = _mapper.Map<AuthResponseDto>(user);
            return ApiResponse<AuthResponseDto>.SuccessResponse(dto, $"Successfully topped up ₹{amount}");
        }
    }
}
