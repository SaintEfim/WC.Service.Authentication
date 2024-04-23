using AutoMapper;
using WC.Service.Authentication.Data.Models;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAuthenticationModel, UserAuthenticationEntity>().ReverseMap();
    }
}