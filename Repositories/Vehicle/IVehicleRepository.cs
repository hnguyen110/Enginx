namespace API.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task Save(Models.Vehicle vehicle, CancellationToken cancellationToken);
    public Task<Models.Vehicle?> RetrieveVehicleById(string? owner, string? id, CancellationToken cancellationToken);
    
    public Task<List<Models.Vehicle>> RetrieveAllVehicles(string? owner, CancellationToken cancellationToken);
}