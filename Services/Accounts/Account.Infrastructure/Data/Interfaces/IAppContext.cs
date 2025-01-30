using Accounts.Domain.Entities;
using MongoDB.Driver;

namespace Accounts.Infrastructure.Data.Interfaces
{
    public interface IAppContext
    {
        IMongoCollection<Account> Accounts { get; }
    }
}
