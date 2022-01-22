namespace API.Repositories.Account;

public interface IAccountRepository
{
    public Task Save(Models.Account account, CancellationToken cancellationToken);
    public Task<Models.Account?> FindByUsername(string username, CancellationToken cancellationToken);
}