namespace API.Repositories.Account;

public interface IAccountRepository
{
    public Task Save(Models.Account account, CancellationToken cancellationToken);
    public Task<Models.Account?> FindByUsername(string username, CancellationToken cancellationToken);
    public Task<Models.Account?> FindById(string id, CancellationToken cancellationToken);
    public Task<List<Models.Account>> RetrieveAllClientAccounts(CancellationToken cancellationToken);
    public Task ChangePassword(string id, string oldPassword, string newPassword, CancellationToken cancellationToken);

    public Task ApproveAccount(Models.Account account, CancellationToken cancellationToken);
    public Task DisapproveAccount(Models.Account account, CancellationToken cancellationToken);

    public Task DeleteClientAccount(Models.Account account, CancellationToken cancellationToken);
}