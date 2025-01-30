using Assessments.Application.DTOs;
using MediatR;

namespace Assessments.Application.UseCases.GenerateReport
{
    public record GenerateReportCommand(string phoneNumber) : IRequest<string>;
}
