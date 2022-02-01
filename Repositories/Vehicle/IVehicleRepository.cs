using System.Xml.Linq;

namespace API.Repositories.Vehicle;

public interface IVehicleRepository
{
    public Task Save(Models.Vehicle Vehicle, CancellationToken cancellationToken);
}