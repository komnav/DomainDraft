using Application;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

public class AuthenticatedUser: IAuthenticatedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor, string userId)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string CompanyId => _httpContextAccessor.HttpContext.User.FindFirst("companyId")?.Value!;

    public UserDto GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        return new UserDto(user.FindFirst("companyId")?.Value);
    }
}