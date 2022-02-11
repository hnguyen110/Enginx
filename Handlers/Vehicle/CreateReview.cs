using API.Models;
using API.Repositories.Review;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Vehicle;

public class CreateReview
{
    public class Command : IRequest<Unit>
    {
        public string? Vehicle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IReviewRepository _repository;

        public Handler(ICredentialAccessor accessor, IReviewRepository repository)
        {
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var reviewer = _accessor.RetrieveAccountId();
            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Reviewer = reviewer,
                Vehicle = request.Vehicle,
                Date = DateTime.Today,
                Title = request.Title,
                Description = request.Description
            };
            await _repository.Save(review, cancellationToken);
            return Unit.Value;
        }
    }
}