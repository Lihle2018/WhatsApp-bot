using Accounts.Domain.Entities;
namespace Accounts.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task SaveAsync(Account account);
        Task<Account> GetByEmailAsync(string email);
    }
}
