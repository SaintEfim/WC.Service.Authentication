using AutoMapper;
using WC.Library.Web.Models;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.Login;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.API;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LoginRequestDto, LoginRequestModel>();

        CreateMap<LoginResponseModel, LoginResponseDto>();

        CreateMap<ResetPasswordDto, ResetPasswordModel>();
    }
}