using API.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Address;

public class AddressRepository : IAddressRepository
{
    private readonly Context _context;

    public AddressRepository(Context context)
    {
        _context = context;
    }

    public async Task<Models.Address?> FindAddressById(string id, CancellationToken cancellationToken)
    {
        return await _context
            .Address!
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken
            );
    }

    public async Task UpdateAddress(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}