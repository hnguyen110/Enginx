using API.DTOs.Vehicle;
using API.Models;
using API.Repositories.Vehicle;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllReviews
{
    public class Query : IRequest<List<RetrieveAllReviewsDTO>>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllReviewsDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;


        public Handler(IMapper mapper, IVehicleRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<RetrieveAllReviewsDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _repository
                .RetrieveAllVehicleReviews(
                    request.Id,
                    cancellationToken
                );
            return _mapper.Map<List<Review>, List<RetrieveAllReviewsDTO>>(records);
        }
    }
}