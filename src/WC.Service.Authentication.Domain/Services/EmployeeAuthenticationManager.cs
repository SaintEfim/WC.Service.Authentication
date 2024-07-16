using FluentValidation;
using WC.Library.BCryptPasswordHash;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Library.Domain.Validators;
using WC.Library.Employee.Shared.Exceptions;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Employees.gRPC.Client.Clients;
using WC.Service.Employees.gRPC.Client.Models.Employee;
using WC.Service.Employees.gRPC.Client.Models.Employee.GetOneByEmailEmployee;

namespace WC.Service.Authentication.Domain.Services;

public class EmployeeAuthenticationManager : ValidatorBase<ModelBase>, IEmployeeAuthenticationManager
{
    private readonly IBCryptPasswordHasher _passwordHasher;
    private readonly IGreeterEmployeesClient _employeesClient;

    public EmployeeAuthenticationManager(IEnumerable<IValidator> validators, IBCryptPasswordHasher passwordHasher,
        IGreeterEmployeesClient employeesClient) : base(validators)
    {
        _passwordHasher = passwordHasher;
        _employeesClient = employeesClient;
    }

    public async Task ResetPassword(ResetPasswordModel resetPassword,
        CancellationToken cancellationToken)
    {
        Validate<ResetPasswordModel, IDomainUpdateValidator>(resetPassword, cancellationToken);

        var employee = await _employeesClient.GetOneByEmail(new GetOneByEmailEmployeeRequestModel
        {
            Email = resetPassword.Email
        }, cancellationToken);

        if (!_passwordHasher.Verify(resetPassword.OldPassword, employee.Password))
        {
            throw new PasswordMismatchException("Passwords do not match.");
        }

        employee.Password = _passwordHasher.Hash(resetPassword.NewPassword);

        await _employeesClient.Update(new EmployeeUpdateRequestModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Patronymic = employee.Patronymic,
            Email = employee.Email,
            Password = employee.Password,
            PositionId = employee.PositionId,
            Role = employee.Role
        }, cancellationToken);
    }
}