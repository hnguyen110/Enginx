using API.DatabaseContext;

namespace API.Repositories.BankCard;

public class BankCardRepository : IBankCardRepository
{
    private readonly Context _database;

    public BankCardRepository(Context database)
    {
        _database = database;
    }
    
    public async Task Save(Models.BankCard bankCard, CancellationToken cancellationToken)
    {
        await _database.AddAsync(bankCard, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }
}