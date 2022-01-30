namespace API.Models;

public class BankCard
{
    public string? Id { get; set; }
    public string? Account { get; set; }
    public Account? AccountReference { get; set; }
    public string? CardType { get; set; }
    public string? CardHolderName { get; set; }
    public string? CardNumber { get; set; }
    public DateTime? ExpireDate { get; set; }
    public string? CardVerificationCode { get; set; }
}