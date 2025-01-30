namespace Accounts.Application.UseCases.Login
{
    public record LoginResponse(string AccountId, string Email, string Token);
}
