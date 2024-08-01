
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasteTrove.Application.Authentication.Commands.Register;
using TasteTrove.Application.Authentication.Common;
using TasteTrove.Application.Authentication.Queries.Login;
using TasteTrove.Contracts.Authentication;
using TasteTrove.Domain.Common.Errors;

namespace TasteTrove.Api.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{

  private readonly ISender _mediator;
  private readonly IMapper _mapper;


  public AuthenticationController(
    ISender mediator, IMapper mapper
    )
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register(RegisterRequest request)
  {

    var command = _mapper.Map<RegisterCommand>(request);

    ErrorOr<AuthenticationResult> registerResult = await  _mediator.Send(command);

    return registerResult.Match(
      authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
      errors => Problem(errors)
    );

  }


  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    var query = _mapper.Map<LoginQuery>(request);
    var authResult = await _mediator.Send(query);


    if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
    {
      return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
    }


    return authResult.Match(
     authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
     errors => Problem(errors)
   );
  }
}
