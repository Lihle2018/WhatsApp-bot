using Accounts.Domain.Entities;
using Accounts.Domain.Repositories;
using Accounts.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Accounts.Infrastructure.Repositories
{
    public class AccountRepository: IAccountRepository
    {
        private readonly IAppContext _context;
        public AccountRepository(IAppContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
          var account = await _context.Accounts.Find(x => x.Email == email).FirstOrDefaultAsync();
          return account;
        }

        public async Task SaveAsync(Account account)
        {
            await _context.Accounts.InsertOneAsync(account);
        }
    }
}
