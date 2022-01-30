namespace API.Repositories.BankCard;

public interface IBankCardRepository
{
    public Task Save(Models.BankCard bankCard, CancellationToken cancellationToken);
}