using API.DatabaseContext;

namespace API.Repositories.License;

public class LicenseRepository : ILicenseRepository
{
    private readonly Context _context;

    public LicenseRepository(Context context)
    {
        _context = context;
    }

    public async Task DeleteLicense(Models.License license, CancellationToken cancellationToken)
    {
        _context.License!.Remove(license);
        await _context.SaveChangesAsync(cancellationToken);
    }
}