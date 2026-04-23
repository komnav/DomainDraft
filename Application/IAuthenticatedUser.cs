
namespace Application;

public interface IAuthenticatedUser
{
    string CompanyId { get; }
    UserDto GetCurrentUser();
}

public record UserDto(string CompanyId);