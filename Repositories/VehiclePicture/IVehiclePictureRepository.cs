namespace API.Repositories.VehiclePicture;

public interface IVehiclePictureRepository
{
    public Task<string> SaveVehiclePictures(IFormFile file, CancellationToken cancellationToken);
    public Task SaveToVehicle(string vehicle, string id, CancellationToken cancellationToken);

    public Task<List<string>> RetrieveVehiclePicturesById(string? id,
        CancellationToken cancellationToken);
}