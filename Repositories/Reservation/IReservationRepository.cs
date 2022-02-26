namespace API.Repositories.Reservation;

public interface IReservationRepository
{
    public Task Save(Models.Reservation reservation, CancellationToken cancellationToken);

    public Task<List<Models.Reservation>> RetrieveAllReservationsByVehicleId(string? id,
        CancellationToken cancellationToken);

    public Task<Models.Reservation?> RetrieveUpcomingReservationByAccountId(string? id,
        CancellationToken cancellationToken);

    public Task UpdateUpcomingReservationStatus(
        Models.Reservation reservation, bool status, CancellationToken cancellationToken);
    public Task<List<Models.Reservation>> RetrieveCustomerReservationsById(string id, CancellationToken cancellationToken);
}