namespace API.Models;

public class Transaction
{
    public string? Id { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public string? Sender { get; set; }
    public Account? SenderReference { get; set; }
    public string? Receiver { get; set; }
    public Account? ReceiverReference { get; set; }

    public Reservation? Reservation { get; set; }
}