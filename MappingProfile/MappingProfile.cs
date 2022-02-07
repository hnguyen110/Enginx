using API.DTOs.BankCard;
using API.DTOs.Vehicle;
using API.DTOs.VehiclePicture;
using API.Models;
using AutoMapper;

namespace API.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankCard, RetrieveAllBankCardsDTO>();
        CreateMap<Vehicle, RetrieveVehicleDTO>();
        CreateMap<Vehicle, RetrieveAllVehicleDTO>();
        CreateMap<VehiclePicture, RetrieveVehiclePicturesDTO>();
    }
}