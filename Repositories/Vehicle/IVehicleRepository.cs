namespace API.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task Save(Models.Vehicle? vehicle, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrieveVehicleById(string? id, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrieveVehicleById(string? owner, string? id, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrievePublishedVehicleById(string? id, CancellationToken cancellationToken);

    public Task<List<Models.Vehicle>> RetrieveAllVehiclesByOwnerId(string? owner, CancellationToken cancellationToken);

    public Task<List<Models.Vehicle>> RetrieveVehiclesByLocation(string? location,
        CancellationToken cancellationToken);

    public Task<List<Models.Review>> RetrieveAllVehicleReviews(string? vehicle, CancellationToken cancellationToken);
    public Task<List<Models.Vehicle>> RetrieveAllPublishedVehicles(CancellationToken cancellationToken);

    public Task<List<Models.Vehicle>> RetrieveAllVehicles(CancellationToken cancellationToken);
    public Task ApproveVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task RejectVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task DeleteVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task PublishVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task UnpublishVehicle(Models.Vehicle vehicle, CancellationToken cancellationToken);
}