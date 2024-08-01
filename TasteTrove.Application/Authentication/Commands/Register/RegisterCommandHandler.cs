using ErrorOr;
using MediatR;
using TasteTrove.Application.Common.Interfaces.Authentication;
using TasteTrove.Application.Common.Interfaces.Persistence;
using TasteTrove.Application.Authentication.Common;
using TasteTrove.Domain.Entities;
using TasteTrove.Domain.Common.Errors;

namespace TasteTrove.Application.Authentication.Commands.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

        _userRepository.Add(user);

        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
