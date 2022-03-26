using API.DTOs.Account;
using API.DTOs.Authentication;
using API.DTOs.BankCard;
using API.DTOs.Insurance;
using API.DTOs.Profile;
using API.DTOs.Reservation;
using API.DTOs.Review;
using API.DTOs.Vehicle;
using API.Handlers.Authentication;
using API.Handlers.Vehicle;
using API.Models;
using AutoMapper;

namespace API.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankCard, RetrieveAllBankCardsDTO>();
        CreateMap<Vehicle, RetrieveVehicleDTO>();
        CreateMap<Vehicle, RetrieveAllVehiclesDTO>()
            .ForMember(e => e.Owner,
                option =>
                {
                    option.MapFrom(e =>
                        e.OwnerReference!.ContactInformationReference!.FirstName + " " +
                        e.OwnerReference.ContactInformationReference.LastName);
                });
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
                e => e.VehicleId,
                option => option
                    .MapFrom(e => e.VehicleReference!.Id)
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
        CreateMap<Account, RetrieveAllClientAccountDTO>()
            .ForMember(e => e.FirstName, option => option.MapFrom(e => e.ContactInformationReference!.FirstName))
            .ForMember(e => e.MiddleName, option => option.MapFrom(e => e.ContactInformationReference!.MiddleName))
            .ForMember(e => e.LastName, option => option.MapFrom(e => e.ContactInformationReference!.LastName))
            .ForMember(e => e.Email, option => option.MapFrom(e => e.ContactInformationReference!.Email))
            .ForMember(e => e.ContactNumber,
                option => option.MapFrom(e => e.ContactInformationReference!.ContactNumber))
            .ForMember(e => e.StreetNumber, option => option.MapFrom(e => e.AddressReference!.StreetNumber))
            .ForMember(e => e.StreetName, option => option.MapFrom(e => e.AddressReference!.StreetName))
            .ForMember(e => e.City, option => option.MapFrom(e => e.AddressReference!.City))
            .ForMember(e => e.State, option => option.MapFrom(e => e.AddressReference!.State))
            .ForMember(e => e.Country, option => option.MapFrom(e => e.AddressReference!.Country))
            .ForMember(e => e.PostalCode, option => option.MapFrom(e => e.AddressReference!.PostalCode));
        CreateMap<BankCard, BankCard>()
            .ForMember(e => e.Id, option => option.Ignore())
            .ForMember(e => e.Account, option => option.Ignore())
            .ForMember(e => e.AccountReference, option => option.Ignore())
            .ForMember(e => e.CardType, option => option.PreCondition(e => !string.IsNullOrEmpty(e.CardType)))
            .ForMember(e => e.CardHolderName,
                option => option.PreCondition(e => !string.IsNullOrEmpty(e.CardHolderName)))
            .ForMember(e => e.CardNumber, option => option.PreCondition(e => !string.IsNullOrEmpty(e.CardNumber)))
            .ForMember(e => e.ExpireDate,
                option => option.PreCondition(e => e.ExpireDate != null && e.ExpireDate != DateTime.MinValue))
            .ForMember(e => e.CardVerificationCode,
                option => option.PreCondition(e => !string.IsNullOrEmpty(e.CardVerificationCode)));
        CreateMap<UpdateVehicleInformation.Command, Vehicle>()
            .ForMember(e => e.Id, option => option.Ignore())
            .ForMember(e => e.BodyType, option => option.PreCondition(e => !string.IsNullOrEmpty(e.BodyType)))
            .ForMember(e => e.Color, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Color)))
            .ForMember(e => e.Description, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Description)))
            .ForMember(e => e.EngineType, option => option.PreCondition(e => !string.IsNullOrEmpty(e.EngineType)))
            .ForMember(e => e.FuelType, option => option.PreCondition(e => !string.IsNullOrEmpty(e.FuelType)))
            .ForMember(e => e.TransmissionType,
                option => option.PreCondition(e => !string.IsNullOrEmpty(e.TransmissionType)))
            .ForMember(e => e.Location, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Location)))
            .ForMember(e => e.Make, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Make)))
            .ForMember(e => e.Model, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Model)))
            .ForMember(e => e.Mileage, option => option.PreCondition(e => e.Mileage >= 0))
            .ForMember(e => e.Price, option => option.PreCondition(e => e.Price >= 0))
            .ForMember(e => e.Year, option => option.PreCondition(e => e.Year >= 0));
        CreateMap<Reservation, RetrieveCustomerReservationsDTO>()
            .ForMember(e => e.Vehicle, option => option.MapFrom(e => e.VehicleReference!.Id))
            .ForMember(e => e.Name,
                option => option.MapFrom(e => $"{e.VehicleReference!.Make} {e.VehicleReference!.Model}"))
            .ForMember(e => e.Location, option => option.MapFrom(e => e.VehicleReference!.Location))
            .ForMember(e => e.Amount, option => option.MapFrom(e => e.TransactionReference!.Amount));
        CreateMap<UpdateContactInfoDTO, ContactInformation>()
            .ForMember(e => e.FirstName, option => option.PreCondition(e => !string.IsNullOrEmpty(e.FirstName)))
            .ForMember(e => e.MiddleName, option => option.PreCondition(e => !string.IsNullOrEmpty(e.MiddleName)))
            .ForMember(e => e.LastName, option => option.PreCondition(e => !string.IsNullOrEmpty(e.LastName)))
            .ForMember(e => e.Email, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Email)))
            .ForMember(e => e.ContactNumber,
                option => option.PreCondition(e => !string.IsNullOrEmpty(e.ContactNumber)));
        CreateMap<UpdateAddressDTO, Address>()
            .ForMember(e => e.StreetNumber, option => option.PreCondition(e => e.StreetNumber >= 0))
            .ForMember(e => e.StreetName, option => option.PreCondition(e => !string.IsNullOrEmpty(e.StreetName)))
            .ForMember(e => e.City, option => option.PreCondition(e => !string.IsNullOrEmpty(e.City)))
            .ForMember(e => e.State, option => option.PreCondition(e => !string.IsNullOrEmpty(e.State)))
            .ForMember(e => e.Country, option => option.PreCondition(e => !string.IsNullOrEmpty(e.Country)))
            .ForMember(e => e.PostalCode, option => option.PreCondition(e => !string.IsNullOrEmpty(e.PostalCode)));
        CreateMap<SignUp.Command, SignUpDTO>();
        CreateMap<Account, ApproveAccountDTO>();
        CreateMap<Account, DisapproveAccountDTO>();
        CreateMap<Vehicle, PublishVehicleDTO>();
        CreateMap<Vehicle, HideVehicleDTO>();
        CreateMap<BankCard, CreateBankCardDTO>();
        CreateMap<Insurance, CreateInsuranceDTO>();
        CreateMap<Review, CreateVehicleReviewDTO>();

        CreateMap<BankCard, RetrieveBankCardDTO>();

    }
    
}