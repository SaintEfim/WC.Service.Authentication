using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using WC.Library.Domain.Models;
using WC.Library.Web.Controllers;
using WC.Library.Web.Models;
using WC.Service.Authentication.API.Models;
using WC.Service.Authentication.API.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Models;
using WC.Service.Authentication.Domain.Models.AuthenticationLogin;
using WC.Service.Authentication.Domain.Services;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace WC.Service.Authentication.API.Controllers;

/// <summary>
///     The employee type management controller.
/// </summary>
[Route("api/v1/authentication")]
public class AuthenticationController : ApiControllerBase<AuthenticationController>
{
    private readonly IAuthenticationManager _manager;
    private readonly IAuthenticationProvider _provider;

    /// <inheritdoc/>
    public AuthenticationController(
        IMapper mapper,
        ILogger<AuthenticationController> logger,
        IAuthenticationManager manager,
        IAuthenticationProvider provider)
        : base(mapper, logger)
    {
        _manager = manager;
        _provider = provider;
    }

    /// <summary>
    ///     Logs in a employee using provided authentications.
    /// </summary>
    /// <param name="authenticationLoginRequest">The login request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("login")]
    [OpenApiOperation(nameof(Login))]
    [SwaggerResponse(Status201Created, typeof(AuthenticationLoginResponseDto))]
    [SwaggerResponse(Status401Unauthorized, typeof(ErrorDto))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<ActionResult<AuthenticationLoginResponseDto>> Login(
        [FromBody] AuthenticationLoginRequestDto authenticationLoginRequest,
        CancellationToken cancellationToken = default)
    {
        var createResult =
            await _provider.Login(Mapper.Map<AuthenticationLoginRequestModel>(authenticationLoginRequest),
                cancellationToken);

        return Ok(Mapper.Map<AuthenticationLoginResponseDto>(createResult));
    }

    [HttpGet("getId")]
    [SwaggerResponse(Status200OK, typeof(ModelBase))]
    [SwaggerResponse(Status401Unauthorized, typeof(ErrorDto))]
    [OpenApiOperation(nameof(GetId))]
    public IActionResult GetId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized();
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return BadRequest("Invalid user ID format");
        }

        return Ok(new ModelBase { Id = userId });
    }

    /// <summary>
    ///     Refreshes a employee's login session.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("refresh")]
    [Authorize(Roles = "Admin, User")]
    [OpenApiOperation(nameof(RefreshToken))]
    [SwaggerResponse(Status200OK, typeof(AuthenticationLoginResponseDto))]
    public async Task<ActionResult<AuthenticationLoginResponseDto>> RefreshToken(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var res = await _provider.Refresh(refreshToken, cancellationToken);
        return Ok(Mapper.Map<AuthenticationLoginResponseDto>(res));
    }

    /// <summary>
    ///     Resets a employee's password.
    /// </summary>
    /// <param name="authenticationResetPassword">The reset password request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPatch("authenticationResetPassword")]
    [Authorize(Roles = "Admin, User")]
    [OpenApiOperation(nameof(ResetPassword))]
    [SwaggerResponse(Status200OK, typeof(void))]
    [SwaggerResponse(Status404NotFound, typeof(ErrorDto))]
    public async Task<IActionResult> ResetPassword(
        [FromBody] AuthenticationResetPasswordDto authenticationResetPassword,
        CancellationToken cancellationToken = default)
    {
        await _manager.ResetPassword(Mapper.Map<AuthenticationResetPasswordModel>(authenticationResetPassword),
            cancellationToken);
        return Ok();
    }
}
