namespace API.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task Save(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrieveVehicleById(string? owner, string? id, CancellationToken cancellationToken);

    public Task<Models.Vehicle?> RetrievePublishedVehicleById(string? id, CancellationToken cancellationToken);

    public Task<List<Models.Vehicle>> RetrieveAllVehicles(string? owner, CancellationToken cancellationToken);
    public Task<List<Models.Review>> RetrieveAllVehicleReviews(string? vehicle, CancellationToken cancellationToken);
    public Task<List<Models.Vehicle>> RetrieveAllPublishedVehicles(CancellationToken cancellationToken);
    public Task ApproveVehicle(string? Id, CancellationToken cancellationToken);
}