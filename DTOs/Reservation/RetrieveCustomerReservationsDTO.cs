namespace API.DTOs.Reservation;

public class RetrieveCustomerReservationsDTO
{
    public string? Date { get; set; }
    public string? Vehicle { get; set; }
    public string? Insurance { get; set; }
    public string? CheckInDate { get; set; }
    public string? CheckOutDate { get; set; }
    public string? CheckInTime { get; set; }
    public string? CheckOutTime { get; set; }
}