using API.DTOs.VehiclePicture;
using API.Models;
using AutoMapper;

namespace API.MappingVehiclePicture;

public class MappingVehiclePicture : Profile
{
    public MappingVehiclePicture()
    {
        CreateMap<VehiclePicture, RetrieveVehiclePicturesDTO>();
    }
}