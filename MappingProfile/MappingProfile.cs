using API.DTOs.BankCard;
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
    }
}