using ErrorOr;
using MediatR;
using TasteTrove.Application.Authentication.Common;

namespace TasteTrove.Application.Authentication.Commands.Register;
  public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
  ) : IRequest<ErrorOr<AuthenticationResult>>;