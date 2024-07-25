using TasteTrove.Domain.Entities;

namespace TasteTrove.Application.Common.Interfaces.Persistence;

  public interface IUserRepository
  {
    User? GetUserByEmail(string email);
    void Add(User user);
  }
