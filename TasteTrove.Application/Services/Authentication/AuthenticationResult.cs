using TasteTrove.Domain.Entities;

namespace TasteTrove.Application.Services.Authentication;
public record AuthenticationResult(
  User User,
string Token
);