using AutoMapper;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.Login;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.API;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAuthenticationModel, UserAuthenticationDto>();
        
        CreateMap<LoginRequestDto, UserAuthenticationModel>();
        CreateMap<LoginRequestDto, LoginRequestModel>();
        CreateMap<LoginResponseModel, LoginResponseDto>();
        
        CreateMap<ResetPasswordDto, ResetPasswordModel>();
    }
}