using API.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Vehicle;

public class VehicleRepository : IVehicleRepository
{
    private readonly Context _database;

    public VehicleRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.Vehicle vehicle, CancellationToken cancellationToken)
    {
        await _database.AddAsync(vehicle, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Vehicle?> RetrieveVehicleById(string? owner, string? id,
        CancellationToken cancellationToken)
    {
        var record = await _database
            .Vehicle!
            .FirstOrDefaultAsync(
                e => owner != null ? e.Owner == owner && e.Id == id : e.Id == id,
                cancellationToken
            );
        return record;
    }

    public async Task<List<Models.Vehicle>> RetrieveAllVehicles(string? owner, CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .Where(e => e.Owner == owner)
            .ToListAsync(cancellationToken);
        return records;
    }
    
    public async Task<List<Models.Vehicle>> RetrieveAllPublishedVehicles(CancellationToken cancellationToken)
    {
        var records = await _database
            .Vehicle!
            .Where(e => e.Published == true)
            .ToListAsync(cancellationToken);
        return records;
    }
}