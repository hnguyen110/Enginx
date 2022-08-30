using System.Net;
using API.DTOs.Account;
using API.Exceptions;
using API.Repositories.Account;
using API.Utilities.Messages;
using API.Utilities.Security;
using AutoMapper;
using MediatR;

namespace API.Handlers.Account;

public class DisapproveAccount
{
    public class Command : IRequest<DisapproveAccountDTO>
    {
        public string? Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, DisapproveAccountDTO>
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

        public async Task<DisapproveAccountDTO> Handle(Command request, CancellationToken cancellationToken)
        {
            var isAdministrator = await _authorization.IsAdministrator();
            if (!isAdministrator)
                throw new ApiException(
                    HttpStatusCode.Unauthorized,
                    ApiErrorMessages.Unauthorized
                );

            var record = await _account.FindById(request.Id!, cancellationToken);
            if (record == null)
                throw new ApiException(
                    HttpStatusCode.NotFound,
                    ApiErrorMessages.NotFound
                );

            await _account.DisapproveAccount(record, cancellationToken);
            return _mapper.Map<Models.Account, DisapproveAccountDTO>(record);
        }
    }
}