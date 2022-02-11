namespace API.Models;

public class Reservation
{
    public string? Id { get; set; }
    public DateTime? Date { get; set; }

    public string? Vehicle { get; set; }
    public Vehicle? VehicleReference { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }
    public string? Transaction { get; set; }
    public Transaction? TransactionReference { get; set; }

    public string? Insurance { get; set; }
    public Insurance? InsuranceReference { get; set; }
}