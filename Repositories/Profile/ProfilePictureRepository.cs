using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Models;
using API.Utilities.Constants;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Profile;

public class ProfilePictureRepository : IProfilePictureRepository
{
    private readonly Context _context;
    private readonly IWebHostEnvironment _environment;

    public ProfilePictureRepository(IWebHostEnvironment environment, Context context)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<string> SaveProfilePicture(IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null)
            throw new ApiException(HttpStatusCode.BadRequest, ValidationErrorMessages.Required);
        var extensions = new[] { "image/jpg", "image/jpeg", "image/png" };
        if (!extensions.Contains(file.ContentType))
            throw new ApiException(HttpStatusCode.BadRequest, ValidationErrorMessages.UnsupportedFileFormat);
        if (file.Length > AccountConstants.ProfilePictureSizeLimit)
            throw new ApiException(HttpStatusCode.BadRequest, ValidationErrorMessages.LargeFile);
        var id = Guid.NewGuid().ToString();
        await using var stream = new FileStream(
            Path.Combine(_environment.ContentRootPath, $"Uploads/{id}"),
            FileMode.Create,
            FileAccess.Write);
        await file.CopyToAsync(stream, cancellationToken);
        return id;
    }

    public async Task SaveToAccount(string account, string id, CancellationToken cancellationToken)
    {
        var result = await
            _context
                .Account!
                .FirstOrDefaultAsync(
                    e =>
                        e.Id == account, cancellationToken
                );
        if (result == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.NotFound
            );
        var picture = new ProfilePicture
        {
            Id = Guid.NewGuid().ToString(),
            FilePath = Path.Combine(_environment.ContentRootPath, $"Uploads/{id}")
        };
        await _context.AddAsync(picture, cancellationToken);
        result.ProfilePicture = picture.Id;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<string?> RetrieveProfilePictureByAccount(string account, CancellationToken cancellationToken)
    {
        var record = await _context
            .Account!
            .FirstOrDefaultAsync(
                e => e.Id == account,
                cancellationToken
            );
        if (record?.ProfilePicture == null) return null;
        var path = await _context
            .ProfilePicture!
            .FirstOrDefaultAsync(
                e => e.Id == record.ProfilePicture,
                cancellationToken
            );
        return path?.FilePath;
    }

    public async Task DeleteProfilePicture(ProfilePicture profilePicture, CancellationToken cancellationToken)
    {
        _context.ProfilePicture!.Remove(profilePicture);
        await _context.SaveChangesAsync(cancellationToken);
    }
}