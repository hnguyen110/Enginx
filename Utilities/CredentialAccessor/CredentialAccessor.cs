using System.Security.Claims;

namespace API.Utilities.CredentialAccessor;

public class CredentialAccessor : ICredentialAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public CredentialAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public int RetrieveAccountId()
    {
        var value = _accessor.HttpContext?.User.Claims
            .FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
        return value != null ? int.Parse(value) : 0;
    }

    public string? RetrieveAccountName()
    {
        return _accessor
            .HttpContext
            ?.User
            .Claims
            .FirstOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
    }
}