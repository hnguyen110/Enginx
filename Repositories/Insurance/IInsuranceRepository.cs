namespace API.Repositories.Insurance;

public interface IInsuranceRepository
{
    public Task Save(Models.Insurance insurance, CancellationToken cancellationToken);
}