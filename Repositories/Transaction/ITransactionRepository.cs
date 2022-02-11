namespace API.Repositories.Transaction;

public interface ITransactionRepository
{
    public Task Save(Models.Transaction transaction, CancellationToken cancellationToken);
}