using API.DatabaseContext;

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
}