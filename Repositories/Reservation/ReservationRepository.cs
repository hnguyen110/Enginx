using API.DatabaseContext;
using API.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Models.Reservation>> RetrieveAllReservationsByVehicleId(string? id,
        CancellationToken cancellationToken)
    {
        var records = await _database
            .Reservation!
            .Where(e => e.Vehicle == id)
            .ToListAsync(cancellationToken);
        return records;
    }

    public async Task<List<Models.Reservation>> RetrieveCustomerReservationsById(string id, CancellationToken cancellationToken)
    {
        var records = await _database.Reservation!
            .Where(e=> e.TransactionReference!.Sender == id)
            .ToListAsync(cancellationToken);
        return records;
    }
}