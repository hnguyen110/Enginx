using API.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Insurance;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly Context _database;

    public InsuranceRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.Insurance insurance, CancellationToken cancellationToken)
    {
        await _database.AddAsync(insurance, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Insurance?> RetrieveInsuranceById(string? id, CancellationToken cancellationToken)
    {
        var record = await _database
            .Insurance!
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken
            );
        return record;
    }

    public async Task<List<Models.Insurance>> RetrieveAllInsurances(CancellationToken cancellationToken)
    {
        var records = await _database
            .Insurance!
            .ToListAsync(cancellationToken);

        return records;
    }
}