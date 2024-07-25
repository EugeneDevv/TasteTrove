

using Mapster;
using TasteTrove.Application.Authentication.Commands.Register;
using TasteTrove.Application.Authentication.Common;
using TasteTrove.Application.Authentication.Queries.Login;
using TasteTrove.Contracts.Authentication;

namespace TasteTrove.Api.Mapping;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest, src => src.User);
    }
}
