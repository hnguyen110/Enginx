namespace API.Repositories.ContactInformation;

public interface IContactInformationRepository
{
    public Task<Models.ContactInformation?> FindContactInformationById(string id, CancellationToken cancellationToken);
    public Task DeleteContactInformation(Models.ContactInformation contact, CancellationToken cancellationToken);
    public Task UpdateContactInformation(CancellationToken cancellationToken);
}