using BlazorShop.Api.Security.Authentication.Interfaces;
using System.Security.Claims;

namespace BlazorShop.Api.Security.Authentication;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public int UserId
    {
        get
        {
            return int.Parse(_httpContextAccessor.HttpContext!
                .User
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);
        }
    }
}

