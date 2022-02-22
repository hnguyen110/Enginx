using API.DTOs.BankCard;
using API.DTOs.Insurance;
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
        CreateMap<Vehicle, RetrieveAllVehiclesDTO>();
        CreateMap<VehiclePicture, RetrieveVehiclePicturesDTO>();
        CreateMap<Review, RetrieveAllReviewsDTO>()
            .ForMember(e => e.Reviewer,
                option =>
                {
                    option.MapFrom(e =>
                        $"{e.ReviewerReference!.ContactInformationReference!.FirstName} {e.ReviewerReference!.ContactInformationReference!.LastName}");
                });
        CreateMap<Reservation, RetrieveAllReservationsDTO>();
        CreateMap<Insurance, RetrieveAllInsurancesDTO>();
        CreateMap<Reservation, RetrieveUpcomingReservationDTO>()
            .ForMember(
                e => e.Location,
                option => option
                    .MapFrom(e => e.VehicleReference!.Location)
            )
            .ForMember(
                e => e.Price,
                option => option
                    .MapFrom(e => e.TransactionReference!.Amount)
            )
            .ForMember(
                e => e.Insurance,
                option => option
                    .MapFrom(e => e.InsuranceReference!.Name)
            )
            .ForMember(
                e => e.Vehicle,
                option => option
                    .MapFrom(e => $"{e.VehicleReference!.Make} {e.VehicleReference!.Model}")
            )
            .ForMember(
                e => e.BodyType,
                option => option
                    .MapFrom(e => e.VehicleReference!.BodyType)
            )
            .ForMember(
                e => e.Color,
                option => option
                    .MapFrom(e => e.VehicleReference!.Color)
            )
            .ForMember(
                e => e.FuelType,
                option => option
                    .MapFrom(e => e.VehicleReference!.FuelType)
            )
            .ForMember(
                e => e.EngineType,
                option => option
                    .MapFrom(e => e.VehicleReference!.EngineType)
            );
        CreateMap<Insurance, Insurance>()
            .ForMember(e => e.Id, option => option.Ignore())
            .ForMember(e => e.Name, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Name)))
            .ForMember(e => e.Description, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Description)))
            .ForMember(e => e.Price, option => option.PreCondition(e => e.Price >= 0));
    }
}