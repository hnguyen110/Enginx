using API.DTOs.BankCard;
using API.DTOs.Reservation;
using API.DTOs.Vehicle;
using API.Models;
using AutoMapper;

namespace API.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankCard, RetrieveAllBankCardsDTO>();
        CreateMap<Vehicle, RetrieveVehicleDTO>();
        CreateMap<Reservation, RetrieveAllReservationsDTO>();
    }
}