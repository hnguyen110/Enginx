using System.Net;
using API.DatabaseContext;
using API.Exceptions;
using API.Utilities.Messages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Review;

public class ReviewRepository : IReviewRepository
{
    private readonly Context _context;

    public ReviewRepository(Context context)
    {
        _context = context;
    }

    public async Task Save(Models.Review review, CancellationToken cancellationToken)
    {
        var account = await _context
            .Account!
            .FirstOrDefaultAsync(
                e => e.Id == review.Reviewer,
                cancellationToken
            );
        if (account == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.RecordNotFound
            );
        var vehicle = await _context
            .Vehicle!
            .FirstOrDefaultAsync(
                e => e.Id == review.Vehicle
                     && e.Approved == true
                     && e.Published == true,
                cancellationToken
            );
        if (vehicle == null)
            throw new ApiException(
                HttpStatusCode.NotFound,
                ApiErrorMessages.RecordNotFound
            );
        await _context.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}