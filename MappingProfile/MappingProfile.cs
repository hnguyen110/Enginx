using API.DTOs.BankCard;
using API.Models;
using AutoMapper;

namespace API.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BankCard, RetrieveAllBankCardsDTO>();
    }
}