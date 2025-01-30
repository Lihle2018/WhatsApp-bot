namespace Accounts.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Accounts.Domain.Entities.Account account);
    }
}
