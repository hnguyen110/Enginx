namespace API.Repositories.BankCard;

public interface IBankCardRepository
{
    public Task Save(Models.BankCard bankCard, CancellationToken cancellationToken);

    public Task<List<Models.BankCard>> RetrieveAllBankCardsByAccount(string? account,
        CancellationToken cancellationToken);
}