using API.DatabaseContext;
using API.Models;
using API.Utilities.CredentialAccessor;
using Microsoft.EntityFrameworkCore;

namespace API.Utilities.Security;

public class Authorization : IAuthorization
{
    private readonly ICredentialAccessor _accessor;
    private readonly Context _context;

    public Authorization(Context context, ICredentialAccessor accessor)
    {
        _context = context;
        _accessor = accessor;
    }

    public async Task<bool> IsAdministrator()
    {
        var record = await _context
            .Account!
            .FirstOrDefaultAsync(
                e => e.Id == _accessor.RetrieveAccountId()
            );
        return record?.Role == Role.Administrator;
    }
}