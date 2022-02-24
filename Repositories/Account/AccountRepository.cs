using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Models;
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
            .Account!
            .Include(e => e.ContactInformationReference)
            .Include(e => e.AddressReference)
            .Include(e => e.LicenseReference)
            .Include(e => e.ProfilePictureReference)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        return record;
    }

    public async Task<List<Models.Account>> RetrieveAllClientAccounts(CancellationToken cancellationToken)
    {
        var records = await _database
            .Account!
            .Where(e =>
                e.Role == Role.Customer
                || e.Role == Role.Owner
            )
            .Include(e => e.ProfilePictureReference)
            .Include(e => e.AddressReference)
            .Include(e => e.ContactInformationReference)
            .ToListAsync(cancellationToken);
        return records;
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

    public async Task ApproveAccount(Models.Account account, CancellationToken cancellationToken)
    {
        if (!account.Approved)
        {
            account.Approved = true;
            await _database.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DisapproveAccount(Models.Account account, CancellationToken cancellationToken)
    {
        if (account.Approved)
        {
            account.Approved = false;
            await _database.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteClientAccount(Models.Account account, CancellationToken cancellationToken)
    {
        _database.Account!.Remove(account);
        await _database.SaveChangesAsync(cancellationToken);
    }
}