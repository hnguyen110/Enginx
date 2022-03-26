using API.DTOs.Vehicle;
using API.Repositories.Vehicle;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveAllPublishedVehicles
{
    public class Query : IRequest<List<RetrieveAllVehiclesDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllVehiclesDTO>>

    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IVehicleRepository _repository;

        public Handler(IVehicleRepository repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }


        public async Task<List<RetrieveAllVehiclesDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _repository.RetrieveAllPublishedVehicles(cancellationToken);
            var results = _mapper.Map<List<Models.Vehicle>, List<RetrieveAllVehiclesDTO>>(records);

            foreach (var vehicle in results)
            {
                var pictures = await _mediator
                    .Send(
                        new RetrieveVehiclePictures.Query { Id = vehicle.Id },
                        cancellationToken
                    );

                vehicle.Pictures = pictures;
            }

            return results;
        }
    }
}