namespace API.DTOs.Reservation;

public class RetrieveAllReservationsDTO
{
    public string? Date { get; set; }
    public string? CheckInDate { get; set; }
    public string? CheckOutDate { get; set; }
    public string? CheckInTime { get; set; }
    public string? CheckOutTime { get; set; }
    
}