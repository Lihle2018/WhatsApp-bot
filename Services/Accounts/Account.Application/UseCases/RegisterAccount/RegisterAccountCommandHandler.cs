using Accounts.Application.Helpers;
using Accounts.Domain.Repositories;
using BuildingBlocks.BaseEntities;
using MediatR;

namespace Accounts.Application.UseCases.RegisterAccount
{
    public class RegisterAccountCommandHandler(IAccountRepository _accountRepository) : IRequestHandler<RegisterAccountCommand, ResponseBase<RegisterAccountResponse>>
    {
        public async Task<ResponseBase<RegisterAccountResponse>> Handle(RegisterAccountCommand command, CancellationToken cancellationToken)
        {
            var (salt, hash) = PasswordHashing.GenerateSaltAndHash(command.Password);
            var account = Domain.Entities.Account.Create(command.Email, hash, salt);

            await _accountRepository.SaveAsync(account);

            return new ResponseBase<RegisterAccountResponse> { Data = new RegisterAccountResponse(account.Id, account.Email), Success = true };

        }

    }
}
