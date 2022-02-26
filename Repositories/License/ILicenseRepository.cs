namespace API.Repositories.License;

public interface ILicenseRepository
{
    public Task DeleteLicense(Models.License license, CancellationToken cancellationToken);
}