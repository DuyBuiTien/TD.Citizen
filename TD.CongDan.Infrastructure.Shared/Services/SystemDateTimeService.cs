using TD.CongDan.Application.Interfaces.Shared;
using System;

namespace TD.CongDan.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}