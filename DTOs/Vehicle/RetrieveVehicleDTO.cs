namespace API.DTOs.Vehicle;

public class RetrieveVehicleDTO
{
    public string? BodyType { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public string? EngineType { get; set; }
    public string? FuelType { get; set; }
    public string? Location { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public double Mileage { get; set; }
    public double Price { get; set; }
    public int Year { get; set; }
}