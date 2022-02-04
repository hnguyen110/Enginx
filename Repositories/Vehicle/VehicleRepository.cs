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
}