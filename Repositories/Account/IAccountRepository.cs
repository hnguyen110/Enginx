namespace API.Repositories.Account;

public interface IAccountRepository
{
    public Task Save(Models.Account account, CancellationToken cancellationToken);
    public Task<Models.Account?> FindByUsername(string username, CancellationToken cancellationToken);
    public Task<Models.Account?> FindById(string id, CancellationToken cancellationToken);
    public Task ChangePassword(string id, string oldPassword, string newPassword, CancellationToken cancellationToken);
}