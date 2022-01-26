using System.Security.Claims;

namespace API.Utilities.CredentialAccessor;

public class CredentialAccessor : ICredentialAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public CredentialAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string RetrieveAccountId()
    {
        var value = _accessor.HttpContext?.User.Claims
            .FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier)?.Value;
        return value!;
    }

    public string? RetrieveAccountName()
    {
        return _accessor
            .HttpContext
            ?.User
            .Claims
            .FirstOrDefault(e => e.Type == ClaimTypes.Name)?.Value;
    }

    public bool IsAdmin()
    {
        throw new NotImplementedException();
    }
}