using API.Handlers.Account;
using API.Handlers.Address;
using API.Handlers.ContactInformation;
using MediatR;

namespace API.Handlers.Authentication;

public class SignUp
{
    public class Command : IRequest<Unit>
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IMediator _mediator;

        public Handler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var contactInformation = await _mediator.Send(new CreateContactInformation.Command
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                ContactNumber = request.ContactNumber
            }, cancellationToken);

            var address = await _mediator.Send(new CreateAddress.Command
            {
                StreetNumber = request.StreetNumber,
                StreetName = request.StreetName,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode
            }, cancellationToken);

            var credential = await _mediator.Send(new CreateAccount.Command
            {
                Username = request.Username,
                Password = request.Password,
                Address = address,
                ContactInformation = contactInformation
            }, cancellationToken);

            return Unit.Value;
        }
    }
}