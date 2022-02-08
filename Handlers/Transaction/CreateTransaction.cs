using API.Repositories.Transaction;
using API.Utilities.CredentialAccessor;
using MediatR;

namespace API.Handlers.Transaction;

public class CreateTransaction
{
    public class Command : IRequest<Unit>
    {
        public double Amount { get; set; }
        public string? Receiver { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ICredentialAccessor _accessor;
        private readonly ITransactionRepository _repository;

        public Handler(ICredentialAccessor accessor, ITransactionRepository repository)
        {
            _accessor = accessor;
            _repository = repository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var sender = _accessor.RetrieveAccountId();
            var transaction = new Models.Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Amount = request.Amount,
                Date = DateTime.Today,
                Time = DateTime.Now,
                Sender = sender,
                Receiver = request.Receiver
            };
            await _repository.Save(transaction, cancellationToken);
            return Unit.Value;
        }
    }
}