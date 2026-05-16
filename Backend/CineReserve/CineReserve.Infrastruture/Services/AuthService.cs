using CineReserve.Application.Common;
using CineReserve.Application.DTOs.Auth;
using CineReserve.Application.Interfaces.Repositories;
using CineReserve.Application.Interfaces.Services;
using CineReserve.Application.Settings;
using CineReserve.Domain.Entities;
using CineReserve.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CineReserve.Infrastruture.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepo, IOptions<JwtSettings> jwtSettings)
        {
            _userRepo = userRepo;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userRepo.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return ApiResponse<AuthResponseDto>.FailResponse("Email already registered");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreditBalance = 5000m, // Mock credit balance for hackathon
                Role = UserRole.User
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            var response = new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token,
                CreditBalance = user.CreditBalance
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Registration successful", 201);
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email.ToLower());
            if (user == null)
                return ApiResponse<AuthResponseDto>.FailResponse("Invalid email or password", 401);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return ApiResponse<AuthResponseDto>.FailResponse("Invalid email or password", 401);

            var token = GenerateJwtToken(user);
            var response = new AuthResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = token,
                CreditBalance = user.CreditBalance
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Login successful");
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
