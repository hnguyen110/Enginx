using System.Net;
using API.DTOs.Insurance;
using API.Exceptions;
using API.Repositories.Insurance;
using API.Utilities.Messages;
using API.Utilities.Security;
using AutoMapper;
using MediatR;

namespace API.Handlers.Insurance;

public class CreateInsurance
{
    public class Command : IRequest<RetrieveAllInsurancesDTO>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }

    public class Handler : IRequestHandler<Command, RetrieveAllInsurancesDTO>
    {
        private readonly IAuthorization _authorization;
        private readonly IInsuranceRepository _repository;
        private readonly IMapper _mapper;


        public Handler(IInsuranceRepository repository, IAuthorization authorization , IMapper mapper)
        {
            _repository = repository;
            _authorization = authorization;
            _mapper = mapper;
        }

        public async Task<RetrieveAllInsurancesDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdmin = await _authorization.IsAdministrator();
            if (!isAdmin)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );
            var insurance = new Models.Insurance
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            await _repository.Save(insurance, cancellationToken);
            return _mapper.Map<Models.Insurance , RetrieveAllInsurancesDTO>(insurance);;
        }
    }
}