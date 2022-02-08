namespace API.Repositories.Review;

public interface IReviewRepository
{
    public Task Save(Models.Review review, CancellationToken cancellationToken);
}