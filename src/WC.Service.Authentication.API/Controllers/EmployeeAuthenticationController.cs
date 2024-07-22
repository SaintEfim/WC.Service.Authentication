using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WC.Library.Web.Controllers;
using WC.Library.Web.Models;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.Login;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.Login;
using WC.Service.Authentication.Domain.Services;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WC.Service.Authentication.API.Controllers;

/// <summary>
///     The user type management controller.
/// </summary>
[Route("api/v1/employee-authentication")]
public class EmployeeAuthenticationController : ApiControllerBase<EmployeeAuthenticationController>
{
    private readonly IEmployeeAuthenticationManager _manager;
    private readonly IEmployeeAuthenticationProvider _provider;

    /// <inheritdoc/>
    public EmployeeAuthenticationController(
        IMapper mapper,
        ILogger<EmployeeAuthenticationController> logger,
        IEmployeeAuthenticationManager manager,
        IEmployeeAuthenticationProvider provider)
        : base(mapper, logger)
    {
        _manager = manager;
        _provider = provider;
    }

    /// <summary>
    ///     Logs in a user using provided authentications.
    /// </summary>
    /// <param name="loginRequest">The login request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("login")]
    [OpenApiOperation(nameof(LoginEmployee))]
    [SwaggerResponse(Status201Created, typeof(LoginResponseDto))]
    [SwaggerResponse(Status401Unauthorized, typeof(ErrorDto))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<ActionResult<LoginResponseDto>> LoginEmployee(
        [FromBody] LoginRequestDto loginRequest,
        CancellationToken cancellationToken = default)
    {
        var createResult = await _provider.Login(Mapper.Map<LoginRequestModel>(loginRequest), cancellationToken);

        return Ok(Mapper.Map<LoginResponseDto>(createResult));
    }

    /// <summary>
    ///     Refreshes a user's login session.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("refresh")]
    [OpenApiOperation(nameof(RefreshToken))]
    [SwaggerResponse(Status200OK, typeof(LoginResponseDto))]
    public async Task<ActionResult<LoginResponseDto>> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var res = await _provider.Refresh(refreshToken, cancellationToken);
        return Ok(Mapper.Map<LoginResponseDto>(res));
    }

    /// <summary>
    ///     Resets a user's password.
    /// </summary>
    /// <param name="resetPassword">The reset password request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPatch("resetPassword")]
    [OpenApiOperation(nameof(ResetPassword))]
    [SwaggerResponse(Status200OK, typeof(void))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetPasswordDto resetPassword,
        CancellationToken cancellationToken = default)
    {
        await _manager.ResetPassword(Mapper.Map<ResetPasswordModel>(resetPassword), cancellationToken);
        return Ok();
    }
}
