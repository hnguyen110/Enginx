namespace API.DTOs.BankCard;

public class RetrieveAllBankCardsDTO
{
    public string? Id { get; set; }
    public string? CardType { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? CardVerificationCode { get; set; }
}