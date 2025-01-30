using BuildingBlocks.BaseEntities;
using MediatR;
namespace Accounts.Application.UseCases.RegisterAccount
{
    public record RegisterAccountCommand(string Email,string Password) : IRequest<ResponseBase<RegisterAccountResponse>>;
}
