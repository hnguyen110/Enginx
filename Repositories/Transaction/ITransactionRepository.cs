namespace API.Repositories.Transaction;

public interface ITransactionRepository
{
    public Task Save(Models.Transaction transaction, CancellationToken cancellationToken);

    public Task<Models.Transaction?> RetrieveLatestTransactionByAccountId(string? id,
        CancellationToken cancellationToken);
}