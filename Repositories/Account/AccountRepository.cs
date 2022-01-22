using API.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly Context _database;

    public AccountRepository(Context database)
    {
        _database = database;
    }

    public async Task Save(Models.Account account, CancellationToken cancellationToken)
    {
        await _database.AddAsync(account, cancellationToken);
        await _database.SaveChangesAsync(cancellationToken);
    }

    public async Task<Models.Account?> FindByUsername(string username, CancellationToken cancellationToken)
    {
        var record = await _database
            .Account
            !.FirstOrDefaultAsync(e => e.Username == username, cancellationToken);
        return record;
    }
}