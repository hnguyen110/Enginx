using API.DatabaseContext;

namespace API.Repositories.Reservation;

public class ReservationRepository : IReservationRepository
{
    private readonly Context _database;

    public ReservationRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.Reservation reservation, CancellationToken cancellationToken)
    {
        await _database.AddAsync(reservation, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<Models.Reservation>> RetrieveAllReservationsByVehicleID(string? Id,
        CancellationToken cancellationToken)
    {
        var records = await _database
            .Reservation!
            .Where(e => e.Vehicle == Id)
            .ToListAsync(cancellationToken);
        return records;
    }
}

