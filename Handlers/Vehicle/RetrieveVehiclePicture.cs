using System.Net;
using API.DTOs.Vehicle;
using API.Exceptions;
using API.Models;
using API.Repositories.VehiclePicture;
using API.Utilities.Messages;
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
            if (!records.Any())
                throw new ApiException(HttpStatusCode.NotFound, ApiErrorMessages.NotFound);
            return _mapper
                .Map<List<VehiclePicture>, List<RetrieveVehiclePicturesDTO>>(records);
        }
    }
}