namespace API.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task Save(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrievedVehicleById(string? owner, string? id, CancellationToken cancellationToken);

    public Task<Models.Vehicle?> RetrievedPublishedVehicleById(string? id, CancellationToken cancellationToken);

    public Task<List<Models.Vehicle>> RetrieveAllVehicles(string? owner, CancellationToken cancellationToken);
    public Task<List<Models.Review>> RetrieveAllVehicleReviews(string? vehicle, CancellationToken cancellationToken);
}