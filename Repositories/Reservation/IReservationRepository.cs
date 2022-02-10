namespace API.Repositories.Reservation;

public interface IReservationRepository
{
    public Task Save(Models.Reservation reservation, CancellationToken cancellationToken);
    
    public Task<List<Models.Reservation>> RetrieveAllReservationsByVehicleID(string? Id,
        CancellationToken cancellationToken);
}
//
// namespace API.Repositories.BankCard;
//
// public interface IBankCardRepository
// {
//     public Task Save(Models.BankCard bankCard, CancellationToken cancellationToken);
//
//     public Task<List<Models.BankCard>> RetrieveAllBankCardsByAccount(string? account,
//         CancellationToken cancellationToken);
// }