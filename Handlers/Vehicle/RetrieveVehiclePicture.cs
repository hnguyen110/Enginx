using API.DTOs.VehiclePicture;
using API.Repositories.VehiclePicture;
using AutoMapper;
using MediatR;

namespace API.Handlers.Vehicle;

public class RetrieveVehiclePicture
{
    public class Query : IRequest<List<RetrieveVehiclePicturesDTO>>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RetrieveVehiclePicturesDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IVehiclePictureRepository _vehiclePictureRepository;

        public Handler(IMapper mapper, IVehiclePictureRepository repository)
        {
            _mapper = mapper;
            _vehiclePictureRepository = repository;
        }

        public async Task<List<RetrieveVehiclePicturesDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _vehiclePictureRepository
                .RetrieveVehiclePicturesById(
                    request.Id,
                    cancellationToken
                );
            return _mapper
                .Map<List<Models.VehiclePicture>, List<RetrieveVehiclePicturesDTO>>(records);
        }
    }
}