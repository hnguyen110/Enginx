using API.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.ContactInformation;

public class ContactInformationRepository : IContactInformationRepository
{
    private readonly Context _context;

    public ContactInformationRepository(Context context)
    {
        _context = context;
    }

    public async Task<Models.ContactInformation?> FindContactInformationById(string id,
        CancellationToken cancellationToken)
    {
        var record =
            await _context
                .ContactInformation!
                .FirstOrDefaultAsync(
                    e => e.Id == id,
                    cancellationToken
                );
        return record;
    }

    public async Task DeleteContactInformation(Models.ContactInformation contact, CancellationToken cancellationToken)
    {
        _context.ContactInformation!.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateContactInformation(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}