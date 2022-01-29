using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using API.Utilities.Security;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly Context _database;
    private readonly ISecurity _security;

    public AccountRepository(Context database, ISecurity security)
    {
        _database = database;
        _security = security;
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

    public Task<Models.Account?> FindById(string id, CancellationToken cancellationToken)
    {
        var record = _database
            .Account
            !.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        return record;
    }

    public async Task ChangePassword(string id, string oldPassword, string newPassword,
        CancellationToken cancellationToken)
    {
        var record = await FindById(id, cancellationToken);
        if (record == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.NotFound
            );
        var passwordMatched = _security.Compare(oldPassword, record.PasswordSalt, record.PasswordHash);
        if (!passwordMatched)
            throw new ApiException(
                HttpStatusCode.Unauthorized,
                ApiErrorMessages.Unauthorized
            );
        record.PasswordHash = _security.CreatePasswordHash(newPassword, record.PasswordSalt);
        await _database.SaveChangesAsync(cancellationToken);
    }
}