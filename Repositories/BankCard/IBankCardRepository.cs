namespace API.Repositories.BankCard;

public interface IBankCardRepository
{
    public Task Save(Models.BankCard bankCard, CancellationToken cancellationToken);
    public Task<Models.BankCard?> RetrieveBankCardById(string? id, CancellationToken cancellationToken);

    public Task<List<Models.BankCard>> RetrieveAllBankCardsByAccount(string? account,
        CancellationToken cancellationToken);

    public Task UpdateBankCard(Models.BankCard bankCard, Models.BankCard updates, CancellationToken cancellationToken);

    public Task DeleteBankCard(Models.BankCard? bankcard, CancellationToken cancellationToken);
}