namespace API.DTOs.Reservation;

public class RetrieveCustomerReservationsDTO
{
    public string? Id { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string? Vehicle { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public bool Status { get; set; }
    public double? Amount { get; set; }
}