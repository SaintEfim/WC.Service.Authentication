using System.Security.Authentication;
using FluentValidation;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Clients;
using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.Authentication.Domain.Services.Authentication;

public class AuthenticationManager
    : ValidatorBase<ModelBase>,
        IAuthenticationManager
{
    private readonly IGreeterPersonalDataClient _personalDataClient;

    public AuthenticationManager(
        IEnumerable<IValidator> validators,
        IGreeterPersonalDataClient personalDataClient)
        : base(validators)
    {
        _personalDataClient = personalDataClient;
    }

    public async Task ResetPassword(
        AuthenticationResetPasswordModel authenticationResetPasswordModel,
        CancellationToken cancellationToken = default)
    {
        Validate<AuthenticationResetPasswordModel, IDomainUpdateValidator>(authenticationResetPasswordModel,
            cancellationToken);

        var verifyResponse = await _personalDataClient.VerifyCredentials(
            new VerifyCredentialsRequestModel
            {
                Email = authenticationResetPasswordModel.Email,
                Password = authenticationResetPasswordModel.OldPassword
            }, cancellationToken);

        if (verifyResponse == null)
        {
            throw new AuthenticationException("Invalid email or password.");
        }

        await _personalDataClient.ResetPassword(
            new PersonalDataResetPasswordRequestModel
            {
                Id = verifyResponse.EmployeeId,
                Password = authenticationResetPasswordModel.NewPassword
            }, cancellationToken);
    }
}
