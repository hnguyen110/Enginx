namespace API.Repositories.Address;

public interface IAddressRepository
{
    public Task<Models.Address?> FindAddressById(string id, CancellationToken cancellationToken);
    public Task DeleteAddress(Models.Address address, CancellationToken cancellationToken);
}