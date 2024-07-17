using ErrorOr;
using TasteTrove.Application.Common.Interfaces.Authentication;
using TasteTrove.Application.Common.Interfaces.Persistence;
using TasteTrove.Domain.Common.Errors;
using TasteTrove.Domain.Entities;

namespace TasteTrove.Application.Services.Authentication; public class AuthenticationService : IAuthenticationService
{

  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
  {
    _jwtTokenGenerator = jwtTokenGenerator;
    _userRepository = userRepository;
  }

  public ErrorOr<AuthenticationResult> Login(string email, string password)
  {

    // 1. Validate the user exists
    if (_userRepository.GetUserByEmail(email) is not User user)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    // 2. Validate the password is correct

    if (user.Password != password)
    {
      return Errors.Authentication.InvalidCredentials;
    }

    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }

  public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
  {
    // Validate the user doesn't exist
    if (_userRepository.GetUserByEmail(email) is not null)
    {
      return Errors.User.DuplicateEmail;
    }

    var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };

    _userRepository.Add(user);

    // Create JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);

    return new AuthenticationResult(user, token);
  }
}
