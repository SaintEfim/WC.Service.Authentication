using System.Security.Authentication;
using FluentValidation;
using WC.Library.Domain.Models;
using WC.Library.Domain.Services.Validators;
using WC.Library.Domain.Validators;
using WC.Service.Authentication.Domain.Models;
using WC.Service.PersonalData.gRPC.Client.Clients;
using WC.Service.PersonalData.gRPC.Client.Models;
using WC.Service.PersonalData.gRPC.Client.Models.Verify;

namespace WC.Service.Authentication.Domain.Services;

public class EmployeeAuthenticationManager
    : ValidatorBase<ModelBase>,
        IEmployeeAuthenticationManager
{
    private readonly IGreeterPersonalDataClient _personalDataClient;

    public EmployeeAuthenticationManager(
        IEnumerable<IValidator> validators,
        IGreeterPersonalDataClient personalDataClient)
        : base(validators)
    {
        _personalDataClient = personalDataClient;
    }

    public async Task ResetPassword(
        ResetPasswordModel resetPasswordModel,
        CancellationToken cancellationToken = default)
    {
        Validate<ResetPasswordModel, IDomainUpdateValidator>(resetPasswordModel, cancellationToken);

        VerifyCredentialsResponseModel verifyResponse;
        try
        {
            verifyResponse = await _personalDataClient.VerifyCredentials(
                new VerifyCredentialsRequestModel
                {
                    Email = resetPasswordModel.Email,
                    Password = resetPasswordModel.OldPassword
                }, cancellationToken);

            if (verifyResponse == null)
            {
                throw new AuthenticationException("Invalid email or password.");
            }
        }
        catch (Exception)
        {
            throw new AuthenticationException("Invalid email or password.");
        }

        await _personalDataClient.ResetPassword(
            new PersonalDataResetPasswordRequestModel
            {
                Id = verifyResponse.PersonalDataId,
                Password = resetPasswordModel.NewPassword
            }, cancellationToken);
    }
}
