using AutoMapper;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.EmployeeAuthenticationLogin;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;

namespace WC.Service.Authentication.API;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeAuthenticationLoginRequestDto, EmployeeAuthenticationLoginRequestModel>();

        CreateMap<EmployeeAuthenticationLoginResponseModel, EmployeeAuthenticationLoginResponseDto>();

        CreateMap<EmployeeAuthenticationResetPasswordDto, EmployeeAuthenticationResetPasswordModel>();
    }
}
