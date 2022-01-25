namespace API.Repositories.Account;

public interface IProfilePictureRepository
{
    public Task<string> SaveProfilePicture(IFormFile file, CancellationToken cancellationToken);

    public Task SaveToAccount(string account, string id, CancellationToken cancellationToken);
}