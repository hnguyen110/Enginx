namespace API.Models;

public class VehiclePicture : PhysicalContent
{
    public string? Vehicle { get; set; }
    public Vehicle? VehicleReference { get; set; }
}