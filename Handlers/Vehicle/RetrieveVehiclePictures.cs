using API.Repositories.VehiclePicture;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveVehiclePictures
{
    public class Query : IRequest<List<string>>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<string>>
    {
        private readonly IMapper _mapper;
        private readonly IVehiclePictureRepository _vehiclePictureRepository;

        public Handler(IMapper mapper, IVehiclePictureRepository repository)
        {
            _mapper = mapper;
            _vehiclePictureRepository = repository;
        }

        public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _vehiclePictureRepository
                .RetrieveVehiclePicturesById(
                    request.Id,
                    cancellationToken
                );

            return records;
        }
    }
}