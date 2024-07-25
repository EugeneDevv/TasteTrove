using ErrorOr;
using MediatR;
using TasteTrove.Application.Authentication.Common;

namespace TasteTrove.Application.Authentication.Queries.Login;
  public record LoginQuery
  (
    string Email,
    string Password
  ) : IRequest<ErrorOr<AuthenticationResult>>;