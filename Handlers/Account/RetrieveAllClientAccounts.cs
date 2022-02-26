using System.Net;
using API.DTOs.Account;
using API.Exceptions;
using API.Repositories.Account;
using API.Utilities.Messages;
using API.Utilities.Security;
using AutoMapper;
using MediatR;

namespace API.Handlers.Account;

public class RetrieveAllClientAccounts
{
    public class Query : IRequest<List<RetrieveAllClientAccountDTO>>
    {
    }

    public class Handler : IRequestHandler<Query, List<RetrieveAllClientAccountDTO>>
    {
        private readonly IAccountRepository _account;
        private readonly IAuthorization _authorization;
        private readonly IMapper _mapper;

        public Handler(IAuthorization authorization, IAccountRepository account, IMapper mapper)
        {
            _authorization = authorization;
            _account = account;
            _mapper = mapper;
        }

        public async Task<List<RetrieveAllClientAccountDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var records = await _account.RetrieveAllClientAccounts(cancellationToken);
            return _mapper.Map<List<Models.Account>, List<RetrieveAllClientAccountDTO>>(records);
        }
    }
}