namespace API.Repositories.Profile;

public interface IProfilePictureRepository
{
    public Task<string> SaveProfilePicture(IFormFile file, CancellationToken cancellationToken);

    public Task SaveToAccount(string account, string id, CancellationToken cancellationToken);

    public Task<string?> RetrieveProfilePictureByAccount(string account, CancellationToken cancellationToken);

    public Task DeleteProfilePicture(Models.ProfilePicture profilePicture, CancellationToken cancellationToken);
}