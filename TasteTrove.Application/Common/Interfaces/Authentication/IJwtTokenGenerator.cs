using TasteTrove.Domain.Entities;

namespace TasteTrove.Application.Common.Interfaces.Authentication;

    public interface IJwtTokenGenerator
    {
    string GenerateToken(User user);
    }
