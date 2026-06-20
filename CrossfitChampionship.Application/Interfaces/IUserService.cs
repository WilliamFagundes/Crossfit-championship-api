using CrossfitChampionship.Application.DTOs.Users;

namespace CrossfitChampionship.Application.Interfaces;

public interface IUserService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
}
