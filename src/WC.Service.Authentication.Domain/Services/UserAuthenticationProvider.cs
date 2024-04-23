using AutoMapper;
using Microsoft.Extensions.Logging;
using WC.Library.Domain.Services;
using WC.Service.Authentication.Data.Models;
using WC.Service.Authentication.Data.Repository;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public class UserAuthenticationProvider :
    DataProviderBase<UserAuthenticationProvider, IUserAuthenticationRepository, UserAuthenticationModel, UserAuthenticationEntity>,
    IUserAuthenticationProvider
{
    public UserAuthenticationProvider(IMapper mapper, ILogger<UserAuthenticationProvider> logger,
        IUserAuthenticationRepository repository) : base(mapper, logger, repository)
    {
    }
}