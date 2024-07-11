using TasteTrove.Application.Common.Interfaces.Services;

namespace TasteTrove.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
