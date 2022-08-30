using API.DTOs.Insurance;
using API.Repositories.Insurance;
using AutoMapper;
using MediatR;

namespace API.Handlers.Insurance;

public class RetrieveAllInsurances
{
    public class Query : IRequest<List<RetrieveAllInsurancesDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllInsurancesDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepository _repository;

        public Handler(IMapper mapper, IInsuranceRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<RetrieveAllInsurancesDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var records = await _repository.RetrieveAllInsurances(cancellationToken);

            return _mapper.Map<List<Models.Insurance>, List<RetrieveAllInsurancesDTO>>(records);
        }
    }
}