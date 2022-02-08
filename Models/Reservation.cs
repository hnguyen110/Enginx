namespace API.Models;

public class Reservation
{
    public string? Id { get; set; }
    public string? Transaction { get; set; }
    public Transaction? TransactionReference { get; set; }
}