using Accounts.Application.Helpers;
using Accounts.Application.Services.Interfaces;
using Accounts.Domain.Repositories;
using BuildingBlocks.BaseEntities;
using MediatR;

namespace Accounts.Application.UseCases.Login
{
    public class LoginCommandHandler(IAccountRepository _repository,ITokenService _tokenService) : IRequestHandler<LoginCommand, ResponseBase<LoginResponse>>
    {

        public async Task<ResponseBase<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByEmailAsync(request.Email);

            if (account == null)
            {
                return new ResponseBase<LoginResponse>
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var hashedPassword = PasswordHashing.HashPassword(request.Password, account.PasswordSalt);

            if (!account.VerifyPassword(hashedPassword))
            {
                return new ResponseBase<LoginResponse>
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var token = _tokenService.GenerateToken(account);

            return new ResponseBase<LoginResponse>
            {
                Success = true,
                Data = new LoginResponse(account.Id, account.Email, token)
            };
        }

    }
}
