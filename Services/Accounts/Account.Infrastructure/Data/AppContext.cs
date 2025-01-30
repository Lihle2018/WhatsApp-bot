using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
namespace Accounts.Infrastructure.Data
{
    public class AppContext : IAppContext
    {
        public AppContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);
            Accounts = database.GetCollection<Account>(configuration.GetSection("DatabaseSettings:CollectionName").Value);
        }
        public IMongoCollection<Account> Accounts { get; }
    }
}
