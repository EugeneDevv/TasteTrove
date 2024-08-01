using TasteTrove.Domain.Entities;

namespace TasteTrove.Application.Authentication.Common;
public record AuthenticationResult(
  User User,
string Token
);