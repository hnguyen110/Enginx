using API.DTOs.Review;
using API.Models;
using API.Repositories.Review;
using API.Utilities.CredentialAccessor;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class CreateReview
{
    public class Command : IRequest<CreateVehicleReviewDTO>
    {
        public string? Vehicle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class Handler : IRequestHandler<Command, CreateVehicleReviewDTO>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public Handler(ICredentialAccessor accessor, IReviewRepository repository, IMapper mapper)
        {
            _accessor = accessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateVehicleReviewDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var reviewer = _accessor.RetrieveAccountId();
            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                Reviewer = reviewer,
                Vehicle = request.Vehicle,
                Date = DateTime.Now,
                Title = request.Title,
                Description = request.Description
            };
            await _repository.Save(review, cancellationToken);
            return _mapper.Map<Review, CreateVehicleReviewDTO>(review);
        }
    }
}