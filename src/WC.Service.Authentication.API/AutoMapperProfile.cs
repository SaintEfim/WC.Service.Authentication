﻿using AutoMapper;
using Service.Authify.Domain.Models.Responses;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.RequestsDto;
using WC.Service.Authentication.API.Models.ResponsesDto;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Requests;

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