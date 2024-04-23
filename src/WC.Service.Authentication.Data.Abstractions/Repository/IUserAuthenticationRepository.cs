using WC.Library.Data.Repository;
using WC.Service.Authentication.Data.Models;

namespace WC.Service.Authentication.Data.Repository;

public interface IUserAuthenticationRepository : IRepository<UserAuthenticationEntity>;