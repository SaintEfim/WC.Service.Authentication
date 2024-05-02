using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
[Route("api/[controller]")]
public class UserAuthenticationController : CrudApiControllerBase<UserAuthenticationController, IUserAuthenticationManager,
    IUserAuthenticationProvider, UserAuthenticationModel, UserAuthenticationDto>
{
    /// <inheritdoc/>
    public UserAuthenticationController(
        IMapper mapper,
        ILogger<UserAuthenticationController> logger,
        IEnumerable<IValidator> validators,
        IUserAuthenticationManager manager,
        IUserAuthenticationProvider provider)
        : base(mapper, logger, validators, manager, provider)
    {
    }

    /// <summary>
    ///     Retrieves a list of user authentications.
    /// </summary>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpGet]
    [SwaggerOperation(OperationId = nameof(UserAuthenticationGet))]
    [SwaggerResponse(Status200OK, Type = typeof(List<UserAuthenticationDto>))]
    public async Task<ActionResult<List<UserAuthenticationDto>>> UserAuthenticationGet(
        CancellationToken cancellationToken = default)
    {
        return Ok(await GetMany(cancellationToken));
    }

    /// <summary>
    ///     Logs in a user using provided authentications.
    /// </summary>
    /// <param name="loginRequest">The login request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("login")]
    [SwaggerOperation(OperationId = nameof(UserAuthenticationLogin))]
    [SwaggerResponse(Status200OK)]
    [SwaggerResponse(Status401Unauthorized, Type = typeof(ErrorDto))]
    [SwaggerResponse(Status404NotFound, Type = typeof(ErrorDto))]
    public async Task<ActionResult<LoginResponseDto>> UserAuthenticationLogin(LoginRequestDto loginRequest,
        CancellationToken cancellationToken = default)
    {
        Validate(loginRequest);
        var res = await Manager.Login(Mapper.Map<LoginRequestModel>(loginRequest), cancellationToken);
        return Ok(Mapper.Map<LoginResponseDto>(res));
    }

    /// <summary>
    ///     Refreshes a user's login session.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPost("refresh")]
    [SwaggerOperation(OperationId = nameof(UserAuthenticationRefresh))]
    [SwaggerResponse(Status200OK)]
    public async Task<ActionResult<LoginResponseDto>> UserAuthenticationRefresh(string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var res = await Manager.Refresh(refreshToken, cancellationToken);
        return Ok(Mapper.Map<LoginResponseDto>(res));
    }

    /// <summary>
    ///     Resets a user's password.
    /// </summary>
    /// <param name="resetPassword">The reset password request data.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpPatch("resetPassword")]
    [SwaggerOperation(OperationId = nameof(UserAuthenticationResetPassword))]
    [SwaggerResponse(Status200OK)]
    [SwaggerResponse(Status400BadRequest, Type = typeof(ErrorDto))]
    [SwaggerResponse(Status404NotFound, Type = typeof(ErrorDto))]
    public async Task<IActionResult> UserAuthenticationResetPassword(ResetPasswordDto resetPassword,
        CancellationToken cancellationToken = default)
    {
        Validate(resetPassword);
        await Manager.ResetPassword(Mapper.Map<ResetPasswordModel>(resetPassword), cancellationToken);
        return Ok();
    }

    /// <summary>
    ///     Deletes the specified user authentication.
    /// </summary>
    /// <param name="id">The unique identifier of the user authentication to delete.</param>
    /// <param name="cancellationToken">The operation cancellation token.</param>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(OperationId = nameof(UserAuthenticationDelete))]
    [SwaggerResponse(Status204NoContent)]
    [SwaggerResponse(Status404NotFound, Type = typeof(ErrorDto))]
    [SwaggerResponse(Status400BadRequest, Type = typeof(ErrorDto))]
    [SwaggerResponse(Status409Conflict, Type = typeof(ErrorDto))]
    public async Task<IActionResult> UserAuthenticationDelete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Delete(id, cancellationToken);
    }
}