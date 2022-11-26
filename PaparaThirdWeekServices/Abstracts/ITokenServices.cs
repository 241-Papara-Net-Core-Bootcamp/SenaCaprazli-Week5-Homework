using PaparaThirdWeek.Services.DTOs;

namespace PaparaThirdWeek.Services.Abstracts
{
    public interface ITokenServices
    {
        TokenDto Authenticate(UserDto user);
    }
}