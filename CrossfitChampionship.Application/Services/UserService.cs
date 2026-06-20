using CrossfitChampionship.Domain.Entities;
using CrossfitChampionship.Application.DTOs.Users;
using CrossfitChampionship.Application.Interfaces;

namespace CrossfitChampionship.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u =>
            u.Username == request.Username && u.Password == request.Password);

        if (user is null)
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        return new LoginResponseDto
        {
            Success = true,
            Message = "Login successful",
            User = new UserDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Username = user.Username,
                Role = user.Role.ToString()
            }
        };
    }
}
