using WC.Library.Domain.Services;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public interface IUserAuthenticationProvider : IDataProvider<UserAuthenticationModel>;