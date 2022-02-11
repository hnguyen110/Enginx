namespace API.Repositories.Reservation;

public interface IReservationRepository
{
    public Task Save(Models.Reservation reservation, CancellationToken cancellationToken);

    public Task<List<Models.Reservation>> RetrieveAllReservationsByVehicleId(string? id,
        CancellationToken cancellationToken);
}