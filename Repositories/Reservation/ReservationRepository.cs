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
        // var record =
        // await _database.Reservation!.FirstOrDefaultAsync(e => e. , cancellationToken);

        // if (record != null)
        //     throw new ApiException(HttpStatusCode.BadRequest, ApiErrorMessages.RecordExisted);

        await _database.AddAsync(reservation, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }
}