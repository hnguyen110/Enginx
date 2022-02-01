namespace API.Models;

public class Vehicle
{
    public string? Id { get; set; }
    public string? Account { get; set; }
    public Account? AccountReference { get; set; }
    public string? Location { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? BodyType { get; set; }
    public string? EngineType { get; set; }
    public string? TransmissionType { get; set; }
    public string? FuelType { get; set; }
    public int? Mileage { get; set; }
    public int? NumOfSeats { get; set; }
    public int? MaxNumOfSeats { get; set; }
    public int? RentalPrice { get; set; }   
    public string? Description { get; set; }
    
}