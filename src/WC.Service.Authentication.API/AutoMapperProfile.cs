using AutoMapper;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;

namespace WC.Service.Authentication.API;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AuthenticationLoginRequestDto, AuthenticationLoginRequestModel>();

        CreateMap<AuthenticationLoginResponseModel, AuthenticationLoginResponseDto>();

        CreateMap<AuthenticationResetPasswordDto, AuthenticationResetPasswordModel>();
    }
}
