using API.DatabaseContext;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Utilities.Security;

public class Authorization : IAuthorization
{
    private readonly Context _context;

    public Authorization(Context context)
    {
        _context = context;
    }

    public async Task<bool> IsAdministrator(string id)
    {
        var record = await _context.Account!.FirstOrDefaultAsync(e => e.Id == id);
        return record?.Role == Role.Administrator;
    }
}