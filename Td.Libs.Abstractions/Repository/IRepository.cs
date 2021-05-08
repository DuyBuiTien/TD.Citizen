

namespace TD.Libs.Abstractions.Repository
{
    public interface IRepository<T> : ICommandRepository<T>, IQueryRepository<T> where T : class { }
}
