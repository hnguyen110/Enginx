using API.DatabaseContext;
using API.Repositories.Transaction;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Reservation;

public class ReservationRepository : IReservationRepository
{
    private readonly Context _database;
    private readonly ITransactionRepository _transaction;

    public ReservationRepository(Context database, ITransactionRepository transactionRepository)
    {
        _database = database;
        _transaction = transactionRepository;
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

    public async Task<Models.Reservation?> RetrieveUpcomingReservationByAccountId(
        string? id,
        CancellationToken cancellationToken
    )
    {
        var transaction = await
            _transaction
                .RetrieveLatestTransactionByAccountId(id, cancellationToken);
        if (transaction == null)
            return null;
        var record = await
                _database
                    .Reservation!
                    .Include(e => e.InsuranceReference)
                    .Include(e => e.VehicleReference)
                    .FirstOrDefaultAsync(
                        e => e.Transaction == transaction.Id
                             && e.Status == true,
                        cancellationToken
                    )
            ;
        return record;
    }

    public async Task UpdateUpcomingReservationStatus(Models.Reservation reservation, bool status,
        CancellationToken cancellationToken)
    {
        reservation.Status = status;
        await _database.SaveChangesAsync(cancellationToken);
    }
}