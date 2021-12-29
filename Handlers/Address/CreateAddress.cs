using API.DatabaseContext;
using API.Utilities.CredentialAccessor;
using FluentValidation;
using MediatR;

namespace API.Handlers.Address;

public class CreateAddress
{
    public class Command : IRequest<Unit>
    {
        public int StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(e => e.Country)
                .NotNull()
                .WithMessage("This field can not be null")
                .NotEmpty()
                .WithMessage("This field can not be empty");
        }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly Context _database;

        public Handler(Context database, ICredentialAccessor accessor)
        {
            _database = database;
            _accessor = accessor;
        }
        
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            // var address = new Models.Address
            // {
            //     StreetNumber = request.StreetNumber,
            //     StreetName = request.StreetName,
            //     City = request.City,
            //     State = request.State,
            //     Country = request.Country,
            //     PostalCode = request.PostalCode,
            //     Id = Guid.NewGuid().ToString()
            // };
            //
            // await _database.AddAsync(address, cancellationToken);
            await _database.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}