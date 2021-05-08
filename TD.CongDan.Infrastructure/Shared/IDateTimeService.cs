using System;

namespace TD.CongDan.Infrastructure.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}