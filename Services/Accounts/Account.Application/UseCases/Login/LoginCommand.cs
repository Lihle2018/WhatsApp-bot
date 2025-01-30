using BuildingBlocks.BaseEntities;
using MediatR;

namespace Accounts.Application.UseCases.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<ResponseBase<LoginResponse>>;
}
