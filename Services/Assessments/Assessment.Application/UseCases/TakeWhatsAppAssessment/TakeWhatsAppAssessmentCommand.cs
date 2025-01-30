using Assessments.Application.DTOs;
using MediatR;

namespace Assessments.Application.UseCases.TakeAssessment
{
    public record TakeWhatsAppAssessmentCommand(string sender, string message ):IRequest<QuestionDto>;
}
