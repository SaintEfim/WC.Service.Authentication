using FluentValidation;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models;

namespace WC.Service.Authentication.Domain.Services;

public class EmployeeAuthenticationManager
    : ValidatorBase<ModelBase>,
        IEmployeeAuthenticationManager
{
    public EmployeeAuthenticationManager(
        IEnumerable<IValidator> validators)
        : base(validators)
    {
    }

    public async Task ResetPassword(
        ResetPasswordModel resetPassword,
        CancellationToken cancellationToken = default)
    {
        Validate<ResetPasswordModel, IDomainUpdateValidator>(resetPassword, cancellationToken);

        // var employee =
        //     await _employeesClient.GetOneByEmail(new GetOneByEmailEmployeeRequestModel { Email = resetPassword.Email },
        //         cancellationToken);
        //
        // // if (!_passwordHasher.Verify(resetPassword.OldPassword, employee.Password))
        // // {
        // //     throw new PasswordMismatchException("Passwords do not match.");
        // // }
        // //
        // // employee.Password = _passwordHasher.Hash(resetPassword.NewPassword);
        //
        // await _employeesClient.Update(new EmployeeUpdateRequestModel
        // {
        //     Id = employee.Id,
        //     Name = employee.Name,
        //     Surname = employee.Surname,
        //     Patronymic = employee.Patronymic,
        //     Email = employee.Email,
        //     Password = employee.Password,
        //     PositionId = employee.PositionId,
        //     Role = employee.Role
        // }, cancellationToken);
    }
}
