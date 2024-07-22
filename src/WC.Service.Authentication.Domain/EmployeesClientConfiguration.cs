using Microsoft.Extensions.Configuration;
using WC.Service.Employees.gRPC.Client;

namespace WC.Service.Authentication.Domain;

public class EmployeesClientConfiguration : IEmployeesClientConfiguration
{
    private readonly Lazy<string> _baseUrl;

    public EmployeesClientConfiguration(
        IConfiguration config)
    {
        _baseUrl = new Lazy<string>(() => config.GetValue<string>("EmployeeService:Url") ??
                                          throw new InvalidOperationException(
                                              "Employee REST API service URL must be specified"));
    }

    public string GetBaseUrl()
    {
        return _baseUrl.Value;
    }
}
